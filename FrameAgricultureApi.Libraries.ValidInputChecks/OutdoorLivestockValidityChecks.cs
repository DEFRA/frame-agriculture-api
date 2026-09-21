using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Outdoor livestock excretino input validity checks
/// </summary>
public static class OutdoorLivestockValidityChecks
{
    /// <summary>
    /// Check valid input sfor pig, poutlry and inor livestock excretion outdoors
    /// </summary>
    /// <param name="inputs">Base Outdoor Excreta Inputs object</param>
    /// <returns>String with errors. if any</returns>
    public static string CheckValidOutdoorLivestockInputs(BaseOutdoorExcretaInputs inputs)//, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidAnimalType((Sector)inputs.MySector, inputs.AnimalType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid animal type for " + inputs.MySector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";
            }
            else
            {
                retval += "\nAn invalid animal type for " + inputs.MySector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid animal type.";

            }
        }
        if(inputs.TotalNitrogen <= 0)
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

        if(inputs.TotalAmmoniacalNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The TAN must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the TAN content of the excretaein kg per head per unti time";
            }
            else
            {
                retval += "\nThe TAN content must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the TAN content of the excreta in kg per head per unit time";

            }
        }

        if(inputs.VolatileSolids <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The volatile solids must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unti time";
            }
            else
            {
                retval += "\nThe volatile solids must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unit time";

            }
        }

        //No mitigation for outdoor excreta
        //if (inputs.MitigationMethods == null || inputs.MitigationMethods.Count() <= 0)
        //{
        //    mitigated = false;
        //}
        //else
        //{
        //    mitigated = true;
        //}
        return retval;
    }
    /// <summary>
    /// Check valid inputs for Cattle excretion outdoors
    /// </summary>
    /// <param name="inputs">Cattle OUtdoor Excreat Input object</param>
    /// <returns>String with errors if any occurred</returns>
    public static string CheckValidOutdoorCattleInputs(CattleOutdoorExcretaInputs inputs)//, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidAnimalType((Sector)inputs.MySector, inputs.AnimalType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid cattle type for " + inputs.MySector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid cattle type.";
            }
            else
            {
                retval += "\nAn invalid cattle type for " + inputs.MySector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid cattle type.";

            }
        }

        if(inputs.UrineNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The urine nitrogen must be provided for all cattle excretion outdoors.\n" +
                    "Please provide nitrogen content of the urine in kg per head per unti time";
            }
            else
            {
                retval += "\nThe urine nitrogen must be provided for all cattle excretion outdoors.\n" +
                    "Please provide nitrogen content of the urine in kg per head per unti time";
            }
        }

        if(inputs.DungNitrogen <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The dung nitrogen must be provided for all cattle excretion outdoors.\n" +
                    "Please provide nitrogen content of the dung in kg per head per unti time";
            }
            else
            {
                retval += "\nThe dung nitrogen must be provided for all cattle excretion outdoors.\n" +
                    "Please provide nitrogen content of the dung in kg per head per unti time";
            }
        }

        if(inputs.VolatileSolids <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The volatile solids must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unti time";
            }
            else
            {
                retval += "\nThe volatile solids must be provided for all livestock excretion outdoors.\n" +
                    "Please provide the volatile solids content of the excreta in kg per head per unit time";

            }
        }
        //No mitigation for outdoor excreta
        //if (inputs.MitigationMethods == null || inputs.MitigationMethods.Count() <= 0)
        //{
        //    mitigated = false;
        //}
        //else
        //{
        //    mitigated = true;
        //}
        return retval;
    }
    /// <summary>
    /// Check valid inputs for sheep excretion outdoors
    /// </summary>
    /// <param name="inputs">Sheep outdoor excreate inputs object</param>
    /// <returns>string with any aerror messages</returns>
    public static string CheckValidOutdoorSheepInputs(SheepOutdoorExcretaInputs inputs)//, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidAnimalType((Sector)inputs.MySector, inputs.AnimalType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid sheep type for " + inputs.MySector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep type.";
            }
            else
            {
                retval += "\nAn invalid sheep type for " + inputs.MySector.ToString() + " was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep type.";

            }
        }
        if(!CoreValidityChecks.CheckValidPolynomialCOefficients((GrassEquationCoefficients)inputs.FracLeachCoefficients))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid set of grass equation coefficients for the variable frac leach was supplied, no calculations could be done.\n" +
                    "Please provide a valid set of coefficients for the variable frac leach fourth order polynomial equation.";
            }
            else
            {
                retval += "\nAn invalid set of grass equation coefficients for the variable frac leach was supplied, no calculations could be done.\n" +
                    "Please provide a valid set of coefficients for the variable frac leach fourth order polynomial equation.";
            }
        }

        //No mitigation for outdoor excreta
        //if (inputs.MitigationMethods == null || inputs.MitigationMethods.Count() <= 0)
        //{
        //    mitigated = false;
        //}
        //else
        //{
        //    mitigated = true;
        //}
        return retval;
    }
}
