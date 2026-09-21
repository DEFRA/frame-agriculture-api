using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.Housing;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// Housing component emissions calculation class
/// </summary>
public static class Housing
{

    /// <summary>
    /// Method to calculate unmitigated emissions from housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="animalType">integer indicating eanimal type enumerator for sector</param>
    /// <param name="housingSystem">Housing systme enumerator</param>
    /// <param name="totalN">Total nitrogen entering housing (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen entering housing (kg/head)</param>
    /// <param name="volatilesolids">Volatile soilds in excreta deposited in house (kg/head)</param>
    /// <param name="beddingN">Nitrogen in bedding entering housing (kg/head)</param>
    /// <returns>Housing emissions object</returns>
    public static HousingEmissions UnmitigatedHousingManureManagementEmissions(Sector sector, int animalType, ManureHousingSystem housingSystem, OrganicMatterType organicMatterType, double totalN, double TAN, double volatilesolids, double beddingN)
    {
        //Intialise emission object
        HousingEmissions myEmissions = new();
        myEmissions.InitaliseHousingEmissions();

        //Initialise the nitrogen tracker
        NitrogenTracker nitrogenTracker = new(totalN, TAN);

        //FYM systems - need to add bedding N
        if(beddingN > 0.0)
        {
            nitrogenTracker.AddBedding(beddingN);
        }

        //Store nitrogen entering system
        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = totalN;
        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = TAN;
        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = totalN - TAN;
        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = beddingN;

        //Calculate emissions
        //Direct nitrou soxide
        myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = HousingManureManagement.N2ONEmission(sector, animalType, housingSystem, organicMatterType, nitrogenTracker.TotalNitrogen);
        //Direct ammonia
        myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = HousingManureManagement.NH3NEmission(sector, animalType, housingSystem, organicMatterType, nitrogenTracker.AmmoniacalNitrogen);
        //Direct nitric oxide
        myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = HousingManureManagement.NONEmission(sector, animalType, housingSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
        //Direct dinitrogen
        myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = HousingManureManagement.N2NEmission(sector, animalType, housingSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
        //Indirect nitros oxide from redeposition of volatilise ammonia and nitric oxide
        double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        //No leaching from housing

        //Update nitrogen tracker
        if(nitrogenTracker.UpdateNitrogenBalance(sector, animalType, (int)housingSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
            myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.0, ComponentType.Housing, out string temperror))
        {
            //store nitrogen immobilised and the nitrogen leaving yards
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = nitrogenTracker.ImmobilisedNitrogen;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
        }
        else
        { throw new CustomAppException("The following errors were encountered when calculating housing emissions: " + temperror); }

        //Methane
        myEmissions.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = HousingManureManagement.CH4Emission(sector, animalType, housingSystem, organicMatterType, volatilesolids);

        return myEmissions;

    }
    /// <summary>
    /// Method to calcualte mitigated emissions from housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">integer animal type enumerator</param>
    /// <param name="housingSystem">Housing system enumerator</param>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="totalN">Total nitrogen entering housing (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen entering housing (kg/head)</param>
    /// <param name="volatilesolids">Volatile soilds in excreta deposited in house</param>
    /// <param name="methods">List of mitigaation method IDs</param>
    /// <param name="beddingN">Nitrogen in bedding (kg/head)</param>
    /// <returns>Housing emissions object</returns>
    /// <exception cref="CustomAppException">Exception to report errors with mitigation methods</exception>
    // Todo are there mitigations in the spreadsheets?
    public static HousingEmissions MitigatedHousingManureManagementEmissions(Sector sector, int animalType, ManureHousingSystem housingSystem, OrganicMatterType organicMatterType, double totalN, double TAN, double volatilesolids, List<int> methods, double beddingN)
    {

        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Housing, (int)MitigationMethodLookup.Instance.RetrieveSource(organicMatterType),
             (int)MitigationMethodLookup.Instance.RetrieveState(organicMatterType), (int)organicMatterType, out MitigationFactor multiplier, out string mitigationError))
        {
            //Intialise emission object
            HousingEmissions myEmissions = new();
            myEmissions.InitaliseHousingEmissions();

            //Initialise the nitrogen tracker
            NitrogenTracker nitrogenTracker = new(totalN, TAN);

            //FYM systems - need to add bedding N
            if((organicMatterType.Equals(OrganicMatterType.CattleFYM) || organicMatterType.Equals(OrganicMatterType.MinorLivestockFYM) || organicMatterType.Equals(OrganicMatterType.PigFYM)) && beddingN > 0.0)
            {
                nitrogenTracker.AddBedding(beddingN);
            }

            //Store nitrogen entering system
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = totalN;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = TAN;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = totalN - TAN;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = beddingN;

            //Calculate emissions
            //Direct nitrous oxide
            myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = HousingManureManagement.N2ONEmission(sector, animalType, housingSystem, organicMatterType, nitrogenTracker.TotalNitrogen);
            myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
            //Direct ammonia
            myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = HousingManureManagement.NH3NEmission(sector, animalType, housingSystem, organicMatterType, nitrogenTracker.AmmoniacalNitrogen);
            myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
            //Direct nitric oxide
            myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = HousingManureManagement.NONEmission(sector, animalType, housingSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
            //Direct dinitrogen
            myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = HousingManureManagement.N2NEmission(sector, animalType, housingSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
            //Indirect nitros oxide from redeposition of volatilise ammonia and nitric oxide
            double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
            myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());
            //Note no nitrate leaching from yards
            //Update nitrogen tracker
            if(nitrogenTracker.UpdateNitrogenBalance(sector, animalType, (int)housingSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
                myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.0, ComponentType.Housing, out string temperror))
            {

                //store nitrogen immobilised and the nitrogen leaving yards
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = nitrogenTracker.ImmobilisedNitrogen;
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
            }
            else
            { throw new CustomAppException("The following errors were encoutnered when calculating housing emissions: " + temperror); }

            //Methane
            myEmissions.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value = HousingManureManagement.CH4Emission(sector, animalType, housingSystem, organicMatterType, volatilesolids);
            myEmissions.MethaneEmissions[LivestockEmissions.ManureManagementMethane].Value *= multiplier.FactorMethane;

            return myEmissions;

        }
        else
        {
            throw new CustomAppException("Errors calculating mitigation multipliers for housing: " + mitigationError + ".");
        }
    }

    /// <summary>
    /// Method to calculate unmitigated emissions from housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">integer indicating eanimal type enumerator for sector</param>
    /// <param name="energyBalance"></param>
    /// <returns>Housing emissions object</returns>
    public static HousingEmissions UnmitigatedHousingManureManagementEmissions_Sheep(Sector sector, int animalType, SheepEnergyBalance energyBalance)
    {
        //Intialise emission object
        HousingEmissions myEmissions = new();
        myEmissions.InitaliseHousingEmissions();

        double totalN = energyBalance.HouseTotalNitrogen;
        double TAN = energyBalance.HouseAvailableNitrogen;

        //Initialise the nitrogen tracker
        NitrogenTracker nitrogenTracker = new(totalN, TAN);

        //No bedding component as bedding N is included in total N from energy balance

        //Store nitrogen entering system
        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = totalN;
        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = TAN;
        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = totalN - TAN;

        double dungNitrogen = energyBalance.HouseDungNitrogen;
        double urineNitrogen = energyBalance.HouseUrineNitrogen;
        double tmp_CalculatedBeddingN = totalN - (dungNitrogen + urineNitrogen);
        double NinBedding = tmp_CalculatedBeddingN;

        myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = NinBedding;

        //Calculate emissions - ONLY AMMONIA at housing for sheep - EFs have been set to 0 as appropriate so code shoudl wotk and is ready for splitting of emissions between housing adn storage if needed.
        //Direct nitrous oxide
        myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = HousingManureManagement.N2ONEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, nitrogenTracker.TotalNitrogen);
        //Direct ammonia
        myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = HousingManureManagement.NH3NEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, nitrogenTracker.AmmoniacalNitrogen);
        //Direct nitric oxide
        myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = HousingManureManagement.NONEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
        //Direct dinitrogen
        myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = HousingManureManagement.N2NEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
        //Indirect nitrous oxide from redeposition of volatilised ammonia and nitric oxide
        double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        //Update nitrogen tracker
        if(nitrogenTracker.UpdateNitrogenBalance(sector, animalType, (int)ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
            myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.0, ComponentType.Housing, out string temperror))
        {
            //store nitrogen immobilised and the nitrogen leaving yards
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = nitrogenTracker.ImmobilisedNitrogen;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
        }
        else
        { throw new CustomAppException("The following errors were encountered when calculating housing emissions: " + temperror); }

