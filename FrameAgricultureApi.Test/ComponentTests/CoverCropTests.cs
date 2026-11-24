using FrameAgricultureApi.ComponentLogic.CoverCropComponent;
using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Core.Outputs;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
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
                N_Return = 77m
            },
            // Fertiliser_RatioEFs: IPCCEF1, IPCCEF4, IPCCEF5, FracLeach, Ratio_NON, Ratio_N2N.
            CoverCropEmissionFactors = new CoverCropEmissionFactors
            {
                IpccEf1 = 1m,
                IpccEf4 = 1.4m,
                IpccEf5 = 0.75m,
                FracLeach = 30m,
                NO_N_ratio = 0.4m,
                N2_N_ratio = 3m
            },
            // CoverCrop: Cover Crop Type = All, Item = Frac Leach reduction.
            CoverCropMitigationFactors = new CoverCropMitigationFactors
            {
                FracLeachReduction = 45m
            }
        };
        var component = new CoverCrops();

        var actualEmissions = component.CoverCropEmissions(request);

        // Check unrounded values to verify decimal arithmetic before comparing legacy rounded results.
        Assert.Equal(0.17325m, actualEmissions.UnadjustedIndirected_N2O_N_Leached.Value);
        Assert.Equal(0.0952875m, actualEmissions.Indirect_N2O_N_Leached.Value);

        // Legacy expected values, compared as decimals with midpoint rounding away from zero.
        Assert.Multiple(
            () => AssertEmissionEqual(77m, actualEmissions.N.Value),
            () => AssertEmissionEqual(0.77m, actualEmissions.Direct_N2O_N.Value),
            () => AssertEmissionEqual(12.705m, actualEmissions.Direct_NO3_N_Leached.Value),
            () => AssertEmissionEqual(23.1m, actualEmissions.Unadjusted_NO3_N.Value),
            () => AssertEmissionEqual(0.0953m, actualEmissions.Indirect_N2O_N_Leached.Value),
            () => AssertEmissionEqual(0.1733m, actualEmissions.UnadjustedIndirected_N2O_N_Leached.Value),
            () => AssertEmissionEqual(0.3080m, actualEmissions.Direct_NO_N.Value),
            () => AssertEmissionEqual(2.31m, actualEmissions.Direct_N2_N.Value),
            () => AssertEmissionEqual(0.004312m, actualEmissions.Indirect_N2O_N_Volatilisation.Value)
        );
    }


    private static void AssertEmissionEqual(decimal expected, decimal? actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(
            decimal.Round(expected, 4, MidpointRounding.AwayFromZero),
            decimal.Round(actual.Value, 4, MidpointRounding.AwayFromZero));
    }
}