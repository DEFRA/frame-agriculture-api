using FrameAgricultureApi.Libraries.Housing;
using FrameAgricultureApi.Libraries.LookUps;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class HousingTests
{
    public static IEnumerable<object[]> UnmitigatedHousingManureManagementEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.Beef;
        var animalType = (int)BeefCattleType.Heifersforbreeding;
        var housingSystem = ManureHousingSystem.Solid_Floor_with_Bedding;
        var organicMatterType = OrganicMatterType.CattleFYM;
        var totalN = 100.0;
        var TAN = 50.0;
        var volatileSolids = 50.00;
        var beddingN = 1.0;

        var expectedResult = new HousingEmissions();
        expectedResult.InitaliseHousingEmissions();

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = 100;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = 50;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = 50;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = 1;

        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 2.02;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 8.4;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.202;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 6.06;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.1204;

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = 20;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = 84.31800000;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = 13.31800000;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = 71.00000000;
        expectedResult.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = 0;

        dataList.Add(new object[]{
            sector, animalType, housingSystem, organicMatterType, totalN, TAN, volatileSolids, beddingN, expectedResult
        });

        sector = Sector.Poultry;
        animalType = (int)PoultryType.GrowingPullets;
        housingSystem = ManureHousingSystem.Deep_Litter;
        organicMatterType = OrganicMatterType.PoultryLitter;
        totalN = 0.358770;
        TAN = 0.251139030;
        volatileSolids = 3.6500;
        beddingN = 0.009125;

        expectedResult = new HousingEmissions();
        expectedResult.InitaliseHousingEmissions();

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = 0.3588;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = 0.25114;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = totalN - TAN;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = 0.009125;

        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.001839475;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.033903769;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.000183948;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.005518426;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.000477228;

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = 0;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = 0.326449;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = 0.2097;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = 0.326449 - 0.2097;
        expectedResult.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = 0;

        dataList.Add(new object[]{
            sector, animalType, housingSystem, organicMatterType, totalN, TAN, volatileSolids, beddingN, expectedResult
        });

        sector = Sector.Pigs;
        animalType = (int)PigType.Sows;
        housingSystem = ManureHousingSystem.Solid_Floor_with_Bedding;
        organicMatterType = OrganicMatterType.PigFYM;
        totalN = 24.0200;
        TAN = 14.7700;
        volatileSolids = 167.9;
        beddingN = 2.9200;

        expectedResult = new HousingEmissions();
        expectedResult.InitaliseHousingEmissions();

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = 24.0200;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = 14.7700;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = totalN - TAN;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = 2.9200;

        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.5388;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 4.9480;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.0539;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.6164;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0700;

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = 5.9080;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = 19.7830;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = 1.70497;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = 19.7830 - 1.70497;
        expectedResult.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = 0;

        dataList.Add(new object[]{
            sector, animalType, housingSystem, organicMatterType, totalN, TAN, volatileSolids, beddingN, expectedResult
        });

        sector = Sector.MinorLivestock;
        animalType = (int)MinorLivestockType.Goats;
        housingSystem = ManureHousingSystem.Solid_Floor_with_Bedding;
        organicMatterType = OrganicMatterType.MinorLivestockFYM;
        totalN = 9.5315;
        TAN = 5.0400;
        volatileSolids = 109.5;
        beddingN = 1.1315;

        expectedResult = new HousingEmissions();
        expectedResult.InitaliseHousingEmissions();

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = 9.5315;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = 5.0400;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = totalN - TAN;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = 1.1315;

        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.2133;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.8467;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.02133;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.6398;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0122;

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = 2.0160;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = 8.9419;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = 1.302914;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = 8.9419 - 1.302914;
        expectedResult.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = 0.39;

        dataList.Add(new object[]{
            sector, animalType, housingSystem, organicMatterType, totalN, TAN, volatileSolids, beddingN, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(UnmitigatedHousingManureManagementEmissions_TestGenerator))]
    public void UnmitigatedHousingManureManagementEmissions_AllOutputsCorrect(Sector sector, int animalType, ManureHousingSystem housingSystem, OrganicMatterType organicMatterType,
        double totalN, double TAN, double volatilesolids, double beddingN, HousingEmissions expectedResult)
    {
        var actualEmissions = Housing.UnmitigatedHousingManureManagementEmissions(sector, animalType, housingSystem, organicMatterType, totalN, TAN, volatilesolids, beddingN);

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

    public static IEnumerable<object[]> UnmitigatedHousingManureManagementEmissions_Sheep_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.Sheep;
        var animalType = (int)SheepType.Ram;

        var energyBalance = new SheepEnergyBalance
        {
            FieldEntericMethane = 6.827447259,
            FieldVolatileSolids = 137.0920614,
            FieldTotalNitrogen = 8.164032536,
            FieldAvailableNitrogen = 4.336841577,
            FieldGrossEnergy = 7488.366586,
            FieldMetabolisableEnergy = 4324.480082,
            FieldDryMatterIntake = 400.7941338,
            FieldDays = 360,
            FieldDungNitrogen = 3.717980112,
            FieldUrineNitrogen = 4.446052424,
            HouseEntericMethane = 0.095689575,
            HouseVolatileSolids = 2.196151093,
            HouseTotalNitrogen = 0.106709841,
            HouseAvailableNitrogen = 0.032557243,
            HouseGrossEnergy = 105.0147233,
            HouseMetabolisableEnergy = 55.21276458,
            HouseDryMatterIntake = 5.636256082,
            HouseDays = 5,
            HouseDungNitrogen = 0.048893501,
            HouseUrineNitrogen = 0.050446436,
            FieldDungNitrogenCP200 = 4.050711348,
            FieldUrineNitrogenCP200 = 7.045596623,
            FieldDungNitrogenCP100 = 3.234258436,
            FieldUrineNitrogenCP100 = 2.97492381,
            FieldTotalNitrogenCP200 = 11.09630797,
            FieldAvailableNitrogenCP200 = 6.596363831,
            FieldTotalNitrogenCP100 = 6.209182246,
            FieldAvailableNitrogenCP100 = 3.013824004,
            FieldNitrogenIntake = 8.631024169,
            HouseNitrogenIntake = 0.1057542,
            FieldNitrogenIntakeCP100 = 6.676173879,
            FieldNitrogenIntakeCP200 = 11.5632996,
            FieldExcretaMass = 1103.306293,
            FieldExcretaVolume = 2.97492381,
            HouseExcretaMass = 8.802569406,
            HouseExcretaVolume = 10.25542182,
            HouseManureMass = 10.51650072,
            HouseManureVolume = 21.42414139
        };

        var expectedResult = new HousingEmissions();
        expectedResult.InitaliseHousingEmissions();

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = 0.106709841;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = 0.032557243;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = 0.106709841 - 0.032557243;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = 0.007369905;

        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.0;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.009116028;
        //Todo These aren't in the spreadsheet?
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.0;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.0;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.000127624;

        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = 0.0;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = 0.097593813;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = 0.023441215;
        expectedResult.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = 0.097593813 - 0.023441215;

        dataList.Add(new object[] {
            sector, animalType, energyBalance, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(UnmitigatedHousingManureManagementEmissions_Sheep_TestGenerator))]
    public void UnmitigatedHousingManureManagementEmissions_Sheep_AllOutputsCorrect(Sector sector, int animalType, SheepEnergyBalance energyBalance, HousingEmissions expectedResult)
    {
        var actualEmissions = Housing.UnmitigatedHousingManureManagementEmissions_Sheep(sector, animalType, energyBalance);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        expectedResult.RoundEmissions(4);

        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }
}
