using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.OutdoorLivestock;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class OutdoorLivestockTests
{
    public static IEnumerable<object[]> OutdoorExcretaEmissions_Cattle_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.Beef;
        var animalType = BeefCattleType.Heifersforbreeding;
        var urine = 0.45;
        var dung = 1;
        var volatileSolids = 3038;
        var expectedResult = new OutdoorLivestockEmission();
        expectedResult.InitialiseGrazingEmissions();

        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.UrineNitrogen].Value = 0.45;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.DungNitrogen].Value = 1;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value = 1.45;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value = 0.45;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value = 1;

        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.027;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.0047605;
        expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.145;
        expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.0010875;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.0028563;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 3 * 0.0047605;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.000417988;
        expectedResult.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value = 3.663828;

        dataList.Add(new object[]
        {
            sector, animalType, urine, dung, volatileSolids, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(OutdoorExcretaEmissions_Cattle_TestGenerator))]
    public void OutdoorExcretaEmissions_Cattle_AllOutputsCorrect(Sector sector, int animalType, double urine, double dung, double volatilesolids, OutdoorLivestockEmission expectedResult)
    {
        OutdoorLivestockEmission actualEmissions = OutdoorLivestock.OutdoorExcretaEmissions_Cattle(sector, animalType, urine, dung, volatilesolids);

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

    //Todo sector doesn't need to be an input?
    public static IEnumerable<object[]> OutdoorExcretaEmissions_Sheep_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sheepEnergyBalance = new SheepEnergyBalance
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

        var sector = Sector.Sheep;
        var animalType = (int)SheepType.Ram;
        var fertiliserRate = 100;

        var grassFracLeachEquationCoefficients = new GrassEquationCoefficients();
        var grazingNitrogen = new GrassEquationCoefficients
        {
            C = 2.47440487,
            Coeff_X = -0.001428945,
            Coeff_X2 = 3.52E-06,
            Coeff_X3 = 1.33E-08,
            Coeff_X4 = -2.24E-11
        };

        grassFracLeachEquationCoefficients.C = 3.294907339;
        grassFracLeachEquationCoefficients.Coeff_X = 0.022446974;
        grassFracLeachEquationCoefficients.Coeff_X2 = -9.64E-05;
        grassFracLeachEquationCoefficients.Coeff_X3 = 4.95E-07;
        grassFracLeachEquationCoefficients.Coeff_X4 = -4.79E-10;

        OutdoorLivestockEmission expectedResult = new();
        expectedResult.InitialiseGrazingEmissions();

        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.272999023;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.018414702;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.011288066;
        expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.407190424;
        expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.003053928;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.00397667;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.055244106;

        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.DungNitrogen].Value = 3.750135989;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.UrineNitrogen].Value = 4.69113332;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value = 8.441269308;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value = 4.54998372;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value = 3.891285588;

        expectedResult.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value = 0.174518194;

        double grazedFracLeachScalar = 0.9604;
        double geneticYieldDilutionScalar = 0.9802;

        dataList.Add(
                new object[] { sheepEnergyBalance, sector, animalType, fertiliserRate, grassFracLeachEquationCoefficients, grazingNitrogen, grazedFracLeachScalar, geneticYieldDilutionScalar,expectedResult });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(OutdoorExcretaEmissions_Sheep_TestGenerator))]
    public void OutdoorExcretaEmissions_Sheep_AllOutputsCorrect(SheepEnergyBalance sheepEnergyBalance, Sector sector, int animalType, double fertiliserRate, GrassEquationCoefficients GrassFracLeachEquationCoefficients, GrassEquationCoefficients GrazingNitrogen,
        double grazedFracLeachScalar, double geneticYieldDilutionScalar,OutdoorLivestockEmission expectedResult)
    {
        var actualResult = OutdoorLivestock.OutdoorExcretaEmissions_Sheep(sheepEnergyBalance, sector, animalType, fertiliserRate, GrassFracLeachEquationCoefficients, GrazingNitrogen, grazedFracLeachScalar, geneticYieldDilutionScalar);
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

        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value, actualResult.EmissionsCore[CoreEmissions.DirectNH3N].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value, actualResult.EmissionsCore[CoreEmissions.DirectN2ON].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value, actualResult.EmissionsCore[CoreEmissions.DirectNON].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value, actualResult.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value, actualResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value, actualResult.EmissionsCore[CoreEmissions.N2ONleached].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value, actualResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value, 0.1));

        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.DungNitrogen].Value, actualResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.DungNitrogen].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.UrineNitrogen].Value, actualResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.UrineNitrogen].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value, actualResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value, actualResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value, actualResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value, 0.1));

        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value, actualResult.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value, 0.1));
    }

    public static IEnumerable<object[]> OutdoorExcretaEmissions_PigsPoultryMinorLivestock_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.Poultry;
        var animalType = PoultryType.GrowingPullets;
        var availableNitrogen = 0.07534170897;
        var totalNitrogen = 0.107631013;
        var volatileSolids = 1.095;

        OutdoorLivestockEmission expectedResult = new();
        expectedResult.InitialiseGrazingEmissions();

        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value = 0.107631013;
        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value = 0.03228930385;

        expectedResult.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value = 0.07534170897;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.026369598;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.000430524;
        expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.010763101;
        expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value = 8.07233E-05;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.000258314;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.001291572;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.000372791;
        expectedResult.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value = 0.00264114;

        dataList.Add(new object[] {
            sector, animalType, availableNitrogen, totalNitrogen, volatileSolids, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(OutdoorExcretaEmissions_PigsPoultryMinorLivestock_TestGenerator))]
    public void OutdoorExcretaEmissions_PigsPoultryMinorLivestock_AllOutputsCorrect(Sector sector, int animalType, double availableNitrogen, double totalNitrogen, double volatilesolids, OutdoorLivestockEmission expectedResult)
    {
        var actualResult = OutdoorLivestock.OutdoorExcretaEmissions_PigsPoultryMinorLivestock(sector, animalType, availableNitrogen, totalNitrogen, volatilesolids);
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
