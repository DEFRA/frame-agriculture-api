using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta;

public static class ExcretaEquations
{
    #region Pigs, Poultry and Minor Livestock
    /// <summary>
    /// Scales exreta for free range poultry.
    /// </summary>
    /// <param name="freeRangeMultiplier"></param>
    /// <param name="proportionInArea"></param>
    /// <returns></returns>
    public static double CalculatePoultryFreeRange(double freeRangeMultiplier, double proportionInArea)
    {
        return freeRangeMultiplier * proportionInArea;
    }
    /// <summary>
    /// Retrieves the default initial nitrogen excretion for the specified animal type and sector.
    /// </summary>
    /// <param name="sector"></param>
    /// <param name="animalType"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double InitialNitrogen(Sector sector, int animalType)
    {
        if(Sector.MinorLivestock != sector && Sector.Pigs != sector && Sector.Poultry != sector)
        {
            throw new CustomAppException("The Sector entered is invalid");
        }

        return ExcretaLookup.GetNitrogenExcretion(sector, animalType);
    }
    /// <summary>
    /// Calculates the percentage of TAN in a location. Related equations: MM_13
    /// </summary>
    /// <param name="nitrogenExcreted"></param>
    /// <param name="percentageTAN"></param>
    /// <returns></returns>
    public static double CalculateTAN(double nitrogenExcreted, double percentageTAN)
    {
        return nitrogenExcreted * percentageTAN * HelperFunctions.Percent_to_Proportion;
    }
    /// <summary>
    /// Retrieve the default initial volatile solid excretion for the specified animal type and sector.
    /// </summary>
    /// <param name="sector"></param>
    /// <param name="animalType"></param>
    /// <returns></returns>
    public static double InitialVolatileSolid(Sector sector, int animalType)
    {
        double volatileSolidForOneDay = ExcretaLookup.GetExcretaVolatileSolids((Sector)sector, animalType);

        return sector switch
        {
            Sector.Pigs or Sector.MinorLivestock or Sector.Poultry => HelperFunctions.Days_per_year * volatileSolidForOneDay,
            _ => 0,
        };
    }

    #endregion

    #region Intake and Methane
    /// <summary>
    /// IS_3 This equation calculates the enteric methane production of a sheep (ECH4)
    /// </summary>
    /// <param name="fieldEntericMethane"></param>
    /// <param name="houseEntericMethane"></param>
    /// <param name="fieldDryMatterIntake"></param>
    /// <param name="houseDryMatterIntake"></param>
    /// <param name="fieldDays"></param>
    /// <param name="houseDays"></param>
    /// <returns></returns>
    public static double SheepEntericMethane(double fieldEntericMethane, double houseEntericMethane, double fieldDryMatterIntake, double houseDryMatterIntake, double fieldDays, double houseDays)
    {
        double entericMethaneEmission = fieldEntericMethane + houseEntericMethane;
        double tmp_AverageDryMatterIntake = (fieldDryMatterIntake + houseDryMatterIntake) / (houseDays + fieldDays);
        double tmp_RumenBypassScalar = entericMethaneEmission / ((5.16 + 12.4 * tmp_AverageDryMatterIntake) * (fieldDays + houseDays) / 1000.0);
        double tmp_EntericMethaneEmission = Math.Max(0.0, 5.16 + 12.4 * tmp_AverageDryMatterIntake) * (houseDays + fieldDays) / 1000.0 * tmp_RumenBypassScalar;

        return tmp_EntericMethaneEmission;
    }

