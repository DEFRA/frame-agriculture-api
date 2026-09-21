using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.OutdoorLivestock.Tests;

public class OutdoorExcretalDepositionTests
{
    [Theory]
    [InlineData(Sector.Pigs, 14.77, 3.6925)]
    [InlineData(Sector.Pigs, 8.176, 2.0440)]
    [InlineData(Sector.Pigs, 13.23, 3.3075)]
    [InlineData(Sector.Pigs, 9.2180, 2.3045)]
    [InlineData(Sector.MinorLivestock, 5.0400, 0.3024)]
    [InlineData(Sector.MinorLivestock, 17.5800, 1.0548)]
    [InlineData(Sector.MinorLivestock, 30.00, 1.800)]
    [InlineData(Sector.MinorLivestock, 77.4, 4.6440)]
    public void NH3NEmission_AllOutputsCorrect(Sector sector, double totalNitrogen, double expectedResult)
    {
        var actualResult = OutdoorExcretalDeposition.NH3NEmission(sector, totalNitrogen);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 21.1, 0.0844)]
    [InlineData(Sector.Pigs, 11.68, 0.0467)]
    [InlineData(Sector.Pigs, 18.9, 0.0756)]
    [InlineData(Sector.Pigs, 13.1686, 0.0527)]
    [InlineData(Sector.MinorLivestock, 8.400, 0.0252)]
    [InlineData(Sector.MinorLivestock, 29.300, 0.0879)]
    [InlineData(Sector.MinorLivestock, 50.0, 0.1500)]
    [InlineData(Sector.MinorLivestock, 129.00, 0.3870)]
    public void N2ONEmission_AllOutputsCorrect(Sector sector, double totalAvailableNitrogen, double expectedResult)
    {
        var actualResult = OutdoorExcretalDeposition.N2ONEmission(sector, totalAvailableNitrogen);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(Sector.Pigs, 21.1, 2.11)]
    [InlineData(Sector.Pigs, 11.68, 1.168)]
    [InlineData(Sector.Pigs, 18.9, 1.890)]
    [InlineData(Sector.Pigs, 13.1686, 1.3169)]
    [InlineData(Sector.MinorLivestock, 8.400, 0.840)]
    [InlineData(Sector.MinorLivestock, 29.300, 2.930)]
    [InlineData(Sector.MinorLivestock, 50.00, 5.000)]
    [InlineData(Sector.MinorLivestock, 129.00, 12.900)]
    public void NO3NEmissionGeneric_AllOutputsCorrect(Sector sector, double totalAvailableNitrogen, double expectedResult)
    {
        var actualResult = OutdoorExcretalDeposition.NO3NEmissionGeneric(sector, totalAvailableNitrogen);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(Sector.Pigs, 0.0844, 0.0506)]
    [InlineData(Sector.Pigs, 0.04672, 0.0280)]
    [InlineData(Sector.Pigs, 0.0756, 0.0454)]
    [InlineData(Sector.Pigs, 0.0527, 0.0316)]
    [InlineData(Sector.MinorLivestock, 0.0252, 0.0151)]
    [InlineData(Sector.MinorLivestock, 0.0879, 0.0527)]
    [InlineData(Sector.MinorLivestock, 0.1500, 0.0900)]
    [InlineData(Sector.MinorLivestock, 0.3870, 0.2322)]
    public void NOEmissions_AllOutputsCorrect(Sector sector, double totalAvailableNitrogen, double expectedResult)
    {
        var actualResult = OutdoorExcretalDeposition.NOEmissions(sector, totalAvailableNitrogen);
        Assert.Equal(expectedResult, actualResult, 4);
    }


}
