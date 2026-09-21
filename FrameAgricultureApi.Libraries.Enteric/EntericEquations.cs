using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric;

public static class EntericEquations
{
    #region diet

    /// <summary>
    /// Calculates average daily milk yield DM_1, BM_8
    /// </summary>
    /// <param name="annualMilkYield">The average annual milk yield if known BM_8</param>
    /// <returns></returns>
    public static double AverageDailyMilkYield(double? annualMilkYield)
    {
        if(annualMilkYield.HasValue)
        {
            return annualMilkYield.Value / HelperFunctions.Days_per_year;
        }

        var defaultYield = 0;

        return defaultYield;
    }
    /// <summary>
    /// Calculates the concentrate intake for dairy cows in kg of dry matter a day DM_2
    /// </summary>
    /// <param name="dailyMilkYield">The average annual milk yield if known</param>
    /// <param name="concentrateUse">The concentration intake in kg of dry matter per kg</param>
    /// <returns></returns>
    public static double ConcentrationIntake(DairyCattle cattleType, double? dailyMilkYield, double concentrateUse)
    {

        switch(cattleType)
        {
            case DairyCattle.DC4_DairyCows:
                if(dailyMilkYield == null)
                {
                    throw new CustomAppException("This cattle type requires a daily milk yield value that is not null.");
                }

                return ConcentrationIntakeDairyCows((double)dailyMilkYield, concentrateUse);
            case DairyCattle.DC3_DairyInCalfHeifers:
            case DairyCattle.DC2_DairyReplacementsFemale:
            case DairyCattle.DC1_DairyCalvesFemale:
                return (double)concentrateUse;
            default:
                throw new ArgumentException("The argument you have provided for cattle type is invalid.");
        }
    }
    /// <summary>
    /// Calculates the concentrate intake for dairy cows in kg of dry matter a day DM_2
    /// </summary>
    /// <param name="dailyMilkYield">The average annual milk yield if known</param>
    /// <param name="concentrate">The concentration intake in kg of dry matter per kg</param>
    /// <returns></returns>
    public static double ConcentrationIntakeDairyCows(double dailyMilkYield, double? concentrate)
    {
        if(concentrate.HasValue)
        {
            return (double)(dailyMilkYield * concentrate);
        }

        var defaultConcentrate = 0;
        return dailyMilkYield * defaultConcentrate;
    }
    /// <summary>
    /// Calculates The ratio between metabolise energy ME and gross energy GE for concentrates DM_3
    /// </summary>
    /// <param name="ConcentrateMe">Concentrate ME content in Mj per kg of dry matter if known</param>
    /// <param name="ConcentrateGe">Concentrate ME content in Mj per kg of dry matter if known</param>
    /// <returns></returns>
    public static double MeGeConcentrateRatio(double concentrateMe, double concentrateGe)
    {
        return concentrateMe / concentrateGe;
    }

    public static double MeGeForageRatio(double forageMe, double forageGe)
    {
        return MeGeConcentrateRatio(forageMe, forageGe);
    }

    /// <summary>
    /// Calculates the dry matter digestibility for the forage component DM_12
    /// </summary>
    /// <param name="ConcentrateMe">Me content of forage diet component in Mj per kg of dry matter</param>
    /// <returns></returns>
    public static double DryMatterDigestibilityForForageComponent(double meContentOfForage)
    {
        return (5.813 * meContentOfForage) + 9.678;
    }
    /// <summary>
    /// Calculates the digestibility concentrate DM_11
    /// </summary>
    /// <param name="meContentOfForage">Me content of forage diet component in Mj per kg of dry matter</param>
    /// <returns></returns>
    public static double DryMatterDigestibilityConcentrate(double meContentOfForage)
    {
        return (7.4513 * meContentOfForage) - 21.545;
    }
    /// <summary>
    /// Calculates the weighted average of a constituent of the forage part of the dies in Mj per kg of dry matter
    /// DM_4, DM_5, DM_6, DM_7
    /// </summary>
    /// <param name="dietPercentageFromForage">The percentage of the diet which is from forage if known</param>
    /// <param name="contentInForage">The constituent content of forage if known</param>
    /// <returns></returns>
    public static double WeightedAverageContentForage(double dietPercentageFromForage, double contentInForage)
    {
        return (dietPercentageFromForage * contentInForage * HelperFunctions.Percent_to_Proportion);
    }

