using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Housing;

public static class HousingManureManagement
{
    /// <summary>
    /// Method to calcualte N2ON emission at housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Mnaure housing system eenumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter type)</param>
    /// <param name="totalN">Total Nitrogen entering housing (kg/head)</param>
    /// <returns>Emission of N2ON from housing (kg/head)</returns>
    public static double N2ONEmission(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalN)
    {
        return GenericEquations.Emission_PercentageEF(totalN, HousingLookup.RetrieveN2ONEmissionFactor(sector, animalType, manureHousingSystem, manureType));
    }
    /// <summary>
    /// Calcualtion of NH3-N emissions from housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Mnaure housing system eenumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter type)</param>
    /// <param name="totalAmmoniacalNitrogen">Total Ammoniacal nitrogen entering housing (kg/head)</param>
    /// <returns>NH3-N Emission from housing (kg/head)</returns>
    public static double NH3NEmission(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalAmmoniacalNitrogen)
    {
        return GenericEquations.Emission_PercentageEF(totalAmmoniacalNitrogen, HousingLookup.RetrieveNH3NEmissionFactor(sector, animalType, manureHousingSystem, manureType));
    }
    /// <summary>
    /// Method to calcualte NON emission at housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Mnaure housing system eenumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter source type)</param>
    /// <param name="totalN2ON">Housing N2ON emission (kg/head)</param>
    /// <returns>Emission of NON (kg/head)</returns>
    public static double NONEmission(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalN2ON)
    {
        return GenericEquations.Emission_ProportionEF(totalN2ON, HousingLookup.RetrieveNONRatio(sector, animalType, manureHousingSystem, manureType));
    }
    /// <summary>
    /// Method to calcualte N2N emission at housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Mnaure housing system eenumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter type)</param>
    /// <param name="totalN2ON">Housing N2ON emission (kg/head)</param>
    /// <returns>Emission of N2N (kg/head)</returns>
    public static double N2NEmission(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalN2ON)
    {
        return GenericEquations.Emission_ProportionEF(totalN2ON, HousingLookup.RetrieveN2NRatio(sector, animalType, manureHousingSystem, manureType));
    }
    /// <summary>
    /// Method to calcualte methane emission at housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Mnaure housing system eenumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter type)</param>
    /// <param name="volatileSolids">Volatile solids deposited in housing (kg/head)</param>
    /// <returns>Emission of CH4 (kg/head)</returns>
    public static double CH4Emission(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double volatileSolids)
    {
        if(sector.Equals(Sector.MinorLivestock))
        {
            return HousingLookup.RetrieveMCF(sector, animalType, manureHousingSystem, manureType);
        }
        else
        {
            return GenericEquations.ManureManagementMethane(volatileSolids, MethaneLookup.RetrieveB0(sector, animalType), HousingLookup.RetrieveMCF(sector, animalType, manureHousingSystem, manureType),
               ManureManagementParameters.methanem3tokg);
        }
    }
}
