using FrameAgricultureApi.IOClasses;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Storage Validity Check class
/// </summary>
public static class StorageValidityChecks
{
    /// <summary>
    /// Storage validity checks
    /// </summary>
    /// <param name="inputs">The Storage Input object</param>
    /// <param name="mitigated">boolean indicating if mitigaiton methods are included</param>
    /// <returns>errors as string</returns>
    public static string CheckValidStorageInputs(StorageInputs inputs, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidAnimalType((Sector)inputs.Sector, inputs.AnimalType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid animal type for " + inputs.Sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";
            }
            else
            {
                retval += "\nAn invalid animal type for " + inputs.Sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";

            }
        }

        if(!CoreValidityChecks.CheckValidStorageType((int)inputs.ManureStorageSystem))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid storage system for " + inputs.Sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid storage system type.";
            }
            else
            {
                retval += "\nAn invalid storage system for " + inputs.Sector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid storage system type.";

            }
        }

        if(!CoreValidityChecks.CheckValidOrganicMatterType((int)inputs.OrganicMatterType))
        {

            if(retval.Equals(""))
            {
                retval = "An invalid manure type for " + inputs.Sector.ToString() + " manure storage was provided,no calculations could be done.\n" +
                    "Please provide a manure type.";
            }
            else
            {
                retval += "\nAn invalid manure type for " + inputs.Sector.ToString() + " manure storage was provided,no calculations could be done.\n" +
                    "Please provide a valid manure type.";

            }
        }

        if(inputs.TotalNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The total N content must be provided for all excretion on yards.\n" +
                    "Please provide the total N content of the excreta in kg per head per unit time.";
            }
            else
            {
                retval += "\nThe total N content must be provided for all excretion on yards.\n" +
                    "Please provide the total N content of the excreta in kg per head per unit time)";

            }
        }

        if(inputs.TotalAmmoniacalNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The TAN must be provided for all excretion on yards.\n" +
                    "Please provide the TAN content of the excreta in kg per head per unit time";
            }
            else
            {
                retval += "\nThe TAN content must be provided for all excretion on yards.\n" +
                    "Please provide the TAN content of the excreta in kg per head per unit time";

            }
        }
        if(inputs.VolatileSolids <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The volatile solids must be provided for all excretion on yards.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unit time";
            }
            else
            {
                retval += "\nThe volatile solids must be provided for all excretion on yards.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unit time";

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
    /// <summary>
    /// Checks if storage inputs are valid
    /// </summary>
    /// <param name="inputs">Sheep storage inputs object</param>
    /// <param name="mitigated">Boolean out variable set to true if mitigation method sincluded in input</param>
    /// <returns>errors as string</returns>
    public static string CheckValidStorageInputs_Sheep(SheepStorageInputs inputs, out bool mitigated)
    {

        string retval = "";
        if(!CoreValidityChecks.CheckValidAnimalType(Sector.Sheep, inputs.AnimalType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid animal type for " + Sector.Sheep.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";
            }
            else
            {
                retval += "\nAn invalid animal type for " + Sector.Sheep.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";

            }
        }
        if(inputs.TotalNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The total N content must be provided for all excretion on yards.\n" +
                    "Please provide the total N content of the excreta in kg per head per unit time.";
            }
            else
            {
                retval += "\nThe total N content must be provided for all excretion on yards.\n" +
                    "Please provide the total N content of the excreta in kg per head per unit time)";

            }
        }

        if(inputs.TotalAmmoniacalNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The TAN must be provided for all excretion on yards.\n" +
                    "Please provide the TAN content of the excreta in kg per head per unit time";
            }
            else
            {
                retval += "\nThe TAN content must be provided for all excretion on yards.\n" +
                    "Please provide the TAN content of the excreta in kg per head per unit time";

            }
        }
        if(inputs.SheepEnergyBalance.Equals(null))
        {
            if(retval.Equals(""))
            {
                retval = "Sheep Energy Balance Object is null, please ensure thet you have provided correct sheep energy balance information.";
            }
            else
            {
                retval += "\nSheep Energy Balance Object is null, please ensure thet you have provided correct sheep energy balance information.";
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
