using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Libraries.Fertiliser;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// The fertiliser Application class
/// </summary>
public static class FertiliserApplication
{
    /// <summary>
    /// Calculation of unmitigated emissions resulting from an application of a specific fertiliser type to land
    ///  Calcualtion is at the level of a single fertiliser type (across 12 months) for ammonia and annual for nitrous oxid and other GHGs.
    /// To calculate emissions across all fertiliser applications, this would need to be run multiple times and the results summed.
    /// </summary>
    /// <param name="FertiliserApplications">an array of doubles (size 12) with the rate of fertiliser application in each month (kg N per hectare)</param>
    /// <param name="fertiliserType">UK GHG AEIA Fertiliser type</param>
    /// <param name="gridSquare_ID">10km grid square in which the farm (or the land parcel to which the application is made) are situated</param>
    /// <param name= "acidicSoil">A boolean indicating if the soil pH is less than 7 (peaty soils or non-alkaline soils)</param>
    /// <param name="totalNapplied">The total amount of N applied per hectare to the crop</param>
    /// <param name="percentageFertiliserType">The ppercentage of the total N applied per hectare that is the fertiliser type</param>
    /// <returns>Fertiliser Emissions Object (kg of the emissions associated with fertilisers)</returns>
    public static FertiliserEmissions UnmitigatedEmissionsFertiliser(FertiliserType fertiliserType, double[] FertiliserApplications, double totalNapplied, double percentageFertiliserType, int gridSquare_ID, bool acidicSoil)
    {
        FertiliserEmissions unmitigatedEmissions = new();
        unmitigatedEmissions.InitialiseFertiliserEmissions();

        if(PhysicalLookup.IsValidGridSquare(gridSquare_ID))
        {
            //Total Annual N applied (this is the total N across all fertilisers)
            double rateN = FertiliserApplications.Sum();
            double totalRateN = totalNapplied;
            double check = totalRateN * percentageFertiliserType * HelperFunctions.Percent_to_Proportion;
            double diff = Math.Abs(rateN - check);

            if(!(diff < 0.0001))
            { throw new CustomAppException("Sum of monthly applications does not match expected total N application (i.e. has an absolute difference of more than 0.0001 kg/ha) - please check input data."); }

            //Base Emissions
            //Calculate Direct N2O-N emissions
            //unmitigated
            if(fertiliserType.Equals(FertiliserType.Urea) || fertiliserType.Equals(FertiliserType.UreaAmmoniumNitrate))
            {
                unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = (percentageFertiliserType * HelperFunctions.Percent_to_Proportion) * FertiliserFunctions.CalculateN2ON_UreaBasedFertiliser(totalRateN, CropLookup.Uncertainty_N2ON_FertiliserModelCoefficient(fertiliserType));
            }
            else
            {
                unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = (percentageFertiliserType * HelperFunctions.Percent_to_Proportion) * FertiliserFunctions.CalculateN2ON_nonUreaBasedFertiliser(totalRateN, PhysicalLookup.RetrieveAnnualAverageRainfall(gridSquare_ID), CropLookup.Uncertainty_N2ON_FertiliserModelCoefficient(fertiliserType));
            }

            //Calculate NH4-N emissions
            //Calculate ammonia emissions factor
            if(FertiliserApplications.Sum() > 0)
            {
                //calculate ammonia emissions by month
                unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;
                for(int i = 0; i < 12; i++)
                {
                    Month month = (Month)i;
                    double ammoniaEF = FertiliserFunctions.CalculateFertiliserAmmoniaEmissionFactor(FertiliserApplications[i], PhysicalLookup.RetrieveMonthlyTemperature(month, gridSquare_ID),
                        acidicSoil, PhysicalLookup.RetrieveRainfallEventProbability(gridSquare_ID), fertiliserType);

                    unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value += FertiliserEquations.FertiliserAmmoniaEmission(FertiliserApplications[i], ammoniaEF, CropLookup.Uncertainty_AmmoniaFertiliserModelCoefficient(fertiliserType));
                }
            }

            //Calculate NO-N emissions
            unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());

            //Calculate N2-N emissions
            unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());

            //Calculate NO3-N emissions
            unmitigatedEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = FertiliserEquations.NO3N_Leached(rateN, CropLookup.RetrieveFracLeach());

            //Calculate Indirect N2ON from leached NO3
            unmitigatedEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(unmitigatedEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5());

            //Calculate Indirect N2ON from redeposition

