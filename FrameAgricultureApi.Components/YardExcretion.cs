using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using FrameAgricultureApi.Libraries.YardExcretionLivestock;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// Class for calculation of emissions from livestock excreta deposited on yards
/// </summary>
public static class YardExcretion
{
    /// <summary>
    /// Method to calcualte unmitigated emissions from livestock excreta deposted on yards
    /// Note that this should only apply to cattle systems and collecting and feeding yards need to be calculated separately.
    /// Note that yard excreta is managed as slurry
    /// </summary>
    /// <param name="sector">Sector enumerator (should b e dairy or beef)</param>
    /// <param name="animalType">Animal Type enumerator as integerr</param>
    /// <param name="totalN">Total nitrogen in excreta deposted on yards (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen in excreta deposted on yards (kg/head)</param>
    /// <param name="volatileSolids">Volatile solids in excreta deposited on yards (kg/head)</param>
    /// <param name="collectingYard">Boolean indicating if yard is collectin yard (true) or feeding yard (false)</param>
    /// <returns>Yard Emission object</returns>
    public static YardEmissions UnmitigatedYardExcretionEmissions(Sector sector, int animalType, double totalN, double TAN, double volatileSolids, bool collectingYard)
    {

        YardEmissions myEmissions = new();
        myEmissions.InitialiseYardEmissions();

        //Initialise the nitrogen tracker - this will enable N passing to storage to be tracked
        NitrogenTracker nitrogenTracker = new(totalN, TAN);

        //Store nitrogen entering yards in excreta
        myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaTotalN].Value = totalN;
        myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaTAN].Value = TAN;
        myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaOrganicN].Value = totalN - TAN;

        //Calculate emissions
        double scrapingMultiplier = (1.0 - (YardLookup.Instance.RetrieveYardScrapingEfficiency(sector, animalType, collectingYard) * HelperFunctions.Percent_to_Proportion));
        //Ammonia
        myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = YardExcretaDeposition.NH3NEmission(sector, animalType, nitrogenTracker.AmmoniacalNitrogen, collectingYard) * scrapingMultiplier;
        //Directnitrous oxide
        myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = YardExcretaDeposition.N2ONEmission(sector, animalType, nitrogenTracker.TotalNitrogen, collectingYard) * scrapingMultiplier;
        //nitric oxide
        myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = YardExcretaDeposition.NONEmission(sector, animalType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, collectingYard);
        //dinitrogen
        myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = YardExcretaDeposition.N2NEmission(sector, animalType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, collectingYard);
        //Indirect nitrous oxide from redeposition of volatisied ammonia and nitric oxide
        double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        //Note - no nitrate leaching from yards

        //Methane
        myEmissions.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = YardExcretaDeposition.CH4Emission(volatileSolids, sector, animalType, collectingYard);

        //Todo this doesn't check for Yard component Type?
        //Update nitrogen tracker to calculate N leaving yards for storage
        if(nitrogenTracker.UpdateNitrogenBalance(sector, animalType, 0, OrganicMatterType.CattleSlurry, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
            myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.0, ComponentType.Yards, out string temperror))
        {

            //Store nitrogen leaving yards
            myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
            myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
            myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
        }
        else
        {
            throw new CustomAppException("The following errors were encoutnered when calculating emissions from yards: " + temperror);
        }

        return myEmissions;
    }
    /// <summary>
    /// Method for calculating mitigated emissions from excreta deposited on yards.
    /// Note that this should only apply to cattle systems and collecting and feeding yards need to be calculated separately.
    /// Note that yard excretea is managed as slurry
    /// </summary>
    /// <param name="sector">Sector enumerator (should b e dairy or beef)</param>
    /// <param name="animalType">Animal Type enumerator as integerr</param>
    /// <param name="totalN">Total nitrogen in excreta deposted on yards (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen in excreta deposted on yards (kg/head)</param>
    /// <param name="volatileSolids">Volatile solids in excreta deposited on yards (kg/head)</param>
    /// <param name="collectingYard">Boolean indicating if yard is collectin yard (true) or feeding yard (false)</param>
    /// <param name="mitigationMethods">List of mitigigation method IDs applied to yards</param>
    /// <returns>Yard Emission object</returns>
    public static YardEmissions MitigatedYardExcretionEmissions(Sector sector, int animalType, double totalN, double TAN, double volatileSolids, bool collectingYard, List<int> mitigationMethods)
    {
        if(MitigationMethodLookup.Instance.CalculateMitigation(mitigationMethods, (int)ComponentType.Yards, (int)OrganicMatterSourceType.Cattle, (int)OrganicMatterState.Liquid, (int)OrganicMatterType.CattleSlurry,
            out MitigationFactor multiplier, out string mitigationErrors))

        {

            YardEmissions myEmissions = new();
            myEmissions.InitialiseYardEmissions();

            //Initialise the nitrogen tracker - this will enable N passing to storage to be tracked
            NitrogenTracker nitrogenTracker = new(totalN, TAN);

            //Store nitrogen entering yards in excreta
            myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaTotalN].Value = totalN;
            myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaTAN].Value = TAN;
            myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.ExcretaOrganicN].Value = totalN - TAN;

            //Calculate emissions
            double scrapingMultiplier = (1.0 - (YardLookup.Instance.RetrieveYardScrapingEfficiency(sector, animalType, collectingYard) * HelperFunctions.Percent_to_Proportion));
            //Ammonia
            myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = YardExcretaDeposition.NH3NEmission(sector, animalType, nitrogenTracker.AmmoniacalNitrogen, collectingYard) * scrapingMultiplier;
            myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
            //Directnitrous oxide
            myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = YardExcretaDeposition.N2ONEmission(sector, animalType, nitrogenTracker.TotalNitrogen, collectingYard) * scrapingMultiplier;
            myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
            //nitric oxide
            myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = YardExcretaDeposition.NONEmission(sector, animalType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, collectingYard);
            //dinitrogen
            myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = YardExcretaDeposition.N2NEmission(sector, animalType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, collectingYard);
            //Indirect nitrous oxide from redeposition of volatisied ammonia and nitric oxide
            double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
            myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

            //Note - no nitrate leaching from yards

            //Methane
            myEmissions.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = YardExcretaDeposition.CH4Emission(volatileSolids, sector, animalType, collectingYard);
            myEmissions.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value *= multiplier.FactorMethane;

            //Update nitrogen tracker to calculate N leaving yards for storage
            if(nitrogenTracker.UpdateNitrogenBalance(sector, animalType, 0, OrganicMatterType.CattleSlurry, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
                myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.0, ComponentType.Yards, out string temperror))
            {

                //Store nitrogen leaving yards
                myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalYardExcretaOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
            }
            else
            {
                throw new CustomAppException("The following errors were encoutnered when calculating emissions from yards: " + temperror);
            }

            return myEmissions;
        }
        else
        {
            throw new CustomAppException("Errors calcualting mitigation multipliers for yards: " + mitigationErrors + ".");
        }
    }
}
