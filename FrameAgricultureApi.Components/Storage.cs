using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using FrameAgricultureApi.Libraries.Storage;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// Storage emission calculations class
/// </summary>
public static class Storage
{
    /// <summary>
    /// Method to calculate unmitigated emissions from storage
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="manureStorageSystem"></param>
    /// <param name="totalN">Total nitrogen entering storage (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen entering storage (kg/head)</param>
    /// <param name="volatilesolids">Volatile soilds in excreta deposited in house</param>
    /// <param name="anaerobicDigestion">Boolean indicating if anaerobic digestion (true) or other system (false)</param>
    /// <returns>Housing emissions object</returns>
    public static StorageEmissions UnmitigatedstorageManureManagementEmissions(Sector sector, int animalType, OrganicMatterType organicMatterType, ManureStorageSystem manureStorageSystem, double totalN, double TAN, double volatilesolids, bool anaerobicDigestion)
    {
        //Intialise emission object
        StorageEmissions myEmissions = new();
        myEmissions.InitialiseStorageEmissions();

        try
        {
            //Create error string
            string errors = "";

            //Initialise the nitrogen tracker
            NitrogenTracker nitrogenTracker = new(totalN, TAN);

            //Store nitrogen entering system
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNin].Value = totalN;
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANin].Value = TAN;
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNin].Value = totalN - TAN;
            myEmissions.TypeIn = organicMatterType;

            int myAnimalType = animalType;
            if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef) || sector.Equals(Sector.Pigs))
            {
                myAnimalType = 0;
            }

            //Mineralisation - 50% porior to emissions (and 50% after emissions)
            if(nitrogenTracker.Mineralisation(sector, myAnimalType, manureStorageSystem, organicMatterType, out string temperror))
            {
                //for cattle and pigs animal type must be set to zero

                //Direct nitrous oxide
                myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = StorageManureManagement.N2ONEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, nitrogenTracker.TotalNitrogen);
                //Direct ammonia
                myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = StorageManureManagement.NH3NEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, nitrogenTracker.AmmoniacalNitrogen);
                //Direct nitric oxide
                myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = StorageManureManagement.NONEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
                //Direct dinitrogen
                myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = StorageManureManagement.N2NEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
                //Indirect nitros oxide from redeposition of volatilise ammonia and nitric oxide
                double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
                myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());
                //Nitrate Leaching
                myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = StorageManureManagement.NO3NEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, nitrogenTracker.TotalNitrogen);
                //Indirect N2ON from Leachged nitrate
                myEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());
            }
            else
            {
                errors += temperror + ".\n";
            }

            //Update nitrogen tracker
            if(!nitrogenTracker.UpdateNitrogenBalance(sector, animalType, (int)manureStorageSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
               myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, ComponentType.Storage, out temperror))
            { errors += temperror + ".\n"; }

            //Apply second half of mineralisation
            if(nitrogenTracker.Mineralisation(sector, myAnimalType, manureStorageSystem, organicMatterType, out temperror))
            {

                //store nitrogen immobilised and the nitrogen leaving yards
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.NMineralised].Value = nitrogenTracker.MineralisedNitrogen;
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
            }
            else
            { errors += temperror + ".\n"; }

            //Methane
            myEmissions.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value = StorageManureManagement.CH4Emission(sector, animalType, manureStorageSystem, organicMatterType, volatilesolids);

            //Set type out
            if(anaerobicDigestion)
            {
                switch(organicMatterType)
                {
                    case OrganicMatterType.CattleFYM:
                        myEmissions.TypeOut = OrganicMatterType.DigestateCattleFYM;
                        break;
                    case OrganicMatterType.CattleSlurry:
                        myEmissions.TypeOut = OrganicMatterType.DigestateCattleSlurry;
                        break;
                    case OrganicMatterType.PigFYM:
                        myEmissions.TypeOut = OrganicMatterType.DigestatePigFYM;
                        break;
                    case OrganicMatterType.PigSlurry:
                        myEmissions.TypeOut = OrganicMatterType.DigestatePigSlurry;
                        break;
                    case OrganicMatterType.PoultryLayerManure:
                    case OrganicMatterType.PoultryLitter:
                    case OrganicMatterType.DuckFYM:
                        myEmissions.TypeOut = OrganicMatterType.PoultryManureDigestate;
                        break;
                }
            }
            else
            {
                myEmissions.TypeOut = myEmissions.TypeIn;
            }

            if(errors.Equals(string.Empty))
            {
                return myEmissions;
            }
            else
            {
                throw new Exception("The following errors were encountered when calculating storage emissions: " + errors);
            }
        }
        catch(Exception excep)
        {
            throw new CustomAppException(excep.Message + ".\n");
        }
    }
    /// <summary>
    /// Method to calcualte mitigated emissions from storage
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="organicMatterType">Organic matter type enumerator</param>
    /// <param name="manureStorageSystem">Manure storage system enumerator</param>
    /// <param name="totalN">Total nitrogen entering storage (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen entering storage (kg/head)</param>
    /// <param name="volatilesolids">Volatile soilds in excreta deposited in house</param>
    /// <param name="anaerobicDigestion">Boolean indicating if anaerobic digestion (true) or other manure system (false)</param>
    /// <param name="methods">List of mitigaation method IDs</param>
    /// <returns>storage emissions object</returns>
    /// <exception cref="CustomAppException">Exception to report errors with mitigation methods</exception>
    public static StorageEmissions MitigatedstorageManureManagementEmissions(Sector sector, int animalType, OrganicMatterType organicMatterType, ManureStorageSystem manureStorageSystem, double totalN, double TAN, double volatilesolids, bool anaerobicDigestion, List<int> methods)
    {
        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Storage, (int)MitigationMethodLookup.Instance.RetrieveSource(organicMatterType),
            (int)MitigationMethodLookup.Instance.RetrieveState(organicMatterType), (int)organicMatterType, out MitigationFactor multiplier, out string mitigationErrors))
        {
            string errors = string.Empty;

            //Intialise emission object
            StorageEmissions myEmissions = new();
            myEmissions.InitialiseStorageEmissions();

            //Initialise the nitrogen tracker
            NitrogenTracker nitrogenTracker = new(totalN, TAN);

            //Store nitrogen entering system
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNin].Value = totalN;
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANin].Value = TAN;
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNin].Value = totalN - TAN;

            int myAnimalType = animalType;
            if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef) || sector.Equals(Sector.Pigs))
            {
                myAnimalType = 0;
            }

            //Calculate emissions
            //Mineralisation - 50% prior to emissions (and 50% after emissions)
            if(nitrogenTracker.Mineralisation(sector, myAnimalType, manureStorageSystem, organicMatterType, out string temperror, multiplier.FactorMineralisation))
            {
                //Direct nitrous oxide
                myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = StorageManureManagement.N2ONEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, nitrogenTracker.TotalNitrogen);
                myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value *= multiplier.FactorNitrousOxide;
                //Direct ammonia
                myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = StorageManureManagement.NH3NEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, nitrogenTracker.AmmoniacalNitrogen);
                myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value *= multiplier.FactorAmmonia;
                //Direct nitric oxide
                myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = StorageManureManagement.NONEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
                //Direct dinitrogen
                myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = StorageManureManagement.N2NEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);
                //Indirect nitros oxide from redeposition of volatilise ammonia and nitric oxide
                double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
                myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());
                //Nitrate Leaching
                myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = StorageManureManagement.NO3NEmission(sector, myAnimalType, manureStorageSystem, organicMatterType, nitrogenTracker.TotalNitrogen);
                myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value *= multiplier.FactorLeaching;
                //Indirect N2ON from Leachged nitrate
                myEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

            }
            else
            {
                errors += temperror + ".\n";
            }

            //Update nitrogen tracker
            if(!nitrogenTracker.UpdateNitrogenBalance(sector, animalType, (int)manureStorageSystem, organicMatterType, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
               myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, ComponentType.Storage, out temperror))
            { errors += temperror + ".\n"; }

            //Apply second half of mineralisation
            if(nitrogenTracker.Mineralisation(sector, myAnimalType, manureStorageSystem, organicMatterType, out temperror))
            {

                //store nitrogen immobilised and the nitrogen leaving yards
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.NMineralised].Value = nitrogenTracker.MineralisedNitrogen;
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNout].Value = nitrogenTracker.TotalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANout].Value = nitrogenTracker.AmmoniacalNitrogen;
                myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNout].Value = nitrogenTracker.OrganicNitrogen;
            }
            else
            { errors += temperror + ".\n"; }

            //Methane
            myEmissions.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value = StorageManureManagement.CH4Emission(sector, animalType, manureStorageSystem, organicMatterType, volatilesolids);
            myEmissions.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value *= multiplier.FactorMethane;

            //Set type out
            if(anaerobicDigestion)
            {
                switch(organicMatterType)
                {
                    case OrganicMatterType.CattleFYM:
                        myEmissions.TypeOut = OrganicMatterType.DigestateCattleFYM;
                        break;
                    case OrganicMatterType.CattleSlurry:
                        myEmissions.TypeOut = OrganicMatterType.DigestateCattleSlurry;
                        break;
                    case OrganicMatterType.PigFYM:
                        myEmissions.TypeOut = OrganicMatterType.DigestatePigFYM;
                        break;
                    case OrganicMatterType.PigSlurry:
                        myEmissions.TypeOut = OrganicMatterType.DigestatePigSlurry;
                        break;
                    case OrganicMatterType.PoultryLayerManure:
                    case OrganicMatterType.PoultryLitter:
                    case OrganicMatterType.DuckFYM:
                        myEmissions.TypeOut = OrganicMatterType.PoultryManureDigestate;
                        break;
                }
            }
            else
            {
                myEmissions.TypeOut = myEmissions.TypeIn;
            }

            if(errors.Equals(string.Empty))
            {
                return myEmissions;
            }
            else
            {
                throw new CustomAppException("The following errors were encountered when calculating mitigated storage emissions: " + errors);
            }
        }
        else
        {
            throw new CustomAppException("Errors calculating mitigation multipliers for storage: " + mitigationErrors);
        }
    }

    /// <summary>
    /// Method to calculate unmitigated emissions from storage
    /// </summary>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="totalN">Total nitrogen entering storage (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen entering storage (kg/head)</param>
    /// <param name="energyBalance">sheep energy balance object</param>
    /// <returns>Storage emissions object</returns>
    public static StorageEmissions UnmitigatedstorageManureManagementEmissionsSheep(int animalType, double totalN, double TAN, SheepEnergyBalance energyBalance)
    {
        //Intialise emission object
        StorageEmissions myEmissions = new();
        myEmissions.InitialiseStorageEmissions();

        //Create error string
        string errors = "";

        //Initialise the nitrogen tracker
        NitrogenTracker nitrogenTracker = new(totalN, TAN);

        //Store nitrogen entering system
        myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNin].Value = totalN;
        myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANin].Value = TAN;
        myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNin].Value = totalN - TAN;
        myEmissions.TypeIn = OrganicMatterType.SheepFYM;

        //No mineralisation

        //Direct ammonia
        double tmp_Ammonia = StorageManureManagement.NH3NEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, nitrogenTracker.AmmoniacalNitrogen);

        //Direct Nitrous Oxide
        double calc_N2ON = StorageManureManagement.N2ONEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, nitrogenTracker.TotalNitrogen);
        double checkN2ON = (nitrogenTracker.AmmoniacalNitrogen * (1.0 - (StorageLookup.RetrieveStorageNH3NEmissionFactor(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM)) * 0.01)) / 4.1;
        double tmp_DirectNitrousOxide = Math.Min(checkN2ON, calc_N2ON);

        //Direct nitric oxide
        double tmp_DirectNON = StorageManureManagement.NONEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, tmp_DirectNitrousOxide);

        //Direct Dinitrogen
        double tmp_DirectN2N = StorageManureManagement.N2NEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, tmp_DirectNitrousOxide);

        //Indirect nitrous oxide from redeposition
        double source = tmp_Ammonia + tmp_DirectNON;
        double tmp_IndirectN2ON_Atmosphere = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        //Calculate available N & total N
        double tmp_availableN = nitrogenTracker.AvailableNitrogen - tmp_Ammonia - tmp_DirectNitrousOxide - tmp_DirectNON - tmp_DirectN2N;
        double tmp_totalN = nitrogenTracker.TotalNitrogen - tmp_Ammonia - tmp_DirectNitrousOxide - tmp_DirectNON - tmp_DirectN2N;

        //Leached Nitrate
        double calc_NO3N = StorageManureManagement.NO3NEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, tmp_totalN);
        double tmp_LeachedNitrate = Math.Min(tmp_availableN, calc_NO3N);

        //Indirect Nnitrous oxide from leached nitrate
        double tmp_IndirectN2ON_LeachedNitrate = GenericEquations.Emission_PercentageEF(tmp_LeachedNitrate, GenericLookup.IPCCEF5());

        //Assign emissions
        myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = tmp_Ammonia;
        myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = tmp_DirectNitrousOxide;
        myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = tmp_DirectNON;
        myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = tmp_DirectN2N;
        myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = tmp_IndirectN2ON_Atmosphere;
        myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = tmp_LeachedNitrate;
        myEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = tmp_IndirectN2ON_LeachedNitrate;

        //Update nitrogen tracker
        if(!nitrogenTracker.UpdateNitrogenBalance(Sector.Sheep, animalType, (int)ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
           myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, ComponentType.Storage, out string temperror))
        { errors += temperror + ".\n"; }

        //Methane
        double volatilesolids = energyBalance.HouseVolatileSolids;
        myEmissions.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value = StorageManureManagement.CH4Emission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, volatilesolids);

        //Set type out
        myEmissions.TypeOut = myEmissions.TypeIn;

        if(errors.Equals(string.Empty))
        {
            return myEmissions;
        }
        else
        {
            throw new CustomAppException("The following errors were encountered when calculating storage emissions for sheep: " + errors);
        }
    }

    /// <summary>
    /// Method to calculate unmitigated emissions from storage
    /// </summary>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="totalN">Total nitrogen entering storage (kg/head)</param>
    /// <param name="TAN">Total ammoniacal nitrogen entering storage (kg/head)</param>
    /// <param name="energyBalance">sheep energy balance object</param>
    /// <param name="methods">List of mitigaation method IDs</param>
    /// <returns>Storage emissions object</returns>
    public static StorageEmissions MitigatedstorageManureManagementEmissionsSheep(int animalType, double totalN, double TAN, SheepEnergyBalance energyBalance, List<int> methods)
    {
        if(MitigationMethodLookup.Instance.CalculateMitigation(methods, (int)ComponentType.Storage, (int)MitigationMethodLookup.Instance.RetrieveSource(OrganicMatterType.SheepFYM),
            (int)MitigationMethodLookup.Instance.RetrieveState(OrganicMatterType.SheepFYM), (int)OrganicMatterType.SheepFYM, out MitigationFactor multiplier, out string mitigationErrors))
        {
            //Intialise emission object
            StorageEmissions myEmissions = new();
            myEmissions.InitialiseStorageEmissions();

            string errors = string.Empty;

            //Initialise the nitrogen tracker
            NitrogenTracker nitrogenTracker = new(totalN, TAN);

            //Store nitrogen entering system
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TotalNin].Value = totalN;
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.TANin].Value = TAN;
            myEmissions.AdditionalOutputs[AdditionalStorageManureManagementOutputs.OrganicNin].Value = totalN - TAN;
            myEmissions.TypeIn = OrganicMatterType.SheepFYM;

            //No mineralisation

            //Direct ammonia
            double tmp_Ammonia = StorageManureManagement.NH3NEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, nitrogenTracker.AmmoniacalNitrogen);
            tmp_Ammonia *= multiplier.FactorAmmonia;

            //Direct Nitrous Oxide
            double calc_N2ON = StorageManureManagement.N2ONEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, nitrogenTracker.TotalNitrogen);
            double checkN2ON = (nitrogenTracker.AmmoniacalNitrogen * (1.0 - (StorageLookup.RetrieveStorageNH3NEmissionFactor(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM)) * 0.01)) / 4.1;
            double tmp_DirectNitrousOxide = Math.Min(checkN2ON, calc_N2ON);
            tmp_DirectNitrousOxide *= multiplier.FactorAmmonia;

            //Direct nitric oxide
            double tmp_DirectNON = StorageManureManagement.NONEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, tmp_DirectNitrousOxide);

            //Direct Dinitrogen
            double tmp_DirectN2N = StorageManureManagement.N2NEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, tmp_DirectNitrousOxide);

            //Indirect nitrous oxide from redeposition
            double source = myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value;
            double tmp_IndirectN2ON_Atmosphere = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

            //Calculate available N & total N
            double tmp_availableN = nitrogenTracker.AvailableNitrogen - tmp_Ammonia - tmp_DirectNitrousOxide - tmp_DirectNON - tmp_DirectN2N;
            double tmp_totalN = nitrogenTracker.TotalNitrogen - tmp_availableN;

            //Leached Nitrate
            double calc_NO3N = StorageManureManagement.NO3NEmission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, tmp_totalN);
            double tmp_LeachedNitrate = Math.Min(tmp_availableN, calc_NO3N);
            tmp_LeachedNitrate *= multiplier.FactorLeaching;

            //Indirect Nnitrous oxide from leached nitrate
            double tmp_IndirectN2ON_LeachedNitrate = GenericEquations.Emission_PercentageEF(tmp_LeachedNitrate, GenericLookup.IPCCEF5());

            //Assign emissions
            myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = tmp_Ammonia;
            myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = tmp_DirectNitrousOxide;
            myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = tmp_DirectNON;
            myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = tmp_DirectN2N;
            myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = tmp_IndirectN2ON_Atmosphere;
            myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = tmp_LeachedNitrate;
            myEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = tmp_IndirectN2ON_LeachedNitrate;

            //Update nitrogen tracker
            if(!nitrogenTracker.UpdateNitrogenBalance(Sector.Sheep, animalType, (int)ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value,
               myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, ComponentType.Storage, out string temperror))
            { errors += temperror + ".\n"; }

            //Methane
            double volatilesolids = energyBalance.HouseVolatileSolids;
            myEmissions.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value = StorageManureManagement.CH4Emission(Sector.Sheep, animalType, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.SheepFYM, volatilesolids);
            myEmissions.MethaneOutputs[LivestockEmissions.ManureManagementMethane].Value *= multiplier.FactorMethane;

            //Set type out
            myEmissions.TypeOut = myEmissions.TypeIn;

            //no anaerobic digestion for sheep FYM

            if(errors.Equals(string.Empty))
            {
                return myEmissions;
            }
            else
            {
                throw new CustomAppException("The following errors were encountered when calculating storage emissions for sheep: " + errors);
            }
        }
        else
        {
            throw new CustomAppException("Errors calculating mitigation multipliers for storage: " + mitigationErrors);
        }
    }
}
