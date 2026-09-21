using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Fertiliser.Tests;

public class FertiliserFunctionsTests
{

    [Theory]
    [InlineData(20, 0, 0.072222637)]
    [InlineData(100, 0, 0.384073136)]
    [InlineData(200, 0, 0.831106392)]
    [InlineData(20, 0.5, 0.119074998)]
    [InlineData(100, 0.5, 0.633229548)]
    [InlineData(200, 0.5, 1.370262787)]
    public void CalculateN2ON_UreaBasedFertiliser_AllOutputsCorrect(double rateN, double uncertainty, double expectedResult)
    {
        var actualResult = FertiliserFunctions.CalculateN2ON_UreaBasedFertiliser(rateN, uncertainty);
        Assert.Equal(expectedResult, actualResult, 5);
    }

    [Theory]
    [InlineData(20, 867.08, 0, 0.136876559)]
    [InlineData(100, 867.08, 0, 0.76215421)]
    [InlineData(200, 867.08, 0, 1.752799457)]
    [InlineData(20, 991.93, 0, 0.16674047)]
    [InlineData(100, 991.93, 0, 0.944488584)]
    [InlineData(200, 991.93, 0, 2.222937023)]
    [InlineData(20, 991.93, 0.5, 0.274909)]
    [InlineData(100, 991.93, 0.5, 1.557198)]
    [InlineData(200, 991.93, 0.5, 3.665004)]
    public void CalculateN2ON_nonUreaBasedFertiliser_AllOutputsCorrect(double rateN, double annualAverageRainfall, double uncertainty, double expectedResult)
    {
        var actualResult = FertiliserFunctions.CalculateN2ON_nonUreaBasedFertiliser(rateN, annualAverageRainfall, uncertainty);
        Assert.Equal(expectedResult, actualResult, 5);
    }

    [Theory]
    [InlineData(20, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(100, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(200, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]

    [InlineData(20, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(100, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(200, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]

    [InlineData(20, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]
    [InlineData(100, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]
    [InlineData(200, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]

    [InlineData(20, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 8.903394)]
    [InlineData(100, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 12.117232)]
    [InlineData(200, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 14.3603129)]

    [InlineData(20, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(100, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(200, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]

    [InlineData(20, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(100, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(200, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]

    [InlineData(20, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 4.55062359)]
    [InlineData(100, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 6.193251911)]
    [InlineData(200, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 7.339715468)]

    [InlineData(20, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 4.55062359)]
    [InlineData(100, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 6.193251911)]
    [InlineData(200, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 7.339715468)]

    [InlineData(20, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 8.90339398)]
    [InlineData(100, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 12.117232)]
    [InlineData(200, 7.37, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 14.36031287)]

    [InlineData(20, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 8.90339398)]
    [InlineData(100, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 12.117232)]
    [InlineData(200, 7.37, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 14.36031287)]

    [InlineData(20, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(100, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(200, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]

    [InlineData(20, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(100, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(200, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]

    [InlineData(20, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]
    [InlineData(100, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]
    [InlineData(200, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]

    [InlineData(20, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 9.7969053)]
    [InlineData(100, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 13.3332708)]
    [InlineData(200, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 15.80146016)]

    [InlineData(20, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(100, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(200, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]

    [InlineData(20, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(100, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(200, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]

    [InlineData(20, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 5.007307154)]
    [InlineData(100, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 6.81478351)]
    [InlineData(200, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 8.076301861)]

    [InlineData(20, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 5.007307154)]
    [InlineData(100, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 6.81478351)]
    [InlineData(200, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 8.076301861)]

    [InlineData(20, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 9.7969053)]
    [InlineData(100, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 13.33327208)]
    [InlineData(200, 8.06, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 15.80146016)]

    [InlineData(20, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 9.7969053)]
    [InlineData(100, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 13.33327208)]
    [InlineData(200, 8.06, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 15.80146016)]

    [InlineData(20, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(100, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(200, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]

    [InlineData(20, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(100, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]
    [InlineData(200, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumNitrate, 1.8)]

    [InlineData(20, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]
    [InlineData(100, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]
    [InlineData(200, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 1.8)]

    [InlineData(20, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 22.0131)]
    [InlineData(100, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 29.959119)]
    [InlineData(200, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, 35.505)]

    [InlineData(20, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(100, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(200, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]

    [InlineData(20, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(100, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]
    [InlineData(200, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.CalciumAmmoniumNitrate, 1.8)]

    [InlineData(20, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 11.25114)]
    [InlineData(100, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 15.3124386)]
    [InlineData(200, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 18.147)]

    [InlineData(20, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 11.25114)]
    [InlineData(100, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 15.3124386)]
    [InlineData(200, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.UreaAmmoniumNitrate, 18.147)]

    [InlineData(20, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 22.0131)]
    [InlineData(100, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 29.959119)]
    [InlineData(200, 60.69, true, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 35.505)]

    [InlineData(20, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 22.0131)]
    [InlineData(100, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 29.959119)]
    [InlineData(200, 60.69, false, new[] { 0.16, 0.11, 0.08, 0.07, 0.06, 0.52 }, FertiliserType.Urea, 35.505)]
    public void CalculateFertiliserAmmoniaEmissionFactor_AllOutputsCorrect(double rateN, double temperatureMonth, bool acidicSoil, double[] RainfallLikelihood, FertiliserType fertiliser, double expectedResult)
    {
        var actuallResult = FertiliserFunctions.CalculateFertiliserAmmoniaEmissionFactor(rateN, temperatureMonth, acidicSoil, RainfallLikelihood, fertiliser);

        Assert.Equal(expectedResult, actuallResult, 5);
    }
}