        //Methane is not emitted at housing due to short time - all included in storage

        return myEmissions;

    }
    /// <summary>
    /// Method to calcualte mitigated emissions from housing
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">integer animal type enumerator</param>
    /// <param name="methods">List of mitigaation method IDs</param>
    ///  <param name="energyBalance"></param>
    /// <returns>Housing emissions object</returns>
    /// <exception cref="CustomAppException">Exception to report errors with mitigation methods</exception>
    public static HousingEmissions MitigatedHousingManureManagementEmissions_Sheep(Sector sector, int animalType, SheepEnergyBalance energyBalance, List<int> methods)
    {
        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Housing, (int)MitigationMethodLookup.Instance.RetrieveSource(OrganicMatterType.SheepFYM),
            (int)MitigationMethodLookup.Instance.RetrieveState(OrganicMatterType.SheepFYM), (int)OrganicMatterType.SheepFYM, out MitigationFactor multiplier, out string mitigationError))
        {
            //Intialise emission object
            HousingEmissions myEmissions = new();
            myEmissions.InitaliseHousingEmissions();

            double totalN = energyBalance.HouseTotalNitrogen;
            double TAN = energyBalance.HouseAvailableNitrogen;

            //Initialise the nitrogen tracker
            NitrogenTracker nitrogenTracker = new(totalN, TAN);

            //Bedding N different for sheep sector for now
            //FYM systems - need to add bedding N
            //if ((organicMatterType.Equals(OrganicMatterType.CattleFYM) || organicMatterType.Equals(OrganicMatterType.MinorLivestockFYM) || organicMatterType.Equals(OrganicMatterType.PigFYM)) && beddingN > 0.0)
            //{
            //    //nitrogenTracker.AddBedding(beddingN); Calcualtions assume beddign sufficient to absorb urine
            //}

            //Store nitrogen entering system
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNin].Value = totalN;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANin].Value = TAN;
            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNin].Value = totalN - TAN;

            double dungNitrogen = energyBalance.HouseDungNitrogen;
            double urineNitrogen = energyBalance.HouseUrineNitrogen;
            double tmp_CalculatedBeddingN = totalN - (dungNitrogen + urineNitrogen);
            double NinBedding = tmp_CalculatedBeddingN;

            myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NinBedding].Value = NinBedding;

            //Calculate emissions
            //Direct nitrous oxide
            myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = HousingManureManagement.N2ONEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, nitrogenTracker.TotalNitrogen);
            myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
            //Direct ammonia
            myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = HousingManureManagement.NH3NEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, nitrogenTracker.AmmoniacalNitrogen);
            myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
            //Direct nitric oxide
            myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = HousingManureManagement.NONEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
            //Direct dinitrogen
            myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = HousingManureManagement.N2NEmission(sector, animalType, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
            //Indirect nitros oxide from redeposition of volatilise ammonia and nitric oxide
            double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
            myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());
            //Update nitrogen tracker
            if(nitrogenTracker.UpdateNitrogenBalance(sector, animalType, (int)ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
                myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.0, ComponentType.Housing, out string temperror))
            {

                //store nitrogen immobilised and the nitrogen leaving yards
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.NImmobilised].Value = nitrogenTracker.ImmobilisedNitrogen;
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalHousingManureManagementOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
            }
            else
            { throw new CustomAppException("The following errors were encoutnered when calculating housing emissions: " + temperror); }

            //Methane is not emitted at housing due to short time - all included in storage

            return myEmissions;

        }
        else
        {
            throw new CustomAppException("Errors calculating mitigation multipliers for housing: " + mitigationError + ".");
        }
    }
}
