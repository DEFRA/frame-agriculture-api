using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.Libraries.CropResidues;

/// <summary>
/// This is the Farm Level Emissions API Generic Equations class
/// It contains the generic equations that are called from other classes.
/// </summary>
public static class CropResidueEquations
{
    /// <summary>
    /// Calculate crop primary dry matter yield
    /// </summary>
    /// <param name="cropYieldPrimaryFW">Crop yield as fresh weight (t FW ha)</param>
    /// <param name="cropYieldPrimaryDM">Crop primary dry matter (%)</param>
    /// <returns>Crop dry matter yield (t DM ha)</returns>
    public static double CalculateYieldDryMatter(double cropYieldPrimaryFW, double cropYieldPrimaryDM)
    {
        return cropYieldPrimaryFW * cropYieldPrimaryDM * HelperFunctions.Percent_to_Proportion;
    }

    /// <summary>
    /// Calculate above ground dry matter residue using the harvest index method
    /// </summary>
    /// <param name="FunctionalHarvestIndex">Functional Harvest Index as proportion</param>
    /// <param name="CropYieldDryMatter">Crop primary dry matter yield (t DM ha)</param>
    /// <returns>Above ground dry matter residue (t DM ha)</returns>
    public static double CalculateAboveGroundResidueDryMatterHarvestIndex(double FunctionalHarvestIndex, double CropYieldDryMatter)
    {
        return FunctionalHarvestIndex * CropYieldDryMatter;
    }

    /// <summary>
    /// Calculate above ground dry matter residue using the IPCC method
    /// </summary>
    /// <param name="CropYieldToAboveGroundResidueSlope">IPCC crop yield to above ground residue slope (dimensionless)</param>
    /// <param name="CropYieldDryMatter">Crop primary dry matter yield (t DM ha)</param>
    /// <param name="CropYieldToAboveGroundResidueIntercept">IPCC crop yield to above ground residue intercept (dimensionless)</param>
    /// <returns>Above ground dry matter residue (t DM ha)</returns>
    public static double CalculateAboveGroundResidueDryMatterIPCC(double CropYieldDryMatter, double CropYieldToAboveGroundResidueSlope, double CropYieldToAboveGroundResidueIntercept)
    {
        return (CropYieldToAboveGroundResidueSlope * CropYieldDryMatter) + CropYieldToAboveGroundResidueIntercept;
    }

    /// <summary>
    /// Calculate total above ground residue based on fate of the residue
    /// </summary>
    /// <param name="AboveGroundResidueDryMatter">Above ground dry matter (t DM ha)</param>
    /// <param name="ResidueRemaining">Proportion of residue retained in field (100% if incorporated, less if baled and removed) from lookup at component level</param>
    /// <returns>Total above ground residue dry matter (t DM ha)</returns>
    public static double CalculateAboveGroundResidue(double AboveGroundResidueDryMatter, double ResidueRemaining)
    {
        return AboveGroundResidueDryMatter * (ResidueRemaining * HelperFunctions.Percent_to_Proportion);
    }

    /// <summary>
    /// Calculate dry matter in below ground residue and converts this to nitrogen in below ground residue
    /// </summary>
    /// <param name="CropYieldDryMatter">Crop primary dry matter yield (t DM ha)</param>
    /// <param name="AboveGroundResidueDryMatter">Above ground dry matter (t DM ha)</param>
    /// <param name="AboveToBelowGroundResidueRatio">Above to below ground residue ratio (ratio)</param>
    /// <returns>Nitrogen in below ground residue (kg N ha)</returns>
    public static double CalculateBelowGroundResidueDryMatter(double CropYieldDryMatter, double AboveGroundResidueDryMatter,
        double AboveToBelowGroundResidueRatio)
    {
        double TotalBelowGroundResidue = (CropYieldDryMatter + AboveGroundResidueDryMatter) * AboveToBelowGroundResidueRatio;
        return TotalBelowGroundResidue;
    }

    /// <summary>
    /// Calculate nitrogen (N) in the residues
    /// </summary>
    /// <param name="TotalResidue">Total residue dry matter (t DM ha) for above or below ground resiudes</param>
    /// <param name="ResidueNConcentration">Concentration of nitrogen in residue (kg N /t DM)</param>
    /// <returns>Nitrogen in residue (kg N/ha)</returns>
    public static double CalculateResidueN(double TotalResidue, double ResidueNConcentration)
    {
        return TotalResidue * ResidueNConcentration;
    }

    /// <summary>
    /// Calculate the No3N leached from residue N
    /// </summary>
    /// <param name="totalResidueN">The kg N per hectare in crop residues</param>
    /// <param name="fracLeach">the fraction of N leached (%)</param>
    /// <returns>kg NO3-N leached</returns>
    public static double NO3N_Leached(double totalResidueN, double fracLeach)
    {
        return GenericEquations.Emission_PercentageEF(totalResidueN, fracLeach);
    }
    /// <summary>
    /// Calculates the ammonia emission from decompostion of residues
    /// </summary>
    /// <param name="aboveGroundResidueDryMatterNitrogen">The nitrogen in above ground residue dry matter remainign in the field (kg)</param>
    /// <param name="aboveGroundResidueNitrogenContent">The nitrogen content in above ground residues for the crop type (kg / kg DM)</param>
    /// <param name="residueContributionAmmonia">The proportional contribution of the residues to ammonia emission</param>
    /// <returns></returns>
    public static double NH3N_ResidueDecomposition(double aboveGroundResidueDryMatterNitrogen, double aboveGroundResidueNitrogenContent, double residueContributionAmmonia)
    {
        double tmp_Ammonia = 0.0;

        if(residueContributionAmmonia > 0.0)
        {
            tmp_Ammonia = aboveGroundResidueDryMatterNitrogen * Math.Max(0.0, CropResidueEquationParameters.residueNitrogenEmittedEquationSlope * aboveGroundResidueNitrogenContent + CropResidueEquationParameters.residueNitrogenEmittedEquationconstant) * HelperFunctions.Percent_to_Proportion;
            tmp_Ammonia *= residueContributionAmmonia;
        }
        return tmp_Ammonia;
    }
}
