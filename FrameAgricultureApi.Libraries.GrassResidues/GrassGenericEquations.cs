namespace FrameAgricultureApi.Libraries.GrassResidues;

public static class GrassGenericEquations
{
    /// <summary>
    /// Fourth order polynomial equation (spline from offline grassmodel)
    /// </summary>
    /// <param name="Constant">Constant parameter</param>
    /// <param name="Coeff_X">Multiplier for the X parameter</param>
    /// <param name="Coeff_X2">Multiplier for the square of the X parameter</param>
    /// <param name="Coeff_X3">Multiplier for the cube of the X parameter</param>
    /// <param name="Coeff_X4">Multiplier for the fourht power of the X paramater</param>
    /// <param name="X">The X parameter (usually fertiliser application rate in kg N per hectare)</param>
    /// <returns>double value</returns>
    public static double Grass_QuarticPolynomial(double Constant, double Coeff_X, double Coeff_X2, double Coeff_X3, double Coeff_X4, double X)
    {
        double Y = Constant + (Coeff_X * X) + (Coeff_X2 * Math.Pow(X, 2)) + (Coeff_X3 * Math.Pow(X, 3)) + (Coeff_X4 * Math.Pow(X, 4));

        return Y;
    }
}
