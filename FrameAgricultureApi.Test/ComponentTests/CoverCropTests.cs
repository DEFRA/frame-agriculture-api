using FrameAgricultureApi.ComponentLogic.CoverCropComponent;
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
                NReturn = 77
            },
            // Fertiliser_RatioEFs: IPCCEF1, IPCCEF4, IPCCEF5, FracLeach, Ratio_NON, Ratio_N2N.
            CoverCropEmissionFactors = new CoverCropEmissionFactors
            {
                IpccEf1 = 1,
                IpccEf4 = 1.4,
                IpccEf5 = 0.75,
                FracLeach = 30,
                N0_N_ratio = 0.4,
                N2_N_ratio = 3
            },
            // CoverCrop: Cover Crop Type = All, Item = Frac Leach reduction.
            CoverCropMitigationFactors = new CoverCropMitigationFactors
            {
                FracLeachReduction = 45
            }
        };
        var component = new CoverCrops(
            Substitute.For<IMongoService<CoverCropLookUpEntity>>(),
            Substitute.For<IMongoService<CropNonGhgLookUpEntity>>(),
            Substitute.For<IMongoService<CropResidueLookUpEntity>>(),
            Substitute.For<IMongoService<FertiliserRatioEfsLookUpEntity>>(),
            NullLogger<CoverCrops>.Instance);

        var actualEmissions = component.CoverCropEmissions(request);

        // Expected values and four-decimal precision from FLEA.Components.Tests/CoverCropsTests.cs.
        Assert.Multiple(
            () => Assert.Equal(77, actualEmissions.N.Value, 4),
            () => Assert.Equal(0.77, actualEmissions.DirectN20_N.Value, 4),
            () => Assert.Equal(12.705, actualEmissions.DirectNO3_NLeached.Value, 4),
            () => Assert.Equal(23.1, actualEmissions.UnadjustedN03_N.Value, 4),
            () => Assert.Equal(0.0953, actualEmissions.IndirectN20_NLeached.Value, 4),
            () => Assert.Equal(0.1733, actualEmissions.UnadjustedIndirectedN20_NLeached.Value, 4),
            () => Assert.Equal(0.3080, actualEmissions.DirectNO_N.Value, 4),
            () => Assert.Equal(2.31, actualEmissions.DirectN2_N.Value, 4),
            () => Assert.Equal(0.004312, actualEmissions.IndirectN20_NVolatilisation.Value, 4),
            () => Assert.True(double.IsNaN(actualEmissions.DirectNH3_N.Value)));
    }
}