namespace FrameAgricultureApi.Generic;

/// <summary>
/// Generic Equation class containing the genric equations used within the UK GHG AEIA calcualtions.
/// </summary>
public static class GenericEquations
{
    /// <summary>
    /// Generic Emission Equation
    /// </summary>
    /// <param name="source">Amount of source (N, Carbon, etc) from which to calcualte emission (kg)</param>
    /// <param name="EF">Emission factor as a percentage</param>
    /// <param name="Uncertainty">Uncertainty associated with emissions as a percentage</param>
    /// <returns>kg (N2O-N, NH4, CH4, etc) emitted</returns>
    public static double Emission_PercentageEF(double source, double EF, double Uncertainty = 100.0)
    {
        return source * (EF * HelperFunctions.Percent_to_Proportion) * (Uncertainty * HelperFunctions.Percent_to_Proportion);
    }
    /// <summary>
    /// Generic Emission Equation
    /// </summary>
    /// <param name="source">Amount of source (N, Carbon, etc) from which to calcualte emission (kg)</param>
    /// <param name="EF">Emission factor as a proportion</param>
    /// <param name="Uncertainty">Unceertainty associated with emissions as a percentage</param>
    /// <returns>kg (N2O-N, NH4, CH4, etc) emitted</returns>
    public static double Emission_ProportionEF(double source, double EF, double Uncertainty = 100.0)
    {
        return source * EF * (Uncertainty * HelperFunctions.Percent_to_Proportion);
    }

    /// <summary>
    /// Generic ratio equation
    /// </summary>
    /// <param name="source">kg source</param>
    /// <param name="ratio">ratio of source to output</param>
    /// <returns>kg output</returns>
    public static double SourceOutputRatio(double source, double ratio) { return source * ratio; }
    /// <summary>
    /// Equation to calculate Y for simple regression line Y = mX + C
    /// </summary>
    /// <param name="C">the intercept or constant for the line</param>
    /// <param name="m">the gradient or slope of the line</param>
    /// <param name="X">the X value or variable value</param>
    /// <returns></returns>
    public static double SimpleLinear(double C, double m, double X)
    {
        return ((m * X) + C);
    }
    /// <summary>
    /// Equation to calculate methane emission from manure management
    /// </summary>
    /// <param name="volatileSolids">Volatile solids (kg) in excreta/manure deposited in the unit time</param>
    /// <param name="B0">Methane potential of excreta or manure (m3 per kg VS)</param>
    /// <param name="methaneConversionFactor">Methane conversion factor for excreta or manure</param>
    /// <param name="m3tokg">Conversion factor to convert m3 to kg</param>
    /// <returns>Methane emission (CH4) in kg per unit time</returns>
    public static double ManureManagementMethane(double volatileSolids, double B0, double methaneConversionFactor, double m3tokg)
    {
        return volatileSolids * B0 * methaneConversionFactor * m3tokg * HelperFunctions.Percent_to_Proportion;
    }
}
