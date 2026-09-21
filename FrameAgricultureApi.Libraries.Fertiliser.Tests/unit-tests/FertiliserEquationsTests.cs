using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Fertiliser.Tests.unit_tests;

public class FertiliserEquationsTests
{
    // Takes the fracLeach as a percentage but is shown as a decimal in the excel!
    [Theory]
    [InlineData(20, 30, 6)]
    [InlineData(100, 30, 30)]
    [InlineData(200, 30, 60)]
    [InlineData(20, 60, 12)]
    [InlineData(100, 60, 60)]
    [InlineData(200, 60, 120)]
    public void NO3N_Leached_AllOuptusCorrect(double rateN, double fracLeach, double expectedResult)
    {
        var actualResult = FertiliserEquations.NO3N_Leached(rateN, fracLeach);
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 45)]
    [InlineData(FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(FertiliserType.UreaAmmoniumNitrate, 23)]
    [InlineData(FertiliserType.Urea, 45)]
    public void BaseAmmoniaEF_AllOutputsCorrect(FertiliserType fertiliser, double expectedResult)
    {
        var actualResult = FertiliserEquations.BaseAmmoniaEF(fertiliser);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(100, 45, 37.971)]
    [InlineData(20, 45, 27.9)]
    [InlineData(100, 23, 19.4074)]
    [InlineData(200, 23, 23)]
    public void AmmoniaEF_RateModifiation_AllOutputsCorrect(double rateN, double currentAmmoniaEF, double expectedResult)
    {

        FertiliserEquations.AmmoniaEF_RateModification(rateN, ref currentAmmoniaEF);

        Assert.Equal(expectedResult, currentAmmoniaEF, 2);
    }

    [Theory]
    [InlineData(new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, 37.971, 29.959119)]

    [InlineData(new[] { 0.19, 0.12, 0.09, 0.07, 0.06, 0.47 }, 27.9, 21.204)]
    public void AmmoniaEF_RainfallEventModification_AllOutputsCorrect(double[] rainfallEventLikelihood, double currentAmmoniaEF, double expectedResult)
    {
        FertiliserEquations.AmmoniaEF_RainfallEventModification(rainfallEventLikelihood, ref currentAmmoniaEF);

        Assert.Equal(expectedResult, currentAmmoniaEF, 5);

    }

    [Theory]
    [InlineData(29.959119, 8.06, 13.33327208)]
    public void AmmoniaEF_TemperatureModification_AllOutputsCorrect(double currentAmmoniaEF, double temperatureMonth, double expectedResult)
    {
        FertiliserEquations.AmmoniaEF_TemperatureModification(ref currentAmmoniaEF, temperatureMonth);

        Assert.Equal(expectedResult, currentAmmoniaEF, 5);
    }

    [Theory]
    [InlineData(13.13153477, false, FertiliserType.Urea, 13.1315348)]
    [InlineData(13.33327208, false, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 13.33327208)]
    public void AmmoniaEF_SoilModification_AllOutputsCorrect(double currentAmmoniaEF, bool acidic, FertiliserType fertiliser, double expectedResult)
    {
        FertiliserEquations.AmmoniaEF_SoilModification(ref currentAmmoniaEF, acidic, fertiliser);
        Assert.Equal(expectedResult, currentAmmoniaEF, 5);
    }

    [Theory]
    [InlineData(20, 9.648674517, 0, 1.813951)]
    [InlineData(100, 13.13153477, 0, 12.34364)]
    [InlineData(100, 29.959119, 0, 28.16157)]
    public void FertiliserAmoniaEmission_AllOutputsCorrect(double rateN, double ammoniaEF, double ammoniaModelCoefficientUNcertainty, double expectedResult)
    {
        var actualResult = FertiliserEquations.FertiliserAmmoniaEmission(rateN, ammoniaEF, ammoniaModelCoefficientUNcertainty);

        Assert.Equal(expectedResult, actualResult, 5);
    }
}
