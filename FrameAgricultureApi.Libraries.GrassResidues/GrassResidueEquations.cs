using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.Libraries.GrassResidues;

public static class GrassResidueEquations
{
    /// <summary>
    /// Nitrogen in grasss residues at renewal
    /// </summary>
    /// <param name="C">The constant of the polynomial equation</param>
    /// <param name="Coeff_X">The coefficient for the fertiliser rate</param>
    /// <param name="Coeff_X2">The coefficient for the square of the fertiliser rate</param>
    /// <param name="Coeff_X3">The coefficient for the cube of the fertiliser rate</param>
    /// <param name="Coeff_X4">The coefficient for the fourth power of the fertiliser rate</param>
    /// <param name="FertiliserRate">Rate of Fertiliser applied (kg N/ha)</param>
    /// <param name="Uncertainty">Optional multiplicative uncertainty parameter (default 100.0)</param>
    /// <returns>N content of residues (kg N/ha)</returns>
    public static double ResidualNitrogen(double C, double Coeff_X, double Coeff_X2, double Coeff_X3, double Coeff_X4, double FertiliserRate, double Uncertainty = 100.0)
    {
        double retval = 0.0;
        retval = Math.Max(0.0, GrassGenericEquations.Grass_QuarticPolynomial(C, Coeff_X, Coeff_X2, Coeff_X3, Coeff_X4, FertiliserRate))
            * (Uncertainty / 100.0);
        return retval;
    }

