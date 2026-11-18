using System.Text.Json;
using System.Text.Json.Serialization;
using FrameAgricultureApi.Core.Enums;
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
        Assert.Equal(77, emissions.additionalOutputs[AdditionalCoverCropEmissions.NReturn].Value);
        Assert.Equal(23.1, emissions.additionalOutputs[AdditionalCoverCropEmissions.UnadjustedNO3N].Value, 4);
        Assert.Equal(0.1732, emissions.additionalOutputs[AdditionalCoverCropEmissions.UnadjustedN2ONLeach].Value, 4);

        var exported = emissions.ExportEmissions();
        Assert.Equal(9, exported.Count);
        Assert.DoesNotContain(exported, emission => double.IsNaN(emission.Value));
        var leached = Assert.Single(exported, emission => emission.Name == "Indirect N2O-N (leached)");
        Assert.Equal("kg N2O-N", leached.Units);
        Assert.Equal(0.0953, leached.Value, 4);
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
        emissions.additionalOutputs[AdditionalCoverCropEmissions.NReturn].Value = 77.12345;

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
        Assert.Equal(77.123, emissions.additionalOutputs[AdditionalCoverCropEmissions.NReturn].Value);

        var json = JsonSerializer.Serialize(emissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        var restored = JsonSerializer.Deserialize<CoverCropEmissions>(json)!;
        Assert.Equal(1.235, restored.IndirectN20_NVolatilisation.Value);

        emissions.InitialiseCoverCropEmissions();
        Assert.Empty(emissions.ExportEmissions());
    }
}