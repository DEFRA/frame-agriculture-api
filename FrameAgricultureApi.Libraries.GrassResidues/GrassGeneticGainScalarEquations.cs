using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.GrassResidues;

/// <summary>
/// Grass genetic gain scalar equations class
/// </summary>
public static class GrassGeneticGainScalarEquations
{
    /// <summary>
    /// proportional increae in nitrogen uptake due to genetic gains parameter
    /// </summary>
    private static double tmp_GeneticIncreaseNitrogenUptake = GrassGeneticGainParameters.geneticIncreaseNitrogenUptake;
    /// <summary>
    /// Proportion increase in yield due to genetic gains parameter
    /// </summary>
    private static double tmp_GeneticIncreaseYield = GrassGeneticGainParameters.geneticIncreaseYield;
    /// <summary>
    /// Function to calculate the reduction in frac leach due to genetic gain
    /// </summary>
    /// <param name="fertiliserRate">rate of fertiliser applied (kg N per hectare)</param>
    /// <returns>Proportional reduction in frac leach</returns>
    public static double GeneticAllFracLeachReduction(double fertiliserRate)
    {
        return HelperFunctions.Percent_to_Proportion * (2.0 + (fertiliserRate > 50.0 ? 6.0 : 6.0 * fertiliserRate / 50.0));
    }
    /// <summary>
    /// Function to calculate the reduction in frac leach due to genetic gain for manure applications (sheep)
    /// </summary>
    /// <param name="fertiliserRate">rate of fertiliser applied (kg N per hectare)</param>
    /// <returns>Proportional reduction in frac leach</returns>
    public static double GeneticManuredFracLeachReduction(double fertiliserRate)
    {
        return HelperFunctions.Percent_to_Proportion * (2.0 + (fertiliserRate > 50.0 ? 3.0 : 3.0 * fertiliserRate / 50.0));
    }
    /// <summary>
    /// Calculation of scalar for frac leach due to genetic gain
    /// </summary>
    /// <param name="typeGrass">Grass type (enumerator)</param>
    /// <param name="tmp_GeneticAllFracLeachReduction">Proportional reduction in frac leach due to genetic gain</param>
    /// <returns>Scalar for frac leach</returns>
    public static double GeneticFracLeachScalar(GrassType typeGrass, double tmp_GeneticAllFracLeachReduction)
    {
        if(typeGrass.Equals(GrassType.ImprovedTemporary))
        {
            return 1.0 - tmp_GeneticAllFracLeachReduction * tmp_GeneticIncreaseNitrogenUptake / 10.0;
        }
        else if(typeGrass.Equals(GrassType.ImprovedPermanent))
        {
            return 1.0 - 0.5 * tmp_GeneticAllFracLeachReduction * tmp_GeneticIncreaseNitrogenUptake / 10.0;
        }
        else
        {
            return 1.0; //No scaling
        }
    }
    /// <summary>
    /// Calculation of genetic yield dilution correction factor for estimate of rygrass in sward
    /// </summary>
    /// <param name="fertiliserRate">Rate of fertilsier applied (kg N per hectare)</param>
    /// <param name="typeGrass">Grass type (enumerator)</param>
    /// <returns>Correction factor</returns>
    public static double Correction(double fertiliserRate, GrassType typeGrass)
    {
        if(typeGrass.Equals(GrassType.ImprovedTemporary))
        {
            return 1.0 - 0.156 * Math.Exp(-0.00567 * fertiliserRate);
        }
        else if(typeGrass.Equals(GrassType.ImprovedPermanent))
        {
            return 1.0 - 0.078 * Math.Exp(-0.00600 * fertiliserRate);
        }
        else
        {
            return 1.0; //No scaling
        }
    }
    /// <summary>
    /// Calculation of yield dilution scalar due to genetic gain
    /// </summary>
    /// <param name="tmp_Correction">Correction factor for proporiton of ryegrass in sward</param>
    /// <returns>Yield dilution scalar</returns>
    public static double GeneticYieldDilutionScalar(GrassType grassType, double tmp_Correction)
    {
        double retval = 1.0;
        if(grassType.Equals(GrassType.ImprovedTemporary))
        {
            retval = (100.0 + tmp_GeneticIncreaseNitrogenUptake * tmp_Correction * 0.15) / (100.0 + tmp_GeneticIncreaseYield * tmp_Correction * 0.3);
        }
        else if(grassType.Equals(GrassType.ImprovedPermanent))
        {
            retval = (100.0 + 0.5 * tmp_GeneticIncreaseNitrogenUptake * tmp_Correction * 0.15) / (100.0 + 0.5 * tmp_GeneticIncreaseYield * tmp_Correction * 0.3);
        }

        return retval;
    }
    /// <summary>
    /// Calcualtion of scalar for yield due to genetic improvement
    /// </summary>
    /// <param name="typeGrass">Grass type (enumerator)</param>
    /// <param name="tmp_Correction">Correction factor for proportion of ryegrass in sward</param>
    /// <returns>Yield scalar</returns>
    public static double GeneticYieldScalar(GrassType typeGrass, double tmp_Correction)
    {
        if(typeGrass.Equals(GrassType.ImprovedTemporary))
        {
            return 1.0 + HelperFunctions.Percent_to_Proportion * tmp_GeneticIncreaseYield * tmp_Correction;
        }
        else if(typeGrass.Equals(GrassType.ImprovedPermanent))
        {
            return 1.0 + 0.5 * HelperFunctions.Percent_to_Proportion * tmp_GeneticIncreaseYield * tmp_Correction;
        }
        else
        {
            return 1.0; //No scaling
        }
    }
    /// <summary>
    /// Calculation of nitrogen uptake scalar due to geentic improvement
    /// </summary>
    /// <param name="typeGrass">Grass type (enumerator)</param>
    /// <param name="tmp_Correction">Correction factor for proportion of ryegrass in sward</param>
    /// <returns>Nitrogen uptake scalar</returns>
    public static double GeneticNitrogenUptakeScalar(GrassType typeGrass, double tmp_Correction)
    {
        if(typeGrass.Equals(GrassType.ImprovedTemporary))
        {
            return 1.0 + HelperFunctions.Percent_to_Proportion * tmp_GeneticIncreaseNitrogenUptake * tmp_Correction;
        }
        else if(typeGrass.Equals(GrassType.ImprovedPermanent))
        {
            return 1.0 + 0.5 * HelperFunctions.Percent_to_Proportion * tmp_GeneticIncreaseNitrogenUptake * tmp_Correction;
        }
        else
        {
            return 1.0; //No scaling
        }
    }
}
