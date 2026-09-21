using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.YardExcretionLivestock;

public class YardExcretaDeposition
{
    /// <summary>
    /// Method to calcualte nitrous oxide emission
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="totalN">Total nitrogen (kg per head)</param>
    /// <returns>Nitrous oxide emission (kg N2O-N per head)</returns>
    public static double N2ONEmission(Sector sector, int animalType, double totalN, bool collectingYard)
    {
        return GenericEquations.Emission_PercentageEF(totalN, YardLookup.Instance.RetrieveN2ONEmissionFactor(sector, animalType, collectingYard));
    }
    /// <summary>
    /// Method tocalculate ammonia emissions
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be dairy or beef)</param>
    /// <param name="totalAmmoniacalNitrogen">Total ammoniacale nitrogen in excreta deposited on yards</param>
    /// <param name="collectingYard">Boolean indicating if collecting yard (true) or feeding yard (false)</param>
    /// <returns>Direct ammonia emission (kg NH3-N/head)</returns>
    public static double NH3NEmission(Sector sector, int animalType, double totalAmmoniacalNitrogen, bool collectingYard)
    {
        return GenericEquations.Emission_PercentageEF(totalAmmoniacalNitrogen, YardLookup.Instance.RetrieveNH3NEmissionFactor(sector, animalType, collectingYard));
    }
    /// <summary>
    /// Method to calcualte nitric oxide emissions from yards
    /// </summary>
    /// <param name="sector">Sector enumerator (should be dairt or beef)</param>
    /// <param name="directN2ON">The direct nitrous oxide emision from excreta depsoited on yards (kg N2O-N/head)</param>
    /// <param name="collectingYard">Boolean indicating if collecting yard (true) or feeding yard (false)</param>
    /// <returns>Nitric oxide emission (kg NO-N/head)</returns>
    public static double NONEmission(Sector sector, int animalType, double directN2ON, bool collectingYard)
    {
        return GenericEquations.Emission_ProportionEF(directN2ON, YardLookup.Instance.RetrieveNONRatio(sector, animalType, collectingYard));
    }
    /// <summary>
    /// Method to calculte dinitrogen emissions
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be dairy or beef)</param>
    /// <param name="directN2ON">The direct nitrous oxide emision from excreta depsoited on yards (kg N2O-N/head)</param>
    /// <param name="collectingYard">Boolean indicating if collecting yard (true) or feeding yard (false)</param>
    /// <returns>Dinitrogen emission (kg N2-N/head)</returns>
    public static double N2NEmission(Sector sector, int animalType, double directN2ON, bool collectingYard)
    {
        return GenericEquations.Emission_ProportionEF(directN2ON, YardLookup.Instance.RetrieveN2NRatio(sector, animalType, collectingYard));
    }
    /// <summary>
    /// Method to calcualte methaen emission from excreta deposited on collecting yards
    /// </summary>
    /// <param name="sector">Sector enumerator (ahoudl eb dairy or beef)</param>
    /// <param name="volatileSolids">Volatile solids in excreta deposited on yards (kg VS/head)</param>
    /// <param name="collectingYard">Boolean indicating if collecting yard (true) or feeding yard (false)</param>
    /// <returns>Methaen emission (kg CH4/head)</returns>
    public static double CH4Emission(double volatileSolids, Sector sector, int animalType, bool collectingYard = false)
    {
        return GenericEquations.ManureManagementMethane(volatileSolids, MethaneLookup.RetrieveB0(sector, animalType), YardLookup.Instance.RetrieveMCF(sector, animalType, collectingYard),
            ManureManagementParameters.methanem3tokg);
    }
}
