using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.NonGHG;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// The non-GHG emissions from crop and grassland class
/// </summary>
public class NonGHGEmissions
{
    /// <summary>
    /// Non GHG emissions from crop production calculation
    /// </summary>
    /// <param name="cropType">UK GHG AEIA Crop type - used to return emission factors</param>
    /// <returns>NonGHGCropGrassEmissions object with kg NMVOC, KG PM2.5 + kg PM10</returns>
    public static NonGHGCropGrassEmissions NonGHG_Crop(CropType cropType)
    {
        NonGHGCropGrassEmissions myEmissions = new();
        myEmissions.InitialiseNonGHGCropGrassEmissions();

        myEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = CropLookup.RetrieveNMVOC(cropType);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = CropLookup.RetrieveCultivationPM2_5(cropType) + CropLookup.RetrieveCleaningPM2_5(cropType) + CropLookup.RetrieveDryingPM2_5(cropType) + CropLookup.RetrieveHarvestingPM2_5(cropType);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = CropLookup.RetrieveCultivationPM10(cropType) + CropLookup.RetrieveCleaningPM10(cropType) + CropLookup.RetrieveDryingPM10(cropType) + CropLookup.RetrieveHarvestingPM10(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = CropLookup.RetrieveCultivationPM2_5(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = CropLookup.RetrieveHarvestingPM2_5(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = CropLookup.RetrieveCleaningPM2_5(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = CropLookup.RetrieveDryingPM2_5(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = CropLookup.RetrieveCultivationPM10(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = CropLookup.RetrieveHarvestingPM10(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = CropLookup.RetrieveCleaningPM10(cropType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = CropLookup.RetrieveDryingPM10(cropType);

        return myEmissions;
    }

    /// <summary>
    /// Non GHG emissions from grass production calculation
    /// </summary>
    /// <param name="grassType">UK GHG AEIA grass type - used to return emission factors</param>
    /// <returns>NonGHGCropGrassEmissions object with kg NMVOC, KG PM2.5 + kg PM10</returns>
    public static NonGHGCropGrassEmissions NonGHG_Grass(GrassType grassType)
    {
        NonGHGCropGrassEmissions myEmissions = new();

        myEmissions.nonGHGEmissions[Enumerators.Enumerators.NonGHG_Emissions.NMVOC].Value = GrassLookup.Instance.EF_NMVOC((int)grassType);
        myEmissions.nonGHGEmissions[Enumerators.Enumerators.NonGHG_Emissions.PM2_5].Value = GrassLookup.Instance.EF_PM2_5_Cultivation((int)grassType) + GrassLookup.Instance.EF_PM2_5_Harvesting((int)grassType)
            + GrassLookup.Instance.EF_PM2_5_Cleaning((int)grassType) + GrassLookup.Instance.EF_PM2_5_Drying((int)grassType);
        myEmissions.nonGHGEmissions[Enumerators.Enumerators.NonGHG_Emissions.PM10].Value = GrassLookup.Instance.EF_PM10_Cultivation((int)grassType) + GrassLookup.Instance.EF_PM10_Harvesting((int)grassType) +
            GrassLookup.Instance.EF_PM10_Cleaning((int)grassType) + GrassLookup.Instance.EF_PM10_Drying((int)grassType);
        //No TSP in grass
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = GrassLookup.Instance.EF_PM2_5_Cultivation((int)grassType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = GrassLookup.Instance.EF_PM2_5_Harvesting((int)grassType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = GrassLookup.Instance.EF_PM2_5_Cleaning((int)grassType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = GrassLookup.Instance.EF_PM2_5_Drying((int)grassType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = GrassLookup.Instance.EF_PM10_Cultivation((int)grassType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = GrassLookup.Instance.EF_PM10_Harvesting((int)grassType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = GrassLookup.Instance.EF_PM10_Cleaning((int)grassType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = GrassLookup.Instance.EF_PM10_Drying((int)grassType);
        return myEmissions;
    }

    /// <summary>
    /// Non GHG emissions from Cattle
    /// </summary>
    /// <param name="sector">Sector enumerator (dairy or beef)</param>
    /// <param name="animalType">Animla type enumerator as integer</param>
    /// <param name="ammoniaHousing">Ammonia emissions from housing</param>
    /// <param name="ammoniaStorage">Ammonia emissions from storage</param>
    /// <param name="ammoniaSpreading">Ammonia emissions from spreading</param>
    /// <param name="grossEnergyIntake">Gross energy intake (MJ)</param>
    /// <param name="percentTimeHousingYards">Percentage of total time spend on yards and in housing</param>
    /// <param name="monthly">boolean indicating if monthly or annual calculations required</param>
    /// <returns>Non GHG Livestock emissions object</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static NonGHGLivestockEmissions NonGHG_Cattle(Sector sector, int animalType, double ammoniaHousing, double ammoniaStorage, double ammoniaSpreading, double grossEnergyIntake, double percentTimeHousingYards, bool monthly)
    {
        NonGHGLivestockEmissions myEmissions = new();
        myEmissions.InitialiseNonGHGLivestockEmissions();

        if(monthly)
        {
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = HelperFunctions.DailyToMonthlyConverter(grossEnergyIntake) * (1.0 - (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion)) *
                ExcretaLookup.RetrieveNMVOCGrazing(sector, animalType);
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = HelperFunctions.DailyToMonthlyConverter(grossEnergyIntake) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
                ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType) * ExcretaLookup.FractionSilageStore(sector, animalType);
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = HelperFunctions.DailyToMonthlyConverter(grossEnergyIntake) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
                ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType);
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = HelperFunctions.DailyToMonthlyConverter(grossEnergyIntake) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
                ExcretaLookup.RetrieveNMVOCHouse(sector, animalType);
        }
        else
        {
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = HelperFunctions.DailyToAnnualConverter(grossEnergyIntake) * (1.0 - (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion)) *
                ExcretaLookup.RetrieveNMVOCGrazing(sector, animalType);
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = HelperFunctions.DailyToAnnualConverter(grossEnergyIntake) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
                ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType) * ExcretaLookup.FractionSilageStore(sector, animalType);
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = HelperFunctions.DailyToAnnualConverter(grossEnergyIntake) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
                ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType);
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = HelperFunctions.DailyToAnnualConverter(grossEnergyIntake) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
                ExcretaLookup.RetrieveNMVOCHouse(sector, animalType);
        }

        double multiplierStore = ammoniaStorage / ammoniaHousing;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * multiplierStore;

        double multiplierSpread = ammoniaSpreading / ammoniaHousing;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * multiplierSpread;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value +
             myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value +
              myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = ExcretaLookup.RetrievePM2_5(sector, animalType) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = ExcretaLookup.RetrievePM10(sector, animalType) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = ExcretaLookup.RetrieveTSP(sector, animalType) * (percentTimeHousingYards * HelperFunctions.Percent_to_Proportion);

        if(monthly)
        {
            myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value /= HelperFunctions.MonthsInYear;
            myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value /= HelperFunctions.MonthsInYear;
            myEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value /= HelperFunctions.MonthsInYear;
        }

        return myEmissions;
    }
    /// <summary>
    /// Non-GHG emissions from pigs, poultry and minor livestock
    /// </summary>
    /// <param name="sector">Sector enumerator (pigs, poultyr or minor livestock)</param>
    /// <param name="animalType">Animla type enumerator as integer</param>
    /// <param name="ammoniaHousing">Ammonia emissions from housing</param>
    /// <param name="ammoniaStorage">Ammonia emissions from storage</param>
    /// <param name="ammoniaSpreading">Ammonia e.missions from spreading</param>
    /// <param name="volatileSolidsExcretionAnnual">Annual volatile solids excretion (kg)</param>
    /// <param name="houseDays">Days spent in housing</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static NonGHGLivestockEmissions NonGHG_PigsPoultryMinorLivestock(Sector sector, int animalType, double ammoniaHousing, double ammoniaStorage, double ammoniaSpreading, double volatileSolidsExcretionAnnual, double houseDays)
    {
        NonGHGLivestockEmissions myEmissions = new();
        myEmissions.InitialiseNonGHGLivestockEmissions();

        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = volatileSolidsExcretionAnnual * (1.0 - (houseDays / HelperFunctions.Days_per_year)) *
            ExcretaLookup.RetrieveNMVOCGrazing(sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = volatileSolidsExcretionAnnual * (houseDays / HelperFunctions.Days_per_year) *
            ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType) * ExcretaLookup.FractionSilageStore(sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = volatileSolidsExcretionAnnual * (houseDays / HelperFunctions.Days_per_year) *
            ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = volatileSolidsExcretionAnnual * (houseDays / HelperFunctions.Days_per_year) *
            ExcretaLookup.RetrieveNMVOCHouse(sector, animalType);

        double multiplierStore = ammoniaStorage / ammoniaHousing;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * multiplierStore;

        double multiplierSpread = ammoniaSpreading / ammoniaHousing;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * multiplierSpread;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value +
             myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value +
              myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = ExcretaLookup.RetrievePM2_5(sector, animalType) * (houseDays / HelperFunctions.Days_per_year);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = ExcretaLookup.RetrievePM10(sector, animalType) * (houseDays / HelperFunctions.Days_per_year);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = ExcretaLookup.RetrieveTSP(sector, animalType) * (houseDays / HelperFunctions.Days_per_year);

        return myEmissions;
    }
    /// <summary>
    /// Non-GHG EMissions from sheep
    /// </summary>
    /// <param name="sector">Sector enumerator (Sheep)</param>
    /// <param name="animalType">Animla type enumerator as integer</param>
    /// <param name="ammoniaHousing">Ammonia emissions from housing</param>
    /// <param name="ammoniaStorage">Ammonia emissions from storage</param>
    /// <param name="ammoniaSpreading">Ammonia emissions from spreading</param>
    /// <param name="volatileSolidsExcretion_House">Volatile solids excreetion in thousing (kg)</param>
    /// <param name="volatileSolidsExcretion_Field">Voaltile solids excretion at graxing (kg)</param>
    /// <param name="houseDays">days housed</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static NonGHGLivestockEmissions NonGHG_Sheep(Sector sector, int animalType, double ammoniaHousing, double ammoniaStorage, double ammoniaSpreading, double volatileSolidsExcretion_House,
        double volatileSolidsExcretion_Field, double houseDays)
    {
        NonGHGLivestockEmissions myEmissions = new();
        myEmissions.InitialiseNonGHGLivestockEmissions();

        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = volatileSolidsExcretion_Field *
            ExcretaLookup.RetrieveNMVOCGrazing(sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = volatileSolidsExcretion_House *
            ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType) * ExcretaLookup.FractionSilageStore(sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = volatileSolidsExcretion_House *
            ExcretaLookup.RetrieveNMVOCSilage(sector, animalType) * ExcretaLookup.FractionSilage(sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = volatileSolidsExcretion_House *
            ExcretaLookup.RetrieveNMVOCHouse(sector, animalType);

        double multiplierStore = ammoniaStorage / ammoniaHousing;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * multiplierStore;

        double multiplierSpread = ammoniaSpreading / ammoniaHousing;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * multiplierSpread;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value +
             myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value +
              myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = ExcretaLookup.RetrievePM2_5(sector, animalType) * (houseDays / HelperFunctions.Days_per_year);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = ExcretaLookup.RetrievePM10(sector, animalType) * (houseDays / HelperFunctions.Days_per_year);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = ExcretaLookup.RetrieveTSP(sector, animalType) * (houseDays / HelperFunctions.Days_per_year);

        return myEmissions;
    }
}