    public static double WeightedAverageDryMatterDigestibility(double dryMatterDigestibilityForage, double? forageDietPercent)
    {
        return (double)(dryMatterDigestibilityForage * forageDietPercent * HelperFunctions.Percent_to_Proportion);
    }

    /// <summary>
    /// Calculates the dry matter digestibility for the entire diet DM_15
    /// </summary>
    /// <param name="digestibilityConcentrate">The digestibility concentrate DM_11 </param>
    /// <param name="averageDryMatterDigestibility">Weighted average dry matter digestibility (DMD) in the forage part of the diet in Mj per kg of dry matter DM_13</param>
    /// <param name="concentrateIntake">Concentrate intake in kg of dry matter a day DM_2</param>
    /// <param name="dmi">DMI in kg a day</param>
    /// <returns></returns>
    public static double DryMatterDigestibilityForTheWholeDiet(double digestibilityConcentrate, double averageDryMatterDigestibility, double concentrateIntake, double dmi)
    {
        return ((digestibilityConcentrate * concentrateIntake) / dmi) + ((averageDryMatterDigestibility * (dmi - concentrateIntake)) / dmi);
    }
    /// <summary>
    /// Calculates the dry matter digestibility for the entire diet DM_8
    /// </summary>
    /// <param name="ratioMeGeForConcentrate">The ME/GE ratio for concentrates DM_3 </param>
    /// <param name="ratioMeGeForForage">The ME/GE ratio for concentrates DM_7 </param>
    /// <param name="concentrateIntake">Concentrate intake in kg of dry matter a day DM_2</param>
    /// <param name="dmi">DMI in kg a day</param>
    /// <returns></returns>
    public static double RatioMeGeForTheWholeDiet(double ratioMeGeForConcentrate, double ratioMeGeForForage, double concentrateIntake, double dmi)
    {
        return ((ratioMeGeForConcentrate * concentrateIntake) / dmi) + ((ratioMeGeForForage * (dmi - concentrateIntake)) / dmi);
    }
    /// <summary>
    /// Calculates the dry matter content for the entire diet DM_16
    /// </summary>
    /// <param name="dryMatterContentOfForage">The digestibility concentrate DM_11 </param>
    /// <param name="FnDMC">Todo: this isn't described in the notes </param>
    /// <param name="concentrateIntake">Concentrate intake in kg of dry matter a day DM_2</param>
    /// <param name="dmi">DMI in kg a day</param>
    /// <returns></returns>
    public static double DryMatterContentForTheWholeDiet(double dryMatterContentOfForage, double dryMatterContentOfConcentrate, double concentrateIntake, double dmi)
    {
        return ((dryMatterContentOfConcentrate * (concentrateIntake / dmi)) + (
                    dryMatterContentOfForage * ((dmi - concentrateIntake) / dmi)));
    }

    #endregion

