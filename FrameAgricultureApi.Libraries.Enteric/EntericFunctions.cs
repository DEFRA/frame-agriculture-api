using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Libraries.Enteric.DTO;
using FrameAgricultureApi.Libraries.Excreta;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric;

public static class EntericFunctions
{
    #region Diet

    public static DietOutputs CalculateDietInformation(DietInputs dietInputs)
    {
        var dietOutputs = new DietOutputs();

        var weightedMEGERatioForage = 0.0;
        var weightedMEForage = 0.0;
        var weightedGEForage = 0.0;
        var weightedCPForage = 0.0;
        var weightedDryMatterContentForage = 0.0;
        var weightedDryMatterDigestibilityForage = 0.0;

        foreach(var forageComponent in dietInputs.ForageComponents)
        {
            var componentMEGERatioForage = EntericEquations.MeGeForageRatio(forageComponent.ForageMEContent, forageComponent.ForageGEContent);
            weightedMEGERatioForage += EntericEquations.WeightedAverageContentForage(forageComponent.ForageComponentPercent, componentMEGERatioForage);
            weightedMEForage += EntericEquations.WeightedAverageContentForage(forageComponent.ForageComponentPercent, forageComponent.ForageMEContent);
            weightedGEForage += EntericEquations.WeightedAverageContentForage(forageComponent.ForageComponentPercent, forageComponent.ForageGEContent);
            weightedCPForage += EntericEquations.WeightedAverageContentForage(forageComponent.ForageComponentPercent, forageComponent.ForageCPContent);
            weightedDryMatterContentForage += EntericEquations.WeightedAverageContentForage(forageComponent.ForageComponentPercent, forageComponent.ForageDryMatterContent);
            var componentDryMatterDigestibility = EntericEquations.DryMatterDigestibilityForForageComponent(forageComponent.ForageMEContent);
            weightedDryMatterDigestibilityForage = EntericEquations.WeightedAverageContentForage(forageComponent.ForageComponentPercent, componentDryMatterDigestibility);
        }

        dietOutputs.WeightedMEGERatioForage = weightedMEGERatioForage;
        dietOutputs.WeightedMEForage = weightedMEForage;
        dietOutputs.WeightedGEForage = weightedGEForage;
        dietOutputs.WeightedCPForage = weightedCPForage;
        dietOutputs.WeightedDryMatterContentForage = weightedDryMatterContentForage;
        dietOutputs.WeightedDryMatterDigestibilityForage = weightedDryMatterDigestibilityForage;

        var weightedMEGERatioConcentrate = 0.0;
        var weightedMEConcentrate = 0.0;
        var weightedGEConcentrate = 0.0;
        var weightedCPConcentrate = 0.0;
        var weightedDryMatterContentConcentrate = 0.0;
        var weightedDryMatterDigestibilityConcentrate = 0.0;

        foreach(var concentrateComponent in dietInputs.ConcentrateComponents)
        {
            var componentMEGERatioConcentrate = EntericEquations.MeGeForageRatio(concentrateComponent.ConcentrateMEContent, concentrateComponent.ConcentrateGEContent);
            weightedMEGERatioConcentrate += EntericEquations.WeightedAverageContentForage(concentrateComponent.ConcentrateComponentPercent, componentMEGERatioConcentrate);
            weightedMEConcentrate += EntericEquations.WeightedAverageContentForage(concentrateComponent.ConcentrateComponentPercent, concentrateComponent.ConcentrateMEContent);
            weightedGEConcentrate += EntericEquations.WeightedAverageContentForage(concentrateComponent.ConcentrateComponentPercent, concentrateComponent.ConcentrateGEContent);
            weightedCPConcentrate += EntericEquations.WeightedAverageContentForage(concentrateComponent.ConcentrateComponentPercent, concentrateComponent.ConcentrateCPContent);
            weightedDryMatterContentConcentrate += EntericEquations.WeightedAverageContentForage(concentrateComponent.ConcentrateComponentPercent, concentrateComponent.ConcentrateDryMatterContent);
            var componentDryMatterDigestibility = EntericEquations.DryMatterDigestibilityConcentrate(concentrateComponent.ConcentrateMEContent);
            weightedDryMatterDigestibilityConcentrate = EntericEquations.WeightedAverageContentForage(concentrateComponent.ConcentrateComponentPercent, componentDryMatterDigestibility);
        }

        dietOutputs.WeightedMEGERatioConcentrate = weightedMEGERatioConcentrate;
        dietOutputs.WeightedMEConcentrate = weightedMEConcentrate;
        dietOutputs.WeightedGEConcentrate = weightedGEConcentrate;
        dietOutputs.WeightedCPConcentrate = weightedCPConcentrate;
        dietOutputs.WeightedDryMatterContentConcentrate = weightedDryMatterContentConcentrate;
        dietOutputs.WeightedDryMatterDigestibilityConcentrate = weightedDryMatterDigestibilityConcentrate;

        dietOutputs.MEGERatioWholeDiet = EntericEquations.RatioMeGeForTheWholeDiet(weightedMEGERatioConcentrate, weightedMEGERatioForage, dietInputs.ConcentrateIntake, dietInputs.DMIDaily);

        var dryMatterDigestibilityOfConcentrate = EntericEquations.DryMatterDigestibilityConcentrate(weightedMEConcentrate);
        var dryMatterDigestibilityOfForage = EntericEquations.DryMatterDigestibilityForForageComponent(weightedMEForage);

        dietOutputs.DryMatterDigestibility = EntericEquations.DryMatterDigestibilityForTheWholeDiet(dryMatterDigestibilityOfConcentrate, dryMatterDigestibilityOfForage,
            dietInputs.ConcentrateIntake, dietInputs.DMIDaily);

        dietOutputs.DryMatterContent = EntericEquations.DryMatterContentForTheWholeDiet(weightedDryMatterContentForage, weightedDryMatterContentConcentrate,
            dietInputs.ConcentrateIntake, dietInputs.DMIDaily);

        dietOutputs.UEMEMaintenance = EntericEquations.UtilisationEfficiencyOfMeForMaintenance(dietOutputs.MEGERatioWholeDiet);
        dietOutputs.UEMEWeightGainNonLactating = EntericEquations.UtilisationEfficiencyOfMeForWeightGainNonLactating(dietOutputs.MEGERatioWholeDiet);

        return dietOutputs;
    }

