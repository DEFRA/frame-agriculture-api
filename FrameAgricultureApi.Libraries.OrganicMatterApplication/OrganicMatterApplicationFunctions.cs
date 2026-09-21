using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.OrganicMatterApplication;

public static class OrganicMatterApplicationFunctions
{
    /// <summary>
    /// Returns NH3-N emission from applciation of organic matter to arable or grass crops
    /// </summary>
    /// <param name="type">The organic matter type (enumerator)</param>
    /// <param name="sourceN">The N content of the organic matter applied (kg/ha)</param>
    /// <param name="landAppliedTo>Land use that organic matter is applied to (either arable or grass)</param>
    /// <param name="month">Month of application</param>
    /// <returns>Nh3-N emission (kg/ha)</returns>
    public static double NH3NEmission(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month)
    {
        return GenericEquations.Emission_PercentageEF(sourceN, OrganicMatterSpreadingLookup.RetrieveNH3NEF(type, landAppliedTo, month));
    }
    /// <summary>
    /// Returns N2O-N emission fromapplication of organic matter to arable or grass crops
    /// </summary>
    /// <param name="type">The organic matter type (enumerator)</param>
    /// <param name="sourceN">The N content of the organic matter applied</param>
    /// <param name="landAppliedTo>Land use that organic matter is applied to (either arable or grass)</param>
    /// <param name="month">Month of application</param>
    /// <returns>N2O-N emission (kg/ha)</returns>
    public static double N2ONEmission(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month)
    {
        return GenericEquations.Emission_PercentageEF(sourceN, OrganicMatterSpreadingLookup.RetrieveN2ONEF(type, landAppliedTo, month));
    }
    /// <summary>
    ///  Returns NO3-N emission fromapplication of organic matter to arable or grass crops
    /// </summary>
    /// <param name="type">The organic matter type (enumerator)</param>
    /// <param name="sourceN">The N content of the organic matter applied</param>
    /// <param name="landAppliedTo>Land use that organic matter is applied to (either arable or grass)</param>
    /// <param name="month">Month of application</param>
    /// <returns>NO3-N emission (kg/ha)</returns>
    public static double NO3NLeaching(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month)
    {
        return GenericEquations.Emission_PercentageEF(sourceN, OrganicMatterSpreadingLookup.RetrieveFracLeach(type, landAppliedTo, month));
    }
    /// <summary>
    ///  Returns NO-N emission fromapplication of organic matter to arable or grass crops
    /// </summary>
    /// <param name="type">The organic matter type (enumerator)</param>
    /// <param name="sourceN">The N content of the organic matter applied</param>
    /// <param name="landAppliedTo>Land use that organic matter is applied to (either arable or grass)</param>
    /// <param name="month">Month of application</param>
    /// <returns>NO-N emission (kg/ha)</returns>
    public static double NONEmission(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month)
    {
        return GenericEquations.Emission_ProportionEF(sourceN, OrganicMatterSpreadingLookup.RetrieveNONRatio(type, landAppliedTo, month));
    }
    /// <summary>
    ///  Returns N2-N emission fromapplication of organic matter to arable or grass crops
    /// </summary>
    /// <param name="type">The organic matter type (enumerator)</param>
    /// <param name="sourceN">The N content of the organic matter applied</param>
    /// <param name="landAppliedTo>Enumerator indicating land use that organic matter is applied to (either arable or grass)</param>
    /// <param name="month">Month of application</param>
    /// <returns>N2-N emission (kg/ha)</returns>
    public static double N2NEmission(OrganicMatterType type, double sourceN, SpreadingLandUse landAppliedTo, Month month)
    {
        return GenericEquations.Emission_ProportionEF(sourceN, OrganicMatterSpreadingLookup.RetrieveN2NRatio(type, landAppliedTo, month));
    }
}