    #region Energy Balance
    /// <summary>
    /// DM_17 Calculates the NE milk corrected in Mj per day per kg (of liveweight)
    /// </summary>
    /// <param name="dailyMilkYield">DM_1 Average daily milk yield in kg a day </param>
    /// <param name="meanLiveWeight">DM_4 Mean liveweight in kg </param>
    /// <param name="milkFatContent">Milk fat content as % if known</param>
    /// <returns></returns>
    public static double NEMilkCorrected(DairyCattle cattleType, double dailyMilkYield, double meanLiveWeight, double milkFatContent)
    {
        if(cattleType == DairyCattle.DC4_DairyCows)
        {
            return dailyMilkYield * (1.509 + (0.0406 * milkFatContent * HelperFunctions.Percent_to_Proportion * 1000.0)) / Math.Pow(meanLiveWeight, 0.75);
        }

        return 0;
    }
    /// <summary>
    /// DM_9 Calculates the efficiency of utilisation of ME for maintenance
    /// </summary>
    /// <param name="ratioMeGeWholeDiet">DM_8 ME/GE ratio for whole diet </param>
    /// <returns></returns>
    public static double UtilisationEfficiencyOfMeForMaintenance(double ratioMeGeWholeDiet)
    {
        return (0.35 * ratioMeGeWholeDiet) + 0.503;
    }
    /// <summary>
    /// BM_5 Calculates the efficiency of utilisation of ME for lactation
    /// </summary>
    /// <param name="ratioMeGeWholeDiet"></param>
    /// <returns></returns>
    public static double UtilisationEfficiencyOfMeForLactation(double ratioMeGeWholeDiet)
    {
        return 0.35 * ratioMeGeWholeDiet + 0.42;
    }
    /// <summary>
    /// DM_10, BM_4 Calculates the efficiency of utilisation of ME for weight gain
    /// </summary>
    /// <param name="ratioMeGeWholeDiet">DM_8 ME/GE ratio for whole diet </param>
    /// <returns></returns>
    public static double UtilisationEfficiencyOfMeForWeightGainNonLactating(double ratioMeGeWholeDiet)
    {
        return (0.78 * ratioMeGeWholeDiet) + 0.006;
    }
    /// <summary>
    /// BM_6 Calculates the utilisation efficiency of ME for weight gain in lactation cattle
    /// </summary>
    /// <param name="uEMELactation">Utilisation efficiency of ME for lactation from BM_5</param>
    /// <returns></returns>
    public static double UtilisationEfficiencyOfMeForWeightGainLactating(double uEMELactation)
    {
        return 0.95 * uEMELactation;
    }
    /// <summary>
    /// DM_18, BM_10 Calculates fasting metabolism requirement in Mj a day
    /// </summary>
    /// <param name="meanLiveWeight">DW_4 mean live weight in kg </param>
    /// <param name="genderCorrectionParameter">correction parameter for cattle gender</param>
    /// <returns></returns>
    public static double FastingMetabolismRequirement(double meanLiveWeight, double genderCorrectionParameter)
    {
        return genderCorrectionParameter * 0.53 * Math.Pow((meanLiveWeight / 1.08), 0.67);
    }
    /// <summary>
    /// DM_19 Calculates activity allowance in Mj per day
    /// </summary>
    /// <param name="meanLiveWeight">DW_4 Mean live weight in kg </param>
    /// <param name="activityCoefficient">Activity coefficient in Mj a day</param>
    /// <returns></returns>
    public static double ActivityAllowance(double meanLiveWeight, double activityCoefficient)
    {
        return meanLiveWeight * activityCoefficient;
    }

    /// <summary>
    /// DM_20 Calculate metabolizable energy maintenance and lactation in Mj per day per live weight (kg)
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="nEMilkCorrected"></param>
    /// <param name="fastingMetabolismRequirement"></param>
    /// <param name="utilisationEfficiencyOfMEMaintenance"></param>
    /// <returns></returns>
    public static double MEMaintenanceAndLactation(DairyCattle cattleType, double? nEMilkCorrected, double? fastingMetabolismRequirement, double utilisationEfficiencyOfMEMaintenance)
    {
        if(cattleType == DairyCattle.DC4_DairyCows)
        {
            if(nEMilkCorrected == null)
            {
                throw new CustomAppException("This cattle type requires a value for NE Milk corrected that is not null.");
            }

            return (Math.Log((5.06 - (double)nEMilkCorrected) / (5.06 + 0.453))) / -0.1326;
        }

        if(fastingMetabolismRequirement == null)
        {
            throw new CustomAppException("This cattle type requires a value for fasting metabolism requirement that is not null.");
        }

        return (double)fastingMetabolismRequirement / utilisationEfficiencyOfMEMaintenance;
    }
    /// <summary>
    /// DM_21 Calculates metabolizable energy activity in Mj per day
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="meanLiveWeight">DW_4 mean live weight in kg  </param>
    /// <param name="utilisationEfficiencyOfMEMaintenance">DM_9 Efficiency of utilisation of ME for maintenance </param>
    /// <param name="activityAllowance"></param>
    /// <returns></returns>
    public static double MEActivity(DairyCattle cattleType, double? meanLiveWeight, double utilisationEfficiencyOfMEMaintenance, double? activityAllowance)
    {
        if(cattleType == DairyCattle.DC4_DairyCows)
        {
            if(meanLiveWeight == null)
            {
                throw new CustomAppException("This cattle type requires a value for mean live weight that is not null.");
            }

            return 0.0013 * ((double)meanLiveWeight / utilisationEfficiencyOfMEMaintenance);
        }

        if(activityAllowance == null)
        {
            throw new CustomAppException("This cattle type requires a value for activity allowance that is not null.");
        }

        return (double)activityAllowance / utilisationEfficiencyOfMEMaintenance;
    }

