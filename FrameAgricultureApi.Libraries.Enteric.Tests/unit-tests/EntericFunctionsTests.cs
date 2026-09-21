using FrameAgricultureApi.Libraries.Enteric.DTO;
using FrameAgricultureApi.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.Tests.unit_tests;

public class EntericFunctionsTests
{
    #region Diet

    public static IEnumerable<object[]> CalculateDietInformation_TestGenerator()
    {

        var dataList = new List<object[]>();

        DietInputs inputs = new DietInputs
        {
            ConcentrateIntake = 9.238168070684,
            DMIDaily = 15.00000000,
            ForageComponents = new List<ForageComponent>()
        };
        inputs.ForageComponents.Add(new ForageComponent());
        inputs.ForageComponents[0].ForageComponentPercent = 100;
        inputs.ForageComponents[0].ForageMEContent = 10.7200972;
        inputs.ForageComponents[0].ForageGEContent = 18.68905232;
        inputs.ForageComponents[0].ForageCPContent = 117.7386518;
        inputs.ForageComponents[0].ForageDryMatterContent = 314.100000000;
        inputs.ConcentrateComponents = new List<ConcentrateComponent>();
        inputs.ConcentrateComponents.Add(new ConcentrateComponent());
        inputs.ConcentrateComponents[0].ConcentrateComponentPercent = 100;
        inputs.ConcentrateComponents[0].ConcentrateMEContent = 12.733333;
        inputs.ConcentrateComponents[0].ConcentrateGEContent = 18.300000;
        inputs.ConcentrateComponents[0].ConcentrateCPContent = 217.666667;
        inputs.ConcentrateComponents[0].ConcentrateDryMatterContent = 860.00000000;

        DietOutputs outputs = new DietOutputs
        {
            MEGERatioWholeDiet = 0.6488679,
            UEMEMaintenance = 0.730103782,
            UEMEWeightGainNonLactating = 0.512117000,
            DryMatterDigestibility = 72.8197935,
            DryMatterContent = 650.3077299857,
            WeightedMEGERatioConcentrate = 0.695810565,
            WeightedMEConcentrate = 12.733333,
            WeightedGEConcentrate = 18.300000,
            WeightedCPConcentrate = 217.666667,
            WeightedDryMatterContentConcentrate = 860.00000000,
            WeightedDryMatterDigestibilityConcentrate = 73.33489,
            WeightedMEGERatioForage = 0.574,
            WeightedMEForage = 10.7200972,
            WeightedGEForage = 18.68905232,
            WeightedCPForage = 117.7386518,
            WeightedDryMatterContentForage = 314.100000000,
            WeightedDryMatterDigestibilityForage = 71.99392
        };

        dataList.Add(new object[] { inputs, outputs });

        inputs = new DietInputs
        {
            ConcentrateIntake = 0.602867078277,
            DMIDaily = 8.10000,
            ForageComponents = new List<ForageComponent>()
        };
        inputs.ForageComponents.Add(new ForageComponent());
        inputs.ForageComponents[0].ForageComponentPercent = 100;
        inputs.ForageComponents[0].ForageMEContent = 10.7200972;
        inputs.ForageComponents[0].ForageGEContent = 18.68905232;
        inputs.ForageComponents[0].ForageCPContent = 117.7386518;
        inputs.ForageComponents[0].ForageDryMatterContent = 314.100000000;
        inputs.ConcentrateComponents = new List<ConcentrateComponent>();
        inputs.ConcentrateComponents.Add(new ConcentrateComponent());
        inputs.ConcentrateComponents[0].ConcentrateComponentPercent = 100;
        inputs.ConcentrateComponents[0].ConcentrateMEContent = 12.500000;
        inputs.ConcentrateComponents[0].ConcentrateGEContent = 18.300000;
        inputs.ConcentrateComponents[0].ConcentrateCPContent = 200.000000;
        inputs.ConcentrateComponents[0].ConcentrateDryMatterContent = 860.00000000;

        outputs = new DietOutputs
        {
            MEGERatioWholeDiet = 0.5817497,
            UEMEMaintenance = 0.706612399,
            UEMEWeightGainNonLactating = 0.459764774,
            DryMatterDigestibility = 71.9643266,
            DryMatterContent = 354.73026,
            WeightedMEGERatioConcentrate = 0.683060109,
            WeightedMEConcentrate = 12.500000,
            WeightedGEConcentrate = 18.300000,
            WeightedCPConcentrate = 200.000000,
            WeightedDryMatterContentConcentrate = 860.00000000,
            WeightedDryMatterDigestibilityConcentrate = 71.59625,
            WeightedMEGERatioForage = 0.5736030,
            WeightedMEForage = 10.7200972,
            WeightedGEForage = 18.68905232,
            WeightedCPForage = 117.7386518,
            WeightedDryMatterContentForage = 314.100000000,
            WeightedDryMatterDigestibilityForage = 71.99392
        };

        dataList.Add(new object[] { inputs, outputs });

        inputs = new DietInputs
        {
            ConcentrateIntake = 0.602867078277,
            DMIDaily = 8.10000,
            ForageComponents = new List<ForageComponent>()
        };
        inputs.ForageComponents.Add(new ForageComponent());
        inputs.ForageComponents[0].ForageComponentPercent = 100;
        inputs.ForageComponents[0].ForageMEContent = 10.7200972;
        inputs.ForageComponents[0].ForageGEContent = 18.68905232;
        inputs.ForageComponents[0].ForageCPContent = 117.7386518;
        inputs.ForageComponents[0].ForageDryMatterContent = 314.100000000;
        inputs.ConcentrateComponents = new List<ConcentrateComponent>();
        inputs.ConcentrateComponents.Add(new ConcentrateComponent());
        inputs.ConcentrateComponents[0].ConcentrateComponentPercent = 100;
        inputs.ConcentrateComponents[0].ConcentrateMEContent = 12.500000;
        inputs.ConcentrateComponents[0].ConcentrateGEContent = 18.300000;
        inputs.ConcentrateComponents[0].ConcentrateCPContent = 200.000000;
        inputs.ConcentrateComponents[0].ConcentrateDryMatterContent = 860.00000000;

        outputs = new DietOutputs
        {
            MEGERatioWholeDiet = 0.5817497,
            UEMEMaintenance = 0.706612399,
            UEMEWeightGainNonLactating = 0.459764774,
            DryMatterDigestibility = 71.9643266,
            DryMatterContent = 354.73026,
            WeightedMEGERatioConcentrate = 0.683060109,
            WeightedMEConcentrate = 12.500000,
            WeightedGEConcentrate = 18.300000,
            WeightedCPConcentrate = 200.000000,
            WeightedDryMatterContentConcentrate = 860.00000000,
            WeightedDryMatterDigestibilityConcentrate = 71.59625,
            WeightedMEGERatioForage = 0.5736030,
            WeightedMEForage = 10.7200972,
            WeightedGEForage = 18.68905232,
            WeightedCPForage = 117.7386518,
            WeightedDryMatterContentForage = 314.100000000,
            WeightedDryMatterDigestibilityForage = 71.99392
        };

        dataList.Add(new object[] { inputs, outputs });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(CalculateDietInformation_TestGenerator))]
    public void CalculateDietInformation_AllOutputsCorrect(DietInputs inputs,
        DietOutputs expectedResult)
    {
        var actualResult = EntericFunctions.CalculateDietInformation(inputs);

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

    #endregion

    #region Energy Balance
    public static IEnumerable<object[]> EnergyBalanceDairy_TestGenerator()
    {
        var dataList = new List<object[]>();

        DairyEnergyBalanceInputs inputs = new DairyEnergyBalanceInputs
        {
            CattleType = DairyCattle.DC4_DairyCows,
            MeanLiveweightEnteric = 549.6617564,
            GrowthRateEnteric = 0.116177,
            GrowthRateExcretion = 0.116177,
            AverageAnnualMilkYield = 9162.856918,
            MilkFatContent = 4.14,
            MilkProteinContent = 3.29,
            CalvingInterval = 398,
            MEGERatioWholeDiet = 0.6488679,
            UEMEMaintenance = 0.730103782,
            UEMEWeightGainNonLactating = 0.512117000,
            DryMatterDigestibility = 72.8197935,
            DryMatterContent = 650.3077299857,
            ConcentrateIntake = 9.238168070684,
            MEContentOfConcentrate = 12.733333,
            MEContentOfForage = 10.7200972,
            GEContentOfForage = 18.68905232,
            GEContentOfConcentrate = 18.300000,
            CPContentOfForage = 117.7386518,
            CPContentOfConcentrate = 217.666667,
            BirthWeight = 38.97012197
        };

        EnergyBalanceOutputsDairy expectedResult = new EnergyBalanceOutputsDairy
        {
            DailyMERequirement = 200.5212211,
            DailyDryMatterIntake = 16.97023864,
            DailyGEIntake = 313.5635472,
            DailyNIntake = 467.3927704,
            DailyNExcretion = 10.08954437 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year),
            RationMod = 11.81605193,
            MEMaintenanceLactation = 201.9352327 / Math.Pow(549.6617564, 0.75),
            MEActivity = 0.978710563,
            MELiveWeightGain = 3.449576551,
            MEPregnancy = 4.15770136,
            MEUncertaintyScalar = 1,
            DMIAsPercentageOfLiveweight = 0.030873966,
            NitrogenRetentionInMilk = 129.4533399,
            NitrogenRetentionInPregnancy = 3.663184469,
            NitrogenRetentionInLiveweightGain = 2.565198065
        };

        dataList.Add(
            new object[]
            {
                    inputs,
                    expectedResult
            });

        inputs = new DairyEnergyBalanceInputs
        {
            CattleType = DairyCattle.DC3_DairyInCalfHeifers,
            MeanLiveweightEnteric = 442.7637286,
            GrowthRateEnteric = 0.262512,
            GrowthRateExcretion = 0.262512,
            AverageAnnualMilkYield = 9162.856918,
            MilkFatContent = 4.14,
            MilkProteinContent = 3.29,
            CalvingInterval = 398,
            MEGERatioWholeDiet = 0.5817497,
            UEMEMaintenance = 0.706612399,
            UEMEWeightGainNonLactating = 0.459764774,
            DryMatterDigestibility = 71.9643266,
            DryMatterContent = 354.73026,
            ConcentrateIntake = 0.602867078277,
            MEContentOfConcentrate = 12.500000,
            MEContentOfForage = 10.7200972,
            GEContentOfForage = 18.68905232,
            GEContentOfConcentrate = 18.300000,
            CPContentOfForage = 117.7386518,
            CPContentOfConcentrate = 200,
            BirthWeight = 38.97012197
        };

        expectedResult = new EnergyBalanceOutputsDairy
        {
            DailyMERequirement = 66.40089267,
            DailyDryMatterIntake = 6.09396043,
            DailyGEIntake = 113.65579850,
            DailyNIntake = 122.733975,
            DailyNExcretion = 3.398477072 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year),
            RationMod = 10.89618048,
            MEMaintenanceLactation = 42.23144717,
            MEActivity = 4.448864014,
            MELiveWeightGain = 13.15405315,
            MEPregnancy = 6.566528339,
            MEUncertaintyScalar = 1,
            DMIAsPercentageOfLiveweight = 0.013763459,
            NitrogenRetentionInMilk = 0.0,
            NitrogenRetentionInPregnancy = 5.206955067,
            NitrogenRetentionInLiveweightGain = 5.796266915
        };

        dataList.Add(
            new object[]
            {
                    inputs,
                    expectedResult
            });

        inputs = new DairyEnergyBalanceInputs
        {
            CattleType = DairyCattle.DC2_DairyReplacementsFemale,
            MeanLiveweightEnteric = 356.2674620,
            GrowthRateExcretion = 0.468366,
            GrowthRateEnteric = 0.4683366,
            AverageAnnualMilkYield = 9162.856918,
            MilkFatContent = 4.14,
            MilkProteinContent = 3.29,
            CalvingInterval = 398,
            MEGERatioWholeDiet = 0.5834520,
            UEMEMaintenance = 0.707208200,
            UEMEWeightGainNonLactating = 0.461092561,
            DryMatterDigestibility = 71.9581419,
            DryMatterContent = 363.22017,
            ConcentrateIntake = 0.602867078277,
            MEContentOfConcentrate = 12.500000,
            MEContentOfForage = 10.7200972,
            GEContentOfForage = 18.68905232,
            GEContentOfConcentrate = 18.300000,
            CPContentOfForage = 117.7386518,
            CPContentOfConcentrate = 200,
            BirthWeight = 38.97012197
        };

        expectedResult = new EnergyBalanceOutputsDairy
        {
            DailyMERequirement = 61.02783829,
            DailyDryMatterIntake = 5.592747212,
            DailyGEIntake = 104.2885985,
            DailyNIntake = 113.2920281,
            DailyNExcretion = 3.131410962 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year),
            RationMod = 10.91196079,
            MEMaintenanceLactation = 36.47746864,
            MEActivity = 3.576738758,
            MELiveWeightGain = 20.97363089,
            MEPregnancy = 0.0,
            MEUncertaintyScalar = 1,
            DMIAsPercentageOfLiveweight = 0.01569817,
            NitrogenRetentionInMilk = 0.0,
            NitrogenRetentionInPregnancy = 0.0,
            NitrogenRetentionInLiveweightGain = 10.34153069
        };

        dataList.Add(
            new object[]
            {
                    inputs,
                    expectedResult
            });

        inputs = new DairyEnergyBalanceInputs
        {
            CattleType = DairyCattle.DC1_DairyCalvesFemale,
            MeanLiveweightEnteric = 177.2791440,
            GrowthRateExcretion = 0.733052,
            GrowthRateEnteric = 0.765011,
            AverageAnnualMilkYield = 9162.856918,
            MilkFatContent = 4.14,
            MilkProteinContent = 3.29,
            CalvingInterval = 398,
            MEGERatioWholeDiet = 0.6287675,
            UEMEMaintenance = 0.723068612,
            UEMEWeightGainNonLactating = 0.496438622,
            DryMatterDigestibility = 73.3536122,
            DryMatterContent = 440.16251,
            ConcentrateIntake = 1.405467202907,
            MEContentOfConcentrate = 12.500000,
            MEContentOfForage = 11.1391789,
            GEContentOfForage = 18.70522385,
            GEContentOfConcentrate = 18.300000,
            CPContentOfForage = 173.7835782,
            CPContentOfConcentrate = 200.000000,
            BirthWeight = 38.97012197
        };

        expectedResult = new EnergyBalanceOutputsDairy
        {
            DailyMERequirement = 46.00198817,
            DailyDryMatterIntake = 3.958047457,
            DailyGEIntake = 73.46663487,
            DailyNIntake = 115.9503953,
            DailyNExcretion = 3.034506931 * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year),
            RationMod = 11.62239429,
            MEMaintenanceLactation = 22.35131435,
            MEActivity = 1.740750325,
            MELiveWeightGain = 21.90992349,
            MEPregnancy = 0.0,
            MEUncertaintyScalar = 1,
            DMIAsPercentageOfLiveweight = 0.022326639,
            NitrogenRetentionInMilk = 0.0,
            NitrogenRetentionInPregnancy = 0.0,
            NitrogenRetentionInLiveweightGain = 16.18578389
        };

        dataList.Add(
            new object[]
            {
                    inputs,
                    expectedResult
            });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(EnergyBalanceDairy_TestGenerator))]
    public void EnergyBalanceDairy_AllOutputsCorrect(DairyEnergyBalanceInputs inputs,
        EnergyBalanceOutputsDairy expectedResult)
    {
        var actualResult = EntericFunctions.EnergyAndNitrogenBalanceDairy(inputs);

        actualResult.RoundMembers(2);
        string actualEmissionJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundMembers(2);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> EnergyBalanceBeef_TestGenerator()
    {
        var dataList = new List<object[]>();

        BeefEnergyBalanceInputs input = new()
        {
            CattleType = BeefCattleType.Heifersforbreeding,
            CattleBreed = BeefCattleBreed.Upland,
            DietCrudeProteinContent = 131.00000,
            DietGrossEnergyContent = 18.06000,
            DietMetabolizableEnergyContent = 10.67000,
            MeanLiveweight = 530.2633071,
            MeanLiveweightForEntericMethane = 0.0,
            MeanLiveweightForNitrogenExcretion = 0.0,
            MeanGrowthRate = 0.0,
            BirthWeight = 33.000,
            TimePeriodScalar = 1.0,
            PercentCattleTypeGestating = 0,
            PercentCattleTypeLactating = 0,
            LactationLength = 182.5,
            MilkYield = 4.125831781,
            MilkFat = 36,
            MilkProtein = 32,
            LactationScalar = 1.05
        };

        EnergyBalanceOutputsBeef output = new()
        {
            DailyGEIntake = 97.70019317,
            DailyCPIntake = 708.6780346,
            DailyDryMatterIntake = 5.409755989,
            DailyNIntake = 113.3884855,
            DailyNExcretion = 113.3884855,
            DailyMEMaintenance = 57.72209641,
            DailyMELiveweightGain = 0,
            DailyMEPregnancy = 0,
            DailyMELactation = 0
        };

        dataList.Add(new object[]{
            input,output
        });

        input = new()
        {
            CattleType = BeefCattleType.Beeffemalesforslaughter,
            CattleBreed = BeefCattleBreed.Upland,
            DietCrudeProteinContent = 128.00000,
            DietGrossEnergyContent = 18.25000,
            DietMetabolizableEnergyContent = 11.17000,
            MeanLiveweight = 486.1873176,
            MeanLiveweightForEntericMethane = 0.0,
            MeanLiveweightForNitrogenExcretion = 0.0,
            MeanGrowthRate = 0.0,
            BirthWeight = 33.000,
            TimePeriodScalar = 1.0,
            PercentCattleTypeGestating = 0,
            PercentCattleTypeLactating = 0,
            LactationLength = 182.5,
            MilkYield = 0.0,
            MilkFat = 36,
            MilkProtein = 32,
            LactationScalar = 1.05
        };

        output = new()
        {
            DailyGEIntake = 87.61642201,
            DailyCPIntake = 614.5151791,
            DailyDryMatterIntake = 4.800899836,
            DailyNIntake = 98.32242865,
            DailyNExcretion = 98.32242865,
            DailyMEMaintenance = 53.62605117,
            DailyMELiveweightGain = 0,
            DailyMEPregnancy = 0,
            DailyMELactation = 0
        };

        dataList.Add(new object[]{
            input,output
        });

        input = new()
        {
            CattleType = BeefCattleType.Bullsforbreeding,
            CattleBreed = BeefCattleBreed.Dairy,
            DietCrudeProteinContent = 131.00000,
            DietGrossEnergyContent = 18.06000,
            DietMetabolizableEnergyContent = 10.67000,
            MeanLiveweight = 1016.615488,
            MeanLiveweightForEntericMethane = 0.0,
            MeanLiveweightForNitrogenExcretion = 0.0,
            MeanGrowthRate = 0.0,
            BirthWeight = 47.600,
            TimePeriodScalar = 1.0,
            PercentCattleTypeGestating = 0,
            PercentCattleTypeLactating = 0,
            LactationLength = 182.5,
            MilkYield = 0.0,
            MilkFat = 36,
            MilkProtein = 32,
            LactationScalar = 1.0
        };

        output = new()
        {
            DailyGEIntake = 157.553399,
            DailyCPIntake = 1142.829196,
            DailyDryMatterIntake = 8.723886989,
            DailyNIntake = 182.8526713,
            DailyNExcretion = 182.8526713,
            DailyMEMaintenance = 93.08387417,
            DailyMELiveweightGain = 0,
            DailyMEPregnancy = 0,
            DailyMELactation = 0
        };

        dataList.Add(new object[]{
            input,output
        });

        input = new()
        {
            CattleType = BeefCattleType.Cerealfedbull,
            CattleBreed = BeefCattleBreed.Dairy,
            DietCrudeProteinContent = 132.00000,
            DietGrossEnergyContent = 18.17000,
            DietMetabolizableEnergyContent = 11.77000,
            MeanLiveweight = 655.083389,
            MeanLiveweightForEntericMethane = 0.0,
            MeanLiveweightForNitrogenExcretion = 0.0,
            MeanGrowthRate = 0.0,
            BirthWeight = 47.600,
            TimePeriodScalar = 1.0,
            PercentCattleTypeGestating = 0,
            PercentCattleTypeLactating = 0,
            LactationLength = 182.5,
            MilkYield = 0.0,
            MilkFat = 36,
            MilkProtein = 32,
            LactationScalar = 1.0
        };

        output = new()
        {
            DailyGEIntake = 101.1466934,
            DailyCPIntake = 734.8026161,
            DailyDryMatterIntake = 5.566686486,
            DailyNIntake = 117.5684186,
            DailyNExcretion = 117.5684186,
            DailyMEMaintenance = 65.51989994,
            DailyMELiveweightGain = 0,
            DailyMEPregnancy = 0,
            DailyMELactation = 0
        };

        dataList.Add(new object[]{
            input,output
        });

        input = new()
        {
            CattleType = BeefCattleType.Steers,
            CattleBreed = BeefCattleBreed.Continental,
            DietCrudeProteinContent = 166.00000,
            DietGrossEnergyContent = 18.69000,
            DietMetabolizableEnergyContent = 11.78000,
            MeanLiveweight = 369.3832409,
            MeanLiveweightForEntericMethane = 0.0,
            MeanLiveweightForNitrogenExcretion = 0.0,
            MeanGrowthRate = 1.083087435,
            BirthWeight = 47.100,
            TimePeriodScalar = 1.0,
            PercentCattleTypeGestating = 0,
            PercentCattleTypeLactating = 0,
            LactationLength = 182.5,
            MilkYield = 0.0,
            MilkFat = 36,
            MilkProtein = 32,
            LactationScalar = 1.05
        };

        output = new()
        {
            DailyGEIntake = 121.9687418,
            DailyCPIntake = 1083.296477,
            DailyDryMatterIntake = 6.525882389,
            DailyNIntake = 173.3274362,
            DailyNExcretion = 149.2241649,
            DailyMEMaintenance = 43.54949096,
            DailyMELiveweightGain = 33.32540358,
            DailyMEPregnancy = 0,
            DailyMELactation = 0
        };

        dataList.Add(new object[]{
            input,output
        });

        input = new()
        {
            CattleType = BeefCattleType.Cows,
            CattleBreed = BeefCattleBreed.Continental,
            DietCrudeProteinContent = 171.00000,
            DietGrossEnergyContent = 18.76000,
            DietMetabolizableEnergyContent = 11.92000,
            MeanLiveweight = 525.7554076,
            MeanLiveweightForEntericMethane = 0.0,
            MeanLiveweightForNitrogenExcretion = 0.0,
            MeanGrowthRate = 0.191675026,
            BirthWeight = 47.100,
            TimePeriodScalar = 1.0,
            PercentCattleTypeGestating = 62,
            PercentCattleTypeLactating = 91,
            LactationLength = 182.5,
            MilkYield = 4.560129863,
            MilkFat = 36,
            MilkProtein = 32,
            LactationScalar = 1.05
        };

        output = new()
        {
            DailyGEIntake = 137.2795376,
            DailyCPIntake = 1251.322011,
            DailyDryMatterIntake = 7.317672579,
            DailyNIntake = 200.2115218,
            DailyNExcretion = 171.7235584,
            DailyMEMaintenance = 56.13003998,
            DailyMELiveweightGain = 6.825280845,
            DailyMEPregnancy = 5.083054898,
            DailyMELactation = 19.18828142
        };

        dataList.Add(new object[]{
            input,output
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(EnergyBalanceBeef_TestGenerator))]
    public void EnergyBalanceBeef_AllOutputsCorrect(BeefEnergyBalanceInputs inputs,
    EnergyBalanceOutputsBeef expectedResult)
    {
        var actualResult = EntericFunctions.EnergyAndNitrogenBalanceBeef(inputs);

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

    #endregion

    #region Live Weight

    public static IEnumerable<object[]> LiveweightsDairy_TestGenerator()
    {
        var dataList = new List<object[]>();

        DairyLiveWeightInputs inputs = new()
        {
            Country = Country.England,
            CattleSize = DairyBreedSize.Large,
            AgeFirstConception = 82.7857,
            AgeFirstCalving = 122.0000,
            AgeAtDeath = 296.2857143,
            MatureWeight = 620.5300
        };

        LiveWeightOutputsDairy outputs = new();
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC1_DairyCalvesFemale, 188.8334133);
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC2_DairyReplacementsFemale, 382.7743296);
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC3_DairyInCalfHeifers, 476.4964527);
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC4_DairyCows, 568.0330739);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC1_DairyCalvesFemale, 183.5310);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC2_DairyReplacementsFemale, 382.7743296);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC3_DairyInCalfHeifers, 476.4964527);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC4_DairyCows, 568.0330739);
        outputs.GrowthRates.GrowthRateFirstMonthToFirstYear = 0.828920;
        outputs.GrowthRates.GrowthRateConceptionToCalving = 0.284442;
        outputs.GrowthRates.GrowthRateCalvingToDeath = 0.086061;
        outputs.GrowthRates.GrowthRateBirthToFirstYear = 0.794291;
        outputs.GrowthRates.GrowthRateFirstYearToFirstConception = 0.507494;
        outputs.LiveweightAtBirth = 38.97012197;
        outputs.LiveweightAtOneMonth = 49.57493;
        outputs.LiveweightAtOneYear = 328.091901;
        outputs.LiveweightAtFirstConception = 437.456758;
        outputs.LiveweightAtFirstCalving = 515.536148;

        dataList.Add(new object[] { inputs, outputs });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(LiveweightsDairy_TestGenerator))]
    public void LiveweightsDairy_AllOutputsCorrect(DairyLiveWeightInputs inputs,
    LiveWeightOutputsDairy expectedResult)
    {
        var actualResult = EntericFunctions.LiveweightsDairy(inputs);

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

    public static IEnumerable<object[]> DairyLiveweightGain_TestGenerator()
    {
        var dataList = new List<object[]>();

        LiveWeightOutputsDairy outputs = new();
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC1_DairyCalvesFemale, 188.8334133);
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC2_DairyReplacementsFemale, 382.7743296);
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC3_DairyInCalfHeifers, 476.4964527);
        outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC4_DairyCows, 578.0330739);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC1_DairyCalvesFemale, 185.5310);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC2_DairyReplacementsFemale, 382.7743);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC3_DairyInCalfHeifers, 476.4965);
        outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC4_DairyCows, 568.0331);
        outputs.GrowthRates.GrowthRateFirstMonthToFirstYear = 0.828920;
        outputs.GrowthRates.GrowthRateConceptionToCalving = 0.284442;
        outputs.GrowthRates.GrowthRateCalvingToDeath = 0.086061;
        outputs.GrowthRates.GrowthRateBirthToFirstYear = 0.794291;
        outputs.GrowthRates.GrowthRateFirstYearToFirstConception = 0.507494;
        outputs.LiveweightAtBirth = 38.97012197;
        outputs.LiveweightAtOneMonth = 49.57493;
        outputs.LiveweightAtOneYear = 328.09101;
        outputs.LiveweightAtFirstConception = 437.456758;
        outputs.LiveweightAtFirstCalving = 515.536148;

        double result = 0;

        dataList.Add(new object[] { DairyCattle.DC4_DairyCows, outputs, 2.61767459 });
        dataList.Add(new object[] { DairyCattle.DC3_DairyInCalfHeifers, outputs, 8.65178427 });
        dataList.Add(new object[] { DairyCattle.DC2_DairyReplacementsFemale, outputs, 15.4362616 });
        dataList.Add(new object[] { DairyCattle.DC1_DairyCalvesFemale, outputs, 24.1596725 });

        return dataList;
    }
    [Theory]
    [MemberData(nameof(DairyLiveweightGain_TestGenerator))]
    public void LiveweightGainDairy_AllOutputsCorrect(DairyCattle CattleType, LiveWeightOutputsDairy LWdata, double expectedResult)
    {
        var actualResult = EntericFunctions.LiveWeightGainDairy(CattleType, LWdata, 12.0);

        Assert.Equal(expectedResult, actualResult, 2);
    }

    public static IEnumerable<object[]> LiveweightsBeef_TestGenerator()
    {
        var dataList = new List<object[]>();

        BeefLiveWeightInputs inputs = new()
        {
            Country = Country.England,
            CattleType = BeefCattleType.Heifersforbreeding,
            CattleBreed = BeefCattleBreed.Upland,
            LowerBoundaryAge = 1042.8600,
            UpperBoundaryAge = 1303.5700,
            MatureAge = 260.7100,
            CalfBirthWeight = 33.000,
            MatureWeight = 558.000
        };

        LiveWeightOutputsBeef outputs = new()
        {
            MeanLiveweight = 530.2633071,
            //outputs.MeanLiveweightForEntericMethane = ;
            //outputs.MeanLiveweightForNitrogenExcretion = ;
            MeanGrowthRate = 0,
            MatureLiveweight = 530.2633071
        };

        dataList.Add(new object[] { inputs, outputs });

        inputs = new()
        {
            Country = Country.England,
            CattleType = BeefCattleType.Beeffemalesforslaughter,
            CattleBreed = BeefCattleBreed.Upland,
            LowerBoundaryAge = 1042.8600,
            UpperBoundaryAge = 1303.5700,
            MatureAge = 260.7100,
            CalfBirthWeight = 33.000,
            MatureWeight = 493.000
        };

        outputs = new()
        {
            MeanLiveweight = 486.1873176,
            //outputs.MeanLiveweightForEntericMethane = ;
            //outputs.MeanLiveweightForNitrogenExcretion = ;
            MeanGrowthRate = 0,
            MatureLiveweight = 486.1873176
        };

        dataList.Add(new object[] { inputs, outputs });

        inputs = new()
        {
            Country = Country.England,
            CattleType = BeefCattleType.Bullsforbreeding,
            CattleBreed = BeefCattleBreed.Dairy,
            LowerBoundaryAge = 1042.8600,
            UpperBoundaryAge = 1303.5700,
            MatureAge = 260.7100,
            CalfBirthWeight = 47.600,
            MatureWeight = 1150.000
        };

        outputs = new()
        {
            MeanLiveweight = 1016.615488,
            //outputs.MeanLiveweightForEntericMethane = ;
            //outputs.MeanLiveweightForNitrogenExcretion = ;
            MeanGrowthRate = 0,
            MatureLiveweight = 1016.615488
        };

        dataList.Add(new object[] { inputs, outputs });

        inputs = new()
        {
            Country = Country.England,
            CattleType = BeefCattleType.Cerealfedbull,
            CattleBreed = BeefCattleBreed.Dairy,
            LowerBoundaryAge = 1042.8600,
            UpperBoundaryAge = 1303.5700,
            MatureAge = 260.7100,
            CalfBirthWeight = 47.600,
            MatureWeight = 703.000
        };

        outputs = new()
        {
            MeanLiveweight = 655.083389,
            //outputs.MeanLiveweightForEntericMethane = ;
            //outputs.MeanLiveweightForNitrogenExcretion = ;
            MeanGrowthRate = 0,
            MatureLiveweight = 655.083389
        };

        dataList.Add(new object[] { inputs, outputs });

        inputs = new()
        {
            Country = Country.England,
            CattleType = BeefCattleType.Steers,
            CattleBreed = BeefCattleBreed.Continental,
            LowerBoundaryAge = 26.0700,
            UpperBoundaryAge = 39.1100,
            MatureAge = 260.7100,
            CalfBirthWeight = 47.100,
            MatureWeight = 693.000
        };

        outputs = new()
        {
            MeanLiveweight = 369.3832409,
            //outputs.MeanLiveweightForEntericMethane = ;
            //outputs.MeanLiveweightForNitrogenExcretion = ;
            MeanGrowthRate = 1.083087435,
            MatureLiveweight = 667.4748215
        };

        dataList.Add(new object[] { inputs, outputs });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(LiveweightsBeef_TestGenerator))]
    public void LiveweightsBeef_AllOutputsCorrect(BeefLiveWeightInputs inputs,
    LiveWeightOutputsBeef expectedResult)
    {
        var actualResult = EntericFunctions.LiveweightsBeef(inputs);

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

    public static IEnumerable<object[]> BeefLiveweightGain_TestGenerator()
    {
        var dataList = new List<object[]>();

        LiveWeightOutputsBeef outputs = new LiveWeightOutputsBeef
        {
            MeanGrowthRate = 0.77831834
        };
        dataList.Add(new object[] { outputs, 23.6738495 });

        outputs = new LiveWeightOutputsBeef
        {
            MeanGrowthRate = 0.551789248
        };
        dataList.Add(new object[] { outputs, 16.78358961 });

        outputs = new LiveWeightOutputsBeef
        {
            MeanGrowthRate = 0.617129868
        };
        dataList.Add(new object[] { outputs, 18.77103348 });

        outputs = new LiveWeightOutputsBeef
        {
            MeanGrowthRate = 0.18764166
        };
        dataList.Add(new object[] { outputs, 5.707434009 });

        outputs = new LiveWeightOutputsBeef
        {
            MeanGrowthRate = 0.07468229
        };
        dataList.Add(new object[] { outputs, 2.271586326 });

        outputs = new LiveWeightOutputsBeef
        {
            MeanGrowthRate = 0.0
        };
        dataList.Add(new object[] { outputs, 0.0 });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(BeefLiveweightGain_TestGenerator))]
    public void LiveweightGainBeef_AllOutputsCorrect(LiveWeightOutputsBeef data, double expectedResult)
    {
        var actualResult = EntericFunctions.LiveweightGainBeef(data, 12.0);

        Assert.Equal(expectedResult, actualResult, 2);
    }

    #endregion

}