    /// <summary>
    /// Calculates the CH4 emission for outdoor poultry. Related equations: MM_7
    /// </summary>
    /// <param name="animalType"></param>
    /// <param name="outdoorVolatileSolids"></param>
    /// <returns></returns>
    public static double CH4EmissionOutdoorPoultry(PoultryType animalType, double outdoorVolatileSolids)
    {
        //Todo this should always be zero
        return outdoorVolatileSolids * MethaneLookup.RetrieveB0(Sector.Poultry, (int)animalType) * HelperFunctions.Percent_to_Proportion * ExcretaEquationsParameters.CH4m3ToKg;
    }
    /// <summary>
    /// Returns CH4 emission from enteric fermentation in the livestock
    /// </summary>
    /// <param name="sector"></param>
    /// <param name="animalType"></param>
    /// <returns></returns>
    public static double CH4Emission(Sector sector, int animalType)
    {
        if(Sector.Pigs != sector && Sector.MinorLivestock != sector && Sector.Poultry != sector)
        {
            throw new CustomAppException("The Sector is invalid");
        }

        return EntericMethaneLookup.RetrieveCH4(sector, animalType);
    }
    /// <summary>
    /// BN_1
    /// </summary>
    /// <param name="meanGrowthRate">from BW_5</param>
    /// <param name="meanLiveWeight">from BW_4</param>
    /// <param name="correctionFactorForNRetention"></param>
    /// <returns></returns>
    public static double NetProteinContentOfLiveWeightGain(double meanGrowthRate, double meanLiveWeight, double correctionFactorForNRetention)
    {
        return meanGrowthRate * correctionFactorForNRetention * (168.07 - (0.16869 * meanLiveWeight) + (0.0001633 * Math.Pow(meanLiveWeight, 2))) * (1.12 - (0.1223 * meanGrowthRate));
    }
    /// <summary>
    /// BN_2
    /// </summary>
    /// <param name="gestationLength"></param>
    /// <returns></returns>
    public static double ProteinRetentionInFullTerm(double gestationLength)
    {
        return Math.Pow(10, 3.707 - 5.698 * Math.Exp(-0.00262 * gestationLength));
    }
    /// <summary>
    /// BM_1 This equation calculates enteric methane (CH4enteric) emitted from beef cattle in units of kg CH4 per unit time(month).
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="totalDMI"></param>
    /// <returns></returns>
    public static double CalculateEntericMethaneBeef(BeefCattleType cattleType, double totalDMI)
    {

        if(cattleType == BeefCattleType.Cows)
        {
            return (((totalDMI * 15.7 + 88.6) * HelperFunctions.Days_per_year) / 12) / 1000;
        }

        return ((((totalDMI * 17.6) + 45.9) * HelperFunctions.Days_per_year) / 12) / 1000;
    }
    /// <summary>
    /// DM_29 Enteric Methane
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="totalDMI"></param>
    /// <returns></returns>
    public static double CalculateEntericMethaneDairy(DairyCattle cattleType, double totalDMI)
    {

        if(cattleType == DairyCattle.DC4_DairyCows)
        {
            return (((totalDMI * 15.815 + 88.6002) * HelperFunctions.Days_per_year) / 12) / 1000;
        }

        return ((((totalDMI * 17.5653) + 45.8688) * HelperFunctions.Days_per_year) / 12) / 1000;
    }
    /// <summary>
    /// DN_1 Only used for DC4 cattle type
    /// </summary>
    /// <param name="averageGestationPeriodForDairyCow"></param>
    /// <returns></returns>
    public static double ProteinRetentionInDevelopingFoetus(double averageGestationPeriodForDairyCow)
    {
        return Math.Pow(10, (3.707 - (5.698 * Math.Exp(-0.00262 * averageGestationPeriodForDairyCow))));
    }
    /// <summary>
    /// BN_3
    /// </summary>
    /// <param name="milkYield">from BM_8</param>
    /// <param name="milkProteinContent"></param>
    /// <param name="milkProteinToNRatio"></param>
    /// <param name="ScalingFactorOfCattleLactating"></param>
    /// <returns></returns>
    public static double NitrogenRetentionInMilkBeef(double milkYield, double milkProteinContent, double milkProteinToNRatio, double ScalingFactorOfCattleLactating)
    {
        return ((milkYield * milkProteinContent) / milkProteinToNRatio) * (ScalingFactorOfCattleLactating * HelperFunctions.Percent_to_Proportion);
    }
    /// <summary>
    /// DN_2 Only used for DC4 cattle type
    /// </summary>
    /// <param name="averageDailyMilkYield"></param>
    /// <param name="milkProteinContent"></param>
    /// <param name="milkProteinToNRatio"></param>
    /// <returns></returns>
    public static double NitrogenRetentionInMilkDairy(double averageDailyMilkYield, double milkProteinContent, double milkProteinToNRatio)
    {
        return (averageDailyMilkYield * 1000 * (milkProteinContent * HelperFunctions.Percent_to_Proportion)) / milkProteinToNRatio;
    }
    /// <summary>
    /// DN_3
    /// </summary>
    /// <param name="growthRate">from DW_5</param>
    /// <param name="netProteinContentOfLiveWeightGain"></param>
    /// <param name="proteinToNRatio"></param>
    /// <returns></returns>
    public static double NitrogenRetentionInLiveWeightGainDairy(double growthRate, double netProteinContentOfLiveWeightGain, double proteinToNRatio)
    {
        return (netProteinContentOfLiveWeightGain * growthRate) / proteinToNRatio;
    }
    /// <summary>
    /// DN_4 Only used for DC4 and DC3
    /// </summary>
    /// <param name="birthWeight">from DW_1</param>
    /// <param name="proteinRetentionInDevelopingFoetus">from DN_1</param>
    /// <param name="proteinToNRatio"></param>
    /// <param name="calvingInterval"></param>
    /// <param name="averageGestationPeriodDairyCow"></param>
    /// <returns></returns>
    public static double AverageDailyNitrogenRetentionForPregnancyDairy(DairyCattle cattleType, double birthWeight, double proteinRetentionInDevelopingFoetus, double proteinToNRatio,
        double calvingInterval, double averageGestationPeriodDairyCow)
    {
        return cattleType switch
        {
            DairyCattle.DC4_DairyCows => ((0.025 * birthWeight * proteinRetentionInDevelopingFoetus * 1000) / calvingInterval) / proteinToNRatio,
            DairyCattle.DC3_DairyInCalfHeifers => ((0.025 * birthWeight * proteinRetentionInDevelopingFoetus * 1000) / averageGestationPeriodDairyCow) / proteinToNRatio,
            _ => throw new CustomAppException("The cattle type is not valid"),
        };
    }
    /// <summary>
    /// BN_4
    /// </summary>
    /// <param name="proteinRetentionForFoetus">from BN_2</param>
    /// <param name="percentageOfCattleGestating"></param>
    /// <param name="liveWeightAtBirth"></param>
    /// <param name="proteinToNRatio"></param>
    /// <returns></returns>
    public static double AverageDairyNitrogenRetentionForPregnancyBeef(double proteinRetentionForFoetus, double percentageOfCattleGestating,
        double liveWeightAtBirth, double proteinToNRatio)
    {
        return ((liveWeightAtBirth / 40 * 1000) * ((proteinRetentionForFoetus / 365) / proteinToNRatio)) * (percentageOfCattleGestating / 100);
    }