    /// <summary>
    /// DM_22 Calculates metabolizable energy live weight gain in Mj per day
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="growthRate">DW_5 growth rate in kg per day </param>
    /// <param name="liveWeight">DW_3 live weight in kg </param>
    /// <param name="utilisationEfficiencyMEForWeightGain">DM_10 efficiency of utilisation of ME for weight gain </param>
    /// <param name="netEnergyValueOfWeightGain">net energy value of weight gain in Mj per kg</param>
    /// <param name="correctionFactorEnergyContentLiveWeightGain">correction factor for energy content in live weight gain</param>
    /// <returns></returns>
    public static double MELiveWeightGain(DairyCattle cattleType, double growthRate, double meanLiveWeight, double utilisationEfficiencyMEForWeightGain,
        double netEnergyValueOfWeightGain, double correctionFactorEnergyContentLiveWeightGain)
    {
        if(cattleType == DairyCattle.DC4_DairyCows)
        {
            return netEnergyValueOfWeightGain * (growthRate / EntericEquationsParameters.EUMEDairyCowsWeightGain);
        }

        return growthRate * (((correctionFactorEnergyContentLiveWeightGain * (4.1 + (0.0332 * meanLiveWeight) - (0.000009 * Math.Pow(meanLiveWeight, 2.0)))) /
                             (1.0 - (0.1475 * growthRate))) / utilisationEfficiencyMEForWeightGain);
    }

