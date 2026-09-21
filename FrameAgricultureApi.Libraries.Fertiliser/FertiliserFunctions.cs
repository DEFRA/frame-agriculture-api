using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Fertiliser;

/// <summary>
/// Fertiliser functions class
/// </summary>
public static class FertiliserFunctions
{

    /// <summary>
    /// Nitrous Oxide emission from urea-based fertilisers
    /// </summary>
    /// <param name="rateN">N applied (kg/ha)</param>
    /// <param name="uncertainty">Optional uncertainty value (no units) with defualt of 0</param>
    /// <returns>kg N2O-N emitted</returns>
    public static double CalculateN2ON_UreaBasedFertiliser(double rateN, double uncertainty = 0)
    {
        double withFertiliser = FertiliserEquations.N2ON_Urea(rateN, uncertainty);
        double withoutFertiliser = FertiliserEquations.N2ON_Urea(0, uncertainty);

        return (withFertiliser - withoutFertiliser) * FertiliserEquationParameters.n20N_Urea_lognormaltransformScalar;
    }
    /// <summary>
    /// Nitrous oxide emissions from non-urea-based fertilisers
    /// </summary>
    /// <param name="rateN">N applied (kg/ha)</param>
    /// <param name="annualAverageRainfall">Annual avergae rianfall (mm)</param>
    /// <param name="uncertainty">Optional uncertainty value (no units) with defualt of 0</param>
    /// <returns>kg N2O-N emitted</returns>
    public static double CalculateN2ON_nonUreaBasedFertiliser(double rateN, double annualAverageRainfall, double uncertainty = 0)
    {
        double withFertiliser = FertiliserEquations.N2ON_Other(annualAverageRainfall, rateN, uncertainty);
        double withoutFertiliser = FertiliserEquations.N2ON_Other(annualAverageRainfall, 0, uncertainty);

        return (withFertiliser - withoutFertiliser) * FertiliserEquationParameters.n20N_Other_lognormaltransformScalar;
    }

    /// <summary>
    /// Fertiliser Ammonia Emission Factor
    /// </summary>
    /// <param name="rateN">Napplied (kg/ha)</param>
    /// <param name="temperatureMonth">Monthly average temperature for location</param>
    /// <param name="acidicSoil">Boolean indicating if the soil is non-alkaline</param>
    /// <param name="RainfallLikelihood">Array (double) of likelihood that a rainfall event occurs in the 6 days following a fertiliser application</param>
    /// <param name="fertiliser">Fertiliser type (enum)</param>
    /// <returns>Ammonia emission factor (% of N applied)</returns>
    public static double CalculateFertiliserAmmoniaEmissionFactor(double rateN, double temperatureMonth, bool acidicSoil, double[] RainfallLikelihood, FertiliserType fertiliser)
    {
        //set base EF
        double currentEF = FertiliserEquations.BaseAmmoniaEF(fertiliser);

        //apply modifiers
        switch(fertiliser)
        {
            case FertiliserType.AmmoniumNitrate:
            case FertiliserType.CalciumAmmoniumNitrate:
            case FertiliserType.OtherNitrogenincludingCompoundBlends:
                //do nothing
                break;

            case FertiliserType.Urea:
            case FertiliserType.UreaAmmoniumNitrate:
            case FertiliserType.AmmoniumSulphate_DiammoniumPhosphate:
                FertiliserEquations.AmmoniaEF_RateModification(rateN, ref currentEF);
                FertiliserEquations.AmmoniaEF_RainfallEventModification(RainfallLikelihood, ref currentEF);
                FertiliserEquations.AmmoniaEF_TemperatureModification(ref currentEF, temperatureMonth);
                FertiliserEquations.AmmoniaEF_SoilModification(ref currentEF, acidicSoil, fertiliser);
                break;
        }

        return currentEF;
    }
}