    /// <summary>
    /// BN_5 only for cows and heifers called Wpn
    /// </summary>
    /// <param name="lactationLength">from BM_9</param>
    /// <param name="netProteinContentOfLiveWeightGain">from BN_1 wpnnl</param>
    /// <param name="scalingFactorOfCattleLactating"></param>
    /// <param name="netProteinContentOfLiveWeightGainInLactatingRuminants">called Wpnl</param>
    /// <returns></returns>
    public static double ProteinContentWeightGain(double lactationLength, double netProteinContentOfLiveWeightGain,
        double scalingFactorOfCattleLactating, double netProteinContentOfLiveWeightGainInLactatingRuminants)
    {
        return (((lactationLength * scalingFactorOfCattleLactating * HelperFunctions.Percent_to_Proportion) / HelperFunctions.Days_per_year) * netProteinContentOfLiveWeightGainInLactatingRuminants) +
                                ((1 - ((lactationLength * scalingFactorOfCattleLactating * HelperFunctions.Percent_to_Proportion) / HelperFunctions.Days_per_year)) * netProteinContentOfLiveWeightGain);
    }
    /// <summary>
    /// BN_6 called Ng or Nga
    /// </summary>
    /// <param name="proteinContentInWeightGain">from BN_5</param>
    /// <param name="proteinToNRatio">set at 6.25</param>
    /// <returns></returns>
    public static double NitrogenRetentionInLiveWeightGainBeef(double proteinContentInWeightGain, double proteinToNRatio)
    {
        return proteinContentInWeightGain / proteinToNRatio;
    }
    /// <summary>
    /// BN_7
    /// </summary>
    /// <param name="cPWholeDiet"></param>
    /// <param name="proteinToNRatio">set at 6.25</param>
    /// <returns></returns>
    public static double NitrogenDensityInWholeDiet(double cPWholeDiet, double proteinToNRatio)
    {
        return cPWholeDiet / proteinToNRatio;
    }
    /// <summary>
    /// BN_8
    /// </summary>
    /// <param name="nitrogenDensityInWholeDiet">BN_7</param>
    /// <param name="totalDMI">BM_23</param>
    /// <returns></returns>
    public static double NitrogenIntakeBeef(double nitrogenDensityInWholeDiet, double totalDMI)
    {
        return nitrogenDensityInWholeDiet * totalDMI;
    }
    /// <summary>
    /// BN_9
    /// </summary>
    /// <param name="nitrogenIntake">from BN_8</param>
    /// <param name="nitrogenRetentionInLiveWeightGain">from BN_6</param>
    /// <param name="nitrogenRetentionInMilk">from BN_3</param>
    /// <param name="averageDailyNitrogenRetentionForPregnancy">from BN_4</param>
    /// <returns></returns>
    public static double NitrogenExcretionBeef(double nitrogenIntake, double nitrogenRetentionInLiveWeightGain,
        double nitrogenRetentionInMilk, double averageDailyNitrogenRetentionForPregnancy)
    {
        return nitrogenIntake - nitrogenRetentionInMilk - nitrogenRetentionInLiveWeightGain - averageDailyNitrogenRetentionForPregnancy;
    }
    /// <summary>
    /// BN_10
    /// </summary>
    /// <param name="nitrogenIntake">from BN_8</param>
    /// <param name="totalNitrogenExcretion">from BN_9</param>
    /// <returns></returns>
    public static double ExcretedNAsUrine(double nitrogenIntake, double totalNitrogenExcretion)
    {
        return ((((nitrogenIntake * 0.353) + 16.3) / ((nitrogenIntake * 0.638) + 33.9)) * totalNitrogenExcretion) * (HelperFunctions.Days_per_year / 12000);
    }
    /// <summary>
    /// BN_11
    /// </summary>
    /// <param name="excretedNAsUrine">from BN_10</param>
    /// <param name="totalNitrogenExcretion">from BN_9</param>
    /// <returns></returns>
    public static double ExcretedNAsFaeces(double monthlyExcretedNAsUrine, double totalNitrogenExcretion)
    {
        return (totalNitrogenExcretion * (HelperFunctions.Days_per_year / 12000)) - monthlyExcretedNAsUrine;
    }
    /// <summary>
    /// DN_5
    /// </summary>
    /// <param name="totalDMI">from DM_26</param>
    /// <param name="weightedAverageCPContentOfForage">from DM_6</param>
    /// <param name="concentrateIntake">from DM_2 in the case of heifers this may not be from an eq and may be inputted</param>
    /// <param name="proteinToNRatio"></param>
    /// <param name="concentrateCPContent"></param>
    /// <returns></returns>
    public static double NitrogenIntakeDairy(double totalDMI, double weightedAverageCPContentOfForage, double concentrateIntake,
        double proteinToNRatio, double concentrateCPContent)
    {
        return ((concentrateIntake * concentrateCPContent) + ((totalDMI - concentrateIntake) * weightedAverageCPContentOfForage)) / proteinToNRatio;
    }
    /// <summary>
    /// BN_9
    /// </summary>
    /// <param name="nitrogenIntake">from BN_8</param>
    /// <param name="nitrogenRetentionInLiveWeightGain">from BN_6</param>
    /// <param name="nitrogenRetentionInMilk">from BN_3</param>
    /// <param name="averageDailyNitrogenRetentionForPregnancy">from BN_4</param>
    /// <returns></returns>
    public static double TotalNitrogenExcretionBeef(double nitrogenIntake, double nitrogenRetentionInLiveWeightGain,
       double nitrogenRetentionInMilk, double averageDailyNitrogenRetentionForPregnancy)
    {
        return nitrogenIntake - nitrogenRetentionInMilk - nitrogenRetentionInLiveWeightGain - averageDailyNitrogenRetentionForPregnancy;
    }
    /// <summary>
    /// DN_6
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="nitrogenIntake">from DN_5</param>
    /// <param name="nitrogenRetentionInMilk">from DN_2</param>
    /// <param name="nitrogenRetentionInLiveWeightGain">from DN_3</param>
    /// <param name="averageDailyNitrogenRetentionForPregnancy">from DN_4</param>
    /// <returns></returns>
    public static double NitrogenExcretionDairy(DairyCattle cattleType, double nitrogenIntake, double nitrogenRetentionInMilk, double nitrogenRetentionInLiveWeightGain,
        double nitrogenRetentionForPregnancy)
    {
        return (nitrogenIntake - (nitrogenRetentionForPregnancy + nitrogenRetentionInLiveWeightGain + nitrogenRetentionInMilk)) * HelperFunctions.Days_per_year / 12000;
    }
    /// <summary>
    /// DN_7
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="nitrogenIntake">DN_5</param>
    /// <returns></returns>
    public static double TotalNExcretion(DairyCattle cattleType, double nitrogenIntake)
    {
        if(cattleType == DairyCattle.DC4_DairyCows)
        {
            return 20.3 + (0.654 * nitrogenIntake);
        }

        return 15.1 + (0.828 * nitrogenIntake);
    }
    /// <summary>
    /// DN_8
    /// </summary>
    /// <param name="cattleType"></param>
    /// <param name="nitrogenIntake">from DN_5</param>
    /// <returns></returns>
    public static double UrinaryNExcretion(DairyCattle cattleType, double nitrogenIntake)
    {
        if(cattleType == DairyCattle.DC4_DairyCows)
        {
            return 12.0 + (0.333 * nitrogenIntake);
        }

        return 14.3 + (0.510 * nitrogenIntake);
    }

