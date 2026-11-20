using FrameAgricultureApi.ComponentLogic.CoverCropComponent;
using FrameAgricultureApi.Core.Outputs;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace FrameAgricultureApi.Test.ComponentTests;

/// <summary>
/// Regression tests for cover crop emissions grouped by pollutant and pathway.
/// </summary>
public class CoverCropTests
{
    /// <summary>
    /// Replays the legacy All cover crop scenario using values resolved from its lookup collections.
    /// </summary>
    [Fact]
    public void AllCoverCropEmissions_MatchLegacyExpectedOutputs()
    {
        // Values queried from FRAMESeparatedByLUT for the legacy CoverCropType.All scenario.
        var request = new CoverCropRequest
        {
            // CoverCrop: Cover Crop Type = All, Item = N return.
            CoverCropInputs = new CoverCropInputs
            {
                NReturn = 77m
            },
            // Fertiliser_RatioEFs: IPCCEF1, IPCCEF4, IPCCEF5, FracLeach, Ratio_NON, Ratio_N2N.
            CoverCropEmissionFactors = new CoverCropEmissionFactors
            {
                IpccEf1 = 1m,
                IpccEf4 = 1.4m,
                IpccEf5 = 0.75m,
                FracLeach = 30m,
                N0_N_ratio = 0.4m,
                N2_N_ratio = 3m
            },
            // CoverCrop: Cover Crop Type = All, Item = Frac Leach reduction.
            CoverCropMitigationFactors = new CoverCropMitigationFactors
            {
                FracLeachReduction = 45m
            }
        };
        var component = new CoverCrops(
            Substitute.For<IMongoService<CoverCropLookUpEntity>>(),
            Substitute.For<IMongoService<CropNonGhgLookUpEntity>>(),
            Substitute.For<IMongoService<CropResidueLookUpEntity>>(),
            Substitute.For<IMongoService<FertiliserRatioEfsLookUpEntity>>(),
            NullLogger<CoverCrops>.Instance);

        var actualEmissions = component.CoverCropEmissions(request);

        // Check unrounded values to verify decimal arithmetic before comparing legacy rounded results.
        Assert.Equal(0.17325m, actualEmissions.UnadjustedIndirectedN20_NLeached.Value);
        Assert.Equal(0.0952875m, actualEmissions.IndirectN20_NLeached.Value);

        // Legacy expected values, compared as decimals with midpoint rounding away from zero.
        Assert.Multiple(
            () => AssertEmissionEqual(77m, actualEmissions.N.Value),
            () => AssertEmissionEqual(0.77m, actualEmissions.DirectN20_N.Value),
            () => AssertEmissionEqual(12.705m, actualEmissions.DirectNO3_NLeached.Value),
            () => AssertEmissionEqual(23.1m, actualEmissions.UnadjustedN03_N.Value),
            () => AssertEmissionEqual(0.0953m, actualEmissions.IndirectN20_NLeached.Value),
            () => AssertEmissionEqual(0.1733m, actualEmissions.UnadjustedIndirectedN20_NLeached.Value),
            () => AssertEmissionEqual(0.3080m, actualEmissions.DirectNO_N.Value),
            () => AssertEmissionEqual(2.31m, actualEmissions.DirectN2_N.Value),
            () => AssertEmissionEqual(0.004312m, actualEmissions.IndirectN20_NVolatilisation.Value),
            () => Assert.Null(actualEmissions.DirectNH3_N.Value));
    }

    /// <summary>
    /// Rounds core and additional decimal outputs without replacing missing values.
    /// </summary>
    [Fact]
    public void RoundEmissions_RoundsDecimalMidpointsAndPreservesNulls()
    {
        var emissions = new CoverCropEmissions();
        emissions.DirectN20_N.Value = 0.12345m;
        emissions.UnadjustedIndirectedN20_NLeached.Value = 0.17325m;

        emissions.RoundEmissions(4);

        Assert.Equal(0.1235m, emissions.DirectN20_N.Value);
        Assert.Equal(0.1733m, emissions.UnadjustedIndirectedN20_NLeached.Value);
        Assert.Null(emissions.DirectNH3_N.Value);
        Assert.Null(emissions.N.Value);
    }

    /// <summary>
    /// Clears previously calculated core and additional decimal outputs.
    /// </summary>
    [Fact]
    public void InitialiseCoverCropEmissions_ResetsValuesToNull()
    {
        var emissions = new CoverCropEmissions();
        emissions.DirectN20_N.Value = 0.77m;
        emissions.N.Value = 77m;

        emissions.InitialiseCoverCropEmissions();

        Assert.Null(emissions.DirectN20_N.Value);
        Assert.Null(emissions.N.Value);
    }

    private static void AssertEmissionEqual(decimal expected, decimal? actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(
            decimal.Round(expected, 4, MidpointRounding.AwayFromZero),
            decimal.Round(actual.Value, 4, MidpointRounding.AwayFromZero));
    }
}