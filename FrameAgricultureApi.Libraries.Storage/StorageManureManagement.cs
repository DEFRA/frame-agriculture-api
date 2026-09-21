using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Storage;

public static class StorageManureManagement
{

    /// <summary>
    /// Method to calculate nitrous oxide emissions from excretal deposition at storage
    /// </summary>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="totalN">Total nitrogen entering storage (kg/head)</param>
    /// <returns>Nitrous oxide emission (kg N2O-N/head)</returns>
    public static double N2ONEmission(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalN)
    {
        return GenericEquations.Emission_PercentageEF(totalN, StorageLookup.RetrieveStorageN2ONEmissionFactor(sector, animalType, manureStorageSystem, organicMatterType));
    }
    /// <summary>
    /// Method to calculate ammonia emissoins from excretal depositin in storage
    /// </summary>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="totalAmmoniacalNitrogen">Total ammoniacal nitrogen entering storage (kg/head)</param>
    /// <returns>Ammonia emission (kg NH3-N/head)</returns>
    public static double NH3NEmission(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalAmmoniacalNitrogen)
    {
        return GenericEquations.Emission_PercentageEF(totalAmmoniacalNitrogen, StorageLookup.RetrieveStorageNH3NEmissionFactor(sector, animalType, manureStorageSystem, organicMatterType));
    }
    /// <summary>
    /// Method to calculate nbitric oxide emissions from excretal deposition in storage
    /// </summary>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="totalN2ON">Total nitrogen in excreta deposited in storage (kg/head)</param>
    /// <returns>Nitric oxide emissions (kg NON/head)</returns>
    public static double NONEmission(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalN2ON)
    {
        return GenericEquations.Emission_ProportionEF(totalN2ON, StorageLookup.RetrieveStorageNONRatio(sector, animalType, manureStorageSystem, organicMatterType));
    }
    /// <summary>
    /// Method to calculte dinitrogen emissions from excretal deposition in storage
    /// </summary>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="totalN2ON">Total nitrogen in excreta deposited in storage (kg/head)</param>
    /// <returns>Dinitrogen emission (kg N2-N/head)</returns>
    public static double N2NEmission(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalN2ON)
    {
        return GenericEquations.Emission_ProportionEF(totalN2ON, StorageLookup.RetrieveStorageN2NRatio(sector, animalType, manureStorageSystem, organicMatterType));
    }
    /// <summary>
    /// Nitrate leached
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">manure storage system enumerator</param>
    /// <param name="organicMatterType">organic matter tyoe enumerator</param>
    /// <param name="totalNitrogen">Total nitrogen entering storage</param>
    /// <returns></returns>
    public static double NO3NEmission(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalNitrogen)
    {
        return GenericEquations.Emission_PercentageEF(totalNitrogen, StorageLookup.RetrieveFracLeach(sector, animalType, manureStorageSystem, organicMatterType));
    }
    /// <summary>
    /// emission of methane from stored manures
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal Type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure storage system enumerator</param>
    /// <param name="organicMatterType">organic matter type enumerator</param>
    /// <param name="volatileSolids">volatile solids as double</param>
    /// <returns></returns>
    public static double CH4Emission(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double volatileSolids)
    {
        return sector switch
        {
            Sector.Pigs or Sector.Dairy or Sector.Beef => GenericEquations.ManureManagementMethane(volatileSolids, MethaneLookup.RetrieveB0(sector, animalType),
                        StorageLookup.RetrieveStorageMethaneConversionFactor(sector, 0, manureStorageSystem, organicMatterType), ManureManagementParameters.methanem3tokg),
            _ => GenericEquations.ManureManagementMethane(volatileSolids, MethaneLookup.RetrieveB0(sector, animalType),
                        StorageLookup.RetrieveStorageMethaneConversionFactor(sector, animalType, manureStorageSystem, organicMatterType), ManureManagementParameters.methanem3tokg),
        };
    }
}