    /// <summary>
    /// DN_9
    /// </summary>
    /// <param name="totalNExcretion">from DN_7</param>
    /// <param name="urinaryNExcreted">from D</param>
    /// <returns></returns>
    public static double PercentOfTotalNExcretionAsUrinaryN(DairyCattle cattleType, double nitrogenIntake)
    {
        return (UrinaryNExcretion(cattleType, nitrogenIntake) / TotalNExcretion(cattleType, nitrogenIntake)) * 100;
    }
    /// <summary>
    /// DN_10
    /// </summary>
    /// <param name="nitrogenExcretion">from DN_6</param>
    /// <param name="percentOfTotalNAsExcretionUrinary">from DN_9</param>
    /// <returns></returns>
    public static double NitrogenExcretionAsUrineDairy(double nitrogenExcretion, double percentOfTotalNAsExcretionUrinary)
    {
        return nitrogenExcretion * (percentOfTotalNAsExcretionUrinary * HelperFunctions.Percent_to_Proportion);
    }
    /// <summary>
    /// DN_11
    /// </summary>
    /// <param name="nitrogenExcretion">from DN_6</param>
    /// <param name="nitrogenExcretionAsUrine">from DN_10</param>
    /// <returns></returns>
    public static double NitrogenExcretionAsFaeces(double nitrogenExcretion, double nitrogenExcretionAsUrine)
    {
        return nitrogenExcretion - nitrogenExcretionAsUrine;
    }