            double source = unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectNON].Value + unmitigatedEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value;
            unmitigatedEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, CropLookup.RetrieveIPCCEmissionFactor4());

            //Calculate CH4 emissions
            //No methane emissions from fertiliser application

            //additional outputs - e.g Napplied etc
            unmitigatedEmissions.AdditionalOutputs[fertiliserType].Value = rateN;

        }
        else
        {
            throw new CustomAppException("An invalid Grid Square ID was provided,no calculations could be done.");
        }
        return unmitigatedEmissions;

    }

    /// <summary>
    /// Calulation of mitigated emissions from fertiliser application to arabel crop.
    /// Calcualtion is at the level of a single fertiliser type (across 12 months) for ammonia and annual for nitrous oxid and other GHGs.
    /// To calculate emissions across all fertiliser applications, this would need to be run multiple times and the results summed.
    /// </summary>
    /// <param name="FertiliserApplications">an array of doubles (size 12) with the rate of fertiliser application in each month (kg N per hectare)</param>
    /// <param name="fertiliserType">UK GHG AEIA Fertiliser type</param>
    /// <param name="gridSquare_ID">10km grid square in which the farm (or the land parcel to which the application is made) are situated</param>
    /// <param name= "acidicSoil">A boolean indicating if the soil pH is less than 7 (peaty soils or non-alkaline soils)</param>
    /// <param name="totalNapplied">The total amount of N applied per hectare to the crop</param>
    /// <param name="percentageFertiliserType">The ppercentage of the total N applied per hectare that is the fertiliser type</param>
    /// <param name="mitigationMethods">the list of mitigaiton method IDs</param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static FertiliserEmissions MitigatedEmissionsFertiliser(FertiliserType fertiliserType, double[] FertiliserApplications, double totalNapplied, double percentageFertiliserType, int gridSquare_ID, bool acidicSoil, List<int> mitigationMethods)
    {
        if(MitigationMethodLookup.Instance.CalculateMitigation(mitigationMethods, (int)ComponentType.Fertiliser, (int)fertiliserType, (int)OrganicMatterState.NotSet,
                (int)OrganicMatterType.NotSet, out MitigationFactor multiplier, out string mitigationErrors))
        {
            multiplier.FactorMethane = 1;

            FertiliserEmissions mitigatedEmissions = new();
            mitigatedEmissions.InitialiseFertiliserEmissions();

            if(PhysicalLookup.IsValidGridSquare(gridSquare_ID))
            {
                //Total Annual N applied (this is the total N across all fertilisers)
                double rateN = FertiliserApplications.Sum();
                double totalRateN = totalNapplied;
                double check = totalRateN * percentageFertiliserType * HelperFunctions.Percent_to_Proportion;
                double diff = Math.Abs(rateN - check);

                if(!(diff < 0.0001))
                { throw new CustomAppException("Sum of monthly applications does not match expected total N application (i.e. has an absolute difference of more than 0.0001 kg/ha) - please check input data."); }

                //Base Emissions
                //Calculate Direct N2O-N emissions
                //unmitigated
                if(fertiliserType.Equals(FertiliserType.Urea) || fertiliserType.Equals(FertiliserType.UreaAmmoniumNitrate))
                {
                    mitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = (percentageFertiliserType * HelperFunctions.Percent_to_Proportion) * FertiliserFunctions.CalculateN2ON_UreaBasedFertiliser(totalRateN, CropLookup.Uncertainty_N2ON_FertiliserModelCoefficient(fertiliserType));
                    mitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
                }
                else
                {
                    mitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = (percentageFertiliserType * HelperFunctions.Percent_to_Proportion) * FertiliserFunctions.CalculateN2ON_nonUreaBasedFertiliser(totalRateN, PhysicalLookup.RetrieveAnnualAverageRainfall(gridSquare_ID), CropLookup.Uncertainty_N2ON_FertiliserModelCoefficient(fertiliserType));
                    mitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
                }

                //Calculate NH4-N emissions
                //Calculate ammonia emissions factor
                if(FertiliserApplications.Sum() > 0)
                {
                    //calculate ammonia emissions by month
                    mitigatedEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;
                    for(int i = 0; i < 12; i++)
                    {
                        Month month = (Month)i;
                        double ammoniaEF = FertiliserFunctions.CalculateFertiliserAmmoniaEmissionFactor(FertiliserApplications[i], PhysicalLookup.RetrieveMonthlyTemperature(month, gridSquare_ID),
                            acidicSoil, PhysicalLookup.RetrieveRainfallEventProbability(gridSquare_ID), fertiliserType);

                        mitigatedEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value += FertiliserEquations.FertiliserAmmoniaEmission(FertiliserApplications[i], ammoniaEF, CropLookup.Uncertainty_AmmoniaFertiliserModelCoefficient(fertiliserType));
                    }
                    mitigatedEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
                }

                //Calculate NO-N emissions
                mitigatedEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(mitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());

                //Calculate N2-N emissions
                mitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(mitigatedEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());

                //Calculate NO3-N emissions
                mitigatedEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = FertiliserEquations.NO3N_Leached(rateN, CropLookup.RetrieveFracLeach());
                mitigatedEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value *= multiplier.FactorLeaching;

                //Calculate Indirect N2ON from leached NO3
                mitigatedEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(mitigatedEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5());

                //Calculate Indirect N2ON from redeposition

                double source = mitigatedEmissions.EmissionsCore[CoreEmissions.DirectNON].Value + mitigatedEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value;
                mitigatedEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, CropLookup.RetrieveIPCCEmissionFactor4());

                //Calculate CH4 emissions
                //No methane emissions from fertiliser application

                //additional outputs - e.g Napplied etc
                mitigatedEmissions.AdditionalOutputs[fertiliserType].Value = rateN;

                return mitigatedEmissions;
            }
            else
            {
                throw new CustomAppException("An invalid Grid Square ID was provided,no calculations could be done.");
            }
        }
        else
        {

            throw new CustomAppException("Error calculating mitigation multipliers: " + mitigationErrors + ".");
        }
    }
}
