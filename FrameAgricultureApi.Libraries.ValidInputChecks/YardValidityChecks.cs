using FrameAgricultureApi.IOClasses;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Yard validity checks class
/// </summary>
public static class YardValidityChecks
{
    /// <summary>
    /// Yard validity checks
    /// </summary>
    /// <param name="inputs">the Yarn Input object</param>
    /// <param name="mitigated">boolean indicating if mitigaiton methods are included</param>
    /// <returns>errors as string</returns>
    public static string CheckValidYardInputs(YardInputs inputs, out bool mitigated)
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

        if(inputs.MitigationMethods == null || inputs.MitigationMethods.Count() <= 0)
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
