using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Manure mass and volumen input validity checks
/// </summary>
public static class ManureMassVolumeValidityChecks
{
    /// <summary>
    /// Base checks for validity of manure mass and volume inputs
    /// </summary>
    /// <param name="inputs">the base manre mass and volume input object</param>
    /// <returns>Errors as string</returns>
    public static string CheckValidMMVInputs(ManureMassVolumeInputs inputs)
    {
        string retval = "";

        retval = CheckValid_CoreInputs(inputs.Sector, (int)inputs.AnimalType, (int)inputs.OrganicMatterType, retval);

        return retval;
    }
    /// <summary>
    /// Core input checks
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="organicMatterType">organic matter type enumerator as integer</param>
    /// <param name="retval">current error string</param>
    /// <returns>Errors as string</returns>
    private static string CheckValid_CoreInputs(Enumerators.Enumerators.Sector sector, int animalType, int organicMatterType, string retval)
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

        return retval;
    }
    /// <summary>
    /// Checks manure mass and volume inputs for cattle are valid
    /// </summary>
    /// <param name="inputs">the cattle manure mass and volume input object</param>
    /// <returns>Errors as string</returns>
    public static string CheckValidCattleMMVInputs(ManureMassVolumeCattleInputs inputs)
    {
        string retval = "";

        retval = CheckValid_CoreInputs(inputs.Sector, (int)inputs.AnimalType, (int)inputs.OrganicMatterType, retval);

        if(inputs.TimeInLocation == null)
        {
            if(retval.Equals(""))
            {
                retval = "The time cattle spend at graxing, on yards or in housing has not been initialised.\nPlease initialise the time in locations.";
            }
            else
            {
                retval += "\nThe time cattle spend at graxing, on yards or in housing has not been initialised.\nPlease initialise the time in locations.";
            }
        }
        if(inputs.TimeInLocation.Sum() < 100)
        {
            inputs.TimeInLocation.Redistribute();
        }

        if(inputs.CattleDryMatterIntake <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The dry matter intake of cattle must be greater than zero.\n Please provide a dry matter intake (kg/head/unit time).";
            }
            else
            {
                retval += "\nThe dry matter intake of cattle must be greater than zero.\n Please provide a dry matter intake (kg/head/unit time).";
            }
        }

        if(inputs.CattleDryMatterDigestibility <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The dry matter digestibility of cattle diets must be greater than zero.\n Please provide a value for digestible dry matter in the diet (kg/kg).";
            }
            else
            {
                retval += "\nThe dry matter digestibility of cattle must be greater than zero.\n Please provide a value for digestible dry matter in the diet (kg/kg).";
            }
        }
        if(inputs.CattleDryMatterContent <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The dry matter content of cattle diets must be greater than zero.\n Please provide a value for digestible dry matter in the diet (kg/kg).";
            }
            else
            {
                retval += "\nThe dry matter content of cattle must be greater than zero.\n Please provide a value for digestible dry matter in the diet (kg/kg).";
            }
        }

        return retval;
    }

    /// <summary>
    /// Base checks for validity of manure mass and volume inputs
    /// </summary>
    /// <param name="animalType">animal typ eenumerator as integere</param>
    /// <param name="sheepEnergyBalance">Sheep energy balance parameters object</param>
    /// <returns>Errors as string</returns>
    public static string CheckValidSheepMMVInputs(int animalType, SheepEnergyBalance sheepEnergyBalance)
    {
        string retval = "";

        retval = CheckValid_CoreInputs(Sector.Sheep, animalType, (int)OrganicMatterType.SheepFYM, retval);

        if(sheepEnergyBalance.Equals(null))
        {
            if(retval.Equals(""))
            {
                retval = "Sheep energy balance object is null, please provide non-null object.";
            }
            else
            {
                retval += "\nSheep energy balance object is null, please provide non-null object.";
            }
        }

        return retval;
    }
}
