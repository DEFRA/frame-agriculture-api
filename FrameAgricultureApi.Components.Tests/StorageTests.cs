using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.Storage;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class StorageTests
{
    public static IEnumerable<object[]> UnmitigatedstorageManureManagementEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.Beef;
        var animalType = BeefCattleType.Cerealfedbull;
        var organicMatterType = OrganicMatterType.CattleSlurry;
        var manureStorageSystem = ManureStorageSystem.Slurry_AboveGroundTanks;
        var totalN = 83;
        var TAN = 13.0;
        var volatileSolids = 83.4;
        var anaerobicDigestion = true;

        StorageEmissions expectedResult = new();
        expectedResult.InitialiseStorageEmissions();

        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNin].Value = totalN;
        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANin].Value = TAN;
        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNin].Value = totalN - TAN;
        expectedResult.TypeIn = organicMatterType;
        expectedResult.TypeOut = OrganicMatterType.DigestateCattleSlurry;

        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 1.65;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0231;
        expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0;
        expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value = 0;

        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.NMineralised].Value = 7;
        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNout].Value = 81.35;
        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANout].Value = 18.35;
        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNout].Value = 63;

        expectedResult.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value = 1.7098668;

        dataList.Add(new object[]
        {
            sector, animalType, organicMatterType, manureStorageSystem, totalN, TAN, volatileSolids, anaerobicDigestion, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(UnmitigatedstorageManureManagementEmissions_TestGenerator))]
    public void UnmitigatedstorageManureManagementEmissions(Sector sector, int animalType, OrganicMatterType organicMatterType, ManureStorageSystem manureStorageSystem, double totalN, double TAN, double volatilesolids, bool anaerobicDigestion, StorageEmissions expectedResult)
    {
        var actualResult = Storage.UnmitigatedstorageManureManagementEmissions(sector, animalType, organicMatterType, manureStorageSystem, totalN, TAN, volatilesolids, anaerobicDigestion);
        actualResult.RoundEmissions(4);

        string actualResultJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualResultJson);
    }

    public static IEnumerable<object[]> UnmitigatedstorageManureManagementEmissionsSheep_TestGenerator()
    {
        List<object[]> dataList = new();

        var animalType = (int)SheepType.Ram;
        var totalN = 0.097593813;
        var TAN = 0.023441215;

        SheepEnergyBalance energyBalance = new()
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
            FieldExcretaVolume = 1171.555705,
            HouseExcretaMass = 8.802569406,
            HouseExcretaVolume = 10.25542182,
            HouseManureMass = 10.51650072,
            HouseManureVolume = 21.42414139
        };

        StorageEmissions expectedResult = new();
        expectedResult.InitialiseStorageEmissions();

        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNin].Value = totalN;
        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANin].Value = TAN;
        expectedResult.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNin].Value = totalN - TAN;
        expectedResult.TypeIn = OrganicMatterType.SheepFYM;

        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.00616504;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.000487969;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 4.87969E-05;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.001463907;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.000136352;
        expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.002146274;
        expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value = 1.60971E-05;

        expectedResult.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value = 0.005591401;

        expectedResult.TypeOut = OrganicMatterType.SheepFYM;

        dataList.Add(new object[]
        {
            animalType, totalN, TAN, energyBalance, expectedResult
        });

        return dataList;

    }
    //Todo tmp_availableN and calc_NO3N is NaN in the UnmitigatedstorageManureManagementEmissionsSheep
    [Theory]
    [MemberData(nameof(UnmitigatedstorageManureManagementEmissionsSheep_TestGenerator))]
    public void UnmitigatedstorageManureManagementEmissionsSheep_AllOutputsCorrect(int animalType, double totalN, double TAN, SheepEnergyBalance energyBalance, StorageEmissions expectedResult)
    {
        var actualResult = Storage.UnmitigatedstorageManureManagementEmissionsSheep(animalType, totalN, TAN, energyBalance);
        actualResult.RoundEmissions(4);

        string actualResultJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualResultJson);
    }
}
