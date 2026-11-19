using FrameAgricultureApi.Core.Outputs;
using System.Text.Json;
using System.Text.Json.Serialization;
using FrameAgricultureApi.Controllers;
using FrameAgricultureApi.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using FrameAgricultureApi.ComponentLogic.CoverCropComponent;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using NSubstitute;

namespace FrameAgricultureApi.Test.ComponentTests;

/// <summary>
/// Regression tests for cover crop emissions grouped by pollutant and pathway.
/// </summary>
public class CoverCropTests
{
    /// <summary>
    /// Verifies calculated nitrogen outputs and their pollutant and pathway assignments.
    /// </summary>
    [Fact]
    public void CoverCropEmissions_PreservesCalculatedValuesForEachPollutantAndPathway()
    {
        var coverCrops = CreateComponent();

        var emissions = coverCrops.CoverCropEmissions(CoverCropType.All);
        emissions.RoundEmissions(4);

        Assert.Equal(0.77, emissions.DirectN20_N.Value, 4);
        Assert.Equal(0.0953, emissions.IndirectN20_NLeached.Value, 4);
        Assert.Equal(0.0043, emissions.IndirectN20_NVolatilisation.Value, 4);
        Assert.Equal(SummarisePollutantType.NitrousOxide, emissions.IndirectN20_NVolatilisation.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.IndirectDeposition, emissions.IndirectN20_NVolatilisation.SummarisePathwayType);
        Assert.Equal(12.705, emissions.DirectNO3_NLeached.Value, 4);
        Assert.Equal(0.308, emissions.DirectNO_N.Value, 4);
        Assert.Equal(2.31, emissions.DirectN2_N.Value, 4);
        Assert.True(double.IsNaN(emissions.DirectNH3_N.Value));
        Assert.Equal(77, emissions.N.Value);
        Assert.Equal(23.1, emissions.UnadjustedN03_N.Value, 4);
        Assert.Equal(0.1732, emissions.UnadjustedIndirectedN20_NLeached.Value, 4);

        Assert.Equal(SummarisePollutantType.Nitrogen, emissions.N.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.Direct, emissions.N.SummarisePathwayType);
        Assert.Equal(SummarisePollutantType.NitrateNitrogen, emissions.UnadjustedN03_N.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.Direct, emissions.UnadjustedN03_N.SummarisePathwayType);
    }

    /// <summary>
    /// Verifies the controller returns the requested cover crop type and calculated nested outputs.
    /// </summary>
    [Fact]
    public void Controller_ReturnsCoverCropOutputWithCalculatedEmissions()
    {
        var controller = new CoverCropsController(CreateComponent());
        var result = Assert.IsType<OkObjectResult>(controller.CoverCropEmissions(new CoverCropRequest
        {
            CoverCropType = CoverCropType.All,
            NReturn = 77
        }));
        var response = Assert.IsType<CoverCropOutput>(result.Value);
        Assert.Equal(SectorType.Crop, response.SectorType);
        Assert.Equal(CoverCropType.All, response.CoverCropType);
        Assert.Equal(77, response.Emissions.N.Value);
        Assert.Equal(0.77, response.Emissions.DirectN20_N.Value, 10);
        Assert.Equal(12.705, response.Emissions.DirectNO3_NLeached.Value, 10);

        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };
        var json = JsonSerializer.Serialize(response, options);
        using var responseJson = JsonDocument.Parse(json);
        var emissionsJson = responseJson.RootElement.GetProperty("Emissions");
        Assert.Equal(JsonValueKind.Object, emissionsJson.ValueKind);
        Assert.Equal(77, emissionsJson.GetProperty("N").GetProperty("Value").GetDouble());
        Assert.Equal("NaN", emissionsJson.GetProperty("DirectNH3_N").GetProperty("Value").GetString());

