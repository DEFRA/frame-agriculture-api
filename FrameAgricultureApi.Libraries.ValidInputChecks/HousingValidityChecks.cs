using FrameAgricultureApi.IOClasses;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Housing input data validity checks
/// </summary>
public static class HousingValidityChecks
{
    /// <summary>
    /// Check housing inputs (not sheep)
    /// </summary>
    /// <param name="inputs">Housing INputs object</param>
    /// <param name="mitigated">Boolean indicating if mitigation methods have been included</param>
    /// <returns>String containing list of errors, if any</returns>
    public static string CheckValidHousingInputs(HousingInputs inputs, out bool mitigated)
    {
        string retval = "";
        retval = CoreChecks(inputs.Sector, (int)inputs.AnimalType, (int)inputs.OrganicMatterType, (int)inputs.ManureHousingSystem, retval);
        retval = NitrogenChecks(inputs.TotalNitrogen, inputs.TotalAmmoniacalNitrogen, inputs.VolatileSolids, retval);

        if(inputs.MitigationMethods == null || inputs.MitigationMethods.Length <= 0)
        {
            mitigated = false;
        }
        else
        {
            mitigated = true;
        }

        return retval;
    }
    /// <summary>
    /// Checks the nitrogen inputs
    /// </summary>
    /// <param name="totalNitrogen">Total nitrogen in excreta deposited at housing (kg/head/unit time)</param>
    /// <param name="totalAmmoniacalNitrogen">Total ammoniaval nitrogen in excreta deposited at housing (kg/head/unit time)</param>
    /// <param name="volatileSolids">Volatile solids in excreta deposited at housing (kg/head/unit time)</param>
    /// <param name="retval">String containing list of errors, if any</param>
    /// <returns>String containing list of errors, if any</returns>
    private static string NitrogenChecks(double totalNitrogen, double totalAmmoniacalNitrogen, double volatileSolids, string retval)
    {
        if(totalNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The total N content must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the total N content of the excreta in kg per head per unit time.";
            }
            else
            {
                retval += "\nThe total N content must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the total N content of the excreta in kg per head per unit time)";

            }
        }

        if(totalAmmoniacalNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The TAN must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the TAN content of the excretaein kg per head per unit time";
            }
            else
            {
                retval += "\nThe TAN content must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the TAN content of the excreta in kg per head per unit time";

            }
        }
        if(volatileSolids <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The volatile solids must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unit time";
            }
            else
            {
                retval += "\nThe volatile solids must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unit time";

            }
        }

        return retval;
    }
    /// <summary>
    /// Checks the core inputs (enumerators)
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="organicMatterType">organic matter type enumerator as integer</param>
    /// <param name="manureHousingSystem">housing system enumerator as integer</param>
    /// <param name="retval">String containing list of errors, if any</param>
    /// <returns>String containing list of errors, if any</returns>
    private static string CoreChecks(Sector sector, int animalType, int organicMatterType, int manureHousingSystem, string retval)
    {
        if(!CoreValidityChecks.CheckValidAnimalType(sector, animalType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid animal type for " + sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";
            }
            else
            {
                retval += "\nAn invalid animal type for " + sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";

            }
        }

        if(!CoreValidityChecks.CheckValidOrganicMatterType(organicMatterType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid manure type for " + sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid manure type.";
            }
            else
            {
                retval += "\nAn invalid manure type for " + sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid manure type.";

            }
        }
        if(!CoreValidityChecks.CheckValidHousingType(manureHousingSystem))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid housing type for " + sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid housing type.";
            }
            else
            {
                retval += "\nAn invalid housing type for " + sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid housing type.";

            }
        }

        return retval;
    }
    /// <summary>
    /// Check housing inputs for sheep
    /// </summary>
    /// <param name="inputs">Housing INputs object</param>
    /// <param name="mitigated">Boolean indicating if mitigation methods have been included</param>
    /// <returns>String containing list of errors, if any</returns>
    public static string CheckValidSheepHousingInputs(SheepHousingInputs inputs, out bool mitigated)
    {
        string retval = "";
        retval = CoreChecks(inputs.Sector, (int)inputs.AnimalType, (int)OrganicMatterType.SheepFYM, (int)ManureHousingSystem.SheepHousing, retval);

        if(inputs.SheepEnergyBalance.Equals(null))
        {
            if(retval.Equals(""))
            { retval = "Sheep energy balance object is null."; }
            else
            {
                retval += "\nSheep energy balance object is null.";
            }
        }

        if(inputs.MitigationMethods == null || inputs.MitigationMethods.Length <= 0)
        {
            mitigated = false;
        }
        else
        {
            mitigated = true;
        }

        return retval;
    }
}
