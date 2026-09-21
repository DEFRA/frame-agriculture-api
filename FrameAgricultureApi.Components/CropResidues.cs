
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Libraries.CropResidues;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// The crop residues component class
/// </summary>
public class CropResidues
{
    /// <summary>
    /// Calculation of unmitigated emissions from crop residues
    /// </summary>
    /// <param name="yieldFreshWeight">the fresh weight yield of the crop type (kg per hectare)</param>
    /// <param name="incorporated">Boolean indicating if the residues are incorporated (true if incorpoorated, false if baled and removed)</param>
    /// <param name="cropType">Enumerator indicating the crop type</param>
    /// <param name="methods">The list of mitigation method UIDs</param>
    /// <param name="cropDryMatterContent">Dry matter content of the crop (%)</param>
    /// <param name="cropHarvestIndex">the harvest index for the crop (a double value between 0 and 1)</param>
    /// <returns>An object of type CropResideuEmissions</returns>
    /// <exception cref="CustomAppException">If grid square is invalid returns a custom app exception or if erros calculating mitigatio multipliers</exception>
    public static CropResidueEmissions MitigatedCropResidueEmissions_HarvestIndex(double yieldFreshWeight, bool incorporated, CropType cropType, List<int> methods,
        double cropDryMatterContent, double cropHarvestIndex)
    {

        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Residues, (int)OrganicMatterSourceType.NotSet, (int)OrganicMatterState.NotSet,
            (int)OrganicMatterType.NotSet, out MitigationFactor multiplier, out string mitigationErrors))
        {
            double freshWeightTonnes = yieldFreshWeight / 1000.0;
            double yieldDryMatterContent;
            if(!double.IsNaN(cropDryMatterContent))
            {
                yieldDryMatterContent = cropDryMatterContent;
            }
            else
            {
                yieldDryMatterContent = CropLookup.RetrieveDryMatterContent(cropType);
            }

            double functionalHarvestIndex = double.NaN;
            functionalHarvestIndex = (1 - cropHarvestIndex) / cropHarvestIndex;

            double residuesRemainingPercentage = 100.0;
            double residuesRemovedPercentage = 0.0;
            if(!incorporated)
            {
                residuesRemainingPercentage = CropLookup.RetrieveResiduesRemaining(cropType);
                residuesRemovedPercentage = 100.0 - residuesRemainingPercentage;
            }

            double residueAboveGroundNContent = CropLookup.RetrieveResidueNContent(cropType, true);
            double aboveGroundBelowGroundRatio = CropLookup.RetrieveAbovetoBelowGroundRatio(cropType);
            double residueBelowGroundNContent = CropLookup.RetrieveResidueNContent(cropType, false);
            double residueAmmoniaContribution = CropLookup.RetrieveAmmoniaContribution(cropType);

            //Calculate Yield Dry matter offtake as additional output
            double yieldDryMatterOfftake = CropResidueEquations.CalculateYieldDryMatter(freshWeightTonnes, yieldDryMatterContent);
            double yieldDryMatterOfftakeNitrogen = yieldDryMatterOfftake * CropLookup.RetrieveYieldNContent(cropType);

            // Calculate N in above ground residues (accounting for removal & baling of residues)
            double aboveGroundResidueDryMatter = CropResidueFunctions.CalculateAboveGroundResidueDryMatter(freshWeightTonnes, yieldDryMatterContent, functionalHarvestIndex, cropType);
            double remainingAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemainingPercentage);
            double aboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(remainingAboveGroundResidueDryMatter, residueAboveGroundNContent);

            //Calculate N in residues removed
            double removedAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemovedPercentage);
            double removedAboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(removedAboveGroundResidueDryMatter, residueAboveGroundNContent);

            // Calculate N in below ground residues
            double belowGroundResidueDryMatter = CropResidueEquations.CalculateBelowGroundResidueDryMatter(yieldDryMatterOfftake, aboveGroundResidueDryMatter, aboveGroundBelowGroundRatio);
            double belowGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(belowGroundResidueDryMatter, residueBelowGroundNContent);

            //Calculate unmitigated emissions
            CropResidueEmissions cropResidueEmissions = new();
            cropResidueEmissions.InitialiseCropResidueEmissions();
            double totalResidueDryMatterN = aboveGroundResidueDryMatterN + belowGroundResidueDryMatterN;

            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(totalResidueDryMatterN, CropLookup.RetrieveIPCCEmissionFactor1(cropType));
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
            cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = CropResidueEquations.NO3N_Leached(totalResidueDryMatterN, CropLookup.RetrieveFracLeach(cropType));
            cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value *= multiplier.FactorLeaching;
            cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5(cropType));
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());
            cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, CropLookup.RetrieveIPCCEmissionFactor4());

            //Ammonia from decomposion of residues
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = CropResidueEquations.NH3N_ResidueDecomposition(aboveGroundResidueDryMatterN, residueAboveGroundNContent, residueAmmoniaContribution);
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;

            //Additional N2O-N from volatilisation of redeposited ammonia
            cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value += GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, CropLookup.RetrieveIPCCEmissionFactor4());

            //No methane

            //Additional Outputs
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = yieldDryMatterOfftake * 1000.0;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = yieldDryMatterOfftakeNitrogen;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = remainingAboveGroundResidueDryMatter * 1000.0;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = aboveGroundResidueDryMatterN;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = belowGroundResidueDryMatter * 1000.0;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = belowGroundResidueDryMatterN;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = removedAboveGroundResidueDryMatterN;

            return cropResidueEmissions;
        }
        else
        {
            throw new CustomAppException("Error calcualting mitigation multipliers: " + mitigationErrors + ".");
        }
    }

    /// <summary>
    /// Calculation of unmitigated emissions from crop residues
    /// </summary>
    /// <param name="yieldFreshWeight">the fresh weight yield of the crop type (kg per hectare)</param>
    /// <param name="incorporated">Boolean indicating if the residues are incorporated (true if incorpoorated, false if baled and removed)</param>
    /// <param name="cropType">Enumerator indicating the crop type</param>
    /// <param name="cropDryMatterContent">Dry matter content of the crop (%)</param>
    /// <param name="cropHarvestIndex">the harvest index for the crop (a double value between 0 and 1)</param>
    /// <returns>An object of type CropResideuEmissions</returns>
    /// <exception cref="CustomAppException">If grid square is invalid returns a custom app exception</exception>
    public static CropResidueEmissions UnmitigatedCropResidueEmissions_HarvestIndex(double yieldFreshWeight, bool incorporated, CropType cropType,
        double cropDryMatterContent, double cropHarvestIndex)
    {

        double freshWeightTonnes = yieldFreshWeight / 1000.0;
        double yieldDryMatterContent;
        yieldDryMatterContent = cropDryMatterContent;

        double functionalHarvestIndex = double.NaN;
        functionalHarvestIndex = (1 - cropHarvestIndex) / cropHarvestIndex;

        double residuesRemainingPercentage = 100.0;
        double residuesRemovedPercentage = 0.0;
        if(!incorporated)
        {
            residuesRemainingPercentage = CropLookup.RetrieveResiduesRemaining(cropType);
            residuesRemovedPercentage = 100.0 - residuesRemainingPercentage;
        }

        double residueAboveGroundNContent = CropLookup.RetrieveResidueNContent(cropType, true);
        double aboveGroundBelowGroundRatio = CropLookup.RetrieveAbovetoBelowGroundRatio(cropType);
        double residueBelowGroundNContent = CropLookup.RetrieveResidueNContent(cropType, false);
        double residueAmmoniaContribution = CropLookup.RetrieveAmmoniaContribution(cropType);

        //Calculate Yield Dry matter offtake as additional output
        double yieldDryMatterOfftake = CropResidueEquations.CalculateYieldDryMatter(freshWeightTonnes, yieldDryMatterContent);
        double yieldDryMatterOfftakeNitrogen = yieldDryMatterOfftake * CropLookup.RetrieveYieldNContent(cropType);

        // Calculate N in above ground residues (accounting for removal & baling of residues)
        double aboveGroundResidueDryMatter = CropResidueFunctions.CalculateAboveGroundResidueDryMatter(freshWeightTonnes, yieldDryMatterContent, functionalHarvestIndex, cropType);
        double remainingAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemainingPercentage);
        double aboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(remainingAboveGroundResidueDryMatter, residueAboveGroundNContent);

        //Calculate N in residues removed
        double removedAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemovedPercentage);
        double removedAboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(removedAboveGroundResidueDryMatter, residueAboveGroundNContent);

        // Calculate N in below ground residues
        double belowGroundResidueDryMatter = CropResidueEquations.CalculateBelowGroundResidueDryMatter(yieldDryMatterOfftake, aboveGroundResidueDryMatter, aboveGroundBelowGroundRatio);
        double belowGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(belowGroundResidueDryMatter, residueBelowGroundNContent);

        //Calculate unmitigated emissions
        CropResidueEmissions cropResidueEmissions = new();
        cropResidueEmissions.InitialiseCropResidueEmissions();
        double totalResidueDryMatterN = aboveGroundResidueDryMatterN + belowGroundResidueDryMatterN;

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(totalResidueDryMatterN, CropLookup.RetrieveIPCCEmissionFactor1(cropType));
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = CropResidueEquations.NO3N_Leached(totalResidueDryMatterN, CropLookup.RetrieveFracLeach(cropType));
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5(cropType));
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, CropLookup.RetrieveIPCCEmissionFactor4());

        //Ammonia from decomposion of residues
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = CropResidueEquations.NH3N_ResidueDecomposition(aboveGroundResidueDryMatterN, residueAboveGroundNContent, residueAmmoniaContribution);

        //Additional N2O-N from volatilisation of redeposited ammonia
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value += GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, CropLookup.RetrieveIPCCEmissionFactor4());

        //No methane

        //Additional Outputs
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = yieldDryMatterOfftake * 1000.0;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = yieldDryMatterOfftakeNitrogen;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = remainingAboveGroundResidueDryMatter * 1000.0;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = aboveGroundResidueDryMatterN;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = belowGroundResidueDryMatter * 1000.0;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = belowGroundResidueDryMatterN;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = removedAboveGroundResidueDryMatterN;

        return cropResidueEmissions;

    }

    /// <summary>
    /// Calculation of unmitigated emissions from crop residues
    /// </summary>
    /// <param name="yieldFreshWeight">the fresh weight yield of the crop type (kg per hectare)</param>
    /// <param name="incorporated">Boolean indicating if the residues are incorporated (true if incorpoorated, false if baled and removed)</param>
    /// <param name="cropType">Enumerator indicating the crop type</param>
    /// <param name="methods">The list of mitigation method UIDs</param>
    /// <param name="cropDryMatterContent">Dry matter content of the crop (%)</param>
    /// <param name="slopeIPCC">the IPCC equation for above ground residues slope</param>
    /// <param name="interceptIPCC">the IPCC equation for above ground residues intercept</param>
    /// <returns>An object of type CropResideuEmissions</returns>
    /// <exception cref="CustomAppException">If grid square is invalid returns a custom app exception or if erros calculating mitigatio multipliers</exception>
    public static CropResidueEmissions MitigatedCropResidueEmissions_IPCC(double yieldFreshWeight, bool incorporated, CropType cropType, List<int> methods,
        double cropDryMatterContent, double slopeIPCC, double interceptIPCC)
    {

        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Residues, (int)OrganicMatterSourceType.NotSet, (int)OrganicMatterState.NotSet,
            (int)OrganicMatterType.NotSet, out MitigationFactor multiplier, out string mitigationErrors))
        {
            double freshWeightTonnes = yieldFreshWeight / 1000.0;
            double yieldDryMatterContent;
            if(!double.IsNaN(cropDryMatterContent))
            {
                yieldDryMatterContent = cropDryMatterContent;
            }
            else
            {
                yieldDryMatterContent = CropLookup.RetrieveDryMatterContent(cropType);
            }

            double residuesRemainingPercentage = 100.0;
            double residuesRemovedPercentage = 0.0;
            if(!incorporated)
            {
                residuesRemainingPercentage = CropLookup.RetrieveResiduesRemaining(cropType);
                residuesRemovedPercentage = 100.0 - residuesRemainingPercentage;
            }

            double residueAboveGroundNContent = CropLookup.RetrieveResidueNContent(cropType, true);
            double aboveGroundBelowGroundRatio = CropLookup.RetrieveAbovetoBelowGroundRatio(cropType);
            double residueBelowGroundNContent = CropLookup.RetrieveResidueNContent(cropType, false);
            double residueAmmoniaContribution = CropLookup.RetrieveAmmoniaContribution(cropType);

            //Calculate Yield Dry matter offtake as additional output
            double yieldDryMatterOfftake = CropResidueEquations.CalculateYieldDryMatter(freshWeightTonnes, yieldDryMatterContent);
            double yieldDryMatterOfftakeNitrogen = yieldDryMatterOfftake * CropLookup.RetrieveYieldNContent(cropType);

            // Calculate N in above ground residues (accounting for removal & baling of residues)
            double aboveGroundResidueDryMatter = CropResidueFunctions.CalculateAboveGroundResidueDryMatter(freshWeightTonnes, yieldDryMatterContent, slopeIPCC, interceptIPCC, cropType);
            double remainingAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemainingPercentage);
            double aboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(remainingAboveGroundResidueDryMatter, residueAboveGroundNContent);

            //Calculate N in residues removed
            double removedAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemovedPercentage);
            double removedAboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(removedAboveGroundResidueDryMatter, residueAboveGroundNContent);

            // Calculate N in below ground residues
            double belowGroundResidueDryMatter = CropResidueEquations.CalculateBelowGroundResidueDryMatter(yieldDryMatterOfftake, aboveGroundResidueDryMatter, aboveGroundBelowGroundRatio);
            double belowGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(belowGroundResidueDryMatter, residueBelowGroundNContent);

            //Calculate unmitigated emissions
            CropResidueEmissions cropResidueEmissions = new();
            cropResidueEmissions.InitialiseCropResidueEmissions();
            double totalResidueDryMatterN = aboveGroundResidueDryMatterN + belowGroundResidueDryMatterN;

            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(totalResidueDryMatterN, CropLookup.RetrieveIPCCEmissionFactor1(cropType));
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
            cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = CropResidueEquations.NO3N_Leached(totalResidueDryMatterN, CropLookup.RetrieveFracLeach(cropType));
            cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value *= multiplier.FactorLeaching;
            cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5(cropType));
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());
            cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, CropLookup.RetrieveIPCCEmissionFactor4());

            //Ammonia from decomposion of residues
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = CropResidueEquations.NH3N_ResidueDecomposition(aboveGroundResidueDryMatterN, residueAboveGroundNContent, residueAmmoniaContribution);
            cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;

            //Additional N2O-N from volatilisation of redeposited ammonia
            cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value += GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, CropLookup.RetrieveIPCCEmissionFactor4());

            //No methane

            //Additional Outputs
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = yieldDryMatterOfftake * 1000.0;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = yieldDryMatterOfftakeNitrogen;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = remainingAboveGroundResidueDryMatter * 1000.0;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = aboveGroundResidueDryMatterN;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = belowGroundResidueDryMatter * 1000.0;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = belowGroundResidueDryMatterN;
            cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = removedAboveGroundResidueDryMatterN;

            return cropResidueEmissions;
        }
        else
        {
            throw new CustomAppException("Error calcualting mitigaiton multipliers: " + mitigationErrors + ".");
        }
    }

    /// <summary>
    /// Calculation of unmitigated emissions from crop residues
    /// </summary>
    /// <param name="yieldFreshWeight">the fresh weight yield of the crop type (kg per hectare)</param>
    /// <param name="incorporated">Boolean indicating if the residues are incorporated (true if incorpoorated, false if baled and removed)</param>
    /// <param name="cropType">Enumerator indicating the crop type</param>
    /// <param name="cropDryMatterContent">Dry matter content of the crop (%)</param>
    /// <param name="slopeIPCC">the IPCC equation for above ground residues slope</param>
    /// <param name="interceptIPCC">the IPCC equation for above ground residues intercept</param>
    /// <returns>An object of type CropResideuEmissions</returns>
    /// <exception cref="CustomAppException">If grid square is invalid returns a custom app exception</exception>
    public static CropResidueEmissions UnmitigatedCropResidueEmissions_IPCC(double yieldFreshWeight, bool incorporated, CropType cropType,
        double cropDryMatterContent, double slopeIPCC, double interceptIPCC)
    {

        double freshWeightTonnes = yieldFreshWeight / 1000.0;
        double yieldDryMatterContent;
        yieldDryMatterContent = cropDryMatterContent;

        double residuesRemainingPercentage = 100.0;
        double residuesRemovedPercentage = 0.0;
        if(!incorporated)
        {
            residuesRemainingPercentage = CropLookup.RetrieveResiduesRemaining(cropType);
            residuesRemovedPercentage = 100.0 - residuesRemainingPercentage;
        }

        double residueAboveGroundNContent = CropLookup.RetrieveResidueNContent(cropType, true);
        double aboveGroundBelowGroundRatio = CropLookup.RetrieveAbovetoBelowGroundRatio(cropType);
        double residueBelowGroundNContent = CropLookup.RetrieveResidueNContent(cropType, false);
        double residueAmmoniaContribution = CropLookup.RetrieveAmmoniaContribution(cropType);

        //Calculate Yield Dry matter offtake as additional output
        double yieldDryMatterOfftake = CropResidueEquations.CalculateYieldDryMatter(freshWeightTonnes, yieldDryMatterContent);
        double yieldDryMatterOfftakeNitrogen = yieldDryMatterOfftake * CropLookup.RetrieveYieldNContent(cropType);

        // Calculate N in above ground residues (accounting for removal & baling of residues)
        double aboveGroundResidueDryMatter = CropResidueFunctions.CalculateAboveGroundResidueDryMatter(freshWeightTonnes, yieldDryMatterContent, slopeIPCC, interceptIPCC, cropType);
        double remainingAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemainingPercentage);
        double aboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(remainingAboveGroundResidueDryMatter, residueAboveGroundNContent);

        //Calculate N in residues removed
        double removedAboveGroundResidueDryMatter = CropResidueEquations.CalculateAboveGroundResidue(aboveGroundResidueDryMatter, residuesRemovedPercentage);
        double removedAboveGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(removedAboveGroundResidueDryMatter, residueAboveGroundNContent);

        // Calculate N in below ground residues
        double belowGroundResidueDryMatter = CropResidueEquations.CalculateBelowGroundResidueDryMatter(yieldDryMatterOfftake, aboveGroundResidueDryMatter, aboveGroundBelowGroundRatio);
        double belowGroundResidueDryMatterN = CropResidueEquations.CalculateResidueN(belowGroundResidueDryMatter, residueBelowGroundNContent);

        //Calculate unmitigated emissions
        CropResidueEmissions cropResidueEmissions = new();
        cropResidueEmissions.InitialiseCropResidueEmissions();
        double totalResidueDryMatterN = aboveGroundResidueDryMatterN + belowGroundResidueDryMatterN;

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(totalResidueDryMatterN, CropLookup.RetrieveIPCCEmissionFactor1(cropType));
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = CropResidueEquations.NO3N_Leached(totalResidueDryMatterN, CropLookup.RetrieveFracLeach(cropType));
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5(cropType));
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, CropLookup.RetrieveIPCCEmissionFactor4());

        //Ammonia from decomposion of residues
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = CropResidueEquations.NH3N_ResidueDecomposition(aboveGroundResidueDryMatterN, residueAboveGroundNContent, residueAmmoniaContribution);

        //Additional N2O-N from volatilisation of redeposited ammonia
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value += GenericEquations.Emission_PercentageEF(cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, CropLookup.RetrieveIPCCEmissionFactor4());

        //No methane
        //Additional Outputs
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = yieldDryMatterOfftake * 1000.0;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = yieldDryMatterOfftakeNitrogen;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = remainingAboveGroundResidueDryMatter * 1000.0;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = aboveGroundResidueDryMatterN;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = belowGroundResidueDryMatter * 1000.0;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = belowGroundResidueDryMatterN;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = removedAboveGroundResidueDryMatterN;

        return cropResidueEmissions;
    }
}
