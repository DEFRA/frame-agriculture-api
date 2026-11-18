using System.Text.Json;
using System.Text.Json.Serialization;
using FrameAgricultureApi.Controllers;
using FrameAgricultureApi.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using FrameAgricultureApi.Libraries.CoverCrops;
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
        var coverCropLookup = Substitute.For<IMongoService<CoverCropLookUpEntity>>();
        coverCropLookup.FindOne(Arg.Any<FilterDefinition<CoverCropLookUpEntity>>())
            .Returns(new CoverCropLookUpEntity { Item = "N return", Value = 77 });
        var coverCrops = new CoverCrops(
            coverCropLookup,
            Substitute.For<IMongoService<CropNonGhgLookUpEntity>>(),
            Substitute.For<IMongoService<CropResidueLookUpEntity>>(),
            Substitute.For<IMongoService<FertiliserRatioEfsLookUpEntity>>(),
            NullLogger<CoverCrops>.Instance);

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

        var exported = emissions.ExportEmissions();
        Assert.Equal(9, exported.Count);
        Assert.DoesNotContain(exported, emission => double.IsNaN(emission.Value));
        var leached = Assert.Single(exported, emission => emission.Name == "Indirect N2O-N (leached)");
        Assert.Equal("kg N2O-N", leached.Units);
        Assert.Equal(0.0953, leached.Value, 4);
        var nitrogenReturn = Assert.Single(exported, emission => emission.Name == "Cover crops N return");
        Assert.Equal("kg N", nitrogenReturn.Units);
        Assert.Equal(77, nitrogenReturn.Value);
        var unadjustedNitrate = Assert.Single(exported, emission => emission.Units == "kg NO3-N" && emission.Name.StartsWith("Unadjusted"));
        Assert.Equal(23.1, unadjustedNitrate.Value, 4);
        var unadjustedNitrousOxide = Assert.Single(exported, emission => emission.Name == "Unadjusted indirect leach N2O-N");
        Assert.Equal(0.1732, unadjustedNitrousOxide.Value, 4);

        var controller = new CoverCropsController(coverCrops);
        var result = Assert.IsType<OkObjectResult>(controller.CoverCropEmissions(new CoverCropRequest
        {
            CoverCropType = CoverCropType.All,
            CropType = CropType.Wheat
        }));
        var response = Assert.IsType<StandardOutput>(result.Value);
        Assert.Equal(9, response.Emissions.Count);
        Assert.Equal("Cover Crops", response.Source);
        using var responseJson = JsonDocument.Parse(JsonSerializer.Serialize(response));
        Assert.Equal(9, responseJson.RootElement.GetProperty("Emissions").GetArrayLength());
    }

    /// <summary>
    /// Verifies rounding, export, serialisation and reinitialisation of grouped emissions.
    /// </summary>
    [Fact]
    public void RoundEmissions_AndExport_HandleEveryPathwayAndSkipUnsetValues()
    {
        var emissions = new CoverCropEmissions();
        emissions.InitialiseCoverCropEmissions();
        Assert.Empty(emissions.ExportEmissions());

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
        var exported = emissions.ExportEmissions();
        Assert.Equal(8, exported.Count);
        Assert.Equal(7, exported.Count(emission => emission.Value == 1.235));
        Assert.Equal(77.123, emissions.N.Value);

        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };
        var json = JsonSerializer.Serialize(emissions, options);
        var restored = JsonSerializer.Deserialize<CoverCropEmissions>(json, options)!;
        Assert.Equal(1.235, restored.IndirectN20_NVolatilisation.Value);
        Assert.Equal(77.123, restored.N.Value);
        Assert.Null(restored.N.SummarisePollutantType);
        Assert.Null(restored.N.SummarisePathwayType);
        Assert.True(double.IsNaN(restored.UnadjustedN03_N.Value));

        emissions.InitialiseCoverCropEmissions();
        Assert.Empty(emissions.ExportEmissions());
    }

    /// <summary>
    /// Verifies additional outputs are rounded, exported, serialised and reset independently.
    /// </summary>
    [Fact]
    public void AdditionalEmissions_PreserveMetadataAndValuesAcrossExportAndSerialisation()
    {
        var emissions = new CoverCropEmissions();
        emissions.N.Value = 77.12345;
        emissions.UnadjustedN03_N.Value = 23.12345;
        emissions.UnadjustedIndirectedN20_NLeached.Value = 0.17325;
        emissions.RoundEmissions(3);

        Assert.Equal(77.123, emissions.N.Value);
        Assert.Equal(23.123, emissions.UnadjustedN03_N.Value);
        Assert.Equal(0.173, emissions.UnadjustedIndirectedN20_NLeached.Value);
        var exported = emissions.ExportEmissions();
        Assert.Equal(3, exported.Count);
        Assert.Equal(new[] { 77.123, 23.123, 0.173 }, exported.Select(output => output.Value));

        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };
        var restored = JsonSerializer.Deserialize<CoverCropEmissions>(JsonSerializer.Serialize(emissions, options), options)!;
        Assert.Equal(23.123, restored.UnadjustedN03_N.Value);
        Assert.Equal(SummarisePollutantType.Nitrate, restored.UnadjustedN03_N.SummarisePollutantType);
        Assert.Null(restored.UnadjustedN03_N.SummarisePathwayType);
        Assert.Equal(0.173, restored.UnadjustedIndirectedN20_NLeached.Value);
        Assert.Equal(SummarisePollutantType.NitrousOxide, restored.UnadjustedIndirectedN20_NLeached.SummarisePollutantType);
        Assert.Equal(SummarisePathwayType.IndirectLeaching, restored.UnadjustedIndirectedN20_NLeached.SummarisePathwayType);

        emissions.InitialiseCoverCropEmissions();
        Assert.Empty(emissions.ExportEmissions());
        Assert.True(double.IsNaN(emissions.N.Value));
        Assert.True(double.IsNaN(emissions.UnadjustedN03_N.Value));
        Assert.True(double.IsNaN(emissions.UnadjustedIndirectedN20_NLeached.Value));
    }

}