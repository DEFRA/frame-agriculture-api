using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ManureManagement;

/// <summary>
/// Class for tracking Nitrogen Balance
/// </summary>
public class NitrogenTracker
{
    private bool Mineralised = false;
    /// <summary>
    /// Total Nitrogen (kg)
    /// </summary>
    public double TotalNitrogen { get; set; } = 0.0;
    /// <summary>
    /// Total ammonical nitrogen - TAN (kg)
    /// </summary>
    public double AmmoniacalNitrogen { get; set; } = 0.0;
    /// <summary>
    /// Organic Nitrogen (kg)
    /// </summary>
    public double OrganicNitrogen { get; set; } = 0.0;
    /// <summary>
    /// Nitrogen immobilised (kg)
    /// </summary>
    public double ImmobilisedNitrogen { get; set; } = 0.0;
    /// <summary>
    /// Nitrogen mineralised (50% occurs before emission and 50% after emission)
    /// </summary>
    public double MineralisedNitrogen { get; set; } = double.NaN;
    /// <summary>
    /// Nitrogen available from which emissions can be calculated
    /// (This should mean that TAN will never be less than zero as emissions constrained by available nitrogen)
    /// [NB this is a difference from the UK GHG AEIA methodology for all livestock except sheep]
    /// </summary>
    public double AvailableNitrogen { get; set; } = double.NaN;
    /// <summary>
    /// Flag indicating if TAN fell below zero
    /// </summary>
    public bool FlagZeroTAN { get; set; } = false;
    /// <summary>
    /// Nitrogen tracker class constructore
    /// </summary>
    /// <param name="totalNitrogen">Total nitrogen (kg)</param>
    /// <param name="ammoniacalNitrogen">Total ammoniacal nitrogen (kg)</param>
    /// <param name="organicNitrogen">organic nitrogen (kg)</param>
    public NitrogenTracker(double totalNitrogen, double ammoniacalNitrogen, double organicNitrogen)
    {
        this.TotalNitrogen = totalNitrogen;
        this.AmmoniacalNitrogen = ammoniacalNitrogen;
        this.OrganicNitrogen = organicNitrogen;
        this.AvailableNitrogen = ammoniacalNitrogen;
    }
    /// <summary>
    /// Nitrogen tracker class constructor (overload)
    /// </summary>
    /// <param name="totalNitrogen">Total nitrogen (kg)</param>
    /// <param name="ammoniacalNitrogen">Total ammoniacal nitrogen (kg)</param>
    public NitrogenTracker(double totalNitrogen, double ammoniacalNitrogen)
    {
        this.TotalNitrogen = totalNitrogen;
        this.AmmoniacalNitrogen = ammoniacalNitrogen;
        this.OrganicNitrogen = totalNitrogen - ammoniacalNitrogen;
        this.AvailableNitrogen = ammoniacalNitrogen;
    }
    /// <summary>
    /// Mineralisation of organic manure
    /// </summary>
    /// <param name="system">Manure storage system enumerator</param>
    /// <returns>Errors encountered as string</returns>
    public bool Mineralisation(Sector sector, int animalType, ManureStorageSystem system, OrganicMatterType organicMatterType, out string error, double mitigationFactor = 1.0)
    {
        try
        {
            if(!Mineralised)
            {
                int myAnimalType = animalType;
                if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef) || sector.Equals(Sector.Pigs))
                {
                    myAnimalType = 0;
                }
                MineralisedNitrogen = OrganicNitrogen * StorageLookup.RetrieveStorageMineralisationFactor(sector, myAnimalType, system, organicMatterType) * HelperFunctions.Percent_to_Proportion * mitigationFactor;
                Mineralised = true;
            }
            AmmoniacalNitrogen += 0.5 * MineralisedNitrogen;
            OrganicNitrogen -= 0.5 * MineralisedNitrogen;
            error = "";
            return true;
        }
        catch(Exception ex)
        {
            error = "Error calculating mineralisation: " + ex.Message + ".\n";
            return false;
        }
    }
    /// <summary>
    /// Add bedding nitrogen
    /// </summary>
    /// <param name="beddingNitrogen">Nitrogen content of bedding (kg per head)</param>
    /// <returns>Any errors encountered as string</returns>
    public string AddBedding(double beddingNitrogen)
    {
        string errors = "";
        try
        {
            if(beddingNitrogen > 0)
            {
                TotalNitrogen += beddingNitrogen;
                OrganicNitrogen += beddingNitrogen;
            }
        }
        catch(Exception excep)
        {
            errors = "Error adding bedding nitrogen: " + excep.Message + ".\n";
        }
        return errors;
    }
    /// <summary>
    /// Mwthod to calculate nitrogen balance in livestock systems
    /// </summary>
    /// <param name="emissionsN2ON">Emission of nitrous oxide as N (kg/head)</param>
    /// <param name="emissionsNH3N">Emission of ammonia as N (kg/head)</param>
    /// <param name="emissionsNON">Emission of nitric oxide as N (kg/head)</param>
    /// <param name="emissionsN2N">Emission of dinitrogen as N (kg/head)</param>
    /// <param name="component">Enumerator indicating the component (Grazing, yard, housing, storage, application)</param>
    /// <param name="system">an integer indicating the system type (for housing and storage only)</param>
    /// <returns>errors encountered as a string</returns>
    public bool UpdateNitrogenBalance(Sector sector, int animalType, int system, OrganicMatterType manureType, double emissionsN2ON, double emissionsNH3N, double emissionsNON, double emissionsN2N, double emissionsNO3N, ComponentType component, out string error)
    {
        try
        {
            if(component.Equals(ComponentType.Housing))
            {
                ManureHousingSystem mySystem = (ManureHousingSystem)system;

                ImmobilisedNitrogen = AmmoniacalNitrogen * HousingLookup.PercentImmobilisation(sector, animalType, mySystem, manureType) * HelperFunctions.Percent_to_Proportion;

                error = UpdateNitrogenBalanceBase(emissionsN2ON, emissionsNH3N, emissionsNON, emissionsN2N, emissionsNO3N);
                if(error.Equals(""))
                {
                    AmmoniacalNitrogen -= ImmobilisedNitrogen;
                    if(AmmoniacalNitrogen < 0.0)
                    {
                        AmmoniacalNitrogen = 0.0;
                        TotalNitrogen -= ImmobilisedNitrogen;
                        OrganicNitrogen = TotalNitrogen - AmmoniacalNitrogen;
                        FlagZeroTAN = true;
                    }
                    else
                    {
                        OrganicNitrogen += ImmobilisedNitrogen;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                error = UpdateNitrogenBalanceBase(emissionsN2ON, emissionsNH3N, emissionsNON, emissionsN2N, emissionsNO3N);
                if(error.Equals(""))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //else if (component.Equals(ComponentType.Storage))
            //{
            //    if (Mineralised)
            //    {
            //        error = UpdateNitrogenBalanceBase(emissionsN2ON, emissionsNH3N, emissionsNON, emissionsN2N, emissionsNO3N);
            //        if (error.Equals(""))
            //        {
            //            AmmoniacalNitrogen += 0.5 * MineralisedNitrogen;
            //            OrganicNitrogen -= 0.5 * MineralisedNitrogen;
            //            return true;
            //        }
            //        else
            //        { return false; }

            //    }
            //    else
            //    {
            //        error = "Mineralisation is not calculated prior to updating storage emissions - please amend code to include mineralisation prior to calculating storage emissions.\n";
            //        return false;
            //    }
            //}

        }
        catch(Exception excep)
        {
            error = "Error updating nitrogen balance for " + component.ToString() + ":" + excep.Message + ".\n";
            return false;
        }
    }

    /// <summary>
    /// Calcualtion of Nitrogen Balance excluding immobilisation or mineralisation
    /// </summary>
    /// <param name="emissionsN2ON">Emissions of N2O-N (kg per head)</param>
    /// <param name="emissionsNH3N">Emissions of NH3-N (kg per head)</param>
    /// <param name="emissionsNON">Emissions of NO-N (kg per head)</param>
    /// <param name="emissionsN2N">Emissions of N2-N (kg per head)</param>
    /// <returns>Any errors encountered as a string</returns>
    private string UpdateNitrogenBalanceBase(double emissionsN2ON, double emissionsNH3N, double emissionsNON, double emissionsN2N, double emissionsNO3N)
    {
        string errors = "";

        try
        {

            AmmoniacalNitrogen -= (emissionsN2ON + emissionsNH3N + emissionsNON + emissionsN2N + emissionsNO3N);

            if(AmmoniacalNitrogen < 0.0)
            {
                AmmoniacalNitrogen = 0.0;
                TotalNitrogen -= (emissionsN2ON + emissionsNH3N + emissionsNON + emissionsN2N + emissionsNO3N);
                OrganicNitrogen = TotalNitrogen - AmmoniacalNitrogen;
                FlagZeroTAN = true;
            }
            else
            {
                TotalNitrogen -= (emissionsN2ON + emissionsNH3N + emissionsNON + emissionsN2N + emissionsNO3N);
            }

            //For completeness update available nitrogen as well
            AvailableNitrogen = AmmoniacalNitrogen;

        }
        catch(Exception excep)
        {
            errors = "Error updating nitrogen balance: " + excep.Message + ".\n";
        }

        return errors;
    }
}
