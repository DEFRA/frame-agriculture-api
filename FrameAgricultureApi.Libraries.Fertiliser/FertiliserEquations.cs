using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Fertiliser;

/// <summary>
/// This is the Farm Level Emissions API Generic Equations class
/// It contains the generic equations that are called from other classes.
/// </summary>
public static class FertiliserEquations
{
    /// <summary>
    /// Equation for calcualtion of leached nitrate
    /// </summary>
    /// <param name="rateN">Nitrogen content of fertiliser applied (rate of application)</param>
    /// <param name="fracLeach">Percentage of N source leached as NO3</param>
    /// <returns>The kg NO3-N leached</returns>
    public static double NO3N_Leached(double rateN, double fracLeach) //Possibly use the generic equation in Component rather than have a specific equatoin here?
    {
        return GenericEquations.Emission_PercentageEF(rateN, fracLeach);
    }

    /// <summary>
    /// Calculation of emission of nitrous oxide from urea fertilisers based on Kairsty Topps analysis of experimental data
    /// The equation has to be called twice, once with fertiliser rate and then with a rate of zero (the latter is used to calcualte the background emission)
    /// and the difference between the two used to provide the emission of nitrous oxide due to fertilisers.
    /// IN addition, an uncertainty multiplier is used to reverse the log-normal transformation of the data
    /// </summary>
    /// <param name="Nrate">N application rate for fertiliser applied (kg/ha)</param>
    /// <param name="n2ON_ModelCoefficientUncertainty">Model uncertainty (unitless)</param>
    /// <returns>kg N2O-N emitted</returns>
    public static double N2ON_Urea(double Nrate = 0, double n2ON_ModelCoefficientUncertainty = 0)
    {
        return (Math.Exp(FertiliserEquationParameters.n2ON_Urea_Parameter_1 + FertiliserEquationParameters.n2ON_Urea_Parameter_2 * Nrate +
            n2ON_ModelCoefficientUncertainty) - FertiliserEquationParameters.n2ON_Urea_Parameter_3);
    }
    /// <summary>
    /// Calculation of emission of nitrous oxide from non-urea fertilisers based on Kairsty Topps analysis of experimental data
    /// The equation has to be called twice, once with fertiliser rate and then with a rate of zero (the latter is used to calcualte the background emission) and the different between the two used
    /// to provide the emission of nitrous oxide due to fertilisers.
    /// </summary>
    /// <param name="rainAnnualAverage">annual average rainfall at the location of the farm (10km grid square)</param>
    /// <param name="Nrate">N applicatoin rate for fertiliser applied  (kg/ha)</param>
    /// <param name="n2ON_ModelCoefficientUncertainty">Model uncertainty (unitless)</param>
    /// <returns>kg N2O-N emitted</returns>
    public static double N2ON_Other(double rainAnnualAverage, double Nrate = 0, double n2ON_ModelCoefficientUncertainty = 0)
    {
        return (Math.Exp(FertiliserEquationParameters.n2ON_Other_Parameter_1 +
            FertiliserEquationParameters.n2ON_Other_Parameter_2 * Math.Min(rainAnnualAverage, FertiliserEquationParameters.n2ON_Other_Parameter_MaxRainfall) / FertiliserEquationParameters.n2ON_Other_Parameter_Divisor -
            FertiliserEquationParameters.n2ON_Other_Parameter_3 * Nrate +
            FertiliserEquationParameters.n2ON_Other_Parameter_4 * Math.Min(rainAnnualAverage, FertiliserEquationParameters.n2ON_Other_Parameter_MaxRainfall) / FertiliserEquationParameters.n2ON_Other_Parameter_Divisor * Nrate +
            n2ON_ModelCoefficientUncertainty) - FertiliserEquationParameters.n2ON_Other_Paramter_5);
    }

    /// <summary>
    /// Function to return the base emissoin factor for ammonia based on fertiliser type
    /// </summary>
    /// <param name="fertiliser">fertiliser type</param>
    /// <returns>base ammonia emission factor as a percentage</returns>
    public static double BaseAmmoniaEF(FertiliserType fertiliser)
    {
        double retVal = double.NaN;

        switch(fertiliser)
        {
            case FertiliserType.Urea:
            case FertiliserType.AmmoniumSulphate_DiammoniumPhosphate:
                retVal = FertiliserEquationParameters.baseAmmoniaEF_Urea;
                break;

            case FertiliserType.UreaAmmoniumNitrate:
                retVal = FertiliserEquationParameters.baseAmmoniaEF_UAN;
                break;

            case FertiliserType.AmmoniumNitrate:
            case FertiliserType.CalciumAmmoniumNitrate:
            case FertiliserType.OtherNitrogenincludingCompoundBlends:
                retVal = FertiliserEquationParameters.baseAmmoniaED_Other;
                break;
        }

        return retVal;
    }

