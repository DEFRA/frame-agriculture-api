using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.GrassResidues;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.OutdoorLivestock;

public static class OutdoorExcretalDeposition
{
    /// <summary>
    /// Method to calculate ammonia emissions from urine and dung excreted outdoor by cattle
    /// </summary>
    /// <param name="sector">Sector - should be only dairy, beef or sheep</param>
    /// <param name="nitrogenUrine">nitrogen in urine (kg)</param>
    /// <param name="nitrogenDung">nitrogen in dung (kg)</param>
    /// <returns>Ammonia emission as kg NH3-N</returns>
    public static double NH3NEmission(Sector sector, double nitrogenUrine, double nitrogenDung)
    {
        if(sector.Equals(Sector.Sheep))
        {
            double TAN = ManureManagementGenericEquations.SheepTAN(nitrogenUrine, nitrogenDung);
            return GenericEquations.Emission_PercentageEF(TAN, OutdoorExcretionLookup.RetrieveNH3NEF(sector, false));
        }
        else
        {
            return ManureManagementGenericEquations.Emission_UrineDung(nitrogenUrine, nitrogenDung, OutdoorExcretionLookup.RetrieveNH3NEF(sector, true),
                OutdoorExcretionLookup.RetrieveNH3NEF(sector, false));
        }
    }
    /// <summary>
    /// Method to calculate ammonia emissions from available nitrogen
    /// </summary>
    /// <param name="sector">Sector - should be pig, poultry or minor livestock</param>
    /// <param name="totalAvailableNitrogen">total available nitrogen in excrete deposited outdoors (kg)</param>
    /// <returns>Ammonia emission as kg NH3-N</returns>
    public static double NH3NEmission(Sector sector, double totalAvailableNitrogen)
    {
        return GenericEquations.Emission_PercentageEF(totalAvailableNitrogen, OutdoorExcretionLookup.RetrieveNH3NEF(sector, false));
    }
    /// <summary>
    /// Method to Calculate nitrous oxide emissions from urine and dung excreted outdoor
    /// </summary>
    /// <param name="sector">Sector - shoudl be only dairy, beef or sheep</param>
    /// <param name="nitrogenUrine">nitrogen in urien (kg)</param>
    /// <param name="nitrogenDung">nitrogen in dung (kg)</param>
    /// <returns>Nitrous oxide emission as kg N2O-N</returns>
    public static double N2ONEmission(Sector sector, double nitrogenUrine, double nitrogenDung)
    {

        return ManureManagementGenericEquations.Emission_UrineDung(nitrogenUrine, nitrogenDung, OutdoorExcretionLookup.RetrieveN2ONEF(sector, true),
            OutdoorExcretionLookup.RetrieveN2ONEF(sector, false));

    }
    /// <summary>
    /// Method to calculate nitrous oxide emissions from total nitrogen
    /// </summary>
    /// <param name="sector">Sector - should be pig, poultry or minor livestock</param>
    /// <param name="totalNitrogen">total nitrogen in excrete deposited outdoors (kg)</param>
    /// <returns>Nitrous oxide emission as kg N2O-N</returns>
    public static double N2ONEmission(Sector sector, double totalNitrogen)
    {
        return GenericEquations.Emission_PercentageEF(totalNitrogen, OutdoorExcretionLookup.RetrieveN2ONEF(sector, false));
    }
    /// <summary>
    /// Method to calculate nitrate emissions from urine and dung deposited outdoors by cattle
    /// </summary>
    /// <param name="sector">Sector - should be cattle only</param>
    /// <param name="nitrogenUrine">nitrogen in urine deposuted outdoors (kg)</param>
    /// <param name="nitrogenDung">nitrogen in dung deposited outdooes (kg)</param>
    /// <returns>Nitrate emission as kg NO3-N</returns>
    public static double NO3NEmissionCattle(Sector sector, double nitrogenUrine, double nitrogenDung)
    {

        double fracLeach = Math.Max(0, Math.Min(100, OutdoorExcretionLookup.RetrieveOutdoorFracLeach(sector) + OutdoorExcretionLookup.RetrieveFracLeachUncertainty(sector)));
        return GenericEquations.Emission_PercentageEF(nitrogenUrine + nitrogenDung, fracLeach);
    }
    /// <summary>
    /// Method to calculate nitrate emissions from sheep excreta deposited outdoors
    /// </summary>
    /// <param name="totalNitrogen">total nitrogen in excreta deposited outdoors (kg)</param>
    /// <param name="fertiliserRate">total rate of fertiliser applied (kg N/ha)</param>
    /// <param name="gridCell">integer value indicating 10km grid square in the UK in which the grass field is - required due to use of variable FracLeach for grass</param>
    /// <param name="grassType">Enumerator indicating the type of grass being grown - required due to use of variable FracLeach for grass</param>
    /// <returns>Nitrate emission as kg NO3-N</returns>
    public static double NO3NEmissionSheep(double totalNitrogen, double fertiliserRate, GrassEquationCoefficients GrassFracLeachEquationCoefficient, double geneticGrazedFracLeachScalar)
    {
        double variableFracLeach = Math.Max(0.0, Math.Min(100.0, GrassResidueEquations.GrassFracLeach(GrassFracLeachEquationCoefficient.C, GrassFracLeachEquationCoefficient.Coeff_X, GrassFracLeachEquationCoefficient.Coeff_X2,
            GrassFracLeachEquationCoefficient.Coeff_X3, GrassFracLeachEquationCoefficient.Coeff_X4, fertiliserRate)));
        return totalNitrogen * variableFracLeach * geneticGrazedFracLeachScalar * HelperFunctions.Percent_to_Proportion;
    }
    /// <summary>
    /// Method to calculate nitrate emissions from outdoor livestock
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="totalNitrogen">Total nitrogen in excreta outdoor (kg)</param>
    /// <returns>Nitrate emissions as kg NO3-N</returns>
    public static double NO3NEmissionGeneric(Sector sector, double totalNitrogen)
    {
        return GenericEquations.Emission_PercentageEF(totalNitrogen, OutdoorExcretionLookup.RetrieveOutdoorFracLeach(sector));
    }
    /// <summary>
    /// Method to calculate nitric oxide emissions from excreta deposited outdoors
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="N2ON">Nitrous oxide emission from excreat deposited outdoors (kg N2O-N)</param>
    /// <returns>Nitric oxide emission (kg NO-N)</returns>
    public static double NOEmissions(Sector sector, double N2ON)
    {
        return GenericEquations.Emission_ProportionEF(N2ON, OutdoorExcretionLookup.RetrieveNitricOxideRatio(sector));
    }
    /// <summary>
    /// Method to calculate dinitrogen emissions from excreta deposited outdoors
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="N2ON">Nitrous oxide emission from excreat deposited outdoors (kg N2O-N)</param>
    /// <returns>Dinitrogen emission (kg N2-N)</returns>
    public static double N2Emissions(Sector sector, double N2ON)
    {
        return GenericEquations.Emission_ProportionEF(N2ON, OutdoorExcretionLookup.RetrieveDinitrogenRatio(sector));
    }
    /// <summary>
    /// Method to calculate methane emissions from excreat deposited outdoors
    /// </summary>
    /// <param name="sector">Sector as enumerator</param>
    /// <param name="volatileSolids">Volatile solids in excreat depositded outdoors (kg)</param>
    /// <param name="layingHens">Boolean indiacting if laying hens as different B0 from other poultry</param>
    /// <returns>Methane emission (kg CH4)</returns>
    public static double CH4Emissions(Sector sector, int animaltype, double volatileSolids)
    {
        return GenericEquations.ManureManagementMethane(volatileSolids, MethaneLookup.RetrieveB0(sector, animaltype), OutdoorExcretionLookup.RetrieveMethaneConversionFactor(sector, animaltype), ManureManagementParameters.methanem3tokg);
    }
}
