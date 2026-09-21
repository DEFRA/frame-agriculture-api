using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureMassVolume;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// Manure Mass and Volume Component class contianing methods for calcualtion of manure mass and volume
/// </summary>
public static class ManureMassandVolume
{
    /// <summary>
    /// Manure, mass and volume calcualtions for pigs, poultry and minor livestock kept outdoors
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animaltype">integer value of animal type enumerator for sector</param>
    /// <param name="organicMatterType">organic matter enumerator</param>
    /// <param name="percentExcretionOutdoors">(Optional) Percent of excretion that is outdoors</param>
    /// <returns>Manure mass and volume outputs for outdoor livestock</returns>
    //Todo percentExcretionOutdoors is percentExcretionIndoors in other places
    public static ManureMassVolumeOutput ManureMassVolume_PigPoultryMinorLivestock_Outdoor(Sector sector, int animaltype, OrganicMatterType organicMatterType, double percentExcretionOutdoors = 100.0)
    {

        bool indoor = false; // cannot be indoor if grazing

        //Grazing
        ManureMassVolumeOutput grazingOutputs = new();
        grazingOutputs.InitialiseManureMassVolumeOutput();

        switch(sector)
        {
            case Sector.Pigs:
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Grazing, organicMatterType, (PigType)animaltype, true, false, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Grazing, organicMatterType, (PigType)animaltype, true, true, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Grazing, organicMatterType, (PigType)animaltype, false, false, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Grazing, organicMatterType, (PigType)animaltype, false, true, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.Poultry:
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Grazing, organicMatterType, (PoultryType)animaltype, true, false, indoor) * (1 - percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion);
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Grazing, organicMatterType, (PoultryType)animaltype, true, true, indoor) * (1 - percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion);
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Grazing, organicMatterType, (PoultryType)animaltype, false, false, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Grazing, organicMatterType, (PoultryType)animaltype, false, true, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.MinorLivestock:
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Grazing, organicMatterType, (MinorLivestockType)animaltype, true, false, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Grazing, organicMatterType, (MinorLivestockType)animaltype, true, true, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Grazing, organicMatterType, (MinorLivestockType)animaltype, false, false, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                grazingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Grazing, organicMatterType, (MinorLivestockType)animaltype, false, true, indoor) * percentExcretionOutdoors * HelperFunctions.Percent_to_Proportion;
                break;

        }

        return grazingOutputs;
    }
    /// <summary>
    /// Manure, mass and volume calculations for pigs, poultry and minor livestock in housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animaltype">integer value of animal type enumerator for sector</param>
    /// <param name="organicMatterType">organic matter enumerator</param>
    /// <param name="percentExcretionIndoors">(Optional) Percent of excretion that is indoors</param>
    /// <returns>Manure mass and volume outputs for housing</returns>
    public static ManureMassVolumeOutput ManureMassVolume_PigPoultryMinorLivestock_Housing(Sector sector, int animaltype, OrganicMatterType organicMatterType, double percentExcretionIndoors)
    {
        bool indoor = true; // have to be indoor if housing
        //Housing
        ManureMassVolumeOutput housingOutputs = new();
        housingOutputs.InitialiseManureMassVolumeOutput();
        switch(sector)
        {
            case Sector.Pigs:
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Housing, organicMatterType, (PigType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Housing, organicMatterType, (PigType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Housing, organicMatterType, (PigType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Housing, organicMatterType, (PigType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.Poultry:
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Housing, organicMatterType, (PoultryType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Housing, organicMatterType, (PoultryType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Housing, organicMatterType, (PoultryType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Housing, organicMatterType, (PoultryType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.MinorLivestock:
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Housing, organicMatterType, (MinorLivestockType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Housing, organicMatterType, (MinorLivestockType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Housing, organicMatterType, (MinorLivestockType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                housingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Housing, organicMatterType, (MinorLivestockType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;

        }
        return housingOutputs;
    }
    /// <summary>
    /// Manure, mass and volume calculations for pigs, poultry and minor livestock  storage
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animaltype">integer value of animal type enumerator for sector</param>
    /// <param name="organicMatterType">organic matter enumerator</param>
    /// <param name="percentExcretionIndoors">the percentage of excreta that is deposited indoors</param>
    /// <returns>Manure mass and volume outputs for storage</returns>
    public static ManureMassVolumeOutput ManureMassVolume_PigPoultryMinorLivestock_Storage(Sector sector, int animaltype, OrganicMatterType organicMatterType, double percentExcretionIndoors)
    {
        //indoor set to true as will only store manure from indoor animals
        bool indoor = true;
        //Storage
        ManureMassVolumeOutput storageOutputs = new();
        storageOutputs.InitialiseManureMassVolumeOutput();
        switch(sector)
        {
            case Sector.Pigs:
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Storage, organicMatterType, (PigType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Storage, organicMatterType, (PigType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Storage, organicMatterType, (PigType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Storage, organicMatterType, (PigType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.Poultry:
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Storage, organicMatterType, (PoultryType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Storage, organicMatterType, (PoultryType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Storage, organicMatterType, (PoultryType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Storage, organicMatterType, (PoultryType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.MinorLivestock:
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Storage, organicMatterType, (MinorLivestockType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Storage, organicMatterType, (MinorLivestockType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Storage, organicMatterType, (MinorLivestockType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                storageOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Storage, organicMatterType, (MinorLivestockType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;
        }
        return storageOutputs;
    }
    /// <summary>
    /// Manure, mass and volume calculations for pigs, poultry and minor livestock at spreading
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animaltype">integer value of animal type enumerator for sector</param>
    /// <param name="organicMatterType">organic matter enumerator</param>
    /// <param name="percentExcretionIndoors">Percentage of excreta deposited indoors</param>
    /// <returns>Manure mass and volume outputs for spreading</returns>
    public static ManureMassVolumeOutput ManureMassVolume_PigPoultryMinorLivestock_Spreading(Sector sector, int animaltype, OrganicMatterType organicMatterType, double percentExcretionIndoors)
    {
        //indoor set to true as will only spread manure arising form indoor systems
        bool indoor = true;
        //Spreading
        ManureMassVolumeOutput spreadingOutputs = new();
        spreadingOutputs.InitialiseManureMassVolumeOutput();
        switch(sector)
        {
            case Sector.Pigs:
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Spreading, organicMatterType, (PigType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Spreading, organicMatterType, (PigType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Spreading, organicMatterType, (PigType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Pigs(Sector.Pigs, ComponentType.Spreading, organicMatterType, (PigType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.Poultry:
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Spreading, organicMatterType, (PoultryType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Spreading, organicMatterType, (PoultryType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Spreading, organicMatterType, (PoultryType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_Poultry(Sector.Poultry, ComponentType.Spreading, organicMatterType, (PoultryType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;

            case Sector.MinorLivestock:
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Spreading, organicMatterType, (MinorLivestockType)animaltype, true, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Spreading, organicMatterType, (MinorLivestockType)animaltype, true, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Spreading, organicMatterType, (MinorLivestockType)animaltype, false, false, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                spreadingOutputs.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = ManureMassVolumeGenericFunctions.ManureMassVolume_MinorLivestock(Sector.MinorLivestock, ComponentType.Spreading, organicMatterType, (MinorLivestockType)animaltype, false, true, indoor) * percentExcretionIndoors * HelperFunctions.Percent_to_Proportion;
                break;
        }
        return spreadingOutputs;
    }
    /// <summary>
    /// Manure mass and volume calculations for cattle at grazing
    /// </summary>
    /// <param name="dryMatterIntake">dry matter intake (kg/unit time)</param>
    /// <param name="dryMatterDigestibility">dry matter digestibility kg/kg</param>
    /// <param name="drymattercontent">dry matter content of diet kg \dm per kg DMI</param>
    /// <param name="percentTimeGrazing">Percentage of time at grazing</param>
    /// <returns>Manure mass and volume output object for manure mass and volume entering and leaving grazing</returns>
    //Todo find inputs in spreadsheet
    public static ManureMassVolumeOutput ManureMassVolume_Cattle_Grazing(double dryMatterIntake, double dryMatterDigestibility, double drymattercontent, double percentTimeGrazing)
    {
        ExcretalPools(dryMatterIntake, dryMatterDigestibility, drymattercontent, out double urineMass, out double urineVolume, out double faecesMass, out double faecesVolume);

        //Grazing
        ManureMassVolumeOutput grazing = new();
        grazing.InitialiseManureMassVolumeOutput();

        grazing.ManureMassVolume[ManureMassVolumeOutputs.UrineVolumeIn].Value = urineVolume * percentTimeGrazing * HelperFunctions.Percent_to_Proportion;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.UrineMassIn].Value = urineMass * percentTimeGrazing * HelperFunctions.Percent_to_Proportion;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.DungVolumeIn].Value = faecesVolume * percentTimeGrazing * HelperFunctions.Percent_to_Proportion;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.DungMassIn].Value = faecesMass * percentTimeGrazing * HelperFunctions.Percent_to_Proportion;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericEquations.CattleExcretaMassVolume(faecesMass, urineMass, percentTimeGrazing);
        grazing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericEquations.CattleExcretaMassVolume(faecesVolume, urineVolume, percentTimeGrazing);
        grazing.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = grazing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = grazing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value;

        return grazing;
    }
    /// <summary>
    /// Manure mass and volume calculations for cattle in housing
    /// </summary>
    /// <param name="dryMatterIntake">dry matter intake (kg/unit time)</param>
    /// <param name="dryMatterDigestibility">dry matter digestibility kg/kg</param>
    /// <param name="percentTimeHousing">Percentage of time in housing</param>
    /// <param name="fym">Boolean indicating if FYM (true) or slurry (false)</param>
    /// <param name="masstoStorage">Mass of manure that needs to be passed to the storage method</param>
    /// <param name="volumetoStorage">Volume of manure that needs to be passed to the storage method</param>
    /// <returns>Manure mass and volume output object for manure mass and volume entering and leaving housing</returns>
    public static ManureMassVolumeOutput ManureMassVolume_Cattle_Housing(double dryMatterIntake, double dryMatterDigestibility, double drymattercontent, double percentTimeHousing, bool fym,
        out double masstoStorage, out double volumetoStorage)
    {
        ExcretalPools(dryMatterIntake, dryMatterDigestibility, drymattercontent, out double urineMass, out double urineVolume, out double faecesMass, out double faecesVolume);

        //Housing
        ManureMassVolumeOutput housing = new();
        housing.InitialiseManureMassVolumeOutput();

        housing.ManureMassVolume[ManureMassVolumeOutputs.UrineVolumeIn].Value = urineVolume * percentTimeHousing * HelperFunctions.Percent_to_Proportion;
        housing.ManureMassVolume[ManureMassVolumeOutputs.UrineMassIn].Value = urineMass * percentTimeHousing * HelperFunctions.Percent_to_Proportion;
        housing.ManureMassVolume[ManureMassVolumeOutputs.DungVolumeIn].Value = faecesVolume * percentTimeHousing * HelperFunctions.Percent_to_Proportion;
        housing.ManureMassVolume[ManureMassVolumeOutputs.DungMassIn].Value = faecesMass * percentTimeHousing * HelperFunctions.Percent_to_Proportion;
        housing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericEquations.CattleExcretaMassVolume(faecesMass, urineMass, percentTimeHousing);
        housing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericEquations.CattleExcretaMassVolume(faecesVolume, urineVolume, percentTimeHousing);
        double[] housingMMV = ManureMassVolumeGenericFunctions.ManureMassVolume_CattleHousing(housing.ManureMassVolume[ManureMassVolumeOutputs.UrineMassIn].Value,
            housing.ManureMassVolume[ManureMassVolumeOutputs.DungMassIn].Value, housing.ManureMassVolume[ManureMassVolumeOutputs.UrineVolumeIn].Value, housing.ManureMassVolume[ManureMassVolumeOutputs.DungVolumeIn].Value, fym);
        housing.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = housingMMV[0];
        housing.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = housingMMV[1];
        masstoStorage = housingMMV[0];
        volumetoStorage = housingMMV[1];

        return housing;
    }
    /// <summary>
    /// Manure mass and volume calculations for cattle on yards
    /// </summary>
    /// <param name="dryMatterIntake">dry matter intake (kg/unit time)</param>
    /// <param name="dryMatterDigestibility">dry matter digestibility kg/kg</param>
    /// <param name="percentTimeYards">Percentage of time on yards</param>
    /// <param name="massOutYards">Mass of manure leaving yards</param>
    /// <param name="volumeOutYards">Volume of manure leaving yards</param>
    /// <returns>Manure mass and volume output object for manure mass and volume entering and leaving yards</returns>
    public static ManureMassVolumeOutput ManureMassVolume_Cattle_Yards(double dryMatterIntake, double dryMatterDigestibility, double drymattercontent, double percentTimeYards, out double massOutYards, out double volumeOutYards)
    {
        ExcretalPools(dryMatterIntake, dryMatterDigestibility, drymattercontent, out double urineMass, out double urineVolume, out double faecesMass, out double faecesVolume);

        //Yards
        ManureMassVolumeOutput yards = new();
        yards.InitialiseManureMassVolumeOutput();

        yards.ManureMassVolume[ManureMassVolumeOutputs.UrineVolumeIn].Value = urineVolume * percentTimeYards * HelperFunctions.Percent_to_Proportion;
        yards.ManureMassVolume[ManureMassVolumeOutputs.UrineMassIn].Value = urineMass * percentTimeYards * HelperFunctions.Percent_to_Proportion;
        yards.ManureMassVolume[ManureMassVolumeOutputs.DungVolumeIn].Value = faecesVolume * percentTimeYards * HelperFunctions.Percent_to_Proportion;
        yards.ManureMassVolume[ManureMassVolumeOutputs.DungMassIn].Value = faecesMass * percentTimeYards * HelperFunctions.Percent_to_Proportion;
        yards.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = ManureMassVolumeGenericEquations.CattleExcretaMassVolume(faecesMass, urineMass, percentTimeYards);
        yards.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = ManureMassVolumeGenericEquations.CattleExcretaMassVolume(faecesVolume, urineVolume, percentTimeYards);
        yards.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = yards.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value;
        yards.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = yards.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value;

        massOutYards = yards.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value;
        volumeOutYards = yards.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value;

        return yards;
    }
    /// <summary>
    /// Calcualtions for manure mass and volume leaving storage
    /// </summary>
    /// <param name="manureMassIn">Mass of manure entering storage (kg)</param>
    /// <param name="manureVolumeIn">Volume of manure entering storage (kg)</param>
    /// <param name="fym">Boolean indicating if fym (true) or slurry (false)</param>
    /// <param name="anaerobicDigestion">Boolean indicating if storage systme is anaerobic digestion (true) or other (false)</param>
    /// <param name="massToSpreading">Mass of manure to be passed to spreading method</param>
    /// <param name="volumeToSpreading">Volume of manure to be passed to spreaing method</param>
    /// <returns>Mass (kg) and volume (l) of manures entering and leaving storage</returns>
    public static ManureMassVolumeOutput ManureMassVolume_Cattle_Storage(double manureMassIn, double manureVolumeIn, bool fym, bool anaerobicDigestion,
        out double massToSpreading, out double volumeToSpreading)
    {

        //Storage
        ManureMassVolumeOutput storage = new();
        storage.InitialiseManureMassVolumeOutput();

        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = manureMassIn;
        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = manureVolumeIn;

        double[] storageMMV = ManureMassVolumeGenericFunctions.ManureMassVolume_CattleStorage(manureMassIn, manureVolumeIn, fym, anaerobicDigestion);
        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = storageMMV[0];
        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = storageMMV[1];

        massToSpreading = storageMMV[0];
        volumeToSpreading = storageMMV[1];

        return storage;

    }
    /// <summary>
    /// Calcualtionds for manure mass and volume leaving spreading
    /// </summary>
    /// <param name="manureMassIn">Mass of manure entering storage (kg)</param>
    /// <param name="manureVolumeIn">Volume of manure entering storage (kg)</param>
    /// <returns>Mass (kg) and volume (l) of manures entering and leaving spreading</returns>
    public static ManureMassVolumeOutput ManureMassVolume_CattleSheep_Spreading(double manureMassIn, double manureVolumeIn)
    {
        ManureMassVolumeOutput spreading = new();
        spreading.InitialiseManureMassVolumeOutput();

        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = manureMassIn;
        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = manureMassIn;
        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = manureVolumeIn;
        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = manureVolumeIn;

        return spreading;
    }
    /// <summary>
    /// Calcualtion of excretal pools for cattle
    /// </summary>
    /// <param name="dryMatterIntake">Dry matter intake (kg)</param>
    /// <param name="dryMatterDigestibility">Dry matter digestibility (kg/kg)</param>
    /// <param name="drymattercontent">dry matter content of the diet (kg/kg)</param>
    /// <param name="urineMass">OUT: Mass of urine produced (kg)</param>
    /// <param name="urineVolume">OUT: Volume of urine produced</param>
    /// <param name="faecesMass">OUT: Mass of dung produced</param>
    /// <param name="faecesVolume">OUT: Volume of dung produced</param>
    private static void ExcretalPools(double dryMatterIntake, double dryMatterDigestibility, double drymattercontent, out double urineMass, out double urineVolume, out double faecesMass, out double faecesVolume)
    {
        //Excretal Pools
        double urineDryMatter = ManureMassVolumeGenericEquations.CattleUrineDryMatter(drymattercontent);
        double faecesDryMatter = ManureMassVolumeGenericEquations.FaecesDryMatter(dryMatterDigestibility);

        urineMass = ManureMassVolumeGenericEquations.CattleUrineMass(dryMatterIntake, urineDryMatter);
        urineVolume = ManureMassVolumeGenericEquations.CattleUrineVolume(urineMass);
        faecesMass = ManureMassVolumeGenericEquations.CattleFaecesMass(dryMatterIntake, faecesDryMatter);
        faecesVolume = ManureMassVolumeGenericEquations.CattleFaecesVolume(dryMatterIntake, faecesDryMatter);
    }
    /// <summary>
    /// Calcualtion (lookup) of excretal inputs entering and leaving grazing
    /// </summary>
    /// <returns>Mass (kg) and Volume (l) of excreta entering and leaving grazing</returns>
    public static ManureMassVolumeOutput ManureMassVolume_Sheep_Grazing(SheepEnergyBalance sheepEnergyBalance)
    {
        ManureMassVolumeOutput grazing = new();
        grazing.InitialiseManureMassVolumeOutput();

        grazing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = sheepEnergyBalance.FieldExcretaMass;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = sheepEnergyBalance.FieldExcretaMass;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = sheepEnergyBalance.FieldExcretaVolume;
        grazing.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = sheepEnergyBalance.FieldExcretaVolume;

        return grazing;
    }
    /// <summary>
    /// Calcualtion (lookup) of excretal inputs entering and leaving housing
    /// </summary>
    /// <param name="sheepEnergyBalance">Sheep energy balance object containg all the sheep model information needed for the calculation</param>
    /// <param name="massToStorage">Mass of manure to be passed to Storage method</param>
    /// <param name="volumeToStorage">Volume of money to be passed to Storage method</param>
    /// <returns>Mass (kg) and Volume (l) of excreta/manure entering and leaving housing</returns>
    public static ManureMassVolumeOutput ManureMassVolume_Sheep_Housing(SheepEnergyBalance sheepEnergyBalance, out double massToStorage, out double volumeToStorage)
    {
        ManureMassVolumeOutput housing = new();
        housing.InitialiseManureMassVolumeOutput();

        housing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = sheepEnergyBalance.HouseExcretaMass;
        housing.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = sheepEnergyBalance.HouseManureMass;
        housing.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = sheepEnergyBalance.HouseExcretaVolume;
        housing.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = sheepEnergyBalance.HouseManureVolume;

        massToStorage = sheepEnergyBalance.HouseManureMass;
        volumeToStorage = sheepEnergyBalance.HouseManureVolume;

        return housing;
    }
    /// <summary>
    /// Calculation of manures leaving storage
    /// </summary>
    /// <param name="manureMassIn">Mass of manure entering storage (kg)</param>
    /// <param name="manureVolumeIn">Volume of manure entering storage (l)</param>
    /// <param name="massToSpreading">Mass of manure to be passed to Spreading method</param>
    /// <param name="volumeToSpreading">Volume of manure to be passed to Spreading method</param>
    /// <returns>Mass (kg) and Volume (l) of manure entering and leaving storage</returns>
    public static ManureMassVolumeOutput ManureMassVolume_Sheep_Storage(double manureMassIn, double manureVolumeIn, out double massToSpreading,
        out double volumeToSpreading)
    {

        //Storage
        ManureMassVolumeOutput storage = new();
        storage.InitialiseManureMassVolumeOutput();

        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = manureMassIn;
        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = manureVolumeIn;

        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = manureMassIn * (1.0 - (ManureMassVolumeGenericEquationParameters.reductionFYMComposting * HelperFunctions.Percent_to_Proportion));
        storage.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = manureVolumeIn * (1.0 - (ManureMassVolumeGenericEquationParameters.reductionFYMComposting * HelperFunctions.Percent_to_Proportion));

        massToSpreading = storage.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value;
        volumeToSpreading = storage.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value;

        return storage;
    }
    /// <summary>
    /// Calculation of manures leaving spreading
    /// </summary>
    /// <param name="manureMassIn">Mass of manure entering spreading (kg)</param>
    /// <param name="manureVolumeIn">Volume of manure entering spreading (l)</param>
    /// <returns>Mass (kg) and Volume (l) of manure entering and leaving spreading</returns>
    public static ManureMassVolumeOutput ManureMassVolume_Sheep_Spreading(double manureMassIn, double manureVolumeIn)
    {

        //Spreading
        ManureMassVolumeOutput spreading = new();
        spreading.InitialiseManureMassVolumeOutput();

        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = manureMassIn;
        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = manureVolumeIn;

        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = manureMassIn;
        spreading.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = manureVolumeIn;

        return spreading;
    }
}