    /// <summary>
    /// Function to modify the ammonia emission factor based on the fertiliser rate.
    /// The rate is 0.62 times the base EF if the application rate us below 30kg/ha and then scales linearly to the full base EF at 150kg/ha.
    /// </summary>
    /// <param name="rateN">Rate of N applied (kg/ha)</param>
    /// <param name="currentAmmoniaEF">The current ammonia emission factor for the fertiliser type (%)</param>
    /// <returns>Updated ammonia emission factor (%)</returns>
    public static void AmmoniaEF_RateModification(double rateN, ref double currentAmmoniaEF)
    {
        double retVal = currentAmmoniaEF;

        if(rateN < 30.0)
        {
            currentAmmoniaEF *= FertiliserEquationParameters.fertiliserRateAmmoniaEFModifier_1;
        }
        else if(rateN <= 150.0 && rateN >= 30.0)
        {
            currentAmmoniaEF *= ((rateN * FertiliserEquationParameters.fertiliserRateAmmoniaEFModifier_3_Multiplier) + FertiliserEquationParameters.fertiliserRateAmmoniaEFModifier_3_Constant);
        }
        else
        {
            //Note that if rate is greater than 150 kg/ha then no amendment of the emission factor is needed
            //currentAmmoniaEF *= FertiliserEquationParameters.fertiliserRateAmmoniaEFModifier_2; //This would multiply by 1, but is not really needed
        }

        return;
    }
    /// <summary>
    /// Function to modify ammonia emission factor based on rainfall event probabilities
    /// </summary>
    /// <param name="rainfallEventLikelihood">The likelihood of a rainfall event occurring within days 1 to 6 after application of fetiliser</param>
    /// <param name="currentAmmoniaEF">the current emission factor for ammonia (%)</param>
    /// <returns>updated emission factor for ammonia (%)</returns>
    public static void AmmoniaEF_RainfallEventModification(double[] rainfallEventLikelihood, ref double currentAmmoniaEF)
    {
        double eventmultiplier = HelperFunctions.SumProduct(rainfallEventLikelihood, FertiliserEquationParameters.rainfallEventMultipliers);

        currentAmmoniaEF *= eventmultiplier;

        return;
    }
    /// <summary>
    /// Function to modify the ammonia emission factor based on temperature in the month relative to annual average UK temperature
    /// </summary>
    /// <param name="currentAmmoniaEF">the current ammonia emission factor (%)</param>
    /// <param name="temperatureMonth">the mean temperature in the month of applicatoin (degrees Celsius)</param>
    /// <returns>updated ammonia emission factor (%)</returns>
    public static void AmmoniaEF_TemperatureModification(ref double currentAmmoniaEF, double temperatureMonth)
    {
        currentAmmoniaEF *= Math.Max(FertiliserEquationParameters.AmmoniaEF_Temperature_FixedParameters[0],
             Math.Min(FertiliserEquationParameters.AmmoniaEF_Temperature_FixedParameters[1],
             FertiliserEquationParameters.AmmoniaEF_Temperature_FixedParameters[2] * Math.Exp(FertiliserEquationParameters.AmmoniaEF_TemperatureDifferrenceMultiplier *
                 (temperatureMonth - FertiliserEquationParameters.constantUKAverageAirTemperature))));
        return;
    }
    /// <summary>
    /// Ammmonia Emission Factor modification for non-alkaline soils if DAP
    /// </summary>
    /// <param name="currentAmmoniaEF">current ammonia emission factor (%)</param>
    /// <param name="acidic">boolean indicating if soil is non alkalline</param>
    /// <param name="fertiliser">fertiliser type</param>
    /// <returns></returns>
    public static void AmmoniaEF_SoilModification(ref double currentAmmoniaEF, bool acidic, FertiliserType fertiliser)
    {

        if(fertiliser.Equals(FertiliserType.AmmoniumSulphate_DiammoniumPhosphate) && acidic)
        { currentAmmoniaEF = 1.8; }

        return;

    }
    /// <summary>
    /// Ammonia emission from fertiliser application
    /// </summary>
    /// <param name="rateN">N applied in fertiliser (kg/ha)</param>
    /// <param name="ammoniaEF">Emission factor (%)</param>
    /// <param name="ammoniaModelCoefficientUNcertainty">UNcertainty factor (%)</param>
    /// <returns>kg NH3-N emitted</returns>
    public static double FertiliserAmmoniaEmission(double rateN, double ammoniaEF, double ammoniaModelCoefficientUNcertainty)
    {
        return Math.Min(100.0, Math.Max(0.0, ammoniaEF * 0.94 + ammoniaModelCoefficientUNcertainty)) * HelperFunctions.Percent_to_Proportion * rateN;
    }
}
