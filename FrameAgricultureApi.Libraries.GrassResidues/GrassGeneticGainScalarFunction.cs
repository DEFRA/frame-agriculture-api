using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.GrassResidues;

/// <summary>
/// Grass genetic improvement scalar function class
/// </summary>
public static class GrassGeneticGainScalarFunction
{
    /// <summary>
    /// Calculates the scalars required to represent genetic improvement
    /// </summary>
    /// <param name="grassType">Grass type (enumerator)</param>
    /// <param name="fertiliserRate">Rate of fertiliser applied (kg N per hectare)</param>
    /// <returns>Object containing the genetic improvement scalars</returns>
    public static GeneticGainScalars ReturnGeneticGainScalars(GrassType grassType, double fertiliserRate)
    {
        GeneticGainScalars myScalars = new();

        //Only need to calculate scalars if improved grass
        if(grassType.Equals(GrassType.ImprovedPermanent) || grassType.Equals(GrassType.ImprovedTemporary))
        {
            myScalars.GeneticAllFracLeachScalar = GrassGeneticGainScalarEquations.GeneticFracLeachScalar(grassType,
                GrassGeneticGainScalarEquations.GeneticAllFracLeachReduction(fertiliserRate));
            myScalars.GeneticGrazedFracLeachScalar = myScalars.GeneticAllFracLeachScalar;

            myScalars.GeneticManuredFracLeachScalar = GrassGeneticGainScalarEquations.GeneticFracLeachScalar(grassType,
                GrassGeneticGainScalarEquations.GeneticManuredFracLeachReduction(fertiliserRate));

            myScalars.GeneticYieldDilutionScalar = GrassGeneticGainScalarEquations.GeneticYieldDilutionScalar(grassType,
                GrassGeneticGainScalarEquations.Correction(fertiliserRate, grassType));

            myScalars.GeneticYieldScalar = GrassGeneticGainScalarEquations.GeneticYieldScalar(grassType,
                GrassGeneticGainScalarEquations.Correction(fertiliserRate, grassType));

            myScalars.GeneticNitrogenUptakeScalar = GrassGeneticGainScalarEquations.GeneticNitrogenUptakeScalar(grassType,
                GrassGeneticGainScalarEquations.Correction(fertiliserRate, grassType));
        }
        return myScalars;
    }
}
/// <summary>
/// Genetic Gain Scalar Object
/// </summary>
public class GeneticGainScalars
{
    public double GeneticAllFracLeachScalar { get; set; }
    public double GeneticYieldDilutionScalar { get; set; }
    public double GeneticYieldScalar { get; set; }
    public double GeneticNitrogenUptakeScalar { get; set; }
    public double GeneticGrazedFracLeachScalar { get; set; }
    public double GeneticManuredFracLeachScalar { get; set; }

    public GeneticGainScalars()
    {
        GeneticAllFracLeachScalar = 1.0;
        GeneticNitrogenUptakeScalar = 1.0;
        GeneticYieldDilutionScalar = 1.0;
        GeneticYieldScalar = 1.0;
        GeneticGrazedFracLeachScalar = 1.0;
        GeneticManuredFracLeachScalar = 1.0;
    }
}
