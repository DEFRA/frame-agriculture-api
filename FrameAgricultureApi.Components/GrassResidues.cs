using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.GrassResidues;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// Grass Residue Emission Calculation Component
/// </summary>
public static class GrassProductionEmissions
{
    /// <summary>
    /// Calculation of unmitigated emissions arising from grass production (not rebewal)
    /// </summary>
    /// <param name="LocationID">the Id of the 10km grid cell in the Uk where the grass field/farm is located</param>
    /// <param name="FertiliserRate">The rate of nitrogen applied as fertiliser to the grass field (kg N per hectare)</param>
    /// <param name="soiltexturetype">The ionteger value of the soil texture type enumerator</param>
    /// <param name="grassType">the integer value of the grass type enumerator</param>
    /// <param name="grassUseType">Integer value of the grass use type enumerator</param>
    /// <param name="sownWithClover">boolean indicating if sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if receives managed manure inputs</param>
    /// <param name="geneticGainScalars">Genetic gain scalars from the data helper functions</param>
    /// <returns>Emissions object</returns>
    public static GrassResidueEmissions UnmitigatedGrassProductionEmissions(int LocationID, double FertiliserRate, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure,
        GeneticGainScalars geneticGainScalars)
    {
        //Look up CLIMATEREGION
        int climateRegion = GrassLookup.Instance.ClimateRegion(LocationID);

        //Look up coefficients
        Dictionary<int, GrassEquationCoefficients> coefficients = GrassLookup.Instance.GrassModelCoefficients(climateRegion, soiltexturetype, grassType, grassUseType, sownWithClover, receivesManagedManure);

        //Emissions object
        GrassResidueEmissions Emissions = new GrassResidueEmissions();
        Emissions.InitialiseGrassResidueEmissions();

        //Core outputs whether cut or renewed
        GrassEquationCoefficients eqParameters = coefficients[(int)GrassEquations.ResidualNitrogen];
        double ResidualNitrogen = GrassResidueEquations.ResidualNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);
        ResidualNitrogen *= geneticGainScalars.GeneticNitrogenUptakeScalar;

        eqParameters = coefficients[(int)GrassEquations.FracLeach];
        double FracLeach = GrassResidueEquations.GrassFracLeach(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);
        FracLeach *= geneticGainScalars.GeneticAllFracLeachScalar;

