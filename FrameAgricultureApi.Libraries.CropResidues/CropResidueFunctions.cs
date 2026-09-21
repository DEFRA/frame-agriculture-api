using FrameAgricultureApi.CustomErrorHandling;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.CropResidues;

/// <summary>
/// Crop residue functions class
/// </summary>
public static class CropResidueFunctions
{
    /// <summary>
    /// Calculation of the kg N in above ground residues remaining in the field
    /// </summary>
    /// <param name="cropYieldFreshWeight">tonnes fresh weight yield</param>
    /// <param name="cropPrimaryDryMatter">percent dry matter</param>
    /// <param name="functionalHarvestIndex">the functional harevest index = (1-HI)/HI</param>
    /// <param name="cropType">enumerator indicating crop type</param>
    /// <returns>N content of above ground residues (kg N/ha)</returns>
    public static double CalculateAboveGroundResidueDryMatter(double cropYieldFreshWeight, double cropPrimaryDryMatter, double functionalHarvestIndex,
         CropType cropType)
    {
        // Calculate crop primary yield dry matter
        double primaryYieldDryMatter = CropResidueEquations.CalculateYieldDryMatter(cropYieldFreshWeight, cropPrimaryDryMatter);

        // Calculate above ground residue dry matter
        double aboveGroundResidueDryMatter = double.NaN;
        if(functionalHarvestIndex > 0)
        {
            aboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidueDryMatterHarvestIndex(functionalHarvestIndex, primaryYieldDryMatter);
        }
        else
        {
            throw new CustomAppException("Functional Harvest Index is not avalable for " + cropType.ToString() + ". Please provide a crop harvest index.");
        }

        return aboveGroundResidueDryMatter;

    }

    /// <summary>
    /// Calculation of the kg N in above ground residues remaining in the field
    /// </summary>
    /// <param name="cropYieldFreshWeight">tonnes fresh weight yield</param>
    /// <param name="cropPrimaryDryMatter">percent dry matter</param>
    /// <param name="slopeIPCC">the slope of the IPCC equation to calcualte above ground residues from yield</param>
    /// <param name="interceptIPCC">the intercept of the IPCC equation to calcualte above ground residues from yield</param>
    /// <param name="cropType">enumerator indicating crop type</param>
    /// <returns>N content of above ground residues (kg N/ha)</returns>
    public static double CalculateAboveGroundResidueDryMatter(double cropYieldFreshWeight, double cropPrimaryDryMatter,
        double slopeIPCC, double interceptIPCC, CropType cropType)
    {
        // Calculate crop primary yield dry matter
        double primaryYieldDryMatter = CropResidueEquations.CalculateYieldDryMatter(cropYieldFreshWeight, cropPrimaryDryMatter);

        // Calculate above ground residue dry matter
        double aboveGroundResidueDryMatter = double.NaN;

        if(double.IsNaN(slopeIPCC) || double.IsNaN(interceptIPCC))
        {
            throw new CustomAppException("IPCC intercept and slopes are not avalable for " + cropType.ToString() + ". Please provide a crop harvest index.");
        }
        else
        {
            // Above ground residue dry matter (IPCC method)
            aboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidueDryMatterIPCC(primaryYieldDryMatter, slopeIPCC, interceptIPCC);
        }
        return aboveGroundResidueDryMatter;

    }

    //Emissions themselves can be calculated from generic equations so are not needed here as the equations can be called directly from the Component class
}
