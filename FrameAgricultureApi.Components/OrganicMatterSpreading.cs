using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.OrganicMatterApplication;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// COmponent class to calculte emissions from application of organic matter
/// </summary>
public class OrganicMatterSpreading
{
    /// <summary>
    /// Calculates mitigated emissions from the spreading of organic matter applications
    /// </summary>
    /// <param name="organicMatterType">Organic matter type as enumerator</param>
    /// <param name="quantityperhectare">The quantity of origanic matter applied as kg/ha</param>
    /// <param name="landAppliedTo">A boolean set to true if appplication is to arable land, false if to grass land</param>
    /// <param name="methods">The list of mitigaiton method IDs</param>
    /// <param name="month">Month of application (only required for organic matter of cattle origin)</param>
    /// <param name="unitTotalNperkg">An optional variable indicating the N content of the origanic matter as kg per kg</param>
    /// <param name="percentTAN">the percentage of N in the organic matter that is TAN</param>
    /// <returns>An emissions output object containing the core emissions and additional outputs (N applied, TAN applied and organic N applied) as kg/ha</returns>
    /// <exception cref="CustomAppException">Exceptions produce if erros when clacualting mitigation multiplier</exception>
    public static OrganicMatterSpreadingEmission MitigatedOrganicMatterSpreadingEmissions(OrganicMatterType organicMatterType, double quantityperhectare, SpreadingLandUse landAppliedTo, List<int> methods,
        Month month, double unitTotalNperkg, double percentTAN)
    {
        OrganicMatterSourceType organicsource = MitigationMethodLookup.Instance.RetrieveSource(organicMatterType);
        OrganicMatterState state = MitigationMethodLookup.Instance.RetrieveState(organicMatterType);

        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Spreading, (int)organicsource, (int)state, (int)organicMatterType, out MitigationFactor multiplier, out string mitigationErrors))
        {

            OrganicMatterSpreadingEmission emissions = new();
            emissions.InitialiseOrganicMatterSpreadingEmissions();

            //calculate starting N, but should only be for  non-manure based organic matter - missing N content anda Tan for livestock manures should already have been caught
            double unitNContent = unitTotalNperkg;
            double hectareTotalN = unitNContent * quantityperhectare;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = hectareTotalN;

            double pctTAN = percentTAN;
            double hectareTAN = hectareTotalN * (pctTAN * HelperFunctions.Percent_to_Proportion);
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = hectareTAN;

            double hectareorganicN = hectareTotalN - hectareTAN;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = hectareorganicN;

            //Ammonia
            emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OrganicMatterApplicationFunctions.NH3NEmission(organicMatterType, hectareTAN, landAppliedTo, month);
            emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;

            //Nitrous Oxide
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = OrganicMatterApplicationFunctions.N2ONEmission(organicMatterType, hectareTotalN, landAppliedTo, month);
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;

            //Nitric Oxide
            emissions.EmissionsCore[CoreEmissions.DirectNON].Value = OrganicMatterApplicationFunctions.NONEmission(organicMatterType,
                emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

            //Dinitrogen
            emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OrganicMatterApplicationFunctions.N2NEmission(organicMatterType,
                emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

            //Leached NO3
            emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = OrganicMatterApplicationFunctions.NO3NLeaching(organicMatterType, hectareTotalN, landAppliedTo, month);
            emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value *= multiplier.FactorLeaching;

            //Indirect N2ON - leaching
            emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

            //Indirect N2ON - volatilisation of redeposited N
            double source = emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value;
            emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

            return emissions;
        }
        else
        {
            throw new CustomAppException("Errors calcualting mitigation multipliers for organic matter spreading: " + mitigationErrors + ".");
        }
    }
    /// <summary>
    /// Mitigated emissions from spreading of sheep manure to grass
    /// </summary>
    /// <param name="TotalN">Total nitrogen applied (kg/ha)</param>
    /// <param name="TAN">Total available nitrogen applied (kg/ha)</param>
    /// <param name="methods">The list of mitigaiton method IDs</param>
    /// <param name="firstWinterFracLeach">the first winter manure frac leach coefficient (from sheep helper function)</param>
    /// <param name="manureFracLeachCoefficients">Manure frac leach polynomial coefficients from grass model helper</param>
    /// <param name="GrassFertiliserRate">The rate of all synthetic fertiliser nitrogen applied to the grass (kg/ha)</param>
    /// <param name="month">month enumerator</param>
    /// <returns>Organic Matter SPreadin Emission Object</returns>
    public static OrganicMatterSpreadingEmission MitigatedManureSpreadingEmissionSheep(double TotalN, double TAN, List<int> methods, double firstWinterFracLeach, GrassEquationCoefficients manureFracLeachCoefficients, double GrassFertiliserRate, Month month, double geneticManuredFracLeachScalar)
    {
        OrganicMatterSpreadingEmission emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Spreading, (int)OrganicMatterSourceType.Sheep, (int)OrganicMatterState.Solid, (int)OrganicMatterType.SheepFYM, out MitigationFactor multiplier, out string mitigationErrors))
        {

            //calculate starting N, but should only be for  non-manure based organic matter - missing N content anda Tan for livestock manures should already have been caught
            double hectareTotalN = TotalN;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = hectareTotalN;

            double hectareTAN = TAN;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = hectareTAN;

            double hectareorganicN = TotalN - TAN;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = hectareorganicN;

            //Ammonia
            emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OrganicMatterApplicationFunctions.NH3NEmission(OrganicMatterType.SheepFYM, hectareTAN, SpreadingLandUse.Grassland, month);
            emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
            //Nitrous Oxide
            double proposedN2ON = OrganicMatterApplicationFunctions.N2ONEmission(OrganicMatterType.SheepFYM, hectareTotalN, SpreadingLandUse.Grassland, month);
            proposedN2ON += multiplier.FactorNitrousOxide;
            double limitedN2ON = (hectareTAN - emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value) / 4.1;
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = Math.Min(proposedN2ON, limitedN2ON);

            //Nitric Oxide
            emissions.EmissionsCore[CoreEmissions.DirectNON].Value = OrganicMatterApplicationFunctions.NONEmission(OrganicMatterType.SheepFYM,
                emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, SpreadingLandUse.Grassland, month);

            //Dinitrogen
            emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OrganicMatterApplicationFunctions.N2NEmission(OrganicMatterType.SheepFYM,
                emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, SpreadingLandUse.Grassland, month);

            //Leached NO3
            double firstWinterManureFracLeach = firstWinterFracLeach;
            double manureFracLeach = manureFracLeachCoefficients.C + (manureFracLeachCoefficients.Coeff_X * GrassFertiliserRate) + (manureFracLeachCoefficients.Coeff_X2 * Math.Pow(GrassFertiliserRate, 2.0)) +
                (manureFracLeachCoefficients.Coeff_X3 * Math.Pow(GrassFertiliserRate, 3.0)) + (manureFracLeachCoefficients.Coeff_X4 * Math.Pow(GrassFertiliserRate, 4.0));

            double FracLeach = firstWinterManureFracLeach + Math.Max(0.0, Math.Min(100.0, manureFracLeach));
            FracLeach *= multiplier.FactorLeaching;
            double totalNavailable = hectareTotalN - (emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value +
            emissions.EmissionsCore[CoreEmissions.DirectN2N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value);

            double tmp_NO3N = Math.Min(GenericEquations.Emission_PercentageEF(hectareTotalN, FracLeach), totalNavailable);
            tmp_NO3N *= geneticManuredFracLeachScalar;
            emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = tmp_NO3N;

            //Indirect N2ON - leaching
            emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

            //Indirect N2ON - volatilisation of redeposited N
            double source = emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value;
            emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

            return emissions;
        }
        else
        {
            throw new CustomAppException("Errors calcualting mitigation multipliers for organic matter spreading: " + mitigationErrors + ".");
        }
    }
    /// <summary>
    /// Calculates mitigated emissions from the spreading of livestock manures
    /// </summary>
    /// <param name="organicMatterType">Organic matter type as enumerator</param>
    /// <param name="landAppliedTo">A boolean set to true if appplication is to arable land, false if to grass land</param>
    /// <param name="methods">The list of mitigaiton method IDs</param>
    /// <param name="month">Month of application (only required for organic matter of cattle origin)</param>
    /// <param name="totalN">Total nitrogen in manure applied (kg/ha)</param>
    /// <param name="TAN">Total available nitrogen in manure applied (kg/ha)</param>
    /// <returns>An emissions output object containing the core emissions and additional outputs (N applied, TAN applied and organic N applied) as kg/ha</returns>
    /// <exception cref="CustomAppException">Exceptions produce if erros when clacualting mitigation multiplier</exception>
    public static OrganicMatterSpreadingEmission MitigatedManureSpreadingEmissionsLivestock(OrganicMatterType organicMatterType, SpreadingLandUse landAppliedTo, List<int> methods,
        Month month, double totalN, double TAN)
    {
        OrganicMatterSourceType organicsource = MitigationMethodLookup.Instance.RetrieveSource(organicMatterType);
        OrganicMatterState state = MitigationMethodLookup.Instance.RetrieveState(organicMatterType);

        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Spreading, (int)organicsource, (int)state, (int)organicMatterType, out MitigationFactor multiplier, out string mitigationErrors))
        {

            OrganicMatterSpreadingEmission emissions = new();
            emissions.InitialiseOrganicMatterSpreadingEmissions();

            //calculate starting N, but should only be for  non-manure based organic matter - missing N content anda Tan for livestock manures should already have been caught
            double hectareTotalN = totalN;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = hectareTotalN;

            double hectareTAN = TAN;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = hectareTAN;

            double hectareorganicN = hectareTotalN - hectareTAN;
            emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = hectareorganicN;

            //Ammonia
            emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OrganicMatterApplicationFunctions.NH3NEmission(organicMatterType, hectareTAN, landAppliedTo, month);
            emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;

            //Nitrous Oxide
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = OrganicMatterApplicationFunctions.N2ONEmission(organicMatterType, hectareTotalN, landAppliedTo, month);
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;

            //Nitric Oxide
            emissions.EmissionsCore[CoreEmissions.DirectNON].Value = OrganicMatterApplicationFunctions.NONEmission(organicMatterType,
                emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

            //Dinitrogen
            emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OrganicMatterApplicationFunctions.N2NEmission(organicMatterType,
                emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

            //Leached NO3
            emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = OrganicMatterApplicationFunctions.NO3NLeaching(organicMatterType, hectareTotalN, landAppliedTo, month);
            emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value *= multiplier.FactorLeaching;

            //Indirect N2ON - leaching
            emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

            //Indirect N2ON - volatilisation of redeposited N
            double source = emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value;
            emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

            return emissions;
        }
        else
        {
            throw new CustomAppException("Errors calcualting mitigation multipliers for organic matter spreading: " + mitigationErrors + ".");
        }
    }
    /// <summary>
    /// Calculates unmitigated emissions from the spreading of organic matter applications
    /// </summary>
    /// <param name="organicMatterType">Organic matter type as enumerator</param>
    /// <param name="quantityperhectare">The quantity of origanic matter applied as kg/ha</param>
    /// <param name="landAppliedTo">A boolean set to true if appplication is to arable land, false if to grass land</param>
    /// <param name="month">Month of application (only required for organic matter of cattle origin)</param>
    /// <param name="unitTotalNperkg">An optional variable indicating the N content of the origanic matter as kg per kg</param>
    /// <param name="percentTAN">the percentage of N in the organic matter that is TAN</param>
    /// <returns>An emissions output object containing the core emissions and additional outputs (N applied, TAN applied and organic N applied) as kg/ha</returns>
    public static OrganicMatterSpreadingEmission UnmitigatedOrganicMatterSpreadingEmissions(OrganicMatterType organicMatterType, double quantityperhectare, SpreadingLandUse landAppliedTo, Month month, double unitTotalNperkg, double percentTAN)
    {
        OrganicMatterSpreadingEmission emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        //calculate starting N, but should only be for  non-manure based organic matter - missing N content anda Tan for livestock manures should already have been caught
        double unitNContent = unitTotalNperkg;
        double hectareTotalN = unitNContent * quantityperhectare;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = hectareTotalN;

        double pctTAN = percentTAN;
        double hectareTAN = hectareTotalN * (pctTAN * HelperFunctions.Percent_to_Proportion);
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = hectareTAN;

        double hectareorganicN = hectareTotalN - hectareTAN;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = hectareorganicN;

        //Ammonia
        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OrganicMatterApplicationFunctions.NH3NEmission(organicMatterType, hectareTAN, landAppliedTo, month);

        //Nitrous Oxide
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = OrganicMatterApplicationFunctions.N2ONEmission(organicMatterType, hectareTotalN, landAppliedTo, month);

        //Nitric Oxide
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = OrganicMatterApplicationFunctions.NONEmission(organicMatterType,
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

        //Dinitrogen
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OrganicMatterApplicationFunctions.N2NEmission(organicMatterType,
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

        //Leached NO3
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = OrganicMatterApplicationFunctions.NO3NLeaching(organicMatterType, hectareTotalN, landAppliedTo, month);

        //Indirect N2ON - leaching
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

        //Indirect N2ON - volatilisation of redeposited N
        double source = emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        return emissions;
    }
    /// <summary>
    /// Unmitigated emissions from spreading of sheep manure to grass
    /// </summary>
    /// <param name="TotalN">Total nitrogen applied (kg/ha)</param>
    /// <param name="TAN">Total available nitrogen applied (kg/ha)</param>
    /// <param name="firstWinterFracLeach">the first winter manure frac leach coefficient (from sheep helper function)</param>
    /// <param name="manureFracLeachCoefficients">Manure frac leach polynomial coefficients from grass model helper</param>
    /// <param name="GrassFertiliserRate">The rate of all synthetic fertiliser nitrogen applied to the grass (kg/ha)</param>
    /// <param name="month">month enumerator</param>
    /// <param name="geneticManuredFracLeachScalar">Grass genetic gain scalar for frac leach of spread manure</param>
    /// <returns>Organic Matter SPreadin Emission Object</returns>
    public static OrganicMatterSpreadingEmission UnmitigatedManureSpreadingEmissionSheep(double TotalN, double TAN, double firstWinterFracLeach, GrassEquationCoefficients manureFracLeachCoefficients, double GrassFertiliserRate, Month month, double geneticManuredFracLeachScalar)
    {
        OrganicMatterSpreadingEmission emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        //calculate starting N, but should only be for  non-manure based organic matter - missing N content anda Tan for livestock manures should already have been caught
        double hectareTotalN = TotalN;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = hectareTotalN;

        double hectareTAN = TAN;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = hectareTAN;

        double hectareorganicN = TotalN - TAN;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = hectareorganicN;

        //Ammonia
        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OrganicMatterApplicationFunctions.NH3NEmission(OrganicMatterType.SheepFYM, hectareTAN, SpreadingLandUse.Grassland, month);

        //Nitrous Oxide
        double proposedN2ON = OrganicMatterApplicationFunctions.N2ONEmission(OrganicMatterType.SheepFYM, hectareTotalN, SpreadingLandUse.Grassland, month);
        double limitedN2ON = (hectareTAN - emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value) / 4.1;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = Math.Min(proposedN2ON, limitedN2ON);

        //Nitric Oxide
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = OrganicMatterApplicationFunctions.NONEmission(OrganicMatterType.SheepFYM,
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, SpreadingLandUse.Grassland, month);

        //Dinitrogen
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OrganicMatterApplicationFunctions.N2NEmission(OrganicMatterType.SheepFYM,
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, SpreadingLandUse.Grassland, month);

        //Leached NO3
        double firstWinterManureFracLeach = firstWinterFracLeach;
        double manureFracLeach = manureFracLeachCoefficients.C + (manureFracLeachCoefficients.Coeff_X * GrassFertiliserRate) + (manureFracLeachCoefficients.Coeff_X2 * Math.Pow(GrassFertiliserRate, 2.0)) +
            (manureFracLeachCoefficients.Coeff_X3 * Math.Pow(GrassFertiliserRate, 3.0)) + (manureFracLeachCoefficients.Coeff_X4 * Math.Pow(GrassFertiliserRate, 4.0));

        double FracLeach = firstWinterManureFracLeach + Math.Max(0.0, Math.Min(100.0, manureFracLeach));
        double totalNavailable = hectareTotalN - (emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value +
            emissions.EmissionsCore[CoreEmissions.DirectN2N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value);

        double tmp_NO3N = Math.Min(GenericEquations.Emission_PercentageEF(hectareTotalN, FracLeach), totalNavailable);
        tmp_NO3N *= geneticManuredFracLeachScalar;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = tmp_NO3N;

        //Indirect N2ON - leaching
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

        //Indirect N2ON - volatilisation of redeposited N
        double source = emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        return emissions;
    }
    /// <summary>
    /// Calculates unmitigated emissions from the spreading of organic matter applications
    /// </summary>
    /// <param name="organicMatterType">Organic matter type as enumerator</param>
    /// <param name="landAppliedTo">A boolean set to true if appplication is to arable land, false if to grass land</param>
    /// <param name="month">Month of application (only required for organic matter of cattle origin)</param>
    /// <param name="TotalN">The total N in manure applied to land in kg/ha</param>
    /// <param name="TAN">the percentage of N in the organic matter that is TAN</param>
    /// <returns>An emissions output object containing the core emissions and additional outputs (N applied, TAN applied and organic N applied) as kg/ha</returns>
    public static OrganicMatterSpreadingEmission UnmitigatedManureSpreadingEmissionsLivestock(OrganicMatterType organicMatterType, SpreadingLandUse landAppliedTo, Month month, double TotalN, double TAN)
    {
        OrganicMatterSpreadingEmission emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        //calculate starting N, but should only be for  non-manure based organic matter - missing N content anda Tan for livestock manures should already have been caught
        double hectareTotalN = TotalN;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = hectareTotalN;

        double hectareTAN = TAN;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = hectareTAN;

        double hectareorganicN = hectareTotalN - hectareTAN;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = hectareorganicN;

        //Ammonia
        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OrganicMatterApplicationFunctions.NH3NEmission(organicMatterType, hectareTAN, landAppliedTo, month);

        //Nitrous Oxide
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = OrganicMatterApplicationFunctions.N2ONEmission(organicMatterType, hectareTotalN, landAppliedTo, month);

        //Nitric Oxide
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = OrganicMatterApplicationFunctions.NONEmission(organicMatterType,
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

        //Dinitrogen
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OrganicMatterApplicationFunctions.N2NEmission(organicMatterType,
            emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, landAppliedTo, month);

        //Leached NO3
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = OrganicMatterApplicationFunctions.NO3NLeaching(organicMatterType, hectareTotalN, landAppliedTo, month);

        //Indirect N2ON - leaching
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

        //Indirect N2ON - volatilisation of redeposited N
        double source = emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + emissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        return emissions;
    }
}