        //Standard outputs for all grass production
        eqParameters = coefficients[(int)GrassEquations.YieldDryMatter];
        double YieldDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
        eqParameters = coefficients[(int)GrassEquations.AboveGroundDryMatter];
        double AboveGroundDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
        eqParameters = coefficients[(int)GrassEquations.BelowGroundDryMatter];
        double BelowGroundDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
        eqParameters = coefficients[(int)GrassEquations.NitrogeninGrassHarvestedorEaten];
        double nitrogenInGrassHarvested = Math.Max(0.0, GrassResidueEquations.GrassHarvestedNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
        eqParameters = coefficients[(int)GrassEquations.NitrogenfromCloverFixation];
        double nitrogenCloverFixation = Math.Max(0.0, GrassResidueEquations.GrassNitrogenFromClover(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));

        Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.DryMatterOfftake].Value = YieldDryMatter * geneticGainScalars.GeneticYieldScalar;
        Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.AboveGroundResidueDryMatter].Value = AboveGroundDryMatter * geneticGainScalars.GeneticYieldScalar;
        Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.BelowGroundResidueDryMatter].Value = BelowGroundDryMatter * geneticGainScalars.GeneticYieldScalar;
        Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.NitrogeninGrassHarvested].Value = nitrogenInGrassHarvested * geneticGainScalars.GeneticNitrogenUptakeScalar;
        Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.NitrogenfromCloverfixation].Value = nitrogenCloverFixation;

        //Ammonia from cut grass residue decomposition
        double tmp_Ammonia = 0.0;
        if(grassUseType.Equals((int)GrassUseType.Cut) || grassUseType.Equals((int)GrassUseType.Cut_and_Grazed))
        {
            double nitrogenContent = GrassResidueEquations.ResidueNitrogenContent(YieldDryMatter, nitrogenInGrassHarvested, geneticGainScalars.GeneticYieldDilutionScalar);
            tmp_Ammonia = GrassResidueEquations.ResidueAmmoniaEmissionFactor(nitrogenContent) * nitrogenInGrassHarvested * geneticGainScalars.GeneticNitrogenUptakeScalar * GrassResidueParameters.fractionResiduesCutGrass;

        }

        Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = tmp_Ammonia;
        Emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, GrassLookup.Instance.DepositionN2ONEmissinFactor());

        return Emissions;
    }
    /// <summary>
    /// Calculation of mitigated emissions arising from grass production (not renewal)
    /// </summary>
    /// <param name="LocationID">the Id of the 10km grid cell in the Uk where the grass field/farm is located</param>
    /// <param name="FertiliserRate">The rate of nitrogen applied as fertiliser to the grass field (kg N per hectare)</param>
    /// <param name="soiltexturetype">The ionteger value of the soil texture type enumerator</param>
    /// <param name="grassType">the integer value of the grass type enumerator</param>
    /// <param name="grassUseType">Integer value of the grass use type enumerator</param>
    /// <param name="sownWithClover">boolean indicating if sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if receives managed manure inputs</param>
    /// <param name="geneticGainScalars">Genetic gains sclaras object from data helper function</param>
    /// <param name="Methods">List of mitigatino method ID codes</param>
    /// <returns>Emissions object</returns>
    public static GrassResidueEmissions MitigatedGrassProductionEmissions(int LocationID, double FertiliserRate, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure, GeneticGainScalars geneticGainScalars, List<int> Methods)
    {

        if(MitigationMethodLookup.Instance.CalculateMitigation(Methods, (int)ComponentType.Residues, (int)OrganicMatterSourceType.NotSet, (int)OrganicMatterState.NotSet,
                (int)OrganicMatterType.NotSet, out MitigationFactor multiplier, out string mitigationErrors))
        {
            //Look up CLIMATEREGION
            int climateRegion = GrassLookup.Instance.ClimateRegion(LocationID);

            //Look up coefficients
            Dictionary<int, GrassEquationCoefficients> coefficients = GrassLookup.Instance.GrassModelCoefficients(climateRegion, soiltexturetype, grassType, grassUseType, sownWithClover, receivesManagedManure);

            //Emissions object
            GrassResidueEmissions Emissions = new GrassResidueEmissions();
            Emissions.InitialiseGrassResidueEmissions();

            //Core outputs whether cut or renewed
            GrassEquationCoefficients eqParameters = coefficients[(int)GrassEquations.ResidualNitrogen];
            double ResidualNitrogen = GrassResidueEquations.ResidualNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);
            ResidualNitrogen *= geneticGainScalars.GeneticNitrogenUptakeScalar;

            eqParameters = coefficients[(int)GrassEquations.FracLeach];
            double FracLeach = GrassResidueEquations.GrassFracLeach(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);
            FracLeach *= geneticGainScalars.GeneticAllFracLeachScalar;

            //Standard outputs for all grass production
            eqParameters = coefficients[(int)GrassEquations.YieldDryMatter];
            double YieldDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
            eqParameters = coefficients[(int)GrassEquations.AboveGroundDryMatter];
            double AboveGroundDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
            eqParameters = coefficients[(int)GrassEquations.BelowGroundDryMatter];
            double BelowGroundDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
            eqParameters = coefficients[(int)GrassEquations.NitrogeninGrassHarvestedorEaten];
            double nitrogenInGrassHarvested = Math.Max(0.0, GrassResidueEquations.GrassHarvestedNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
            eqParameters = coefficients[(int)GrassEquations.NitrogenfromCloverFixation];
            double nitrogenCloverFixation = Math.Max(0.0, GrassResidueEquations.GrassNitrogenFromClover(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));

            Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.DryMatterOfftake].Value = YieldDryMatter;
            Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.AboveGroundResidueDryMatter].Value = AboveGroundDryMatter;
            Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.BelowGroundResidueDryMatter].Value = BelowGroundDryMatter;
            Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.NitrogeninGrassHarvested].Value = nitrogenInGrassHarvested * geneticGainScalars.GeneticNitrogenUptakeScalar;
            Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.NitrogenfromCloverfixation].Value = nitrogenCloverFixation;

            //Ammonia from cut grass residue decomposition
            double tmp_Ammonia = 0.0;
            if(grassUseType.Equals((int)GrassUseType.Cut) || grassUseType.Equals((int)GrassUseType.Cut_and_Grazed))
            {
                double nitrogenContent = GrassResidueEquations.ResidueNitrogenContent(YieldDryMatter, nitrogenInGrassHarvested, geneticGainScalars.GeneticYieldDilutionScalar);
                tmp_Ammonia = GrassResidueEquations.ResidueAmmoniaEmissionFactor(nitrogenContent) * nitrogenInGrassHarvested * geneticGainScalars.GeneticNitrogenUptakeScalar * GrassResidueParameters.fractionResiduesCutGrass;
            }

            Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = tmp_Ammonia;
            Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
            Emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, GrassLookup.Instance.DepositionN2ONEmissinFactor());

            return Emissions;
        }
        else
        {
            throw new CustomAppException("Error calcualting mitigation multipliers: " + mitigationErrors + ".");
        }
    }

    /// <summary>
    ///  Calculation of unmitigated emissions arising from grass renewal
    /// </summary>
    /// <param name="LocationID">the Id of the 10km grid cell in the Uk where the grass field/farm is located</param>
    /// <param name="FertiliserRate">The rate of nitrogen applied as fertiliser to the grass field (kg N per hectare)</param>
    /// <param name="soiltexturetype">The ionteger value of the soil texture type enumerator</param>
    /// <param name="grassType">the integer value of the grass type enumerator</param>
    /// <param name="grassUseType">Integer value of the grass use type enumerator</param>
    /// <param name="sownWithClover">boolean indicating if sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if receives managed manure inputs</param>
    /// <param name="geneticGainScalars">Genetic gain scalras object form data helper function</param>
    /// <returns>Emissions object</returns>
    public static GrassResidueEmissions UnmitigatedGrassReseededOrPloughedRenewalEmissions(int LocationID, double FertiliserRate, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure
        , GeneticGainScalars geneticGainScalars)
    {

        //Look up CLIMATEREGION
        int climateRegion = GrassLookup.Instance.ClimateRegion(LocationID);

        //Look up coefficients
        Dictionary<int, GrassEquationCoefficients> coefficients = GrassLookup.Instance.GrassModelCoefficients(climateRegion, soiltexturetype, grassType, grassUseType, sownWithClover, receivesManagedManure);

        //Emissions object
        GrassResidueEmissions Emissions = new GrassResidueEmissions();
        Emissions.InitialiseGrassResidueEmissions();

        //Core outputs whether cut or renewed
        GrassEquationCoefficients eqParameters = coefficients[(int)GrassEquations.ResidualNitrogen];
        double ResidualNitrogen = GrassResidueEquations.ResidualNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);
        ResidualNitrogen *= geneticGainScalars.GeneticNitrogenUptakeScalar;

        eqParameters = coefficients[(int)GrassEquations.FracLeach];
        double FracLeach = GrassResidueEquations.GrassFracLeach(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);

        double leachedNitrate = ResidualNitrogen * Math.Min(100.0, Math.Max(0.0, FracLeach)) * HelperFunctions.Percent_to_Proportion;
        leachedNitrate *= geneticGainScalars.GeneticAllFracLeachScalar;

        //Standard outputs for all grass production
        eqParameters = coefficients[(int)GrassEquations.YieldDryMatter];
        double YieldDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
        eqParameters = coefficients[(int)GrassEquations.NitrogeninGrassHarvestedorEaten];
        double nitrogenInGrassHarvested = Math.Max(0.0, GrassResidueEquations.GrassHarvestedNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));

        double tmp_renewalNitrogenContent = GrassResidueEquations.ResidueNitrogenContent(YieldDryMatter, nitrogenInGrassHarvested, geneticGainScalars.GeneticYieldDilutionScalar) * GrassResidueParameters.nitrogenFractionInOfftake;
        double tmp_ResidueDirectAmmonia = GrassResidueEquations.ResidueAmmoniaEmissionFactor(tmp_renewalNitrogenContent) * (GrassResidueParameters.typicalStubbleDryMatter * HelperFunctions.Percent_to_Proportion * (tmp_renewalNitrogenContent / GrassResidueParameters.percentToKilogramspertonne)) * GrassResidueParameters.glyphosateKillFraction;

        Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.RenewalResidueNitrogen].Value = ResidualNitrogen;
        Emissions.AdditionalOutputs[AdditionalGrassResidueEmissions.GrassFracLeach].Value = FracLeach * geneticGainScalars.GeneticAllFracLeachScalar;

        //Core Emissions
        Emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(ResidualNitrogen, GrassLookup.Instance.DirectN2ONEmissionFactor());
        Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = tmp_ResidueDirectAmmonia;
        Emissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(Emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, GrassLookup.Instance.RationNON());
        Emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(Emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, GrassLookup.Instance.RatioN2N());
        Emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = leachedNitrate;
        Emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(Emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GrassLookup.Instance.LeachedN2ONEmissionFactor());
        Emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF((Emissions.EmissionsCore[CoreEmissions.DirectNON].Value + Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value),
                                                                        GrassLookup.Instance.DepositionN2ONEmissinFactor());

        return Emissions;

    }

    /// <summary>
    ///  Calculation of mitigated emissions from grass renewal
    /// </summary>
    /// <param name="LocationID">the Id of the 10km grid cell in the Uk where the grass field/farm is located</param>
    /// <param name="FertiliserRate">The rate of nitrogen applied as fertiliser to the grass field (kg N per hectare)</param>
    /// <param name="soiltexturetype">The ionteger value of the soil texture type enumerator</param>
    /// <param name="grassType">the integer value of the grass type enumerator</param>
    /// <param name="grassUseType">Integer value of the grass use type enumerator</param>
    /// <param name="sownWithClover">boolean indicating if sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if receives managed manure inputs</param>
    /// <param name="geneticGainScalars">Genetic gain scalars object frmo data Helper function</param>
    /// <param name="Methods">List of mitigatino method ID codes</param>
    /// <returns>Emissions object</returns>
    public static GrassResidueEmissions MitigatedGrassReseededOrPloughedRenewalEmissions(int LocationID, double FertiliserRate, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure, GeneticGainScalars geneticGainScalars,
            List<int> Methods)
    {

        if(MitigationMethodLookup.Instance.CalculateMitigation(Methods, (int)ComponentType.Residues, (int)OrganicMatterSourceType.NotSet, (int)OrganicMatterState.NotSet,
                (int)OrganicMatterType.NotSet, out MitigationFactor multiplier, out string mitigationErrors))
        {
            //Look up CLIMATEREGION
            int climateRegion = GrassLookup.Instance.ClimateRegion(LocationID);

            //Look up coefficients
            Dictionary<int, GrassEquationCoefficients> coefficients = GrassLookup.Instance.GrassModelCoefficients(climateRegion, soiltexturetype, grassType, grassUseType, sownWithClover, receivesManagedManure);

            //Emissions object
            GrassResidueEmissions Emissions = new GrassResidueEmissions();
            Emissions.InitialiseGrassResidueEmissions();

            //Core outputs whether cut or renewed
            GrassEquationCoefficients eqParameters = coefficients[(int)GrassEquations.ResidualNitrogen];
            double ResidualNitrogen = GrassResidueEquations.ResidualNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);
            ResidualNitrogen *= geneticGainScalars.GeneticNitrogenUptakeScalar;

            eqParameters = coefficients[(int)GrassEquations.FracLeach];
            double FracLeach = GrassResidueEquations.GrassFracLeach(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate);

            double leachedNitrate = ResidualNitrogen * Math.Min(100.0, Math.Max(0.0, FracLeach)) * HelperFunctions.Percent_to_Proportion;
            leachedNitrate *= geneticGainScalars.GeneticAllFracLeachScalar;

            //Standard outputs for all grass production
            eqParameters = coefficients[(int)GrassEquations.YieldDryMatter];
            double YieldDryMatter = Math.Max(0.0, GrassResidueEquations.GrassDryMatter(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));
            eqParameters = coefficients[(int)GrassEquations.NitrogeninGrassHarvestedorEaten];
            double nitrogenInGrassHarvested = Math.Max(0.0, GrassResidueEquations.GrassHarvestedNitrogen(eqParameters.C, eqParameters.Coeff_X, eqParameters.Coeff_X2, eqParameters.Coeff_X3, eqParameters.Coeff_X4, FertiliserRate));

            double tmp_renewalNitrogenContent = GrassResidueEquations.ResidueNitrogenContent(YieldDryMatter, nitrogenInGrassHarvested, geneticGainScalars.GeneticYieldDilutionScalar) * GrassResidueParameters.nitrogenFractionInOfftake;
            double tmp_ResidueDirectAmmonia = GrassResidueEquations.ResidueAmmoniaEmissionFactor(tmp_renewalNitrogenContent) * (GrassResidueParameters.typicalStubbleDryMatter * HelperFunctions.Percent_to_Proportion * (tmp_renewalNitrogenContent / GrassResidueParameters.percentToKilogramspertonne)) * GrassResidueParameters.glyphosateKillFraction;

            //Core Emissions
            Emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(ResidualNitrogen, GrassLookup.Instance.DirectN2ONEmissionFactor());
            Emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
            Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = tmp_ResidueDirectAmmonia;
            Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
            Emissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(Emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, GrassLookup.Instance.RationNON());
            Emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(Emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, GrassLookup.Instance.RatioN2N());
            Emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = leachedNitrate;
            Emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value *= multiplier.FactorLeaching;
            Emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(Emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GrassLookup.Instance.LeachedN2ONEmissionFactor());
            Emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF((Emissions.EmissionsCore[CoreEmissions.DirectNON].Value + Emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value),
                                                                            GrassLookup.Instance.DepositionN2ONEmissinFactor());

            return Emissions;
        }
        else
        {
            throw new CustomAppException("Error calcualting mitigation multipliers: " + mitigationErrors + ".");
        }
    }
}