    #endregion

    #region Live weights
    public static LiveWeightOutputsDairy LiveweightsDairy(DairyLiveWeightInputs liveWeightInputs)
    {
        try
        {
            var outputs = new LiveWeightOutputsDairy();

            var birthWeight = EntericEquations.BirthWeight(liveWeightInputs.MatureWeight);

            var birthStageTime = EntericEquations.TimeBoundariesBetweenStages(DairyCattleStages.Birth, null);
            var firstMonthStageTime = EntericEquations.TimeBoundariesBetweenStages(DairyCattleStages.FirstMonth, null);
            var firstYearStageTime = EntericEquations.TimeBoundariesBetweenStages(DairyCattleStages.FirstYear, null);
            var firstConceptionStageTime = EntericEquations.TimeBoundariesBetweenStages(DairyCattleStages.FirstConception, liveWeightInputs.AgeFirstConception);
            var firstCalvingStageTime = EntericEquations.TimeBoundariesBetweenStages(DairyCattleStages.FirstCalving, liveWeightInputs.AgeFirstCalving);
            var deathStageTime = EntericEquations.TimeBoundariesBetweenStages(DairyCattleStages.Death, liveWeightInputs.AgeAtDeath);

            //Calculate asymptotic liveweight from a known weight at a known age (mature weight (death))
            var asymptoticLiveweight = EntericEquations.AsymptoticLiveweight(birthWeight, liveWeightInputs.MatureWeight, liveWeightInputs.AgeAtDeath, EntericEquationsParameters.DairyLiveWeightCurveParameterK, EntericEquationsParameters.DairyLiveWeightCurveParameterC);

            var liveweightAtBirth = EntericEquations.LiveWeightCalculationsAtBoundary(birthWeight, asymptoticLiveweight, birthStageTime, EntericEquationsParameters.DairyLiveWeightCurveParameterK, EntericEquationsParameters.DairyLiveWeightCurveParameterC);
            var liveweightAtOneMonth = EntericEquations.LiveWeightCalculationsAtBoundary(birthWeight, asymptoticLiveweight, firstMonthStageTime, EntericEquationsParameters.DairyLiveWeightCurveParameterK, EntericEquationsParameters.DairyLiveWeightCurveParameterC);
            var liveweightAtOneYear = EntericEquations.LiveWeightCalculationsAtBoundary(birthWeight, asymptoticLiveweight, firstYearStageTime, EntericEquationsParameters.DairyLiveWeightCurveParameterK, EntericEquationsParameters.DairyLiveWeightCurveParameterC);
            var liveweightAtConception = EntericEquations.LiveWeightCalculationsAtBoundary(birthWeight, asymptoticLiveweight, liveWeightInputs.AgeFirstConception, EntericEquationsParameters.DairyLiveWeightCurveParameterK, EntericEquationsParameters.DairyLiveWeightCurveParameterC);
            var liveweightAtCalving = EntericEquations.LiveWeightCalculationsAtBoundary(birthWeight, asymptoticLiveweight, liveWeightInputs.AgeFirstCalving, EntericEquationsParameters.DairyLiveWeightCurveParameterK, EntericEquationsParameters.DairyLiveWeightCurveParameterC);

            outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC1_DairyCalvesFemale, EntericEquations.MeanLiveWeight(liveweightAtOneMonth, liveweightAtOneYear));
            outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC2_DairyReplacementsFemale, EntericEquations.MeanLiveWeight(liveweightAtOneYear, liveweightAtConception));
            outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC3_DairyInCalfHeifers, EntericEquations.MeanLiveWeight(liveweightAtConception, liveweightAtCalving));
            outputs.MeanLiveweightForEntericMethane.Add(DairyCattle.DC4_DairyCows, EntericEquations.MeanLiveWeight(liveweightAtCalving, liveWeightInputs.MatureWeight));

            outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC1_DairyCalvesFemale, EntericEquations.MeanLiveWeight(liveweightAtBirth, liveweightAtOneYear));
            outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC2_DairyReplacementsFemale, EntericEquations.MeanLiveWeight(liveweightAtOneYear, liveweightAtConception));
            outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC3_DairyInCalfHeifers, EntericEquations.MeanLiveWeight(liveweightAtConception, liveweightAtCalving));
            outputs.MeanLiveweightForNExcretion.Add(DairyCattle.DC4_DairyCows, EntericEquations.MeanLiveWeight(liveweightAtCalving, liveWeightInputs.MatureWeight));

            outputs.GrowthRates.GrowthRateBirthToFirstYear = EntericEquations.MeanGrowthRate(liveweightAtBirth, liveweightAtOneYear, birthStageTime, firstYearStageTime);
            outputs.GrowthRates.GrowthRateFirstMonthToFirstYear = EntericEquations.MeanGrowthRate(liveweightAtOneMonth, liveweightAtOneYear, firstMonthStageTime, firstYearStageTime);
            outputs.GrowthRates.GrowthRateFirstYearToFirstConception = EntericEquations.MeanGrowthRate(liveweightAtOneYear, liveweightAtConception, 52, liveWeightInputs.AgeFirstConception);
            outputs.GrowthRates.GrowthRateConceptionToCalving = EntericEquations.MeanGrowthRate(liveweightAtConception, liveweightAtCalving, liveWeightInputs.AgeFirstConception, liveWeightInputs.AgeFirstCalving);
            outputs.GrowthRates.GrowthRateCalvingToDeath = EntericEquations.MeanGrowthRate(liveweightAtCalving, liveWeightInputs.MatureWeight, liveWeightInputs.AgeFirstCalving, liveWeightInputs.AgeAtDeath);

            outputs.LiveweightAtBirth = liveweightAtBirth;
            outputs.LiveweightAtOneMonth = liveweightAtOneMonth;
            outputs.LiveweightAtOneYear = liveweightAtOneYear;
            outputs.LiveweightAtFirstConception = liveweightAtConception;
            outputs.LiveweightAtFirstCalving = liveweightAtCalving;

            return outputs;
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error in energy balance: " + ex.Message);
        }
    }
    /// <summary>
    /// Calculates mean liveweight, mean growth rate, mature liveweight for cattle using BW_1, BW_4 and BW_5
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static LiveWeightOutputsBeef LiveweightsBeef(BeefLiveWeightInputs inputs)
    {
        try
        {
            LiveWeightOutputsBeef outputs = new LiveWeightOutputsBeef();

            var liveWeightCurveParameterK = BeefCattleLiveWeightParameterLookUp.RetrieveLiveWeightCurveParameterK(inputs.CattleType, inputs.CattleBreed);
            var liveWeightCurveParameterC = BeefCattleLiveWeightParameterLookUp.RetrieveLiveWeightCurveParameterC(inputs.CattleType, inputs.CattleBreed);
            var liveWeightAtLowerBoundary = EntericEquations.LiveWeightCalculationsAtBoundary(inputs.CalfBirthWeight, inputs.MatureWeight, inputs.LowerBoundaryAge, liveWeightCurveParameterK, liveWeightCurveParameterC);
            var liveWeightAtUpperBoundary = EntericEquations.LiveWeightCalculationsAtBoundary(inputs.CalfBirthWeight, inputs.MatureWeight, inputs.UpperBoundaryAge, liveWeightCurveParameterK, liveWeightCurveParameterC);

            var matureLiveweight = EntericEquations.LiveWeightCalculationsAtBoundary(inputs.CalfBirthWeight, inputs.MatureWeight, inputs.MatureAge, liveWeightCurveParameterK, liveWeightCurveParameterC);

            var meanLiveweight = EntericEquations.MeanLiveWeightBeef(liveWeightAtUpperBoundary, liveWeightAtLowerBoundary, inputs.LowerBoundaryAge, inputs.MatureAge, matureLiveweight);

            var meanGrowthRate = EntericEquations.MeanGrowthRateBeef(inputs.MatureAge, liveWeightAtLowerBoundary, liveWeightAtUpperBoundary, inputs.LowerBoundaryAge, inputs.UpperBoundaryAge);

            outputs.MeanLiveweight = meanLiveweight;
            outputs.MeanGrowthRate = meanGrowthRate;
            outputs.MatureLiveweight = matureLiveweight;

            return outputs;
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error in energy balance: " + ex.Message);
        }
    }
    /// <summary>
    /// Return liveweight gain for dairy cattle types
    /// </summary>
    /// <param name="type">the dairy cattle type enumerator</param>
    /// <param name="data">Dairy liveweight outputs object</param>
    /// <param name="timePeriodsPerYear">the number of time periods (period over which gain is required) in the year</param>
    /// <returns>Livweight gain (kg/time period)</returns>
    public static double LiveWeightGainDairy(DairyCattle type, LiveWeightOutputsDairy data, double timePeriodsPerYear)
    {
        return type switch
        {
            DairyCattle.DC4_DairyCows => EntericEquations.LiveweightGain(data.GrowthRates.GrowthRateCalvingToDeath, timePeriodsPerYear),
            DairyCattle.DC3_DairyInCalfHeifers => EntericEquations.LiveweightGain(data.GrowthRates.GrowthRateConceptionToCalving, timePeriodsPerYear),
            DairyCattle.DC2_DairyReplacementsFemale => EntericEquations.LiveweightGain(data.GrowthRates.GrowthRateFirstYearToFirstConception, timePeriodsPerYear),
            DairyCattle.DC1_DairyCalvesFemale => EntericEquations.LiveweightGain(data.GrowthRates.GrowthRateBirthToFirstYear, timePeriodsPerYear),
            _ => throw new CustomAppException("Error calculating liveweight gain - cattle type not found."),
        };
    }
    /// <summary>
    /// Calculates the liveweight gain for beef cattle. Is a wrapper around the generic function to calculate live weight gain that allows the function to take
    /// LiveWeightOutputsBeef as an input
    /// </summary>
    /// <param name="data"></param>
    /// <param name="timePeriodsPerYear"></param>
    /// <returns></returns>
    public static double LiveweightGainBeef(LiveWeightOutputsBeef data, double timePeriodsPerYear)
    {
        return EntericEquations.LiveweightGain(data.MeanGrowthRate, timePeriodsPerYear);
    }

    #endregion

    #region Energy Balance
    /// <summary>
    /// Calculates the energy balance for dairy cattle using DM_20, DM_21, DM_22, DM_25, DN_5, DN_6
    /// </summary>
    /// <param name="energyBalanceInputs"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static EnergyBalanceOutputsDairy EnergyAndNitrogenBalanceDairy(DairyEnergyBalanceInputs energyBalanceInputs)
    {
        try
        {

            EnergyBalanceOutputsDairy energyBalanceOutputs = new EnergyBalanceOutputsDairy();

            double? nEMilkCorrected = null;
            double? fastingMetabolismRequirement = null;
            double? activityAllowance = null;
            double mEPregnancy = 0.0;

            var ScalingFactorOfCattleLactating = 0.0;

            var nitrogenRetentionFoetus = 9.352982139;

            var HerdPregnancyProportion = 0.9;

            if(energyBalanceInputs.CattleType == DairyCattle.DC4_DairyCows)
            {
                nEMilkCorrected = EntericEquations.NEMilkCorrected(DairyCattle.DC4_DairyCows, energyBalanceInputs.AverageAnnualMilkYield / HelperFunctions.Days_per_year, energyBalanceInputs.MeanLiveweightEnteric, energyBalanceInputs.MilkFatContent);
            }
            else
            {
                fastingMetabolismRequirement = EntericEquations.FastingMetabolismRequirement(energyBalanceInputs.MeanLiveweightEnteric, EntericEquationsParameters.GenderCorrectionParameter);
                activityAllowance = EntericEquations.ActivityAllowance(energyBalanceInputs.MeanLiveweightEnteric, EntericEquationsParameters.ActivityCoefficient);
            }

            if(energyBalanceInputs.CattleType == DairyCattle.DC4_DairyCows || energyBalanceInputs.CattleType == DairyCattle.DC3_DairyInCalfHeifers)
            {
                mEPregnancy = EntericEquations.MEPregnancy((DairyCattle)energyBalanceInputs.CattleType, energyBalanceInputs.BirthWeight, EntericEquationsParameters.TotalEnergyRetentionGravidFoetus,
                    HerdPregnancyProportion, EntericEquationsParameters.UEMEGrowthConcepta, energyBalanceInputs.CalvingInterval, EntericEquationsParameters.AverageGestationPeriod);
            }

            var nitrogenRetentionInLiveweightGain = 0.0;

            nitrogenRetentionInLiveweightGain = ExcretaEquations.NitrogenRetentionInLiveWeightGainDairy(energyBalanceInputs.GrowthRateExcretion,
                        ExcretaEquationsParameters.NetProteinContentOfLiveWeightGain, ExcretaEquationsParameters.ProteinToNRatio);

            var nitrogenRetentionInPregnancy = 0.0;
            if(energyBalanceInputs.CattleType == DairyCattle.DC4_DairyCows || energyBalanceInputs.CattleType == DairyCattle.DC3_DairyInCalfHeifers)
            {
                nitrogenRetentionInPregnancy = ExcretaEquations.AverageDailyNitrogenRetentionForPregnancyDairy(energyBalanceInputs.CattleType, energyBalanceInputs.BirthWeight, nitrogenRetentionFoetus, ExcretaEquationsParameters.ProteinToNRatio, energyBalanceInputs.CalvingInterval, EntericEquationsParameters.AverageGestationPeriod);
            }

            var nitrogenRetentionInMilk = 0.0;
            if(energyBalanceInputs.CattleType == DairyCattle.DC4_DairyCows)
            {
                nitrogenRetentionInMilk = ExcretaEquations.NitrogenRetentionInMilkDairy(energyBalanceInputs.AverageAnnualMilkYield / HelperFunctions.Days_per_year, energyBalanceInputs.MilkProteinContent, ExcretaEquationsParameters.MilkProteinToNRatio);
            }

            energyBalanceOutputs.MEPregnancy = mEPregnancy;
            energyBalanceOutputs.NitrogenRetentionInLiveweightGain = nitrogenRetentionInLiveweightGain;
            energyBalanceOutputs.NitrogenRetentionInMilk = nitrogenRetentionInMilk;
            energyBalanceOutputs.NitrogenRetentionInPregnancy = nitrogenRetentionInPregnancy;

            energyBalanceOutputs.MEMaintenanceLactation = EntericEquations.MEMaintenanceAndLactation(energyBalanceInputs.CattleType, nEMilkCorrected,
                        fastingMetabolismRequirement, energyBalanceInputs.UEMEMaintenance);

            energyBalanceOutputs.MEActivity = EntericEquations.MEActivity(energyBalanceInputs.CattleType, energyBalanceInputs.MeanLiveweightEnteric, energyBalanceInputs.UEMEMaintenance, activityAllowance);

            energyBalanceOutputs.MELiveWeightGain = EntericEquations.MELiveWeightGain(energyBalanceInputs.CattleType, energyBalanceInputs.GrowthRateEnteric, energyBalanceInputs.MeanLiveweightEnteric,
                        energyBalanceInputs.UEMEWeightGainNonLactating, EntericEquationsParameters.NetEnergyValueOfWeightGain, EntericEquationsParameters.CorrectionFactorEnergyContentLiveWeightGain);

            energyBalanceOutputs.DailyMERequirement = EntericEquations.TotalMERequirement(energyBalanceInputs.CattleType, energyBalanceInputs.MeanLiveweightEnteric, energyBalanceOutputs.MEMaintenanceLactation,
                energyBalanceOutputs.MEActivity, energyBalanceOutputs.MELiveWeightGain, mEPregnancy);

            energyBalanceOutputs.RationMod = EntericEquations.RationMod(energyBalanceOutputs.DailyMERequirement, energyBalanceInputs.ConcentrateIntake, energyBalanceInputs.MEContentOfConcentrate,
                energyBalanceInputs.MEContentOfForage);

            energyBalanceOutputs.DailyDryMatterIntake = EntericEquations.TotalDMI(energyBalanceOutputs.DailyMERequirement, energyBalanceOutputs.RationMod);

            energyBalanceOutputs.DMIAsPercentageOfLiveweight = EntericEquations.DMIAsProportionOfLiveWeight(energyBalanceOutputs.DailyDryMatterIntake, energyBalanceInputs.MeanLiveweightEnteric);

            energyBalanceOutputs.DailyGEIntake = EntericEquations.GEIntake(energyBalanceInputs.ConcentrateIntake, energyBalanceOutputs.DailyDryMatterIntake, energyBalanceInputs.GEContentOfForage,
                energyBalanceInputs.GEContentOfConcentrate);

            var dailyNitrogenIntake = ExcretaEquations.NitrogenIntakeDairy(energyBalanceOutputs.DailyDryMatterIntake, energyBalanceInputs.CPContentOfForage, energyBalanceInputs.ConcentrateIntake, ExcretaEquationsParameters.ProteinToNRatio, energyBalanceInputs.CPContentOfConcentrate);
            energyBalanceOutputs.DailyNIntake = dailyNitrogenIntake;

            var monthlyNExcretion = ExcretaEquations.NitrogenExcretionDairy(energyBalanceInputs.CattleType,
                dailyNitrogenIntake,
                nitrogenRetentionInMilk,
                nitrogenRetentionInLiveweightGain, nitrogenRetentionInPregnancy);

            energyBalanceOutputs.DailyNExcretion = monthlyNExcretion * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);

            // Todo this will need changing in the future
            energyBalanceOutputs.MEUncertaintyScalar = 1.0;

            return energyBalanceOutputs;

        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error in energy balance: " + ex.Message);
        }
    }
    /// <summary>
    /// Calculate the energy and nitrogen balance for beef cattle using BM_24, BN_8, BN_9, BM_17, BM_18, BM_20 and BM_21
    /// </summary>
    /// <param name="energyBalanceInputs"></param>
    /// <returns></returns>
    public static EnergyBalanceOutputsBeef EnergyAndNitrogenBalanceBeef(BeefEnergyBalanceInputs energyBalanceInputs)
    {
        EnergyBalanceOutputsBeef energyBalanceOutputs = new EnergyBalanceOutputsBeef();

        var ratioMEGEWholeDiet = energyBalanceInputs.DietMetabolizableEnergyContent / energyBalanceInputs.DietGrossEnergyContent;
        var uEMEMaintenance = EntericEquations.UtilisationEfficiencyOfMeForMaintenance(ratioMEGEWholeDiet);

        var uEMELactation = EntericEquations.UtilisationEfficiencyOfMeForLactation(ratioMEGEWholeDiet);
        var uEMEWeightGainLactating = EntericEquations.UtilisationEfficiencyOfMeForWeightGainLactating(uEMELactation);
        var uEMEWeightGainNonLactating = EntericEquations.UtilisationEfficiencyOfMeForWeightGainNonLactating(ratioMEGEWholeDiet);

        var activityAllowance = EntericEquations.ActivityAllowance(energyBalanceInputs.MeanLiveweight, EntericEquationsParameters.ErActivityEnergy);

        var eRFasting = EntericEquations.FastingMetabolismRequirement(energyBalanceInputs.MeanLiveweight, EntericEquationsParameters.GenderCorrectionParameter);

        double meMaintenance = (eRFasting + activityAllowance) / uEMEMaintenance;

        var timeGrowing = 0.0;

        var energyGainedThroughLiveWeight = EntericEquations.EnergyGainedThroughLiveWeightGain(energyBalanceInputs.MeanGrowthRate, energyBalanceInputs.MeanLiveweight, ExcretaEquationsParameters.CorrectionFactorForEnergyContentLiveWeightGain[new ExcretaEquationsParameters.CorrectionParameterKey((int)energyBalanceInputs.CattleType, (int)energyBalanceInputs.CattleBreed)]);

        var mEOfGrowth = EntericEquations.ConvertNetToMEOfGrowth(uEMEWeightGainNonLactating, uEMEWeightGainLactating, energyBalanceInputs.LactationLength, energyBalanceInputs.PercentCattleTypeLactating);

        double mEToSupportGrowth = EntericEquations.METoSupportGrowth(energyGainedThroughLiveWeight, mEOfGrowth);

        var mEPregnancy = EntericEquations.MEOfFullTermPregnancyBeef(energyBalanceInputs.BirthWeight, ExcretaEquationsParameters.EnergyRetentionInFullTermGravidUterus[(int)energyBalanceInputs.CattleBreed], EntericEquationsParameters.UEMEGrowthConcepta, energyBalanceInputs.PercentCattleTypeGestating);

        var mELactation = EntericEquations.EnergyOfLactation(energyBalanceInputs.MilkYield, uEMELactation, energyBalanceInputs.MilkFat, energyBalanceInputs.MilkProtein, energyBalanceInputs.PercentCattleTypeLactating);

        var totalMERequirement = meMaintenance + mEToSupportGrowth + mEPregnancy + mELactation;

        var dailyDMI = totalMERequirement / energyBalanceInputs.DietMetabolizableEnergyContent;

        var geIntakeDaily = EntericEquations.GEIntake(dailyDMI, energyBalanceInputs.DietGrossEnergyContent);

        var nitrogenRetentionInMilk = ExcretaEquations.NitrogenRetentionInMilkBeef(energyBalanceInputs.MilkYield, energyBalanceInputs.MilkProtein, ExcretaEquationsParameters.MilkProteinToNRatio, energyBalanceInputs.PercentCattleTypeLactating);

        var netProteinContentOfLiveWeightGain = ExcretaEquations.NetProteinContentOfLiveWeightGain(energyBalanceInputs.MeanGrowthRate, energyBalanceInputs.MeanLiveweight,
            ExcretaEquationsParameters.CorrectionFactorForNitrogenRetention[new ExcretaEquationsParameters.CorrectionParameterKey((int)energyBalanceInputs.CattleType, (int)energyBalanceInputs.CattleBreed)]);

        var proteinRetentionFullTerm = 0.0;
        var dailyNitrogenRetentionForPregnancy = 0.0;
        var proteinContentWeightGain = 0.0;

        switch(energyBalanceInputs.CattleType)
        {
            case BeefCattleType.Cows:
            case BeefCattleType.Heifersforbreeding:
                proteinRetentionFullTerm = ExcretaEquations.ProteinRetentionInFullTerm(ExcretaEquationsParameters.AverageGestationPeriod[(int)energyBalanceInputs.CattleBreed]);
                dailyNitrogenRetentionForPregnancy = ExcretaEquations.AverageDairyNitrogenRetentionForPregnancyBeef(proteinRetentionFullTerm, energyBalanceInputs.PercentCattleTypeGestating, energyBalanceInputs.BirthWeight, ExcretaEquationsParameters.ProteinToNRatio);
                var netProteinContentOfLiveWeightGainInLactatingRuminants = ExcretaEquationsParameters.NetProteinContentOfLiveWeightGainInLactatingRuminantsCoefficient * energyBalanceInputs.MeanGrowthRate;
                proteinContentWeightGain = ExcretaEquations.ProteinContentWeightGain(energyBalanceInputs.LactationLength, netProteinContentOfLiveWeightGain, energyBalanceInputs.PercentCattleTypeLactating, netProteinContentOfLiveWeightGainInLactatingRuminants);
                break;
            default:
                proteinContentWeightGain = netProteinContentOfLiveWeightGain;
                break;

        }

        var nitrogenRetentionInLiveWeightGain = ExcretaEquations.NitrogenRetentionInLiveWeightGainBeef(proteinContentWeightGain, ExcretaEquationsParameters.ProteinToNRatio);

        var nitrogenDensity = ExcretaEquations.NitrogenDensityInWholeDiet(energyBalanceInputs.DietCrudeProteinContent, ExcretaEquationsParameters.ProteinToNRatio);
        var dailyNitrogenIntake = ExcretaEquations.NitrogenIntakeBeef(nitrogenDensity, dailyDMI);
        var dailyNitrogenExcretion = ExcretaEquations.NitrogenExcretionBeef(dailyNitrogenIntake, nitrogenRetentionInLiveWeightGain, nitrogenRetentionInMilk, dailyNitrogenRetentionForPregnancy);

        energyBalanceOutputs.DailyGEIntake = geIntakeDaily;
        energyBalanceOutputs.DailyCPIntake = EntericEquations.CPIntake(dailyDMI, energyBalanceInputs.DietCrudeProteinContent);
        energyBalanceOutputs.DailyDryMatterIntake = dailyDMI;
        energyBalanceOutputs.DailyNIntake = dailyNitrogenIntake;
        energyBalanceOutputs.DailyNExcretion = dailyNitrogenExcretion;
        energyBalanceOutputs.DailyMEMaintenance = meMaintenance;
        energyBalanceOutputs.DailyMELiveweightGain = mEToSupportGrowth;
        energyBalanceOutputs.DailyMEPregnancy = mEPregnancy;
        energyBalanceOutputs.DailyMELactation = mELactation;

        return energyBalanceOutputs;
    }

    // Todo this function has loads of repeated code from EnergyAndNitrogenBalanceBeef!
    /// <summary>
    /// Calculate the energy and nitrogen balance for beef cattle in Ireland
    /// </summary>
    /// <param name="energyBalanceInputs"></param>
    /// <returns></returns>
    public static EnergyBalanceOutputsBeef EnergyAndNitrogenBalanceBeef(IrishBeefEnergyBalanceInputs energyBalanceInputs)
    {
        EnergyBalanceOutputsBeef energyBalanceOutputs = new EnergyBalanceOutputsBeef();

        var ratioMEGEWholeDiet = energyBalanceInputs.DietMetabolizableEnergyContent / energyBalanceInputs.DietGrossEnergyContent;
        var uEMEMaintenance = EntericEquations.UtilisationEfficiencyOfMeForMaintenance(ratioMEGEWholeDiet);

        var uEMELactation = EntericEquations.UtilisationEfficiencyOfMeForLactation(ratioMEGEWholeDiet);
        var uEMEWeightGainLactating = EntericEquations.UtilisationEfficiencyOfMeForWeightGainLactating(uEMELactation);
        var uEMEWeightGainNonLactating = EntericEquations.UtilisationEfficiencyOfMeForWeightGainNonLactating(ratioMEGEWholeDiet);

        var activityAllowance = EntericEquations.ActivityAllowance(energyBalanceInputs.MeanLiveweight, EntericEquationsParameters.ErActivityEnergy);

        var eRFasting = EntericEquations.FastingMetabolismRequirement(energyBalanceInputs.MeanLiveweight, EntericEquationsParameters.GenderCorrectionParameter);

        double meMaintenance = (eRFasting + activityAllowance) / uEMEMaintenance;

        var timeGrowing = 0.0;

        if(energyBalanceInputs.AgeIndex == 103)
        {

            var activityAllowanceMature = EntericEquations.ActivityAllowance(energyBalanceInputs.MatureLiveweight, EntericEquationsParameters.ErActivityEnergy);
            var eRFastingMature = EntericEquations.FastingMetabolismRequirement(energyBalanceInputs.MatureLiveweight, EntericEquationsParameters.GenderCorrectionParameter);
            var totalMEGrowing = eRFasting + activityAllowance;
            var totalMEMatureWeight = eRFastingMature + activityAllowanceMature;

            var mEMature = totalMEMatureWeight / uEMEMaintenance;
            var mEGrowing = totalMEGrowing / uEMEMaintenance;

            double timeToMature = 0.0;
            if(energyBalanceInputs.CattleType == BeefCattleType.Bullsforbreeding)
            {
                timeToMature = 2970.0 - 1825.0;
            }
            if(energyBalanceInputs.CattleType == BeefCattleType.Cows)
            {
                timeToMature = 2722.0 - 1825.0;
            }

            var upperBoundaryAgeInWeeks = EntericEquationsParameters.AgeIndexDictionary[energyBalanceInputs.AgeIndex];
            var lowerBoundaryAgeInWeeks = EntericEquationsParameters.AgeIndexDictionary[energyBalanceInputs.AgeIndex + 1];
            timeGrowing = (upperBoundaryAgeInWeeks - lowerBoundaryAgeInWeeks) * 7;

            var totalTime = timeGrowing + timeToMature;

            var weightedMEMaintenance = EntericEquations.MEForMaintenanceWeighted(mEMature, mEGrowing, timeGrowing, totalTime, timeToMature);

        }
        else
        {
            meMaintenance = (eRFasting + activityAllowance) / uEMEMaintenance;
        }

        var energyGainedThroughLiveWeight = EntericEquations.EnergyGainedThroughLiveWeightGain(energyBalanceInputs.MeanGrowthRate, energyBalanceInputs.MeanLiveweight, ExcretaEquationsParameters.CorrectionFactorForEnergyContentLiveWeightGain[new ExcretaEquationsParameters.CorrectionParameterKey((int)energyBalanceInputs.CattleType, (int)energyBalanceInputs.CattleBreed)]);

        var mEOfGrowth = EntericEquations.ConvertNetToMEOfGrowth(uEMEWeightGainNonLactating, uEMEWeightGainLactating, energyBalanceInputs.LactationLength, energyBalanceInputs.PercentCattleTypeLactating);

        double mEToSupportGrowth = EntericEquations.METoSupportGrowth(energyGainedThroughLiveWeight, mEOfGrowth);

        if(energyBalanceInputs.AgeIndex == 103)
        {
            double timeMature = 0;
            if(energyBalanceInputs.CattleType == BeefCattleType.Cows)
            {
                timeMature = 2722.0 - 1825.0;
            }

            var totalTime = timeGrowing + timeMature;

            mEToSupportGrowth = EntericEquations.METoSupportGrowth(energyGainedThroughLiveWeight, mEOfGrowth, timeGrowing, totalTime);

        }
        else
        {
            mEToSupportGrowth = EntericEquations.METoSupportGrowth(energyGainedThroughLiveWeight, mEOfGrowth);
        }

        var mEPregnancy = EntericEquations.MEOfFullTermPregnancyBeef(energyBalanceInputs.BirthWeight, ExcretaEquationsParameters.EnergyRetentionInFullTermGravidUterus[(int)energyBalanceInputs.CattleBreed], EntericEquationsParameters.UEMEGrowthConcepta, energyBalanceInputs.PercentCattleTypeGestating);

        var mELactation = EntericEquations.EnergyOfLactation(energyBalanceInputs.MilkYield, uEMELactation, energyBalanceInputs.MilkFat, energyBalanceInputs.MilkProtein, energyBalanceInputs.PercentCattleTypeLactating);

        var totalMERequirement = meMaintenance + mEToSupportGrowth + mEPregnancy + mELactation;

        var dailyDMI = totalMERequirement / energyBalanceInputs.DietMetabolizableEnergyContent;

        var geIntakeDaily = EntericEquations.GEIntake(dailyDMI, energyBalanceInputs.DietGrossEnergyContent);

        var nitrogenRetentionInMilk = ExcretaEquations.NitrogenRetentionInMilkBeef(energyBalanceInputs.MilkYield, energyBalanceInputs.MilkProtein, ExcretaEquationsParameters.MilkProteinToNRatio, energyBalanceInputs.PercentCattleTypeLactating);

        var netProteinContentOfLiveWeightGain = ExcretaEquations.NetProteinContentOfLiveWeightGain(energyBalanceInputs.MeanGrowthRate, energyBalanceInputs.MeanLiveweight,
            ExcretaEquationsParameters.CorrectionFactorForNitrogenRetention[new ExcretaEquationsParameters.CorrectionParameterKey((int)energyBalanceInputs.CattleType, (int)energyBalanceInputs.CattleBreed)]);

        var proteinRetentionFullTerm = 0.0;
        var dailyNitrogenRetentionForPregnancy = 0.0;
        var proteinContentWeightGain = 0.0;

        switch(energyBalanceInputs.CattleType)
        {
            case BeefCattleType.Cows:
            case BeefCattleType.Heifersforbreeding:
                proteinRetentionFullTerm = ExcretaEquations.ProteinRetentionInFullTerm(ExcretaEquationsParameters.AverageGestationPeriod[(int)energyBalanceInputs.CattleBreed]);
                dailyNitrogenRetentionForPregnancy = ExcretaEquations.AverageDairyNitrogenRetentionForPregnancyBeef(proteinRetentionFullTerm, energyBalanceInputs.PercentCattleTypeGestating, energyBalanceInputs.BirthWeight, ExcretaEquationsParameters.ProteinToNRatio);
                var netProteinContentOfLiveWeightGainInLactatingRuminants = ExcretaEquationsParameters.NetProteinContentOfLiveWeightGainInLactatingRuminantsCoefficient * energyBalanceInputs.MeanGrowthRate;
                proteinContentWeightGain = ExcretaEquations.ProteinContentWeightGain(energyBalanceInputs.LactationLength, netProteinContentOfLiveWeightGain, energyBalanceInputs.PercentCattleTypeLactating, netProteinContentOfLiveWeightGainInLactatingRuminants);
                break;
            default:
                proteinContentWeightGain = netProteinContentOfLiveWeightGain;
                break;

        }

        var nitrogenRetentionInLiveWeightGain = ExcretaEquations.NitrogenRetentionInLiveWeightGainBeef(proteinContentWeightGain, ExcretaEquationsParameters.ProteinToNRatio);

        var nitrogenDensity = ExcretaEquations.NitrogenDensityInWholeDiet(energyBalanceInputs.DietCrudeProteinContent, ExcretaEquationsParameters.ProteinToNRatio);
        var dailyNitrogenIntake = ExcretaEquations.NitrogenIntakeBeef(nitrogenDensity, dailyDMI);
        var dailyNitrogenExcretion = ExcretaEquations.NitrogenExcretionBeef(dailyNitrogenIntake, nitrogenRetentionInLiveWeightGain, nitrogenRetentionInMilk, dailyNitrogenRetentionForPregnancy);

        energyBalanceOutputs.DailyGEIntake = geIntakeDaily;
        energyBalanceOutputs.DailyCPIntake = EntericEquations.CPIntake(dailyDMI, energyBalanceInputs.DietCrudeProteinContent);
        energyBalanceOutputs.DailyDryMatterIntake = dailyDMI;
        energyBalanceOutputs.DailyNIntake = dailyNitrogenIntake;
        energyBalanceOutputs.DailyNExcretion = dailyNitrogenExcretion;
        energyBalanceOutputs.DailyMEMaintenance = meMaintenance;
        energyBalanceOutputs.DailyMELiveweightGain = mEToSupportGrowth;
        energyBalanceOutputs.DailyMEPregnancy = mEPregnancy;
        energyBalanceOutputs.DailyMELactation = mELactation;

        return energyBalanceOutputs;
    }

    #endregion

}