    /// <summary>
    /// Calcualtion of Frac Leach for grass residues
    /// </summary>
    /// <param name="C">The constant of the polynomial equation</param>
    /// <param name="Coeff_X">The coefficient for the fertiliser rate</param>
    /// <param name="Coeff_X2">The coefficient for the square of the fertiliser rate</param>
    /// <param name="Coeff_X3">The coefficient for the cube of the fertiliser rate</param>
    /// <param name="Coeff_X4">The coefficient for the fourth power of the fertiliser rate</param>
    /// <param name="FertiliserRate">Rate of fertiliser applied (kg N/ha)</param>
    /// <param name="Uncertainty">Optional additive uncertainty parameter (default 0.0)</param>
    /// <returns>The percentage (%) of residue nitrogen that is leached</returns>
    public static double GrassFracLeach(double C, double Coeff_X, double Coeff_X2, double Coeff_X3, double Coeff_X4, double FertiliserRate, double Uncertainty = 0.0)
    {
        double retval = 0.0;
        retval = Math.Max(0.0, Math.Min(100.0, GrassGenericEquations.Grass_QuarticPolynomial(C, Coeff_X, Coeff_X2, Coeff_X3, Coeff_X4, FertiliserRate))) + (Uncertainty / 100.0);
        return retval;
    }
    /// <summary>
    /// Dry matter calculation
    /// </summary>
    /// <param name="C">The constant of the polynomial equation</param>
    /// <param name="Coeff_X">The coefficient for the fertiliser rate</param>
    /// <param name="Coeff_X2">The coefficient for the square of the fertiliser rate</param>
    /// <param name="Coeff_X3">The coefficient for the cube of the fertiliser rate</param>
    /// <param name="Coeff_X4">The coefficient for the fourth power of the fertiliser rate</param>
    /// <param name="FertiliserRate">Rate of fertiliser applied (kg N/ha)</param>
    /// <returns>Dry matter (yield, above ground or below ground, depending on coefficients) in kg per hectare</returns>
    public static double GrassDryMatter(double C, double Coeff_X, double Coeff_X2, double Coeff_X3, double Coeff_X4, double FertiliserRate)
    {
        double retval = 0.0;
        retval = Math.Max(0.0, GrassGenericEquations.Grass_QuarticPolynomial(C, Coeff_X, Coeff_X2, Coeff_X3, Coeff_X4, FertiliserRate));

        return retval;
    }
    /// <summary>
    /// Nitrogen in grass harvested calculation
    /// </summary>
    /// <param name="C">The constant of the polynomial equation</param>
    /// <param name="Coeff_X">The coefficient for the fertiliser rate</param>
    /// <param name="Coeff_X2">The coefficient for the square of the fertiliser rate</param>
    /// <param name="Coeff_X3">The coefficient for the cube of the fertiliser rate</param>
    /// <param name="Coeff_X4">The coefficient for the fourth power of the fertiliser rate</param>
    /// <param name="FertiliserRate">Rate of fertiliser applied (kg N/ha)</param>
    /// <returns>Nitrogen content of harvested grass in kg per hectare</returns>
    public static double GrassHarvestedNitrogen(double C, double Coeff_X, double Coeff_X2, double Coeff_X3, double Coeff_X4, double FertiliserRate)
    {
        double retval = 0.0;
        retval = Math.Max(0.0, GrassGenericEquations.Grass_QuarticPolynomial(C, Coeff_X, Coeff_X2, Coeff_X3, Coeff_X4, FertiliserRate));
        return retval;
    }
    /// <summary>
    /// Nitrogen from clover fixation calculation
    /// </summary>
    /// <param name="C">The constant of the polynomial equation</param>
    /// <param name="Coeff_X">The coefficient for the fertiliser rate</param>
    /// <param name="Coeff_X2">The coefficient for the square of the fertiliser rate</param>
    /// <param name="Coeff_X3">The coefficient for the cube of the fertiliser rate</param>
    /// <param name="Coeff_X4">The coefficient for the fourth power of the fertiliser rate</param>
    /// <param name="FertiliserRate">Rate of fertiliser applied (kg N/ha)</param>
    /// <returns>Nitrogen from clover fixation in kg per hectare</returns>
    public static double GrassNitrogenFromClover(double C, double Coeff_X, double Coeff_X2, double Coeff_X3, double Coeff_X4, double FertiliserRate)
    {
        double retval = 0.0;
        retval = Math.Max(0.0, GrassGenericEquations.Grass_QuarticPolynomial(C, Coeff_X, Coeff_X2, Coeff_X3, Coeff_X4, FertiliserRate));
        return retval;
    }
    /// <summary>
    /// Calculation of residue nitrogen content
    /// </summary>
    /// <param name="grassYieldDryMatter">Dry matter yield of grass removed (kg/ha)</param>
    /// <param name="nitrogenInGrassHarvested">Nitrogen content of grass removed (kg/ha)</param>
    /// <param name="geneticYieldDilutionScalar">Yield dilution scalar</param>
    /// <returns>Nitrogen content of residues (%)</returns>
    public static double ResidueNitrogenContent(double grassYieldDryMatter, double nitrogenInGrassHarvested, double geneticYieldDilutionScalar)
    {
        double residueNitrogenContent = 0.0;
        if(grassYieldDryMatter > 0.0)
        {

            residueNitrogenContent = (nitrogenInGrassHarvested * GrassResidueParameters.kgperkgToPercent / grassYieldDryMatter) * geneticYieldDilutionScalar * GrassResidueParameters.percentToKilogramspertonne;
        }

        return residueNitrogenContent;
    }
    /// <summary>
    /// Calculation of ammonia emitted by residues
    /// </summary>
    /// <param name="residueNitrogenContent">nitrogen content of residues (%)</param>
    /// <returns>Ammonia emission factor (propotion)</returns>
    public static double ResidueAmmoniaEmissionFactor(double residueNitrogenContent)
    {
        double tmp_residueDirectAmmonia = 0.0;
        tmp_residueDirectAmmonia = Math.Max(0.0, GrassResidueParameters.residueNitrogenEmittedEquationSlope * residueNitrogenContent + GrassResidueParameters.residueNitrogenEmittedEquationconstant) * HelperFunctions.Percent_to_Proportion;
        return tmp_residueDirectAmmonia;
    }
}

