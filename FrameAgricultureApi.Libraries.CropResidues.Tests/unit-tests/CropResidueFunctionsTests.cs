using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.CropResidues.Tests.unit_tests;

public class CropResidueFunctionsTests
{
    [Theory]
    [InlineData(5.923419011, 5.094140349, 1.173913, CropType.Oats, 5.9800778015)]
    [InlineData(5.4, 4.644, 0.923077, CropType.Spring_barley, 4.286769)]
    [InlineData(8.97, 7.7142, 1, CropType.Wheat, 7.7142)]
    public void CalculateAboveGroundResidueDryMatter_HarvestIndex_AllOutputsCorrect(double cropYieldFreshWeight,
        double cropPrimaryDryMatter, double functionalHarvestIndex, CropType cropType, double expectedResult)
    {
        var percentCropPrimaryDryMatter = cropPrimaryDryMatter / cropYieldFreshWeight * 100;
        var actualResult = CropResidueFunctions.CalculateAboveGroundResidueDryMatter(cropYieldFreshWeight, percentCropPrimaryDryMatter, functionalHarvestIndex, cropType);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(3.95, 3.397, 1.13, 0.85, CropType.Field_beans_harvesteddry, 4.6886)]
    public void CalculateAboveGroundResidueDryMatter_IPCC_AllOutputsCorrect(double cropYieldFreshWeight,
        double cropPrimaryDryMatter, double slopeIPCC, double interceptIPCC, CropType cropType, double expectedResult)
    {
        var percentCropPrimaryDryMatter = cropPrimaryDryMatter / cropYieldFreshWeight * 100;
        var actualResult = CropResidueFunctions.CalculateAboveGroundResidueDryMatter(cropYieldFreshWeight, percentCropPrimaryDryMatter, slopeIPCC, interceptIPCC, cropType);

        Assert.Equal(expectedResult, actualResult, 4);
    }
}