    /// <summary>
    /// DN_12 and BN_12
    /// </summary>
    /// <param name="nitrogenExcretionAsUrine">from DN_10</param>
    /// <param name="percentageSpentAtLocation"></param>
    /// <returns></returns>
    public static double UrineNAtGrazingYardingHousing(double nitrogenExcretionAsUrine, double percentageSpentAtLocation)
    {
        return (percentageSpentAtLocation * HelperFunctions.Percent_to_Proportion) * nitrogenExcretionAsUrine;
    }

    /// <summary>
    /// DN_13 and BN_13
    /// </summary>
    /// <param name="nitrogenExcretionAsFaeces"></param>
    /// <param name="percentageSpentAtLocation"></param>
    /// <returns></returns>
    public static double FaecalNAtGrazingYardingHousing(double nitrogenExcretionAsFaeces, double percentageSpentAtLocation)
    {
        return (percentageSpentAtLocation * HelperFunctions.Percent_to_Proportion) * nitrogenExcretionAsFaeces;
    }
    /// <summary>
    /// generic version of DN_14 and BN_12
    /// </summary>
    /// <param name="gEIntake">from DM_28</param>
    /// <param name="totalMERequirement">from DM_24</param>
    /// <param name="monthlyDMI">from DM_26</param>
    /// <param name="manureAshContent"></param>
    /// <returns></returns>
    public static double DailyVSExcretion(double gEIntakePerDay, double totalMERequirement, double monthlyDMI, double manureAshContent)
    {
        return ((gEIntakePerDay * (1.0 - (totalMERequirement / gEIntakePerDay))) * ((1.0 - manureAshContent) /
                            (gEIntakePerDay / monthlyDMI)) * 365.0) / 12.0;
    }
    /// <summary>
    /// DN_15 and BN_15
    /// </summary>
    /// <param name="vSExcretion">from DN_14</param>
    /// <param name="percentageSpentAtLocation"></param>
    /// <returns></returns>
    public static double VSAtGrazingYardingHousing(double vSExcretion, double percentageSpentAtLocation)
    {
        return (percentageSpentAtLocation * HelperFunctions.Percent_to_Proportion) * vSExcretion;
    }

