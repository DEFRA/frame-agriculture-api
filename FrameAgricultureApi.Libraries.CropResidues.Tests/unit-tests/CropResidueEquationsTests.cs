namespace FrameAgricultureApi.Libraries.CropResidues.Tests.unit_tests;

public class CropResidueEquationsTests
{
    [Theory]
    [InlineData(5.9234, 5.0941, 5.0941)]
    [InlineData(5.9400, 5.1084, 5.1084)]
    [InlineData(3.95, 3.397, 3.397)]
    public void CalculateYieldDryMatter_AllOutputsCorrect(double cropYieldPrimaryFw, double cropYieldPrimaryDM, double expectedResult)
    {
        double cropYieldPrimaryDMPercent = cropYieldPrimaryDM / cropYieldPrimaryFw * 100;

        var actualResult = CropResidueEquations.CalculateYieldDryMatter(cropYieldPrimaryFw, cropYieldPrimaryDMPercent);
        Assert.Equal(expectedResult, actualResult, 5);
    }

    [Theory]
    [InlineData(1.173913, 5.0941403, 5.9800778)]
    [InlineData(0.923077, 4.6440, 4.28677)]
    [InlineData(1, 7.7142, 7.7142)]
    public void CalculateAboveGroundResidueDryMatterHarvestIndex_AllOutputsCorrect(double FunctionalHarvestIndex, double CropYieldDryMatter, double expectedResult)
    {
        var actualResult = CropResidueEquations.CalculateAboveGroundResidueDryMatterHarvestIndex(FunctionalHarvestIndex, CropYieldDryMatter);
        Assert.Equal(expectedResult, actualResult, 5);
    }

    [Theory]
    [InlineData(3.3970, 1.13, 0.85, 4.68861)]
    [InlineData(7.866, 0.1, 1.06, 1.8466)]
    [InlineData(17.135, 1.07, 1.54, 19.87445)]
    public void CalculateAboveGroundResidueDryMatterIPCC_AllOutputsCorrect(double CropYieldDryMatter, double CropYieldToAboveGroundResidueSlope, double CropYieldToAboveGroundResidueIntercept, double expectedResult)
    {
        var actualResult = CropResidueEquations.CalculateAboveGroundResidueDryMatterIPCC(CropYieldDryMatter, CropYieldToAboveGroundResidueSlope, CropYieldToAboveGroundResidueIntercept);
        Assert.Equal(expectedResult, actualResult, 5);
    }

    [Theory]
    [InlineData(5.9800778015, 50, 2.9900389)]
    [InlineData(7.7142, 50, 3.8571)]
    [InlineData(2.5697368, 20, 0.513947368)]
    public void CalculateAboveGroundResidue_AllOutputsCorrect(double AboveGroundResidueDryMatter, double ResidueRemaining, double expectedResult)
    {
        var actualResult = CropResidueEquations.CalculateAboveGroundResidue(AboveGroundResidueDryMatter, ResidueRemaining);
        Assert.Equal(expectedResult, actualResult, 5);
    }

    [Theory]
    [InlineData(5.0941, 5.9801, 0.25, 2.7686)]
    [InlineData(7.095, 6.549231, 0.22, 3.0017)]
    [InlineData(1.575, 2.56974, 0.349, 1.446513)]
    public void CalculateBelowGroundResidueDryMatter_AllOutputsCorrect(double CropYieldDryMatter, double AboveGroundResidueDryMatter,
        double AboveToBelowGroundResidueRatio, double expectedResult)
    {
        var actualResult = CropResidueEquations.CalculateBelowGroundResidueDryMatter(CropYieldDryMatter, AboveGroundResidueDryMatter,
                AboveToBelowGroundResidueRatio);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(5.9800778, 5.4, 32.2924)]
    [InlineData(2.1433846, 6.7, 14.3606769)]
    [InlineData(3.2746154, 6.7, 21.939923)]
    public void CalculateResidueN_AllOutputsCorrect(double TotalResidue, double ResidueNConcentration, double expectedResult)
    {
        var actualResult = CropResidueEquations.CalculateResidueN(TotalResidue, ResidueNConcentration);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(54.4409, 30, 16.3323)]
    [InlineData(41.8674, 30, 12.5602)]
    [InlineData(55.850808, 30, 16.755242)]
    public void N03N_Leached_AllOutputsCorrect(double totalResidueN, double fracLeach, double expectedResult)
    {
        var actualResult = CropResidueEquations.NO3N_Leached(totalResidueN, fracLeach);

        Assert.Equal(expectedResult, actualResult, 4);
    }
}
