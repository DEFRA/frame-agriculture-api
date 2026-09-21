using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta.Tests.unit_tests;

public class ExcretaEquationsTests
{
    #region Pigs poultry minor livestock

    [Theory]
    [InlineData(PoultryType.GrowingPullets, 1.095, 0.00264114)]
    [InlineData(PoultryType.LayingHens, 0.73, 0.00190749)]
    [InlineData(PoultryType.BreedingFlock, 3.1025, 0.00748323)]
    [InlineData(PoultryType.Broilers, 0.73, 0.00176076)]
    [InlineData(PoultryType.Turkeys, 2.555, 0.00616266)]
    [InlineData(PoultryType.Ducks, 0.73, 0.00176076)]
    [InlineData(PoultryType.Geese, 0.73, 0.00176076)]
    [InlineData(PoultryType.OtherPoultry, 0.365, 0.00088038)]
    public void CH4EmissionOutdoorPoultry(PoultryType animalType, double outdoorVolatileSolids, double expectedResult)
    {
        var actualResult = ExcretaEquations.CH4EmissionOutdoorPoultry(animalType, outdoorVolatileSolids);

        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, 21.1)]
    [InlineData(Sector.Pigs, 2, 11.68)]
    [InlineData(Sector.Pigs, 3, 18.9)]
    [InlineData(Sector.Pigs, 4, 13.1686)]
    [InlineData(Sector.Pigs, 5, 9.5981)]
    [InlineData(Sector.Pigs, 6, 4.0342)]
    [InlineData(Sector.MinorLivestock, 1, 8.400)]
    [InlineData(Sector.MinorLivestock, 2, 29.300)]
    [InlineData(Sector.MinorLivestock, 3, 50.000)]
    [InlineData(Sector.MinorLivestock, 4, 129.00)]
    [InlineData(Sector.MinorLivestock, 5, 50.00)]
    [InlineData(Sector.Poultry, 1, 0.3588)]
    [InlineData(Sector.Poultry, 2, 0.6648)]
    [InlineData(Sector.Poultry, 3, 1.1410)]
    [InlineData(Sector.Poultry, 4, 0.2838)]
    [InlineData(Sector.Poultry, 5, 1.7820)]
    [InlineData(Sector.Poultry, 6, 1.1997)]
    [InlineData(Sector.Poultry, 7, 1.1997)]
    [InlineData(Sector.Poultry, 8, 1.1997)]
    public void InitialNitrogen_AllOutputsCorrect(Sector sector, int animalType, double expectedResult)
    {
        var actualResult = ExcretaEquations.InitialNitrogen(sector, animalType);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, 167.9)]
    [InlineData(Sector.Pigs, 2, 167.9)]
    [InlineData(Sector.Pigs, 3, 167.9)]
    [InlineData(Sector.Pigs, 4, 109.5)]
    [InlineData(Sector.Pigs, 5, 109.5)]
    [InlineData(Sector.Pigs, 6, 109.5)]
    [InlineData(Sector.MinorLivestock, 1, 109.5)]
    [InlineData(Sector.MinorLivestock, 2, 109.5)]
    [InlineData(Sector.MinorLivestock, 3, 777.45)]
    [InlineData(Sector.MinorLivestock, 4, 777.45)]
    [InlineData(Sector.MinorLivestock, 5, 777.45)]
    public void InitialVolatileSolid_AllOutputsCorrect(Sector sector, int animalType, double expectedResult)
    {
        var actualResult = ExcretaEquations.InitialVolatileSolid(sector, animalType);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(0.251139, 70, 0.175797321)]
    [InlineData(0.5983, 70, 0.4188)]
    [InlineData(0.1711, 70, 0.1198)]
    [InlineData(0.2554, 70, 0.1788)]
    [InlineData(1.6038, 70, 1.1226)]
    [InlineData(1.0797, 70, 0.7558)]
    public void CalculateTAN_AllOutputsCorrect(double nitrogenExcreted, double percentageTAN, double expectedResult)
    {
        var actualResult = ExcretaEquations.CalculateTAN(nitrogenExcreted, percentageTAN);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, 1.5)]
    [InlineData(Sector.Pigs, 2, 1.5)]
    [InlineData(Sector.Pigs, 3, 1.5)]
    [InlineData(Sector.Pigs, 4, 1.5)]
    [InlineData(Sector.Pigs, 5, 1.5)]
    [InlineData(Sector.MinorLivestock, 1, 9)]
    [InlineData(Sector.MinorLivestock, 2, 20)]
    [InlineData(Sector.MinorLivestock, 3, 18)]
    [InlineData(Sector.MinorLivestock, 4, 18)]
    [InlineData(Sector.MinorLivestock, 5, 18)]
    public void CH4Emission_AllOutputsCorrect(Sector sector, int animalType, double expectedResult)
    {
        var actualResult = ExcretaEquations.CH4Emission(sector, animalType);

        Assert.Equal(expectedResult, actualResult, 3);
    }

    #endregion

    [Theory]
    [InlineData(6.827447259, 0.095689575, 4324.480082, 55.21276458, 360, 5, 6.923136835)]
    [InlineData(6.05265311, 0.414519762, 250.5529053, 3916.355959, 343.5840798, 21.41592018, 6.467172872)]
    [InlineData(2.284676171, 0.0, 1976.492795, 0.0, 169, 0.0, 2.284676171)]
    public static void SheepEntericMethane_AllOutputsCorrect(double fieldEntericMethane, double houseEntericMethane, double fieldDryMatterIntake, double houseDryMatterIntake, double fieldDays, double houseDays, double expectedOutput)
    {
        var actualResult = ExcretaEquations.SheepEntericMethane(fieldEntericMethane, houseEntericMethane, fieldDryMatterIntake, houseDryMatterIntake, fieldDays, houseDays);

        Assert.Equal(expectedOutput, actualResult, 4);
    }

    [Theory]
    [InlineData(182.5, 0, 0, 0, 0)]
    [InlineData(182.5, 26.17185894, 91, 26.45115361, 26.29893802)]
    public static void ProteinContentWeightGain_AllOutputsCorrect(double lactationLength, double netProteinContentOfLiveWeightGain,
        double scalingFactorOfCattleLactating, double netProteinContentOfLiveWeightGainInLactatingRuminants,
        double expectedResult)
    {
        var actualResult = ExcretaEquations.ProteinContentWeightGain(lactationLength, netProteinContentOfLiveWeightGain, scalingFactorOfCattleLactating, netProteinContentOfLiveWeightGainInLactatingRuminants);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.470680042, 63.58183711, 1.0, 79.01291254)]
    [InlineData(0, 551.6311204, 1.0, 0)]
    [InlineData(1.03878902, 153.5263027, 1.2, 180.7394761)]
    [InlineData(0.201810645, 524.4958693, 1.1, 30.27625855)]
    [InlineData(1.083087435, 369.3832409, 1.1, 150.6454462)]
    [InlineData(0, 600.3559916, 1.0, 0)]
    public static void NetProteinContentOfLiveWeightGain_AllOutputsCorrect(double meanGrowthRate, double meanLiveWeight,
        double correctionFactorForNRetention, double expectedResult)
    {
        var actualResult = ExcretaEquations.NetProteinContentOfLiveWeightGain(meanGrowthRate, meanLiveWeight, correctionFactorForNRetention);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(283, 9.825873352)]
    [InlineData(289, 10.83207007)]
    public static void ProteinRetentionInFullTerm_AllOutputsCorrect(double gestationLength, double expectedResult)
    {
        var actualResult = ExcretaEquations.ProteinRetentionInFullTerm(gestationLength);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(BeefCattleType.Heifersforbreeding, 4.629793011, 3.874607525)]
    [InlineData(BeefCattleType.Beeffemalesforslaughter, 4.800899836, 3.966206712)]
    [InlineData(BeefCattleType.Bullsforbreeding, 8.723886989, 6.066312501)]
    [InlineData(BeefCattleType.Cerealfedbull, 5.566686486, 4.376157832)]
    public static void CalculateEntericMethaneBeef_AllOutputsCorrect(BeefCattleType beefCattleType, double totalDMI, double expectedResult)
    {
        var actualResult = ExcretaEquations.CalculateEntericMethaneBeef(beefCattleType, totalDMI);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(4.560129863, 32, 6.38, 91, 20.81363348)]
    public static void NitrogenRetentionInMilkBeef_AllOutputsCorrect(double milkYield, double milkProteinContent, double milkProteinToNRatio, double scalingFactor, double expectedResult)
    {
        var actualResult = ExcretaEquations.NitrogenRetentionInMilkBeef(milkYield, milkProteinContent, milkProteinToNRatio, scalingFactor);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 16.97023864, 10.85827927)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 6.09396043, 4.651044229)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, 6.98951129, 5.129517239)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, 3.958047457, 3.509873184)]
    public static void CalculateCH4_AllOutputsCorrect(DairyCattle cattleType, double totalDMI, double expectedResult)
    {
        var actualResult = ExcretaEquations.CalculateEntericMethaneDairy(cattleType, totalDMI);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(280, 9.352982139)]
    public static void ProteinRetentionInDevelopingFoetus_AllOutputsCorrect(double averageGestationPeriodForDairyCow, double expectedResult)
    {
        var actualResult = ExcretaEquations.ProteinRetentionInDevelopingFoetus(averageGestationPeriodForDairyCow);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(9162.856918 / 365, 3.29, 6.38, 129.4533399)]
    [InlineData(6538.012614 / 365, 3.31, 6.38, 92.93091318)]
    [InlineData(5394.27749 / 365, 3.51, 6.38, 81.30679774)]
    public static void NitrogenRetentionInMilk_AllOutputsCorrect(double averageDailyMilkYield, double milkProteinContent, double milkProteinToNRatio, double expectedResult)
    {
        var actualResult = ExcretaEquations.NitrogenRetentionInMilkDairy(averageDailyMilkYield, milkProteinContent, milkProteinToNRatio);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.116177, 138, 6.25, 2.565198065)]
    [InlineData(0.262512, 138, 6.25, 5.796266915)]
    [InlineData(0.468366, 138, 6.25, 10.34153069)]
    [InlineData(0.733052, 138, 6.25, 16.18578389)]
    public static void NitrogenRetentionInLiveWeightGain_AllOutputsCorrect(double growthRate, double netProteinContentOfLiveWeightGain, double proteinToNRatio, double expectedResult)
    {
        var actualResult = ExcretaEquations.NitrogenRetentionInLiveWeightGainDairy(growthRate, netProteinContentOfLiveWeightGain, proteinToNRatio);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 38.97012197, 9.352982139, 6.25, 398, 280, 3.663184469)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 38.97012197, 9.352982139, 6.25, 398, 280, 5.206955067)]
    public static void AverageDailyNitrogenRetentionForPregnancy_AllOutputsCorrect(DairyCattle cattleType, double birthWeight, double proteinRetentionInDevelopingFoetus, double proteinToNRatio,
        double calvingInterval, double averageGestationPeriodDairyCow, double expectedResult)
    {
        var actualResult = ExcretaEquations.AverageDailyNitrogenRetentionForPregnancyDairy(cattleType, birthWeight, proteinRetentionInDevelopingFoetus, proteinToNRatio, calvingInterval, averageGestationPeriodDairyCow);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(16.97023864, 117.7386518, 9.238168070684, 6.25, 217.666667, 467.3927704)]
    [InlineData(6.09396043, 117.7386518, 0.602867078277, 6.25, 200.00, 122.733975)]
    [InlineData(6.98951129, 117.7386518, 0.602867078277, 6.25, 200.00, 139.6045272)]
    [InlineData(3.958047457, 173.7835782, 1.405467202907, 6.25, 200.00, 115.9503953)]
    public static void NitrogenIntake_AllOutputsCorrect(double totalDMI, double weightedAverageCPContentOfForage, double concentrateIntake,
        double proteinToNRatio, double concentrateCPContent, double expectedResult)
    {
        var actualResult = ExcretaEquations.NitrogenIntakeDairy(totalDMI, weightedAverageCPContentOfForage, concentrateIntake, proteinToNRatio,
            concentrateCPContent);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 467.3927704, 129.4533399, 2.565198065, 3.663184469, 10.08954437)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 122.733975, 0.0, 5.796266915, 5.206955067, 3.398477072)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, 139.6045272, 0.0, 10.34153069, 0.0, 3.931749476)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, 115.9503953, 0.0, 16.18578389, 0.0, 3.034506931)]
    public static void NitrogenExcretion_AllOutputsCorrect(DairyCattle cattleType, double nitrogenIntake, double nitrogenRetentionInMilk, double nitrogenRetentionInLiveWeightGain,
        double averageDailyNitrogenRetentionForPregnancy, double expectedResult)
    {
        var actualResult = ExcretaEquations.NitrogenExcretionDairy(cattleType, nitrogenIntake, nitrogenRetentionInMilk, nitrogenRetentionInLiveWeightGain, averageDailyNitrogenRetentionForPregnancy);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 467.3927704, 51.42782681)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 122.733975, 65.87720114)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, 139.6045272, 65.41942126)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, 115.9503953, 66.09372015)]
    public void PercentOfTotalNExcretionAsUrinaryN_AllOutputsCorrect(DairyCattle cattleType, double nitrogenIntake, double expectedResult)
    {
        var actualResult = ExcretaEquations.PercentOfTotalNExcretionAsUrinaryN(cattleType, nitrogenIntake);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(10.08954437, 51.42782681, 5.188833407)]
    [InlineData(3.398477072, 65.87720114, 2.238821576)]
    [InlineData(3.931749476, 65.41942126, 2.572127753)]
    [InlineData(3.034506931, 66.09372015, 2.005618519)]
    public static void NitrogenExcretionAsUrine_AllOutputsCorrect(double nitrogenExcretion, double percentOfTotalNAsExcretionUrinary, double expectedResult)
    {
        var actualResult = ExcretaEquations.NitrogenExcretionAsUrineDairy(nitrogenExcretion, percentOfTotalNAsExcretionUrinary);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(10.08954437, 5.188833407, 4.900710968)]
    [InlineData(3.398477072, 2.238821576, 1.159655495)]
    [InlineData(3.931749476, 2.572127753, 1.359621723)]
    [InlineData(3.034506931, 2.005618519, 1.028888412)]
    public static void NitrogenExcretionAsFaeces_AllOutputsCorrect(double nitrogenExcretion, double nitrogenExcretionAsUrine, double expectedResult)
    {
        var actualResult = ExcretaEquations.NitrogenExcretionAsFaeces(nitrogenExcretion, nitrogenExcretionAsUrine);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(5.188833407, 100, 5.188833407)]
    [InlineData(2.238821576, 25, 0.559705394)]
    [InlineData(2.572127753, 10, 0.257212775)]
    [InlineData(2.005618519, 65, 1.303652037)]
    [InlineData(0.393595566, 100, 0.393595566)]
    [InlineData(1.616520239, 40, 0.646608095)]
    [InlineData(0.808418935, 100, 0.808418935)]
    [InlineData(1.700712632, 17.6, 0.299325423)]
    [InlineData(2.115183755, 100, 2.115183755)]
    [InlineData(2.429225922, 100, 2.429225922)]
    public static void UrineNAtGrazingYardingHousing_AllOutputsCorrect(double nitrogenExcretionAsUrine, double percentageSpentAtLocation, double expectedResult)
    {
        var actualResult = ExcretaEquations.UrineNAtGrazingYardingHousing(nitrogenExcretionAsUrine, percentageSpentAtLocation);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(4.900710968, 75, 3.675533226)]
    [InlineData(1.159655495, 10, 0.11596555)]
    [InlineData(1.359621723, 90, 1.223659551)]
    [InlineData(1.028888412, 35, 0.360110944)]
    [InlineData(0.376612708, 0, 0)]
    [InlineData(1.443640551, 60, 0.866184331)]
    [InlineData(0.733524618, 100, 0.733524618)]
    [InlineData(1.509721441, 82.4, 1.244010468)]
    [InlineData(1.840528511, 100, 1.840528511)]
    [InlineData(2.099478523, 100, 2.099478523)]
    public static void FaecalNAtGrazingYardingHousing_AllOutputsCorrect(double nitrogenExcretionAsFaeces, double percentageSpentAtLocation, double expectedResult)
    {
        var actualResult = ExcretaEquations.FaecalNAtGrazingYardingHousing(nitrogenExcretionAsFaeces, percentageSpentAtLocation);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    // Dairy
    [InlineData(313.5635472, 200.5212211, 16.97023864, 0.08, 171.1996655)]
    [InlineData(113.65579850, 66.40089267, 6.09396043, 0.08, 70.9013297)]
    [InlineData(104.2885985, 61.02783829, 5.592747212, 0.08, 64.92051424)]
    [InlineData(73.46663487, 46.00198817, 3.958047457, 0.08, 41.40609861)]
    // Beef
    [InlineData(97.70019317, 57.72209641, 5.409755989, 0.08, 61.94465159)]
    [InlineData(87.61642201, 53.62605117, 4.800899836, 0.08, 52.11856862)]
    [InlineData(157.553399, 93.08387417, 8.723886989, 0.08, 99.89325602)]
    [InlineData(101.1466934, 65.51989994, 5.566686486, 0.08, 54.86826849)]
    [InlineData(121.9687418, 76.87489454, 6.525882389, 0.08, 67.51611345)]
    [InlineData(137.2795376, 87.22665714, 7.317672579, 0.08, 74.66132397)]
    public static void VSExcretion_AllOutputsCorrect(double gEIntake, double totalMERequirement, double totalDMI, double manureAshContent, double expectedResult)
    {
        var actualResult = ExcretaEquations.DailyVSExcretion(gEIntake, totalMERequirement, totalDMI, manureAshContent);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(171.1996655, 100, 171.1996655)]
    [InlineData(70.9013297, 25, 17.72533243)]
    [InlineData(81.58752902, 10, 8.158752902)]
    [InlineData(41.40609861, 65, 26.91396409)]
    [InlineData(202.8549662, 100, 202.8549662)]
    [InlineData(1577.042543, 40, 630.8170172)]
    [InlineData(752.7281586, 100, 752.7281586)]
    [InlineData(1567.04918, 82.4, 1291.248525)]
    [InlineData(2470.373494, 100, 2470.373494)]
    [InlineData(2712.715862, 100, 2712.715862)]
    public static void VSAtGrazingYardingHousing(double vSExcretion, double percentageSpentAtLocation, double expectedResult)
    {
        var actualResult = ExcretaEquations.VSAtGrazingYardingHousing(vSExcretion, percentageSpentAtLocation);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    #region Manure Mass and Volume

    [Theory]
    [InlineData(72.8198, 0.271802065)]
    [InlineData(71.9643, 0.280356734)]
    [InlineData(71.9581, 0.280418581)]
    [InlineData(73.3536, 0.266463878)]
    public static void FaecesDryMatterProduction_AllOutputsCorrect(double dryMatterDigestibilityWholeDiet, double expectedResult)
    {
        var actualResult = ExcretaEquations.FaecesDryMatterProduction(dryMatterDigestibilityWholeDiet);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.271802065, 16.97023864, 167, 27.62003535)]
    [InlineData(0.280356734, 6.09396043, 167, 10.23043619)]
    [InlineData(0.280418581, 6.98951129, 167, 11.73646011)]
    [InlineData(0.266463878, 3.958047457, 167, 6.315429187)]
    public static void MassOfFaeces_AllOutputsCorrect(double cattleFaecesDryMatterProduction, double totalDMI,
        double weightedAverageDryMatterContent, double expectedResult)
    {
        var actualResult = ExcretaEquations.MassOfFaeces(cattleFaecesDryMatterProduction, totalDMI, weightedAverageDryMatterContent);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.271802065, 16.97023864, 28.8284119)]
    [InlineData(0.280356734, 6.09396043, 10.67801777)]
    [InlineData(0.280418581, 6.98951129, 12.24993024)]
    [InlineData(0.266463878, 3.958047457, 6.591729214)]
    public static void VolumeOfFaeces_AllOutputsCorrect(double cattleFaecesDryMatterProduction, double totalDMI, double expectedResult)
    {
        var actualResult = ExcretaEquations.VolumeOfFaeces(cattleFaecesDryMatterProduction, totalDMI);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(650.3077299857, 1.095201458)]
    [InlineData(354.730, 1.247482968)]
    [InlineData(363.220, 1.243108968)]
    [InlineData(440.163, 1.203468274)]
    public static void UrineDryMatterProduction_AllOutputsCorrect(double dryMatterContentWholeDiet, double expectedResult)
    {
        var actualResult = ExcretaEquations.UrineDryMatterProduction(dryMatterContentWholeDiet);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(16.97023864, 1.095201458, 18.58583009)]
    [InlineData(6.09396043, 1.247482968, 7.602111844)]
    [InlineData(6.98951129, 1.243108968, 8.68872417)]
    [InlineData(3.958047457, 1.203468274, 4.763384539)]
    public static void MassOfUrine_AllOutputsCorrect(double totalDMI, double urineDryMatterProduction, double expectedResult)
    {
        var actualResult = ExcretaEquations.MassOfUrine(totalDMI, urineDryMatterProduction);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(18.58583009, 18.04449524)]
    [InlineData(7.602111844, 7.380691111)]
    [InlineData(8.68872417, 8.435654534)]
    [InlineData(4.763384539, 4.624645184)]
    public static void VolumeOfUrine_AllOutputsCorrect(double massOfUrineProducedDaily, double expectedResult)
    {
        var actualResult = ExcretaEquations.VolumeOfUrine(massOfUrineProducedDaily);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(27.62003535, 18.58583009, 75, 1054.071305)]
    [InlineData(10.23043619, 7.602111844, 10, 54.24066694)]
    [InlineData(11.73646011, 8.68872417, 10, 62.12660217)]
    [InlineData(6.315429187, 4.763384539, 65, 219.0373797)]
    [InlineData(27.62003535, 18.58583009, 10, 140.5428407)]
    [InlineData(10.23043619, 7.602111844, 5, 27.12033347)]
    [InlineData(11.73646011, 8.68872417, 0, 0)]
    [InlineData(6.315429187, 4.763384539, 0, 0)]
    [InlineData(27.62003535, 18.58583009, 5, 70.27142036)]
    [InlineData(10.23043619, 7.602111844, 75, 406.805002)]
    [InlineData(11.73646011, 8.68872417, 90, 559.1394195)]
    [InlineData(6.315429187, 4.763384539, 35, 117.9432045)]
    public static void ExcretaMassAtStage_AllOutputsCorrect(double massOfFaeces, double massOfUrine,
        double percentageOfTimeAtLocation, double expectedResult)
    {
        var actualResult = ExcretaEquations.ExcretaMassAtStage(massOfFaeces, massOfUrine, percentageOfTimeAtLocation);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(28.8284119, 18.04449524, 75, 1069.288194)]
    [InlineData(10.67801777, 7.380691111, 10, 54.92857286)]
    [InlineData(12.24993024, 8.435654534, 10, 62.91865367)]
    [InlineData(6.591729214, 4.624645184, 65, 221.7570688)]
    [InlineData(28.8284119, 18.04449524, 10, 142.5717592)]
    [InlineData(10.67801777, 7.380691111, 5, 27.46428643)]
    [InlineData(12.24993024, 8.435654534, 0, 0)]
    [InlineData(6.591729214, 4.624645184, 0, 0)]
    [InlineData(28.8284119, 18.04449524, 5, 71.2858796)]
    [InlineData(10.67801777, 7.380691111, 75, 411.9642964)]
    [InlineData(12.24993024, 8.435654534, 90, 566.2678831)]
    [InlineData(6.591729214, 4.624645184, 35, 119.4076524)]
    public static void ExcretaVolumeAtStage_AllOutputsCorrect(double dailyVolumeOfFaeces,
        double dailyVolumeOfUrine, double percentageOfTimeAtLocation, double expectedResult)
    {
        var actualResult = ExcretaEquations.ExcretaVolumeAtStage(dailyVolumeOfFaeces, dailyVolumeOfUrine, percentageOfTimeAtLocation);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    #endregion

}
