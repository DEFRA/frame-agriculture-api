using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.OrganicMatterApplication.Tests;

public class OrganicMatterApplicationFunctionsTests
{
    //Todo add tests for Cattle Spreading!
    [Theory]
    [InlineData(OrganicMatterType.DigestatePigSlurry, 10.586013, SpreadingLandUse.Grassland, Month.NotSet, 2.561815146)]
    [InlineData(OrganicMatterType.PigFYM, 10.586013, SpreadingLandUse.Grassland, Month.NotSet, 7.230246879)]
    [InlineData(OrganicMatterType.DigestatePigFYM, 10.586013, SpreadingLandUse.Grassland, Month.NotSet, 2.561815146)]
    [InlineData(OrganicMatterType.PigSlurry, 10.586013, SpreadingLandUse.Grassland, Month.NotSet, 2.561815146)]
    [InlineData(OrganicMatterType.DigestatePigSlurry, 10.586013, SpreadingLandUse.Arable, Month.NotSet, 2.561815146)]
    [InlineData(OrganicMatterType.PigFYM, 10.586013, SpreadingLandUse.Arable, Month.NotSet, 7.230246879)]
    [InlineData(OrganicMatterType.DigestatePigFYM, 10.586013, SpreadingLandUse.Arable, Month.NotSet, 2.561815146)]
    [InlineData(OrganicMatterType.PigSlurry, 10.586013, SpreadingLandUse.Arable, Month.NotSet, 2.561815146)]
    [InlineData(OrganicMatterType.PigSlurry, 14.77, SpreadingLandUse.Arable, Month.NotSet, 3.57434)]
    [InlineData(OrganicMatterType.PigSlurry, 14.77, SpreadingLandUse.Grassland, Month.NotSet, 3.57434)]
    [InlineData(OrganicMatterType.SheepFYM, 0.013129228, SpreadingLandUse.Grassland, Month.NotSet, 0.008969888)]
    public void NH3NEmission_AllOutputsCorrect(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month, double expectedResult)
    {
        var actualResult = OrganicMatterApplicationFunctions.NH3NEmission(type, sourceN, landAppliedTo, month);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(OrganicMatterType.PigSlurry, 16.080913, SpreadingLandUse.Grassland, Month.NotSet, 0.120204825)]
    [InlineData(OrganicMatterType.PigFYM, 16.080913, SpreadingLandUse.Grassland, Month.NotSet, 0.053067013)]
    [InlineData(OrganicMatterType.DigestatePigSlurry, 16.080913, SpreadingLandUse.Grassland, Month.NotSet, 0.120204825)]
    [InlineData(OrganicMatterType.DigestatePigFYM, 8.176, SpreadingLandUse.Grassland, Month.NotSet, 0.0611156)]
    [InlineData(OrganicMatterType.DigestatePigFYM, 8.176, SpreadingLandUse.Arable, Month.NotSet, 0.0611156)]
    [InlineData(OrganicMatterType.PigSlurry, 8.176, SpreadingLandUse.Arable, Month.NotSet, 0.0611156)]
    [InlineData(OrganicMatterType.SheepFYM, 0.023441215, SpreadingLandUse.Arable, Month.NotSet, 0.000487969)]
    public void N2ONEmission_AllOutputsCorrect(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month, double expectedResult)
    {
        var actualResult = OrganicMatterApplicationFunctions.N2ONEmission(type, sourceN, landAppliedTo, month);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(OrganicMatterType.DigestatePigSlurry, 8.176, SpreadingLandUse.Arable, Month.NotSet, 2.4528)]
    [InlineData(OrganicMatterType.DigestatePigSlurry, 16.080913, SpreadingLandUse.Arable, Month.NotSet, 4.8242739)]
    [InlineData(OrganicMatterType.DigestatePigFYM, 16.080913, SpreadingLandUse.Arable, Month.NotSet, 4.8242739)]
    [InlineData(OrganicMatterType.PigFYM, 16.080913, SpreadingLandUse.Grassland, Month.NotSet, 1.6080913)]
    [InlineData(OrganicMatterType.SheepFYM, 0.023441215, SpreadingLandUse.Grassland, Month.NotSet, 0.002146274)]
    public void NO3NLeaching_AllOutputsCorrect(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month, double expectedResult)
    {
        var actualResult = OrganicMatterApplicationFunctions.NO3NLeaching(type, sourceN, landAppliedTo, month);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(OrganicMatterType.PigFYM, 0.053067013, SpreadingLandUse.Grassland, Month.NotSet, 0.0318402)]
    [InlineData(OrganicMatterType.PigSlurry, 0.053067013, SpreadingLandUse.Grassland, Month.NotSet, 0.0318402)]
    [InlineData(OrganicMatterType.DigestatePigSlurry, 0.1577225, SpreadingLandUse.Grassland, Month.NotSet, 0.0946335)]
    [InlineData(OrganicMatterType.DigestatePigFYM, 0.1577225, SpreadingLandUse.Grassland, Month.NotSet, 0.0946335)]
    [InlineData(OrganicMatterType.PigSlurry, 0.1577225, SpreadingLandUse.Arable, Month.NotSet, 0.063089)]
    [InlineData(OrganicMatterType.SheepFYM, 0.00028803, SpreadingLandUse.Arable, Month.NotSet, 0.000172818)]
    public void NONEmission_AllOutputsCorrect(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month, double expectedResult)
    {
        var actualResult = OrganicMatterApplicationFunctions.NONEmission(type, sourceN, landAppliedTo, month);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(OrganicMatterType.PigSlurry, 0.1577225, SpreadingLandUse.Arable, Month.NotSet, 0.4731675)]
    [InlineData(OrganicMatterType.PigFYM, 0.06963, SpreadingLandUse.Arable, Month.NotSet, 0.20889)]
    [InlineData(OrganicMatterType.DigestatePigSlurry, 0.1577225, SpreadingLandUse.Grassland, Month.NotSet, 0.4731675)]
    [InlineData(OrganicMatterType.DigestatePigFYM, 0.1577225, SpreadingLandUse.Grassland, Month.NotSet, 0.4731675)]
    [InlineData(OrganicMatterType.SheepFYM, 0.00028803, SpreadingLandUse.Arable, Month.NotSet, 0.00086409)]
    public void N2NEmission_AllOutputsCorrect(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month, double expectedResult)
    {
        var actualResult = OrganicMatterApplicationFunctions.N2NEmission(type, sourceN, landAppliedTo, month);
        Assert.Equal(expectedResult, actualResult, 3);
    }
}
