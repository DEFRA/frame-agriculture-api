using FrameAgricultureApi.Libraries.Excreta.DTO;
using FrameAgricultureApi.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta.Tests.unit_tests;

public class ExcretaFunctionsTests
{
    public static IEnumerable<object[]> DairyNitrogenExcretionAndEntericMethane_TestGenerator()
    {
        var dataList = new List<object[]>();

        DairyNitrogenAndEntericMethaneInputs input = new()
        {
            CattleType = DairyCattle.DC4_DairyCows
        };
        input.NitrogenInputs.DailyNExcretion = 0.3068903;
        input.NitrogenInputs.DailyNIntake = 467.39277;
        input.VolatileSolidsInputs.DailyGEIntake = 313.5635472;
        input.VolatileSolidsInputs.DailyDMIIntake = 16.97023864;
        input.VolatileSolidsInputs.DailyMERequirement = 200.5212211;
        input.TimeScalar = 1;

        NitrogenAndEntericMethaneOutputs expectedResult = new()
        {
            EntericMethane = 10.85827927
        };
        expectedResult.NitrogenOutputs.TotalNitrogenIntake = 467.3927704;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretion = 0.3068903;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsUrine = 0.1578270161;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = 0.1490632919;
        expectedResult.TotalGEIntake = 313.5635472;
        expectedResult.TotalDMIIntake = 16.97023864;
        expectedResult.TotalMERequirement = 200.5212211;
        expectedResult.TotalVolatileSolidsExcretion = 171.1996655;

        dataList.Add(new object[] { input, expectedResult });

        input = new()
        {
            CattleType = DairyCattle.DC3_DairyInCalfHeifers
        };
        input.NitrogenInputs.DailyNExcretion = (3.383036714 * HelperFunctions.Days_per_year) / 12000;
        input.NitrogenInputs.DailyNIntake = 122.2263468;
        input.VolatileSolidsInputs.DailyGEIntake = 113.15218894;
        input.VolatileSolidsInputs.DailyDMIIntake = 6.067013662;
        input.VolatileSolidsInputs.DailyMERequirement = 66.1120207;
        input.TimeScalar = 1;

        expectedResult = new()
        {
            EntericMethane = 4.636647167
        };
        expectedResult.NitrogenOutputs.TotalNitrogenIntake = 122.2263468;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretion = (3.383036714 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsUrine = (2.229173548 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = (1.153863167 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.TotalGEIntake = 113.15218894;
        expectedResult.TotalDMIIntake = 6.067013662;
        expectedResult.TotalMERequirement = 66.1120207;
        expectedResult.TotalVolatileSolidsExcretion = 70.57978403;

        dataList.Add(new object[] { input, expectedResult });

        input = new()
        {
            CattleType = DairyCattle.DC2_DairyReplacementsFemale
        };
        input.NitrogenInputs.DailyNExcretion = (3.131410962 * HelperFunctions.Days_per_year) / 12000;
        input.NitrogenInputs.DailyNIntake = 113.2920281;
        input.VolatileSolidsInputs.DailyGEIntake = 104.2885985;
        input.VolatileSolidsInputs.DailyDMIIntake = 5.592747212;
        input.VolatileSolidsInputs.DailyMERequirement = 61.02783829;
        input.TimeScalar = 1;

        expectedResult = new()
        {
            EntericMethane = 4.383257096
        };
        expectedResult.NitrogenOutputs.TotalNitrogenIntake = 113.2920281;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretion = (3.131410962 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsUrine = (2.072513738 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = (1.058897224 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.TotalGEIntake = 104.2885985;
        expectedResult.TotalDMIIntake = 5.592747212;
        expectedResult.TotalMERequirement = 61.02783829;
        expectedResult.TotalVolatileSolidsExcretion = 64.92051424;

        dataList.Add(new object[] { input, expectedResult });

        input = new()
        {
            CattleType = DairyCattle.DC1_DairyCalvesFemale
        };
        input.NitrogenInputs.DailyNExcretion = (3.034506931 * HelperFunctions.Days_per_year) / 12000;
        input.NitrogenInputs.DailyNIntake = 115.9503953;
        input.VolatileSolidsInputs.DailyGEIntake = 73.46663487;
        input.VolatileSolidsInputs.DailyDMIIntake = 3.958047457;
        input.VolatileSolidsInputs.DailyMERequirement = 46.00198817;
        input.TimeScalar = 1;

        expectedResult = new()
        {
            EntericMethane = 3.509873184
        };
        expectedResult.NitrogenOutputs.TotalNitrogenIntake = 115.9503953;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretion = (3.034506931 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsUrine = (2.005618519 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = (1.028888412 * HelperFunctions.Days_per_year) / 12000;
        expectedResult.TotalGEIntake = 73.46663487;
        expectedResult.TotalDMIIntake = 3.958047457;
        expectedResult.TotalMERequirement = 46.00198817;
        expectedResult.TotalVolatileSolidsExcretion = 41.40609861;

        dataList.Add(new object[] { input, expectedResult });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(DairyNitrogenExcretionAndEntericMethane_TestGenerator))]
    public void DairyNitrogenExcretionAndEntericMethane_AllOutputsCorrect(DairyNitrogenAndEntericMethaneInputs input, NitrogenAndEntericMethaneOutputs expectedResult)
    {
        var actualResult = ExcretaFunctions.DairyNitrogenExcretionAndEntericMethane(input);

        actualResult.RoundMembers(3);
        string actualEmissionJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundMembers(3);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> BeefNitrogenExcretionAndEntericMethane_TestGenerator()
    {

        var dataList = new List<object[]>();

        BeefNitrogenAndEntericMethaneInputs input = new()
        {
            CattleType = BeefCattleType.Heifersforbreeding
        };
        input.NitrogenInputs.DailyNExcretion = 113.3884855;
        input.NitrogenInputs.DailyNIntake = 113.3884855;
        input.VolatileSolidsInputs.DailyGEIntake = 97.70019317;
        input.VolatileSolidsInputs.DailyDMIIntake = 5.409755989;
        input.VolatileSolidsInputs.DailyMERequirement = 57.72209641;
        input.TimeScalar = 1;

        NitrogenAndEntericMethaneOutputs output = new()
        {
            EntericMethane = 4.292147706
        };
        output.NitrogenOutputs.TotalNitrogenIntake = 113.3884855;
        output.NitrogenOutputs.TotalNitrogenExcretion = 113.3884855;
        output.NitrogenOutputs.TotalNitrogenExcretionAsUrine = 1.828499677 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = 1.620400091 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.TotalGEIntake = 97.70019317;
        output.TotalDMIIntake = 5.409755989;
        output.TotalMERequirement = 57.72209641;
        output.TotalVolatileSolidsExcretion = 61.94465159;

        dataList.Add(new object[] { input, output });

        input = new()
        {
            CattleType = BeefCattleType.Beeffemalesforslaughter
        };
        input.NitrogenInputs.DailyNExcretion = 98.32242865;
        input.NitrogenInputs.DailyNIntake = 98.32242865;
        input.VolatileSolidsInputs.DailyGEIntake = 87.61642201;
        input.VolatileSolidsInputs.DailyDMIIntake = 4.800899836;
        input.VolatileSolidsInputs.DailyMERequirement = 53.62605117;
        input.TimeScalar = 1;

        output = new()
        {
            EntericMethane = 3.966206712
        };
        output.NitrogenOutputs.TotalNitrogenIntake = 98.32242865;
        output.NitrogenOutputs.TotalNitrogenExcretion = 98.32242865;
        output.NitrogenOutputs.TotalNitrogenExcretionAsUrine = 1.578666096 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = 1.411974442 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.TotalGEIntake = 87.61642201;
        output.TotalDMIIntake = 4.800899836;
        output.TotalMERequirement = 53.62605117;
        output.TotalVolatileSolidsExcretion = 52.11856862;

        dataList.Add(new object[] { input, output });

        input = new()
        {
            CattleType = BeefCattleType.Bullsforbreeding
        };
        input.NitrogenInputs.DailyNExcretion = 182.8526713;
        input.NitrogenInputs.DailyNIntake = 182.8526713;
        input.VolatileSolidsInputs.DailyGEIntake = 157.553399;
        input.VolatileSolidsInputs.DailyDMIIntake = 8.723886989;
        input.VolatileSolidsInputs.DailyMERequirement = 93.08387417;
        input.TimeScalar = 1;

        output = new()
        {
            EntericMethane = 6.066312501
        };
        output.NitrogenOutputs.TotalNitrogenIntake = 182.8526713;
        output.NitrogenOutputs.TotalNitrogenExcretion = 182.8526713;
        output.NitrogenOutputs.TotalNitrogenExcretionAsUrine = 2.986532056 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = 2.575236696 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.TotalGEIntake = 157.553399;
        output.TotalDMIIntake = 8.723886989;
        output.TotalMERequirement = 93.08387417;
        output.TotalVolatileSolidsExcretion = 99.89325602;

        dataList.Add(new object[] { input, output });

        input = new()
        {
            CattleType = BeefCattleType.Cerealfedbull
        };
        input.NitrogenInputs.DailyNExcretion = 117.5684186;
        input.NitrogenInputs.DailyNIntake = 117.5684186;
        input.VolatileSolidsInputs.DailyGEIntake = 101.1466934;
        input.VolatileSolidsInputs.DailyDMIIntake = 5.566686486;
        input.VolatileSolidsInputs.DailyMERequirement = 65.51989994;
        input.TimeScalar = 1;

        output = new()
        {
            EntericMethane = 4.376157832
        };
        output.NitrogenOutputs.TotalNitrogenIntake = 117.5684186;
        output.NitrogenOutputs.TotalNitrogenExcretion = 117.5684186;
        output.NitrogenOutputs.TotalNitrogenExcretionAsUrine = 1.897929889 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = 1.67810951 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.TotalGEIntake = 101.1466934;
        output.TotalDMIIntake = 5.566686486;
        output.TotalMERequirement = 65.51989994;
        output.TotalVolatileSolidsExcretion = 54.86826849;

        dataList.Add(new object[] { input, output });

        input = new()
        {
            CattleType = BeefCattleType.Steers
        };
        input.NitrogenInputs.DailyNExcretion = 149.2241649;
        input.NitrogenInputs.DailyNIntake = 173.3274362;
        input.VolatileSolidsInputs.DailyGEIntake = 121.9687418;
        input.VolatileSolidsInputs.DailyDMIIntake = 6.525882389;
        input.VolatileSolidsInputs.DailyMERequirement = 76.87489454;
        input.TimeScalar = 1;

        output = new()
        {
            EntericMethane = 4.889647372
        };
        output.NitrogenOutputs.TotalNitrogenIntake = 173.3274362;
        output.NitrogenOutputs.TotalNitrogenExcretion = 149.2241649;
        output.NitrogenOutputs.TotalNitrogenExcretionAsUrine = 2.434162815 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = 2.104738866 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.TotalGEIntake = 121.9687418;
        output.TotalDMIIntake = 6.525882389;
        output.TotalMERequirement = 76.87489454;
        output.TotalVolatileSolidsExcretion = 67.51611345;

        dataList.Add(new object[] { input, output });

        input = new()
        {
            CattleType = BeefCattleType.Cows
        };
        input.NitrogenInputs.DailyNExcretion = 171.7235584;
        input.NitrogenInputs.DailyNIntake = 200.2115218;
        input.VolatileSolidsInputs.DailyGEIntake = 137.2795376;
        input.VolatileSolidsInputs.DailyDMIIntake = 7.317672579;
        input.VolatileSolidsInputs.DailyMERequirement = 87.22665714;
        input.TimeScalar = 1;

        output = new()
        {
            EntericMethane = 6.189410226
        };
        output.NitrogenOutputs.TotalNitrogenIntake = 200.2115218;
        output.NitrogenOutputs.TotalNitrogenExcretion = 171.7235584;
        output.NitrogenOutputs.TotalNitrogenExcretionAsUrine = 2.810599712 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.NitrogenOutputs.TotalNitrogenExcretionAsFaeces = 2.412658521 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        output.TotalGEIntake = 137.2795376;
        output.TotalDMIIntake = 7.317672579;
        output.TotalMERequirement = 87.22665714;
        output.TotalVolatileSolidsExcretion = 74.66132397;

        dataList.Add(new object[] { input, output });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(BeefNitrogenExcretionAndEntericMethane_TestGenerator))]
    public void BeefNitrogenExcretionAndEntericMethane_AllOutputsCorrect(BeefNitrogenAndEntericMethaneInputs input,
        NitrogenAndEntericMethaneOutputs expectedResult)
    {
        var actualResult = ExcretaFunctions.BeefNitrogenExcretionAndEntericMethane(input, input.VolatileSolidsInputs.DailyDMIIntake);

        actualResult.RoundMembers(3);
        string actualEmissionJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundMembers(3);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    #region Pigs, Poultry and Minor Livestock
    public static IEnumerable<object[]> CalculateExcretaEmissionFreeRangePoultry_TestGenerator()
    {
        var dataList = new List<object[]>();

        InitialNitrogenFreeRangePoultryUserInput initialNitrogenFreeRangePoultryUserInput = new()
        {
            PoultryType = PoultryType.GrowingPullets,
            PercentageIndoors = 70
        };

        ExcretaEmissionsFreeRangePoultry expectedResult = new();
        expectedResult.OutdoorsEmissions.Nitrogen = 0.107631013;
        expectedResult.OutdoorsEmissions.EntericMethane = 0.00264114;
        expectedResult.OutdoorsEmissions.TAN = 0.07534170897;
        expectedResult.OutdoorsEmissions.VolatileSolids = 1.095;

        expectedResult.IndoorsEmissions.Nitrogen = 0.251139;
        expectedResult.IndoorsEmissions.EntericMethane = 0.0000;
        expectedResult.IndoorsEmissions.TAN = 0.175797321;
        expectedResult.IndoorsEmissions.VolatileSolids = 2.5550;

        dataList.Add(new object[] { initialNitrogenFreeRangePoultryUserInput, expectedResult });

        initialNitrogenFreeRangePoultryUserInput = new()
        {
            PoultryType = PoultryType.LayingHens,
            PercentageIndoors = 90
        };

        expectedResult = new();
        expectedResult.OutdoorsEmissions.Nitrogen = 0.083095644;
        expectedResult.OutdoorsEmissions.EntericMethane = 0.00190749;
        expectedResult.OutdoorsEmissions.TAN = 0.058166951;
        expectedResult.OutdoorsEmissions.VolatileSolids = 0.73;

        expectedResult.IndoorsEmissions.Nitrogen = 0.5983;
        expectedResult.IndoorsEmissions.EntericMethane = 0.0000;
        expectedResult.IndoorsEmissions.TAN = 0.4188;
        expectedResult.IndoorsEmissions.VolatileSolids = 6.5700;

        dataList.Add(new object[] { initialNitrogenFreeRangePoultryUserInput, expectedResult });

        initialNitrogenFreeRangePoultryUserInput = new()
        {
            PoultryType = PoultryType.BreedingFlock,
            PercentageIndoors = 15
        };

        expectedResult = new();
        expectedResult.OutdoorsEmissions.Nitrogen = 0.969833058;
        expectedResult.OutdoorsEmissions.EntericMethane = 0.00748323;
        expectedResult.OutdoorsEmissions.TAN = 0.67888314;
        expectedResult.OutdoorsEmissions.VolatileSolids = 3.1025;

        expectedResult.IndoorsEmissions.Nitrogen = 0.1711;
        expectedResult.IndoorsEmissions.EntericMethane = 0.0000;
        expectedResult.IndoorsEmissions.TAN = 0.1198;
        expectedResult.IndoorsEmissions.VolatileSolids = 0.5475;

        dataList.Add(new object[] { initialNitrogenFreeRangePoultryUserInput, expectedResult });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(CalculateExcretaEmissionFreeRangePoultry_TestGenerator))]
    public void CalculateExcretaEmissionFreeRangePoultry_AllOutputsCorrect(InitialNitrogenFreeRangePoultryUserInput input, ExcretaEmissionsFreeRangePoultry expectedResult)
    {
        var actualResult = ExcretaFunctions.CalculateExcretaEmissionFreeRangePoultry(input);

        actualResult.RoundMembers(3);
        string actualEmissionJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundMembers(3);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> CalculateExcretaEmissionPigsPoultryMinorLivestock_TestGenerator()
    {
        var dataList = new List<object[]>();

        ExcretaPigsPoultryMinorLivestockUserInput excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Pigs,
            AnimalType = 1
        };

        ExcretaEmissionsPigsPoultryMinorLivestock expectedResult = new()
        {
            Nitrogen = 21.1000,
            TAN = 14.7700,
            VolatileSolids = 167.9,
            EntericMethane = 1.5
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Pigs,
            AnimalType = 2
        };
        ;

        expectedResult = new()
        {
            Nitrogen = 11.68000,
            TAN = 8.1760,
            VolatileSolids = 167.9,
            EntericMethane = 1.5
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Pigs,
            AnimalType = 3
        };

        expectedResult = new()
        {
            Nitrogen = 18.9000,
            TAN = 13.2300,
            VolatileSolids = 167.9,
            EntericMethane = 1.5
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Pigs,
            AnimalType = 4
        };

        expectedResult = new()
        {
            Nitrogen = 13.1686,
            TAN = 9.2180,
            VolatileSolids = 109.5,
            EntericMethane = 1.5
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Pigs,
            AnimalType = 5
        };

        expectedResult = new()
        {
            Nitrogen = 9.5981,
            TAN = 6.7187,
            VolatileSolids = 109.5,
            EntericMethane = 1.5
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Pigs,
            AnimalType = 6
        };

        expectedResult = new()
        {
            Nitrogen = 4.0342,
            TAN = 2.8239,
            VolatileSolids = 109.5,
            EntericMethane = 1.5
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.MinorLivestock,
            AnimalType = 1
        };

        expectedResult = new()
        {
            Nitrogen = 8.4000,
            TAN = 5.0400,
            VolatileSolids = 109.5,
            EntericMethane = 9
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.MinorLivestock,
            AnimalType = 2
        };

        expectedResult = new()
        {
            Nitrogen = 29.3000,
            TAN = 17.5800,
            VolatileSolids = 109.5,
            EntericMethane = 20
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.MinorLivestock,
            AnimalType = 3
        };

        expectedResult = new()
        {
            Nitrogen = 50.0000,
            TAN = 30.0000,
            VolatileSolids = 777.45,
            EntericMethane = 18
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.MinorLivestock,
            AnimalType = 4
        };

        expectedResult = new()
        {
            Nitrogen = 129.0000,
            TAN = 77.4000,
            VolatileSolids = 777.45,
            EntericMethane = 18
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.MinorLivestock,
            AnimalType = 5
        };

        expectedResult = new()
        {
            Nitrogen = 50.0000,
            TAN = 30.0000,
            VolatileSolids = 777.45,
            EntericMethane = 18
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Poultry,
            AnimalType = 1
        };

        expectedResult = new()
        {
            Nitrogen = 0.358770,
            TAN = 0.251139030,
            VolatileSolids = 3.6500,
            EntericMethane = 0.0000
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Poultry,
            AnimalType = 2
        };

        expectedResult = new()
        {
            Nitrogen = 0.6648,
            TAN = 0.4653,
            VolatileSolids = 7.3000,
            EntericMethane = 0.0000
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Poultry,
            AnimalType = 3
        };

        expectedResult = new()
        {
            Nitrogen = 1.1410,
            TAN = 0.7987,
            VolatileSolids = 3.6500,
            EntericMethane = 0.0000
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Poultry,
            AnimalType = 4
        };

        expectedResult = new()
        {
            Nitrogen = 0.2838,
            TAN = 0.1987,
            VolatileSolids = 7.3000,
            EntericMethane = 0.0000
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Poultry,
            AnimalType = 5
        };

        expectedResult = new()
        {
            Nitrogen = 1.7820,
            TAN = 1.2474,
            VolatileSolids = 25.5500,
            EntericMethane = 0.0000
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        excretaPigsPoultryMinorLivestockUserInput = new()
        {
            Sector = Sector.Poultry,
            AnimalType = 6
        };

        expectedResult = new()
        {
            Nitrogen = 1.1997,
            TAN = 0.8398,
            VolatileSolids = 7.3000,
            EntericMethane = 0.0000
        };

        dataList.Add(new object[] { excretaPigsPoultryMinorLivestockUserInput, expectedResult });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(CalculateExcretaEmissionPigsPoultryMinorLivestock_TestGenerator))]
    public void CalculateExcretaEmissionPigsPoultryMinorLivestock_AllOutputsCorrect(ExcretaPigsPoultryMinorLivestockUserInput input, ExcretaEmissionsPigsPoultryMinorLivestock expectedResult)
    {
        var actualResult = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(input.Sector, input.AnimalType);

        actualResult.RoundMembers(3);
        string actualEmissionJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundMembers(3);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    [Theory]
    [InlineData(PoultryType.GrowingPullets, 70, 0.251139, true)]
    [InlineData(PoultryType.GrowingPullets, 70, 0.107631013, false)]
    [InlineData(PoultryType.LayingHens, 90, 0.5983, true)]
    [InlineData(PoultryType.LayingHens, 90, 0.0830956, false)]
    [InlineData(PoultryType.BreedingFlock, 15, 0.1711, true)]
    [InlineData(PoultryType.BreedingFlock, 15, 0.9698, false)]
    [InlineData(PoultryType.Broilers, 90, 0.2554, true)]
    [InlineData(PoultryType.Broilers, 90, 0.028382964, false)]
    [InlineData(PoultryType.Turkeys, 90, 1.6038, true)]
    public void InitialNitrogenFreeRangePoultry(PoultryType animalType, double percentageIndoors, double expectedResult, bool indoors = true)
    {
        var actualResult = ExcretaFunctions.InitialNitrogenFreeRangePoultry(animalType, percentageIndoors, indoors);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(1, 30, 2.5550, false)]
    [InlineData(1, 30, 1.095, true)]
    [InlineData(2, 10, 6.5700, false)]
    [InlineData(2, 10, 0.73, true)]
    [InlineData(3, 15, 3.1025, false)]
    [InlineData(3, 15, 0.5475, true)]
    [InlineData(4, 10, 6.5700, false)]
    [InlineData(4, 10, 0.73, true)]
    [InlineData(5, 10, 22.995, false)]
    [InlineData(5, 10, 2.555, true)]
    [InlineData(6, 10, 6.5700, false)]
    [InlineData(6, 10, 0.73, true)]
    [InlineData(7, 10, 6.5700, false)]
    [InlineData(7, 10, 0.73, true)]
    [InlineData(8, 10, 3.2850, false)]
    [InlineData(8, 10, 0.365, true)]
    public void InitialVolatileSolidPoultry_AllOutputsCorrect(int animalType, double percentageIndoors, double expectedResult, bool indoors = true)
    {
        var actualResult = ExcretaFunctions.InitialVolatileSolidPoultry(animalType, percentageIndoors, indoors);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    #endregion

}
