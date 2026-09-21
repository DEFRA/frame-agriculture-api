using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.Tests.unit_tests;

public class EntericEquationsTests
{

    #region diet

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 9162.85691793355 / 365, 0.368, 9.238168)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, null, 0.602867078276914, 0.602867)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, null, 0.602867078276914, 0.602867078)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, null, 1.40546720290713, 1.405467)]
    public void ConcentrateIntake_AllOutputsCorrect(DairyCattle cattleType, double dailyMilkYield, double concentrateUse, double expectedResult)
    {
        var actualResult = EntericEquations.ConcentrationIntake(cattleType, dailyMilkYield, concentrateUse);

        Assert.Equal(actualResult, expectedResult, 4);
    }

    [Theory]
    [InlineData(12.73, 18.30, 0.695810565)]
    [InlineData(12.50, 18.30, 0.683060)]
    public void MeGeConcentrateRatio_AllOutputsCorrect(double concentrateMe, double concentrateGe, double expectedResult)
    {
        var actualResult = EntericEquations.MeGeConcentrateRatio(concentrateMe, concentrateGe);

        Assert.Equal(actualResult, expectedResult, 3);
    }

    [Theory]
    [InlineData(10.720097, 71.99392)]
    [InlineData(11.139179, 74.43005)]
    public void DryMatterDigestibilityForForageComponent_AllOutputsCorrect(double meContentOfForage, double expectedResult)
    {
        var actualResult = EntericEquations.DryMatterDigestibilityForForageComponent(meContentOfForage);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(12.733333, 73.33489)]
    [InlineData(12.50, 71.59625)]
    public void DryMatterDigestibilityConcentrate_AllOutputsCorrect(double meContentOfForage, double expectedResult)
    {
        var actualResult = EntericEquations.DryMatterDigestibilityConcentrate(meContentOfForage);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(73.33489, 71.99392, 9.238168, 15.00, 72.8198)]
    [InlineData(71.59625, 71.99392, 0.602867, 8.10, 71.9643)]
    [InlineData(71.59625, 71.99392, 0.602867, 6.7, 71.9581)]
    [InlineData(71.59625, 74.43005, 1.405467, 3.7, 73.3536)]
    public void DryMatterDigestibilityForTheWholeDiet_AllOutputsCorrect(double digestibilityConcentrate, double averageDryMatterDigestibility, double concentrateIntake, double dmi, double expectedResult)
    {
        var actualResult = EntericEquations.DryMatterDigestibilityForTheWholeDiet(digestibilityConcentrate, averageDryMatterDigestibility, concentrateIntake, dmi);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.695810565, 0.57360, 9.238168, 15.00, 0.64887)]
    [InlineData(0.683060109, 0.57360, 0.60286708, 8.10, 0.58175)]
    [InlineData(0.683060109, 0.57360, 0.60286708, 6.70, 0.5834520)]
    public void RatioMeGeForTheWholeDiet_AllOutputsCorrect(double ratioMeGeForConcentrate, double ratioMeGeForForage,
        double concentrateIntake, double dmi, double expectedResult)
    {
        var actualResult = EntericEquations.RatioMeGeForTheWholeDiet(ratioMeGeForConcentrate, ratioMeGeForForage, concentrateIntake, dmi);

        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(314.10, 860.00, 9.23816807068369, 15.0, 650.3077299857)]
    [InlineData(314.10, 860.00, 0.60286708, 8.10, 354.73026)]
    [InlineData(314.10, 860.00, 0.60286708, 6.70, 363.22017)]
    [InlineData(183.00, 860.00, 1.405467, 3.70, 440.16251)]
    public void DryMatterContentForTheWholeDiet_AllOutputsCorrect(double dryMatterContentOfForage, double dryMatterContentOfConcentrate,
        double concentrateIntake, double dmi, double expectedResult)
    {
        var actualResult = EntericEquations.DryMatterContentForTheWholeDiet(dryMatterContentOfForage, dryMatterContentOfConcentrate,
            concentrateIntake, dmi);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    #endregion

    #region energy balance
    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 9162.85691793355 / 365, 549.71, 4.14, 0.705354289)]
    public void NEMilkCorrected_AllOutputsCorrect(DairyCattle cattleType, double dailyMilkYield, double meanLiveWeight, double milkFatContent, double expectedResult)
    {
        var actualResult = EntericEquations.NEMilkCorrected(cattleType, dailyMilkYield, meanLiveWeight, milkFatContent);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.6488679, 0.730103782)]
    [InlineData(0.5817497, 0.706612399)]
    [InlineData(0.5834520, 0.7072082)]
    [InlineData(0.6287675, 0.723068612)]
    [InlineData(0.590808416, 0.709782946)]
    [InlineData(0.61399672, 0.717898852)]
    [InlineData(0.665863067, 0.736052073)]
    [InlineData(0.647771051, 0.729719868)]
    [InlineData(0.593322386, 0.710662835)]
    public void UtilisationEfficiencyOfMeForMaintenance_AllOutputsCorrect(double ratioMeGeWholeDiet, double expectedResult)
    {
        var actualResult = EntericEquations.UtilisationEfficiencyOfMeForMaintenance(ratioMeGeWholeDiet);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.6488679, 0.512117)]
    [InlineData(0.5817497, 0.459764774)]
    [InlineData(0.5834520, 0.461092561)]
    [InlineData(0.6287675, 0.496438622)]
    [InlineData(0.590808416, 0.466830565)]
    [InlineData(0.61399672, 0.484917441)]
    [InlineData(0.665863067, 0.525373192)]
    [InlineData(0.647771051, 0.51126142)]
    public void UtilisationEfficiencyOfMeForWeightGainNonLactating_AllOutputsCorrect(double ratioMeGeWholeDiet, double expectedResult)
    {
        var actualResult = EntericEquations.UtilisationEfficiencyOfMeForWeightGainNonLactating(ratioMeGeWholeDiet);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.745877378, 0.681057082)]
    [InlineData(0.61399672, 0.634898852)]
    [InlineData(0.665863067, 0.653052073)]
    [InlineData(0.647771051, 0.646719868)]
    public void UtilisationEfficiencyOfMeForLactation_AllOutputsCorrect(double ratioMeGeWholeDiet, double expectedResult)
    {
        var actualResult = EntericEquations.UtilisationEfficiencyOfMeForLactation(ratioMeGeWholeDiet);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.681057082, 0.647004228)]
    [InlineData(0.634898852, 0.603153909)]
    [InlineData(0.653052073, 0.62039947)]
    [InlineData(0.646719868, 0.614383875)]
    public void UtilisationEfficiencyOfMeForWeightGainLactating_AllOutputsCorrect(double uEMELactation, double expectedResult)
    {
        var actualResult = EntericEquations.UtilisationEfficiencyOfMeForWeightGainLactating(uEMELactation);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(442.8822059, 1, 29.84661397)]
    [InlineData(356.4435872, 1, 25.80570886)]
    [InlineData(177.574561, 1, 16.17957299)]
    [InlineData(63.58183711, 1, 8.130459891)]
    [InlineData(551.6311204, 1, 34.57694523)]
    [InlineData(153.5263027, 1, 14.67656504)]
    [InlineData(524.4958693, 1, 33.42790366)]
    public void FastingMetabolismRequirement_AllOutputsCorrect(double meanLiveWeight, double genderCorrectionParameter, double expectedResult)
    {
        var actualResult = EntericEquations.FastingMetabolismRequirement(meanLiveWeight, genderCorrectionParameter);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 0.705354289, 0.0, 0.730103782, 1.778775)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 0.0, 29.84661397, 0.706612399, 42.2390182)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, 0.0, 25.80570886, 0.7072082, 36.48954982)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, 0.0, 16.17957299, 0.723068612, 22.37626239)]
    public void MEMaintenanceAndLactation_AllOutputsCorrect(DairyCattle cattleType, double nEMilkCorrected, double fastingMetabolismRequirement, double utilisationEfficiencyOfMEMaintenance, double expectedResult)
    {
        var actualResult = EntericEquations.MEMaintenanceAndLactation(cattleType, nEMilkCorrected, fastingMetabolismRequirement, utilisationEfficiencyOfMEMaintenance);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 549.71, 0.730103782, 0.0071 * 549.71, 0.978794663)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 442.8822059, 0.706612399, 0.0071 * 442.8822059, 4.450054467)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, 356.4435872, 0.70720820, 0.0071 * 356.4435872, 3.578506963)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, 177.5745610, 0.723068612, 0.0071 * 177.5745610, 1.743651102)]
    public void MEActivity_AllOutputsCorrect(DairyCattle cattleType, double meanLiveWeight, double utilisationEfficiencyOfMEMaintenance, double activityAllowance, double expectedResult)
    {
        var actualResult = EntericEquations.MEActivity(cattleType, meanLiveWeight, utilisationEfficiencyOfMEMaintenance, activityAllowance);

        Assert.Equal(actualResult, expectedResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 0.1161, 549.7089885, 0.65, 19.3, 1.3, 3.447277484)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 0.2623371, 442.8822059, 0.459764774, 19.3, 1.3, 13.14723982)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, 0.7645011, 356.4435872, 0.461092561, 19.3, 1.3, 35.93153546)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, 0.765011, 177.2791440, 0.496438622, 19.3, 1.3, 21.90992349)]
    public void MELiveWeightGain_AllOutputsCorrect(DairyCattle cattleType, double growthRate, double liveWeight, double utilisationEfficiencyMEForWeightGain,
        double netEnergyValueOfWeightGain, double correctionFactorEnergyContentLiveWeightGain, double expectedResult)
    {
        var actualResult = EntericEquations.MELiveWeightGain(cattleType, growthRate, liveWeight, utilisationEfficiencyMEForWeightGain, netEnergyValueOfWeightGain, correctionFactorEnergyContentLiveWeightGain);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 39.35771886, 251, 0.9, 0.133, 398, 274, 4.199053865)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 39.35771886, 251, 0.9, 0.133, 398, 280, 6.631839041)]
    public void MEPregnancy_AllOutputsCorrect(DairyCattle cattleType, double birthWeight, double totalEnergyRetentionGravidFoetus,
        double correctionFactorHerdPregnancy, double UEMEGrowthConcepta, double calvingInterval, double averageGestationPeriod, double expectedResult)
    {
        var actualResult = EntericEquations.MEPregnancy(cattleType, birthWeight, totalEnergyRetentionGravidFoetus, correctionFactorHerdPregnancy,
            UEMEGrowthConcepta, calvingInterval, averageGestationPeriod);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(DairyCattle.DC4_DairyCows, 549.7089885, 201.9393092, 0.978794663, 3.447277484, 4.199053865, 22924.2137469)]
    [InlineData(DairyCattle.DC3_DairyInCalfHeifers, 442.8822059, 42.2390182, 4.450054467, 13.14723982, 6.631839041, 66.46815153)]
    [InlineData(DairyCattle.DC2_DairyReplacementsFemale, 356.4435872, 36.48954982, 3.578506963, 35.93153546, 0, 75.99959224)]
    [InlineData(DairyCattle.DC1_DairyCalvesFemale, 177.5745610, 22.37626239, 1.743651102, 21.9134667, 0, 46.03338019)]
    public void TotalMERequirement_AllOutputsCorrect(DairyCattle cattleType, double meanLiveWeight, double mEMaintenanceLactation,
        double mEActivity, double mELiveWeightGain, double mEPregnancy, double expectedResult)
    {
        var actualResult = EntericEquations.TotalMERequirement(cattleType, meanLiveWeight, mEMaintenanceLactation,
            mEActivity, mELiveWeightGain, mEPregnancy);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(22924.2137469, 9.2381680707, 12.73333, 10.720097, 10.72880152)]
    [InlineData(66.46815153, 0.602867078277, 12.50000, 10.7200972, 10.89599938)]
    [InlineData(75.99959224, 0.602867078277, 12.50, 10.7200972, 10.87362277)]
    [InlineData(46.03338019, 1.405467202907, 12.50, 11.1391789, 11.62205048)]
    public void RationMod_AllOutputsCorrect(double totalMERequirement, double concentrateIntake, double concentrateMEContent, double weightedAverageForageME, double expectedResult)
    {
        var actualResult = EntericEquations.RationMod(totalMERequirement, concentrateIntake, concentrateMEContent, weightedAverageForageME);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(200.5644353, 11.90087656, 16.85291284)]
    [InlineData(66.46815153, 10.92189395, 6.085771554)]
    [InlineData(75.99959224, 10.89658573, 6.974624355)]
    [InlineData(46.03338019, 11.65852743, 3.948472951)]
    public void TotalDMI_AllOutputsCorrect(double totalMERequirement, double rationMod, double expectedResult)
    {
        var actualResult = EntericEquations.TotalDMI(totalMERequirement, rationMod);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(16.85291284, 549.7089885, 0.030657881)]
    [InlineData(6.085771554, 442.8822059, 0.013741287)]
    [InlineData(6.974624355, 356.4435872, 0.01956726)]
    [InlineData(3.948472951, 177.5745610, 0.022235578)]
    public void DMIAsProportionOfLiveWeight_AllOutputsCorrect(double totalDMIIntake, double meanLiveWeight, double expectedResult)
    {
        var actualResult = EntericEquations.DMIAsProportionOfLiveWeight(totalDMIIntake, meanLiveWeight);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(9.23816807068, 16.85291284, 18.68905, 18.3, 311.370839)]
    [InlineData(0.60286708, 6.085771554, 18.68905232, 18.300, 113.50275617)]
    [InlineData(0.60286708, 6.974624355, 18.68905, 18.3, 130.1145727)]
    [InlineData(1.405467, 3.948472951, 18.70522, 18.3, 73.28754159)]
    public void GEIntake_AllOutputsCorrect(double concentrateIntake, double totalDMIIntake, double weightedAverageGEContentForage, double concentrateGEContent, double expectedResult)
    {
        var actualResult = EntericEquations.GEIntake(concentrateIntake, totalDMIIntake, weightedAverageGEContentForage, concentrateGEContent);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(182.5, 1, 4.342980822)]
    [InlineData(182.5, 1.05, 4.560129863)]
    public void MilkYield_AllOutputsCorrect(double lactationLength, double scalingFactor, double expectedResult)
    {
        var actualResult = EntericEquations.MilkYield(lactationLength, scalingFactor);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(8.130459891 + 0.450159407, 0.764057082, 11.23033801)]
    [InlineData(4.12068447 + 34.57694523, 0.717898852, 53.90401391)]
    [InlineData(14.67656504 + 1.417047774, 0.736052073, 21.86477479)]
    [InlineData(33.42790366 + 6.66109754, 0.729719868, 54.93752186)]
    [InlineData(26.42966564 + 3.409407314, 0.710662835, 41.98766486)]
    [InlineData(36.59450546 + 4.25052042, 0.710138965, 57.51694797)]
    public void MEForMaintenance_AllOutputsCorrect(double totalMaintenanceEnergy, double UEMEMaintenance, double expectedResult)
    {
        var actualResult = EntericEquations.MEForMaintenance(totalMaintenanceEnergy, UEMEMaintenance);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.470680042, 63.58183711, 1, 3.123048087)]
    [InlineData(0.0, 551.6311204, 1, 0.0)]
    [InlineData(1.03878902, 153.5263027, 0.7, 7.715617751)]
    [InlineData(0.201810645, 524.4958693, 0.85, 3.365848866)]
    [InlineData(1.083087435, 369.3832409, 0.85, 16.58342691)]
    public void EnergyGainedThroughLiveWeightGain_AllOutputsCorrect(double meanGrowthRate, double lWm, double correctionFactorForEnergyContentLiveWeightGain, double expectedResult)
    {
        var actualResult = EntericEquations.EnergyGainedThroughLiveWeightGain(meanGrowthRate, lWm, correctionFactorForEnergyContentLiveWeightGain);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.587784355, 0.647004228, 182.5, 0.0, 0.587784355)]
    [InlineData(0.484917441, 0.603153909, 182.5, 0, 0.484917441)]
    [InlineData(0.525373192, 0.62039947, 182.5, 0, 0.525373192)]
    [InlineData(0.51126142, 0.614383875, 182.5, 0, 0.51126142)]
    [InlineData(0.468791461, 0.596279693, 182.5, 0, 0.468791461)]
    [InlineData(0.467623978, 0.595782016, 182.5, 91, 0.525935886)]
    public void ConvertNetToMEOfGrowth_AllOutputsCorrect(double uEMEWeightGainNonLactating, double uEMEWeightGainLactating, double lactationLength, double scalingFactor, double expectedResult)
    {
        var actualResult = EntericEquations.ConvertNetToMEOfGrowth(uEMEWeightGainNonLactating, uEMEWeightGainLactating, lactationLength, scalingFactor);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(3.123048087, 0.587784355, 5.313254868, 91.28, 5.313254868)]
    [InlineData(0, 0.484917441, 0, 5475.05, 0)]
    [InlineData(7.715617751, 0.525373192, 14.68597536, 91.21, 14.68597536)]
    [InlineData(3.365848866, 0.51126142, 6.583420409, 91.28, 6.583420409)]
    [InlineData(16.58342691, 0.468791461, 35.37484847, 91.28, 35.37484847)]
    [InlineData(0, 0.525935886, 0, 1824.97, 0)]
    public void METoSupportGrowth_AllOutputsCorrect(double energyGainedThroughLiveWeightGain, double convertNetToMEOfGrowth,
        double timeBand, double timeTotal, double expectedResult)
    {
        var actualResult = EntericEquations.METoSupportGrowth(energyGainedThroughLiveWeightGain, convertNetToMEOfGrowth);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(47.100, 338, 0.133, 93, 7.624582346)]
    public void MEOfFullTermPregnancyBeef_AllOutputsCorrect(double birthWeight, double energyRententionInFullTermGravidUterus, double uEMEForgrowthOfConcepta, double percentageOfHerdGestating, double expectedResult)
    {
        var actualResult = EntericEquations.MEOfFullTermPregnancyBeef(birthWeight, energyRententionInFullTermGravidUterus, uEMEForgrowthOfConcepta, percentageOfHerdGestating);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(4.560129863, 0.627138965, 36, 32, 91, 19.65485094)]
    public void EnergyOfLactation_AllOutputsCorrect(double dailyAverageMilkYield, double uEMEForLactation, double milkFatContent, double milkProteinContent, double percentageOfHerdLactating, double expectedResult)
    {
        var actualResult = EntericEquations.EnergyOfLactation(dailyAverageMilkYield, uEMEForLactation, milkFatContent, milkProteinContent, percentageOfHerdLactating);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(16.54359288, 17.64000, 0.937845401)]
    [InlineData(53.90401391, 11.23000, 4.800001239)]
    [InlineData(36.55075015, 13.81000, 2.646687194)]
    [InlineData(61.52094227, 11.77000, 5.226927975)]
    [InlineData(77.36251334, 10.84000, 7.136763223)]
    [InlineData(84.79638126, 10.86000, 7.808138237)]
    public void TotalDMIIntake_AllOutputsCorrect(double totalAverageDailyMERequirement, double mEConcentrateWholeDiet, double expectedResult)
    {
        var actualResult = EntericEquations.TotalDMIIntake(totalAverageDailyMERequirement, mEConcentrateWholeDiet);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    #endregion

    #region live weights

    [Theory]
    [InlineData(620.53, 38.97012197)]
    [InlineData(596.91, 37.49064005)]
    public void BirthWeight_AllOutputsCorrect(double matureWeight, double expectedResult)
    {
        var actualResult = EntericEquations.BirthWeight(matureWeight);

        Assert.Equal(actualResult, expectedResult, 4);
    }

    [Theory]
    [InlineData(DairyCattleStages.FirstConception, 19.0, 82.7857)]
    [InlineData(DairyCattleStages.FirstCalving, 28.0, 122.0)]
    [InlineData(DairyCattleStages.Death, 68.0, 296.2857)]
    public void TimeBoundariesBetweenStages_AllOutputsCorrect(DairyCattleStages stage, double? ageAtStage, double expectedResult)
    {
        var actualResult = EntericEquations.TimeBoundariesBetweenStages(stage, ageAtStage);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(39.35771886, 620.530000, 4, 57.96, 1.5215, 49.13838)]
    [InlineData(39.35771886, 620.530000, 52, 57.96, 1.5215, 306.010740)]
    [InlineData(39.35771886, 620.530000, 82.7857, 57.96, 1.5215, 406.876435)]
    [InlineData(39.35771886, 620.530000, 122.0000, 57.96, 1.5215, 478.887977)]
    [InlineData(33.0, 558.0, 91.25, 55.06, 1.8562, 410.2847926)]
    [InlineData(33.0, 558.0, 104.2900, 55.06, 1.8562, 435.1302722)]
    [InlineData(47.100, 624.000, 91.2500, 46.37, 1.8258, 494.1183352)]
    [InlineData(47.100, 624.000, 104.2900, 46.37, 1.8258, 517.014068)]
    public void LiveWeightCalculationsAtBoundary_AllOutputsCorrect(double birthWeight, double matureLiveWeight, double ageBoundary, double liveWeightCurveParameterK, double liveWeightCurveParameterC, double expectedResult)
    {
        var actualResult = EntericEquations.LiveWeightCalculationsAtBoundary(birthWeight, matureLiveWeight, ageBoundary, liveWeightCurveParameterK, liveWeightCurveParameterC);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(39.35771886, 306.010740, 0.0, 52, 0.732563)]
    [InlineData(49.13838, 306.010740, 4, 52, 0.764501)]
    [InlineData(306.010740, 406.876435, 52, 82.7857, 0.468054)]
    [InlineData(406.876435, 478.887977, 82.7857, 122.0000, 0.262337)]
    [InlineData(478.887977, 620.530000, 122.0000, 296.2857, 0.116100)]
    public void MeanGrowthRate_AllOutputsCorrect(double firstWeight, double secondWeight, double firstAgeBoundary, double secondAgeBoundary, double expectedResult)
    {
        var actualResult = EntericEquations.MeanGrowthRate(firstWeight, secondWeight, firstAgeBoundary, secondAgeBoundary);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(260.71, 494.1183352, 517.014068, 91.2500, 104.2900, 0.250829676)]
    [InlineData(260.71, 551.6311204, 578.9897957, 260.7100, 1042.8600, 0.0)]
    public void MeanGrowthRateBeef_AllOutputsCorrect(double matureAge, double firstWeight, double secondWeight, double firstAgeBoundary, double secondAgeBoundary, double expectedResult)
    {
        var actualResult = EntericEquations.MeanGrowthRateBeef(matureAge, firstWeight, secondWeight, firstAgeBoundary, secondAgeBoundary);

        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.937845401, 23.65, 22.18004374)]
    [InlineData(4.800001239, 18.29, 87.79202266)]
    [InlineData(2.646687194, 20.74, 54.8922924)]
    [InlineData(5.226927975, 18.17, 94.97328131)]
    [InlineData(7.136763223, 18.27, 130.3886641)]
    [InlineData(7.808138237, 18.35, 143.2793367)]
    public void GEIntakeBeef_AllOutputsCorrect(double totalDMI, double gEConcentrationOfWholeDiet, double expectedResult)
    {
        var actualResult = EntericEquations.GEIntake(totalDMI, gEConcentrationOfWholeDiet);
        Assert.Equal(actualResult, expectedResult, 4);
    }

    [Theory]
    [InlineData(0.937845401, 253, 237.2748866)]
    [InlineData(4.800001239, 131, 628.8001623)]
    [InlineData(2.646687194, 188, 497.5771925)]
    [InlineData(5.226927975, 132, 689.9544927)]
    [InlineData(7.136763223, 135, 963.4630351)]
    [InlineData(7.808138237, 140, 1093.139353)]
    public void CPIntake_AllOutputsCorrect(double totalDMI, double CPConcentrationOfWholeDiet, double expectedResult)
    {
        var actualResult = EntericEquations.CPIntake(totalDMI, CPConcentrationOfWholeDiet);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(0.086061, 12.0, 2.61767459)] //Dairy DC4
    [InlineData(0.284442, 12.0, 8.65178427)] //Dairy DC3
    [InlineData(0.507494, 12.0, 15.4362616)] //Dairy DC2
    [InlineData(0.794291, 12.0, 24.1596725)] //Dairy DC1
    [InlineData(0.77831834, 12.0, 23.6738495)] //Beef Heifers for breeding (Upland 3-6 months)
    [InlineData(0.551789248, 12.0, 16.78358961)] //Beef Females for slaughter (Upland 12-15 months)
    [InlineData(0.617129868, 12.0, 18.77103348)] //Beef bulls for breeding (Dairy 21-24 months)
    [InlineData(0.187641666, 12.0, 5.707434009)] // Beef cereal fed bull (dairy 24-27 months)
    [InlineData(0.07468229, 12.0, 2.271586326)] //beef steers (Continental 33-36 months)
    [InlineData(0.0, 12.0, 0.0)] // beef cow (Contintental 60-240 months)
    public void LiveWeightGain_AllOutputsCorrect(double growthrate, double timePeriodsperyear, double expectedResult)
    {
        var actualResults = EntericEquations.LiveweightGain(growthrate, timePeriodsperyear);
        Assert.Equal(expectedResult, actualResults, 4);
    }

    [Theory]
    [InlineData(38.97012917, 620.53000, 296.2857143, 57.96, 1.5215, 669.1132332)]
    [InlineData(37.49064005, 596.910000, 296.2857143, 57.96, 1.5215, 643.6436250)]
    [InlineData(26.6179261, 431.310000, 283.2142857, 57.96, 1.5215, 467.5202326)]
    public void AsymptoticLiveweightAllOutputsCorrect(double birthWeight, double knownWeight, double knownAge, double K, double C, double expectedResult)
    {
        var actualResults = EntericEquations.AsymptoticLiveweight(birthWeight, knownWeight, knownAge, K, C);
        Assert.Equal(expectedResult, actualResults, 4);
    }

    #endregion

}