    #endregion

    #region Manure Mass and Volume

    /// <summary>
    /// DV_1
    /// </summary>
    /// <param name="dryMatterDigestibilityWholeDiet">from DM_15</param>
    /// <returns></returns>
    public static double FaecesDryMatterProduction(double dryMatterDigestibilityWholeDiet)
    {
        return 1 - (dryMatterDigestibilityWholeDiet * HelperFunctions.Percent_to_Proportion);
    }

    /// <summary>
    /// DV_2
    /// </summary>
    /// <param name="cattleFaecesDryMatterProduction">from DV_1</param>
    /// <param name="monthlyDMI">from DM_26 convert from daily to monthly</param>
    /// <param name="weightedAverageDryMatterContent"></param>
    /// <returns></returns>
    public static double MassOfFaeces(double cattleFaecesDryMatterProduction, double monthlyDMI,
        double weightedAverageDryMatterContent)
    {
        return monthlyDMI * cattleFaecesDryMatterProduction * (1 / (0.001 * weightedAverageDryMatterContent));
    }

    /// <summary>
    /// DV_3
    /// </summary>
    /// <param name="cattleFaecesDryMatterProduction">from DV_1</param>
    /// <param name="totalDMI">from DM_26</param>
    /// <returns></returns>
    public static double VolumeOfFaeces(double cattleFaecesDryMatterProduction, double totalDMI)
    {
        return (totalDMI * cattleFaecesDryMatterProduction) * (1 / 0.160);
    }

    /// <summary>
    /// DV_4
    /// </summary>
    /// <param name="dryMatterContentWholeDiet">from DM_16</param>
    /// <returns></returns>
    public static double UrineDryMatterProduction(double dryMatterContentWholeDiet)
    {
        return 1.12 * (1.139 - (dryMatterContentWholeDiet - 300) * 0.00046);
    }

    /// <summary>
    /// DV_5
    /// </summary>
    /// <param name="totalDMI">DM_26</param>
    /// <param name="urineDryMatterProduction">DV_4</param>
    /// <returns></returns>
    public static double MassOfUrine(double totalDMI, double urineDryMatterProduction)
    {
        return totalDMI * urineDryMatterProduction;
    }

    /// <summary>
    /// DV_6
    /// </summary>
    /// <param name="massOfUrineProducedDaily">from DV_5</param>
    /// <returns></returns>
    public static double VolumeOfUrine(double massOfUrineProducedDaily)
    {
        return massOfUrineProducedDaily / 1.03;
    }

    /// <summary>
    /// DV_7
    /// </summary>
    /// <param name="massOfFaeces">from DV_2</param>
    /// <param name="massOfUrine">from DV_5</param>
    /// <param name="percentageOfTimeAtLocation"></param>
    /// <returns></returns>
    public static double ExcretaMassAtStage(double massOfFaeces, double massOfUrine,
        double percentageOfTimeAtLocation)
    {
        return HelperFunctions.DailyToMonthlyConverter((massOfFaeces + massOfUrine) * percentageOfTimeAtLocation / 100);
    }

    /// <summary>
    /// DV_7
    /// </summary>
    /// <param name="dailyVolumeOfFaeces">from DV_3</param>
    /// <param name="dailyVolumeOfUrine">from DV_6</param>
    /// <param name="percentageOfTimeAtLocation"></param>
    /// <returns></returns>
    public static double ExcretaVolumeAtStage(double dailyVolumeOfFaeces,
        double dailyVolumeOfUrine, double percentageOfTimeAtLocation)
    {
        return HelperFunctions.DailyToMonthlyConverter((dailyVolumeOfFaeces + dailyVolumeOfUrine) * percentageOfTimeAtLocation / 100);
    }

    #endregion

}