    /// <summary>
    /// DM_24
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="meanLiveWeight"></param>
    /// <param name="mEMaintenanceLactation"></param>
    /// <param name="mEActivity"></param>
    /// <param name="mELiveWeightGain"></param>
    /// <param name="mEPregnancy"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static double TotalMERequirement(Sector sector, int cattleType, double meanLiveWeight, double mEMaintenanceLactation,
        double mEActivity, double mELiveWeightGain, double? mEPregnancy)
    {
        switch(cattleType)
        {
            case (int)DairyCattle.DC4_DairyCows:
                if(mEPregnancy == null)
                {
                    throw new CustomAppException("This cattle type requires a ME pregnancy value that is not null.");
                }

                return (-10 + (mELiveWeightGain + (double)mEPregnancy + (mEMaintenanceLactation * Math.Pow(meanLiveWeight, 0.75)) + mEActivity));
            case (int)DairyCattle.DC3_DairyInCalfHeifers:
                if(mEPregnancy == null)
                {
                    throw new CustomAppException("This cattle type requires a ME pregnancy value that is not null.");
                }

                return mEMaintenanceLactation + mEActivity + mELiveWeightGain + (double)mEPregnancy;
            case (int)DairyCattle.DC2_DairyReplacementsFemale:
            case (int)DairyCattle.DC1_DairyCalvesFemale:
                return mEMaintenanceLactation + mEActivity + mELiveWeightGain;
            default:
                throw new ArgumentException("The argument you have provided for cattle type is invalid.");
        }
    }
    /// <summary>
    /// BM_17 Metabolisable energy requirement of full-term pregnancy
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="birthWeight"></param>
    /// <param name="totalEnergyRetentionGravidFoetus"></param>
    /// <param name="herdPregnancyProportion"></param>
    /// <param name="UEMEGrowthConcepta"></param>
    /// <param name="calvingInterval"></param>
    /// <param name="averageGestationPeriod"></param>
    /// <returns></returns>
    public static double MEPregnancy(DairyCattle cattleType, double birthWeight, double totalEnergyRetentionGravidFoetus,
        double herdPregnancyProportion, double UEMEGrowthConcepta, double calvingInterval, double averageGestationPeriod)
    {

        var partialAnswer = (0.025 * birthWeight * (totalEnergyRetentionGravidFoetus / UEMEGrowthConcepta));

        return cattleType switch
        {
            DairyCattle.DC4_DairyCows => (partialAnswer / calvingInterval) * herdPregnancyProportion,
            DairyCattle.DC3_DairyInCalfHeifers => partialAnswer / averageGestationPeriod,
            _ => 0.0,
        };
    }

    public static double TotalMERequirement(DairyCattle cattleType, double meanLiveWeight, double mEMaintenanceLactation,
        double mEActivity, double mELiveWeightGain, double? mEPregnancy)
    {
        return cattleType switch
        {
            DairyCattle.DC4_DairyCows => (-10 + (mELiveWeightGain + (double)mEPregnancy + (mEMaintenanceLactation * Math.Pow(meanLiveWeight, 0.75)))) + mEActivity,
            DairyCattle.DC3_DairyInCalfHeifers => mEMaintenanceLactation + mEActivity + mELiveWeightGain + (double)mEPregnancy,
            DairyCattle.DC2_DairyReplacementsFemale or DairyCattle.DC1_DairyCalvesFemale => mEMaintenanceLactation + mEActivity + mELiveWeightGain,
            _ => throw new ArgumentException(),
        };
    }

    /// <summary>
    /// DM_25 The equation calculates the rationmod (Rdiet) in megajoules per day (MJ kgDM-1) for all dairy types.
    /// Rationmod is used to determine the total dry matter intake of the animals.
    /// </summary>
    /// <param name="totalMERequirement"></param>
    /// <param name="concentrateIntake"></param>
    /// <param name="concentrateMEContent"></param>
    /// <param name="weightedAverageForageME"></param>
    /// <returns></returns>
    public static double RationMod(double totalMERequirement, double concentrateIntake, double concentrateMEContent, double weightedAverageForageME)
    {
        return totalMERequirement / (concentrateIntake + (totalMERequirement - (concentrateIntake * concentrateMEContent)) / weightedAverageForageME);
    }

    public static double TotalDMI(double totalMERequirement, double rationMod)
    {
        return totalMERequirement / rationMod;
    }

    public static double DMIAsProportionOfLiveWeight(double totalDMIIntake, double meanLiveWeight)
    {
        return totalDMIIntake / meanLiveWeight;
    }

    public static double GEIntake(double concentrateIntake, double totalDMIIntake, double weightedAverageGEContentForage, double concentrateGEContent)
    {
        return (concentrateIntake * concentrateGEContent) + ((totalDMIIntake - concentrateIntake) * weightedAverageGEContentForage);
    }
    /// <summary>
    /// BM_7
    /// </summary>
    /// <param name="lactationLengthYear"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    public static double MilkYield(double lactationLengthYear, double scalingFactor)
    {
        var lactationLengthMonth = lactationLengthYear * HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year;
        return ((-10.137 * Math.Pow(lactationLengthMonth, 2) + (308.65 * lactationLengthMonth)) + 98.22) * scalingFactor / 365;
    }

    /// <summary>
    /// BM_12
    /// </summary>
    /// <param name="fastingMetabolismRequirement"></param>
    /// <param name="activityAllowance"></param>
    /// <returns></returns>
    public static double TotalMaintenanceEnergy(double fastingMetabolismRequirement, double activityAllowance)
    {
        return fastingMetabolismRequirement + activityAllowance;
    }
    /// <summary>
    /// BM_13
    /// </summary>
    /// <param name="totalMaintenanceEnergy"></param>
    /// <param name="UEMEMaintenance"></param>
    /// <returns></returns>
    public static double MEForMaintenance(double totalMaintenanceEnergy, double UEMEMaintenance)
    {
        return totalMaintenanceEnergy / UEMEMaintenance;
    }
    /// <summary>
    /// BM_14
    /// </summary>
    /// <param name="mEMature"></param>
    /// <param name="mEGrowing"></param>
    /// <param name="timeInBand"></param>
    /// <param name="timeTotal"></param>
    /// <param name="timeMature"></param>
    /// <returns></returns>
    public static double MEForMaintenanceWeighted(double mEMature, double mEGrowing, double timeInBand, double timeTotal, double timeMature)
    {
        return (mEGrowing * (timeInBand / timeTotal)) + (mEMature * (timeMature / timeTotal));
    }
    /// <summary>
    /// BM_15
    /// </summary>
    /// <param name="meanGrowthRate"></param>
    /// <param name="lWm"></param>
    /// <param name="correctionFactorForEnergyContentLiveWeightGain"></param>
    /// <returns></returns>
    public static double EnergyGainedThroughLiveWeightGain(double meanGrowthRate, double meanLiveWeight, double correctionFactorForEnergyContentLiveWeightGain)
    {
        return meanGrowthRate * ((correctionFactorForEnergyContentLiveWeightGain * (4.1 + (0.0332 * meanLiveWeight) - (0.000009 * Math.Pow(meanLiveWeight, 2)))) / (1 - (0.1475 * meanGrowthRate)));
    }
    /// <summary>
    /// BM_16
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="uEMEWeightGain"></param>
    /// <param name="lactationLength"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    public static double ConvertNetToMEOfGrowth(double uEMEWeightGainNonLactating, double uEMEWeightGainLactating, double lactationLength, double percentageOfHerdLactating)
    {
        return (((lactationLength) * (percentageOfHerdLactating * 0.01)) / 365.0 * uEMEWeightGainLactating) +
                            ((1 - (((lactationLength) * (percentageOfHerdLactating * 0.01)) / 365.0)) * uEMEWeightGainNonLactating);
    }
    /// <summary>
    /// BM_16 Metabolisable energy requirement for liveweight gain due to growth (Mg) in megajoules per day (MJd-1).
    /// </summary>
    /// <param name="energyGainedThroughLiveWeightGain">BM_14 Energy gained through live weigh gain (Eg) in megajoules (MJ) equation</param>
    /// <param name="convertNetToMEOfGrowth">Factor converts net to metabolizable energy of growth, depending on whether lactating or not(kgf) from equation BM_15a and BM_15b.</param>
    /// <param name="timeBand">Time in age band (Timeband) from equation BW_2 in days</param>
    /// <param name="timeTotal"></param>
    /// <returns></returns>
    public static double METoSupportGrowth(double energyGainedThroughLiveWeightGain, double convertNetToMEOfGrowth, double timeBand, double timeTotal)
    {
        return (energyGainedThroughLiveWeightGain / convertNetToMEOfGrowth) * (timeBand / timeTotal);
    }

    /// <summary>
    /// BM_17
    /// </summary>
    /// <param name="energyGainedThroughLiveWeightGain"></param>
    /// <param name="convertNetToMEOfGrowth"></param>
    /// <param name="timeBand"></param>
    /// <param name="timeTotal"></param>
    /// <returns></returns>
    public static double METoSupportGrowth(double energyGainedThroughLiveWeightGain, double convertNetToMEOfGrowth)
    {
        return (energyGainedThroughLiveWeightGain / convertNetToMEOfGrowth);
    }
    /// <summary>
    /// BM_18
    /// </summary>
    /// <param name="birthWeight"></param>
    /// <param name="energyRetentionInFullTermGravidUterus"></param>
    /// <param name="uEMEForgrowthOfConcepta"></param>
    /// <returns></returns>
    public static double MEOfFullTermPregnancyBeef(double birthWeight, double energyRetentionInFullTermGravidUterus, double uEMEForgrowthOfConcepta, double percentageOfHerdGestating)
    {
        return ((birthWeight / 40) * (energyRetentionInFullTermGravidUterus / uEMEForgrowthOfConcepta)) / HelperFunctions.Days_per_year * (percentageOfHerdGestating * HelperFunctions.Percent_to_Proportion);
    }
    /// <summary>
    /// BM_19
    /// </summary>
    /// <param name="mEFullTermPregnancy"></param>
    /// <param name="percentageOfCattleGestating"></param>
    /// <returns></returns>
    public static double AnnualAverageMEPregnancyWholeHerd(double mEFullTermPregnancy, double percentageOfCattleGestating)
    {
        return (percentageOfCattleGestating * HelperFunctions.Percent_to_Proportion) * (mEFullTermPregnancy);
    }
    /// <summary>
    /// BM_20 and BM_21
    /// </summary>
    /// <param name="dailyAverageMilkYield"></param>
    /// <param name="uEMEForLactation"></param>
    /// <param name="milkFatContent"></param>
    /// <param name="milkProteinContent"></param>
    /// <returns></returns>
    public static double EnergyOfLactation(double dailyAverageMilkYield, double uEMEForLactation, double milkFatContent, double milkProteinContent, double percentageOfHerdLactating)
    {
        return ((0.0376 * milkFatContent) + (0.0209 * milkProteinContent) + 0.948) * dailyAverageMilkYield / uEMEForLactation * percentageOfHerdLactating * HelperFunctions.Percent_to_Proportion;
    }

    /// <summary>
    /// BM_21
    /// </summary>
    /// <param name="energyRequiredForLactation"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    public static double DailyLactationEnergy(double energyRequiredForLactation, double percentageOfHerdLactating)
    {
        return energyRequiredForLactation * percentageOfHerdLactating / 100;
    }
    /// <summary>
    /// BM_22
    /// </summary>
    /// <param name="mEMaintenance"></param>
    /// <param name="mEToSupportGrowth"></param>
    /// <param name="dailyLactationEnergySacledByPercentOfCowsLactating"></param>
    /// <param name="annualAverageMEPregnancyWholeHerd"></param>
    /// <returns></returns>
    public static double TotalMERequirementForMaintenanceAndProduction(double mEMaintenance, double mEToSupportGrowth, double dailyLactationEnergyScaledByPercentOfCowsLactating,
        double annualAverageMEPregnancyWholeHerd)
    {
        return mEMaintenance + mEToSupportGrowth + annualAverageMEPregnancyWholeHerd + dailyLactationEnergyScaledByPercentOfCowsLactating;
    }
    /// <summary>
    /// BM_23
    /// </summary>
    /// <param name="totalAverageDailyMERequirement"></param>
    /// <param name="mEConcentrateWholeDiet"></param>
    /// <returns></returns>
    public static double TotalDMIIntake(double totalAverageDailyMERequirement, double mEConcentrateWholeDiet)
    {
        return totalAverageDailyMERequirement / mEConcentrateWholeDiet;
    }
    /// <summary>
    /// BM_24
    /// </summary>
    /// <param name="totalDMI"></param>
    /// <param name="gEConcnetrateOfWholeDiet"></param>
    /// <returns></returns>
    public static double GEIntake(double totalDMI, double gEConcentrationOfWholeDiet)
    {
        return totalDMI * gEConcentrationOfWholeDiet;
    }
    /// <summary>
    /// DM_25
    /// </summary>
    /// <param name="totalDMI"></param>
    /// <param name="CPConcentrationOfWholeDiet"></param>
    /// <returns></returns>
    public static double CPIntake(double totalDMI, double CPConcentrationOfWholeDiet)
    {
        return totalDMI * CPConcentrationOfWholeDiet;
    }
    #endregion

    #region Live Weights

    public static int ConvertMonthsToAgeIndex(int ageInMonths)
    {
        if(48 <= ageInMonths && ageInMonths < 60)
        {
            return 13;
        }
        else if(60 <= ageInMonths && ageInMonths < 240)
        {
            return 14;
        }
        else if(240 <= ageInMonths)
        {
            return 15;
        }

        return (int)Math.Floor((double)((ageInMonths - 1) / 3));
    }

    /// <summary>
    /// Calculates the birthweight from the mature weight in kg DW_1
    /// </summary>
    /// <param name="matureLiveWeight">mature live weight in kg</param>
    /// <returns></returns>
    public static double BirthWeight(double matureLiveWeight)
    {
        return (Math.Pow(matureLiveWeight, 0.73) - 28.89) / 2.064;
    }
    /// <summary>
    /// Calculates the live weight in kg at the time boundaries of 1 month, 1 year, first conception first DW_3 BW_1
    /// </summary>
    /// <param name="birthWeight">weight at birth in kg from DW_1</param>
    /// <param name="matureLiveWeight"></param>
    /// <param name="ageBoundary"></param>
    /// <param name="liveWeightCurveParameterK"></param>
    /// <param name="liveWeightCurveParameterC"></param>
    /// <returns></returns>
    public static double LiveWeightCalculationsAtBoundary(double birthWeight, double matureWeight, double ageBoundary, double liveWeightCurveParameterK, double liveWeightCurveParameterC)
    {
        return (birthWeight * Math.Pow(liveWeightCurveParameterK, liveWeightCurveParameterC) + matureWeight * Math.Pow(ageBoundary, liveWeightCurveParameterC)) /
            (Math.Pow(liveWeightCurveParameterK, liveWeightCurveParameterC) + Math.Pow(ageBoundary, liveWeightCurveParameterC));
    }
    /// <summary>
    /// Returns the time boundaries in weeks for each of the stages of life. DW_2
    /// </summary>
    /// <param name="stage">The stage eg birth, first month, first year, first conception, first calving, death</param>
    /// <param name="ageAtStageInMonths">The age of the animal in months at the specific stage this is not required for birth, first month and first year</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static double TimeBoundariesBetweenStages(DairyCattleStages stage, double? ageAtStageInMonths)
    {
        switch(stage)
        {
            case DairyCattleStages.Birth:
                return 0;
            case DairyCattleStages.FirstMonth:
                return 4;
            case DairyCattleStages.FirstYear:
                return 52;
            default:
                if(ageAtStageInMonths == null)
                {
                    throw new CustomAppException("This stage of life requires a value for age at stage that is not null.");
                }

                return ((double)ageAtStageInMonths * 30.5) / 7;
        }
    }
    /// <summary>
    /// Calculates the mean of two live weights in kg DW_4
    /// </summary>
    /// <param name="firstWeight">The first live weight in kg</param>
    /// <param name="secondWeight">The second live weight in kg</param>
    /// <returns></returns>
    public static double MeanLiveWeight(double firstWeight, double secondWeight)
    {
        return (firstWeight + secondWeight) / 2;
    }
    /// <summary>
    /// BW_4 This equation calculates the mean liveweight (LWm) for the ath age band, in kilograms(kg) for each cattle type.
    /// </summary>
    /// <param name="liveweightAtUpperBoundary"></param>
    /// <param name="liveweightAtLowerBoundary"></param>
    /// <param name="ageAtLowerBoundary"></param>
    /// <param name="matureAge"></param>
    /// <param name="liveWeightMature"></param>
    /// <returns></returns>
    public static double MeanLiveWeightBeef(double liveweightAtUpperBoundary, double liveweightAtLowerBoundary, double ageAtLowerBoundary, double matureAge, double liveWeightMature)
    {
        if(ageAtLowerBoundary >= matureAge)
        {
            return liveWeightMature;
        }

        return MeanLiveWeight(liveweightAtUpperBoundary, liveweightAtLowerBoundary);
    }

    /// <summary>
    /// This is the more generic version of BW_5
    /// </summary>
    /// <param name="firstWeight"></param>
    /// <param name="secondWeight"></param>
    /// <param name="firstAgeBoundary"></param>
    /// <param name="secondAgeBoundary"></param>
    /// <returns></returns>
    public static double MeanGrowthRate(double firstWeight, double secondWeight, double firstAgeBoundary, double secondAgeBoundary)
    {
        return (secondWeight - firstWeight) / ((secondAgeBoundary - firstAgeBoundary) * HelperFunctions.DaysPerWeek);
    }
    /// <summary>
    /// BW_5 This equation calculates mean growth rate (G) for an age band, in kilograms a day (kg d-1) for each cattle type.
    /// </summary>
    /// <param name="firstWeight"></param>
    /// <param name="secondWeight"></param>
    /// <param name="firstAgeBoundary"></param>
    /// <param name="secondAgeBoundary"></param>
    /// <returns></returns>
    public static double MeanGrowthRateBeef(double matureAge, double firstWeight, double secondWeight, double firstAgeBoundary, double secondAgeBoundary)
    {
        if(firstAgeBoundary >= matureAge)
        {
            return 0.0;
        }

        return MeanGrowthRate(firstWeight, secondWeight, firstAgeBoundary, secondAgeBoundary);
    }

    public static double LengthOfTimeSpentInBoundary(double ageUpperBound, double ageLowerBound)
    {
        return (ageUpperBound - ageLowerBound) * 7;
    }

    /// <summary>
    /// Calculation of liveweight gain in kg per unit time
    /// </summary>
    /// <param name="growthrate">the daily growth rate (kg/day)</param>
    /// <param name="timePeriodsperyear">the number of time periods per year (e.g. 12 for months)</param>
    /// <returns>Livweight gain (kg/time period)</returns>
    public static double LiveweightGain(double growthrate, double timePeriodsperyear)
    {
        return growthrate * 365.0 / timePeriodsperyear;
    }
    /// <summary>
    /// Calcualtion of asymptotic liveweight from a weight at known age (mature weight or weight at death)
    /// </summary>
    /// <param name="birthWeight">Weight of animal at birth (kg)</param>
    /// <param name="knownAgeWeight">Weight of animal at known age (maturity or death) in kg</param>
    /// <param name="knownAge">Age at which weight is known (maturity or death)in kg</param>
    /// <param name="dairyLiveWeightCurveParameterK">growth curve parameter K (constant)</param>
    /// <param name="dairyLiveWeightCurveParameterC">growth curve parameter C (exponent)</param>
    /// <returns>Estimated asymptotic liveweight (liveweight at infinite time) in kg</returns>
    public static double AsymptoticLiveweight(double birthWeight, double knownAgeWeight, double knownAge, double dairyLiveWeightCurveParameterK, double dairyLiveWeightCurveParameterC)
    {
        double asymptoticLiveweight = 0.0;
        asymptoticLiveweight = ((knownAgeWeight * (Math.Pow(dairyLiveWeightCurveParameterK, dairyLiveWeightCurveParameterC) + Math.Pow(knownAge, dairyLiveWeightCurveParameterC))) - (birthWeight *
             Math.Pow(dairyLiveWeightCurveParameterK, dairyLiveWeightCurveParameterC))) / Math.Pow(knownAge, dairyLiveWeightCurveParameterC);

        return asymptoticLiveweight;
    }

    #endregion
}