        var restored = JsonSerializer.Deserialize<CoverCropOutput>(json, options)!;
        Assert.Equal(response.CropType, restored.CropType);
        Assert.Equal(response.CoverCropType, restored.CoverCropType);
        Assert.Equal(response.Emissions.DirectNO3_NLeached.Value, restored.Emissions.DirectNO3_NLeached.Value);
        Assert.True(double.IsNaN(restored.Emissions.DirectNH3_N.Value));
    }

    /// <summary>
    /// Verifies rounding, serialisation and reinitialisation of nested emission values.
    /// </summary>
    [Fact]
    public void RoundEmissions_AndSerialisation_PreserveEveryPathwayAndUnsetValues()
    {
        var emissions = new CoverCropEmissions();
        emissions.InitialiseCoverCropEmissions();
        AssertAllValuesUnset(emissions);

        BaseEmission[] core =
        [
            emissions.DirectN20_N,
            emissions.IndirectN20_NLeached,
            emissions.IndirectN20_NVolatilisation,
            emissions.DirectNH3_N,
            emissions.DirectNO3_NLeached,
            emissions.DirectNO_N,
            emissions.DirectN2_N
        ];
        Assert.Equal(7, core.Length);
        foreach (var emission in core)
        {
            emission.Value = 1.23456;
        }
        emissions.N.Value = 77.12345;

        emissions.RoundEmissions(3);

        Assert.All(core, emission =>
        {
            Assert.Equal(1.235, emission.Value);
            Assert.Equal(FrameAgricultureApi.Core.UnitSymbols.Kg, emission.Unit.Symbol);
            Assert.Equal(1m, emission.Unit.MultipleToBaseFactor);
        });
        Assert.Equal(77.123, emissions.N.Value);
        Assert.True(double.IsNaN(emissions.UnadjustedN03_N.Value));
        Assert.True(double.IsNaN(emissions.UnadjustedIndirectedN20_NLeached.Value));

        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };
        var json = JsonSerializer.Serialize(emissions, options);
        var restored = JsonSerializer.Deserialize<CoverCropEmissions>(json, options)!;
        Assert.Equal(1.235, restored.IndirectN20_NVolatilisation.Value);
        Assert.Equal(77.123, restored.N.Value);
        Assert.Equal(SummarisePollutantType.Nitrogen, restored.N.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.Direct, restored.N.SummarisePathwayType);
        Assert.True(double.IsNaN(restored.UnadjustedN03_N.Value));

        emissions.InitialiseCoverCropEmissions();
        AssertAllValuesUnset(emissions);
    }

    /// <summary>
    /// Verifies additional outputs retain values, units and metadata through serialisation and reset.
    /// </summary>
    [Fact]
    public void AdditionalEmissions_PreserveMetadataAndValuesAcrossSerialisation()
    {
        var emissions = new CoverCropEmissions();
        emissions.N.Value = 77.12345;
        emissions.UnadjustedN03_N.Value = 23.12345;
        emissions.UnadjustedIndirectedN20_NLeached.Value = 0.17325;
        emissions.RoundEmissions(3);

        Assert.Equal(77.123, emissions.N.Value);
        Assert.Equal(23.123, emissions.UnadjustedN03_N.Value);
        Assert.Equal(0.173, emissions.UnadjustedIndirectedN20_NLeached.Value);

        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };
        var restored = JsonSerializer.Deserialize<CoverCropEmissions>(JsonSerializer.Serialize(emissions, options), options)!;
        Assert.Equal(23.123, restored.UnadjustedN03_N.Value);
        Assert.Equal(SummarisePollutantType.NitrateNitrogen, restored.UnadjustedN03_N.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.Direct, restored.UnadjustedN03_N.SummarisePathwayType);
        Assert.Equal(FrameAgricultureApi.Core.UnitSymbols.Kg, restored.UnadjustedN03_N.Unit.Symbol);
        Assert.Equal(1m, restored.UnadjustedN03_N.Unit.MultipleToBaseFactor);
        Assert.Equal(0.173, restored.UnadjustedIndirectedN20_NLeached.Value);
        Assert.Equal(SummarisePollutantType.NitrousOxide, restored.UnadjustedIndirectedN20_NLeached.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.IndirectLeaching, restored.UnadjustedIndirectedN20_NLeached.SummarisePathwayType);

        emissions.InitialiseCoverCropEmissions();
        AssertAllValuesUnset(emissions);
        Assert.Equal(SummarisePollutantType.NitrateNitrogen, emissions.UnadjustedN03_N.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.IndirectLeaching, emissions.UnadjustedIndirectedN20_NLeached.SummarisePathwayType);
    }

    private static CoverCrops CreateComponent()
    {
        var coverCropLookup = Substitute.For<IMongoService<CoverCropLookUpEntity>>();
        coverCropLookup.FindOne(Arg.Any<FilterDefinition<CoverCropLookUpEntity>>())
            .Returns(new CoverCropLookUpEntity { CoverCropType = CoverCropType.All, Item = "N return", Value = 77 });
        return new CoverCrops(
            coverCropLookup,
            Substitute.For<IMongoService<CropNonGhgLookUpEntity>>(),
            Substitute.For<IMongoService<CropResidueLookUpEntity>>(),
            Substitute.For<IMongoService<FertiliserRatioEfsLookUpEntity>>(),
            NullLogger<CoverCrops>.Instance);
    }

    private static void AssertAllValuesUnset(CoverCropEmissions emissions)
    {
        double[] values =
        [
            emissions.DirectN20_N.Value,
            emissions.IndirectN20_NLeached.Value,
            emissions.IndirectN20_NVolatilisation.Value,
            emissions.DirectNH3_N.Value,
            emissions.DirectNO3_NLeached.Value,
            emissions.DirectNO_N.Value,
            emissions.DirectN2_N.Value,
            emissions.N.Value,
            emissions.UnadjustedN03_N.Value,
            emissions.UnadjustedIndirectedN20_NLeached.Value
        ];
        Assert.All(values, value => Assert.True(double.IsNaN(value)));
    }
}