using FrameAgricultureApi.Libraries.YardExcretionLivestock;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class YardExcretionTests
{

    public static IEnumerable<object[]> UnmitigatedYardExcretionEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.Beef;
        var animalType = BeefCattleType.Heifersforbreeding;
        var totalN = 1.45;
        var TAN = 0.45;
        var volatileSolids = 3038;
        var collectingYard = false;

        var expectedResult = new YardEmissions();
        expectedResult.InitialiseYardEmissions();

        expectedResult.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaTotalN].Value = 1.45;
        expectedResult.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaTAN].Value = 0.45;
        expectedResult.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaOrganicN].Value = 1;

        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.23625;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0033075;

        expectedResult.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = 0.3663828 * 10;

        expectedResult.AdditionalOutputs[AdditionalYardExcretaOutputs.TANout].Value = 0.21375;
        expectedResult.AdditionalOutputs[AdditionalYardExcretaOutputs.TotalNout].Value = 1.21375;
        expectedResult.AdditionalOutputs[AdditionalYardExcretaOutputs.OrganicNout].Value = 1;

        dataList.Add(new object[]
        {
            sector, animalType, totalN, TAN, volatileSolids, collectingYard, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(UnmitigatedYardExcretionEmissions_TestGenerator))]
    public void UnmitigatedYardExcretionEmissions_AllOUtputsCorrect(Sector sector, int animalType, double totalN, double TAN, double volatileSolids, bool collectingYard, YardEmissions expectedResult)
    {
        YardEmissions actualEmissions = YardExcretion.UnmitigatedYardExcretionEmissions(sector, animalType, totalN, TAN, volatileSolids, collectingYard);

        actualEmissions.RoundEmissions(3);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(3);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }
}
