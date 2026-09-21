using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.NonGHG;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// The non-GHG emissions from crop and grassland class
/// </summary>
public class NonGHGCropGrassLivestock
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
        myEmissions.InitialiseNonGHGCropGrassEmissions();

        //****TODO **** code to retrieve EFs from Singleton or database
        //*** FOr now using defualt holders

        double emissionFactorNMVOC = GrassLookup.Instance.EF_NMVOC((int)grassType);
        double emissionFactorCultivationPM2_5 = GrassLookup.Instance.EF_PM2_5_Cultivation((int)grassType);
        double emissionFactorCultivationPM10 = GrassLookup.Instance.EF_PM10_Cultivation((int)grassType);
        double emissionFactorHarvestingPM2_5 = GrassLookup.Instance.EF_PM2_5_Harvesting((int)grassType);
        double emissionFactorHarvestingPM10 = GrassLookup.Instance.EF_PM10_Harvesting((int)grassType);
        double emissionFactorCleaningPM2_5 = GrassLookup.Instance.EF_PM2_5_Cleaning((int)grassType);
        double emissionFactorCleaningPM10 = GrassLookup.Instance.EF_PM10_Cleaning((int)grassType);
        double emissionFactorDryingPM2_5 = GrassLookup.Instance.EF_PM2_5_Drying((int)grassType);
        double emissionFactorDryingPM10 = GrassLookup.Instance.EF_PM10_Drying((int)grassType);

        myEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = emissionFactorNMVOC;
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = emissionFactorCleaningPM2_5 + emissionFactorCultivationPM2_5 + emissionFactorDryingPM2_5 + emissionFactorHarvestingPM2_5;
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = emissionFactorHarvestingPM10 + emissionFactorDryingPM10 + emissionFactorCleaningPM10 + emissionFactorCultivationPM10;

        return myEmissions;
    }

    /// <summary>
    /// Non GHG Emissions from dairy and Beef systems
    /// </summary>
    /// <param name="sector">Dairy or beef sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="MonthlyGrossEnergyIntake">The gross energy intake in the month (as MJ per month)</param>
    /// <param name="PercentTimeHousingYards">Percentage of the month spent in the house or on yards</param>
    /// <param name="housingAmmonia">The ammonia emissions from housing in the month for the animal type</param>
    /// <param name="storageAmmonia">Ammonia emissions from storage in the month for the animal type</param>
    /// <param name="spreadingAmmonia">Ammonia emissions from spreading in the month for the animal type</param>
    /// <returns>Non GHG emission object</returns>
    public static NonGHGLivestockEmissions NonGHG_DairyBeef(Sector sector, int animalType, double MonthlyGrossEnergyIntake, double PercentTimeHousingYards, double housingAmmonia, double storageAmmonia, double spreadingAmmonia)
    {
        NonGHGLivestockEmissions myEmissions = new();
        myEmissions.InitialiseNonGHGLivestockEmissions();

        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = MonthlyGrossEnergyIntake * ((100.0 - PercentTimeHousingYards) * HelperFunctions.Percent_to_Proportion) *
            LivestockNonGHGLookup.Instance.RetrieveNMVOCGrazing((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = MonthlyGrossEnergyIntake * (PercentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
            LivestockNonGHGLookup.Instance.RetrieveNMVOCHousing((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = MonthlyGrossEnergyIntake * (PercentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
            LivestockNonGHGLookup.Instance.RetrieveNMVOCSilage((int)sector, animalType) * LivestockNonGHGLookup.Instance.RetrieveFractionSilage((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = MonthlyGrossEnergyIntake * (PercentTimeHousingYards * HelperFunctions.Percent_to_Proportion) *
            LivestockNonGHGLookup.Instance.RetrieveNMVOCSilage((int)sector, animalType) * LivestockNonGHGLookup.Instance.RetrieveFractionSilage((int)sector, animalType) *
            LivestockNonGHGLookup.Instance.RetrieveFractionSilageStore((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * storageAmmonia / housingAmmonia;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * spreadingAmmonia / housingAmmonia;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value +
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value +
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value +
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = PercentTimeHousingYards * HelperFunctions.Percent_to_Proportion * LivestockNonGHGLookup.Instance.RetrievePM25((int)sector, animalType) / 12.0;
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = PercentTimeHousingYards * HelperFunctions.Percent_to_Proportion * LivestockNonGHGLookup.Instance.RetrievePM10((int)sector, animalType) / 12.0;
        myEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = PercentTimeHousingYards * HelperFunctions.Percent_to_Proportion * LivestockNonGHGLookup.Instance.RetrieveTSP((int)sector, animalType) / 12.0;

        return myEmissions;
    }

    /// <summary>
    /// Non GHG Emissions from non cattle livestock systems
    /// </summary>
    /// <param name="sector">Dairy or beef sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="VolatileSolids">The annual volatile solids excreted in the month (as kg per month)</param>
    /// <param name="HouseDays">days in year spent in the house</param>
    /// <param name="housingAmmonia">Annual ammonia emissions from housing for the animal type</param>
    /// <param name="storageAmmonia">Annual ammonia emissions from storage for the animal type</param>
    /// <param name="spreadingAmmonia">Annual ammonia emissions from spreading  for the animal type</param>
    /// <returns>Non GHG emission object</returns>
    public static NonGHGLivestockEmissions NonGHG_Livestock_notCattle(Sector sector, int animalType, double VolatileSolids, double HouseDays, double housingAmmonia, double storageAmmonia, double spreadingAmmonia)
    {
        NonGHGLivestockEmissions myEmissions = new();
        myEmissions.InitialiseNonGHGLivestockEmissions();
        double housetime = HouseDays / 365.0;
        double fieldtime = 1.0 - housetime;

        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = VolatileSolids * fieldtime * LivestockNonGHGLookup.Instance.RetrieveNMVOCGrazing((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = VolatileSolids * housetime * LivestockNonGHGLookup.Instance.RetrieveNMVOCHousing((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = VolatileSolids * housetime *
            LivestockNonGHGLookup.Instance.RetrieveNMVOCSilage((int)sector, animalType) * LivestockNonGHGLookup.Instance.RetrieveFractionSilage((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = VolatileSolids * housetime *
            LivestockNonGHGLookup.Instance.RetrieveNMVOCSilage((int)sector, animalType) * LivestockNonGHGLookup.Instance.RetrieveFractionSilage((int)sector, animalType) *
            LivestockNonGHGLookup.Instance.RetrieveFractionSilageStore((int)sector, animalType);
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * storageAmmonia / housingAmmonia;
        myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value * spreadingAmmonia / housingAmmonia;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value +
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value +
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value + myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value +
            myEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value;

        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = housetime * LivestockNonGHGLookup.Instance.RetrievePM25((int)sector, animalType);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = housetime * LivestockNonGHGLookup.Instance.RetrievePM10((int)sector, animalType);
        myEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = housetime * LivestockNonGHGLookup.Instance.RetrieveTSP((int)sector, animalType);

        return myEmissions;
    }
}
