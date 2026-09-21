using FrameAgricultureApi.IOClasses;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Validity checks for data helper input classes
/// </summary>
public static class DataHelperValidityChecks
{
    /// <summary>
    /// Checks if sheep energy balance key is valid
    /// </summary>
    /// <param name="key">The Sheep energy balance key object</param>
    /// <returns>string containing errors if found</returns>
    public static string CheckValidSheepEnergyBalanceKey(SheepEnergyBalanceKey key)
    {
        string retval = "";

        if(!CoreValidityChecks.CheckValidCountry(key.Country))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid country was provided,no calculations could be done.\n" +
                    "Please provide a valid country.";
            }
            else
            {
                retval += "\nAn invalid country was provided,no calculations could be done.\n" +
                    "Please provide a valid country.";
            }
        }

        if(!CoreValidityChecks.CheckValidAnimalType(Enumerators.Enumerators.Sector.Sheep, key.SheepType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid sheep type was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep type.";
            }
            else
            {
                retval += "\nAn invalid sheep type was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep type.";
            }
        }

        if(!CoreValidityChecks.CheckValidSheepSubType(key.SheepType, key.SheepSubType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid sheep sub-type was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep sub-type.";
            }
            else
            {
                retval += "\nAn invalid sheep sub-type was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep sub-type.";
            }
        }

        if(!CoreValidityChecks.CheckValidSheepSystem(key.SheepSystemType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid sheep system type was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep system type.";
            }
            else
            {
                retval += "\nAn invalid sheep system type was provided,no calculations could be done.\n" +
                    "Please provide a valid sheep system type.";
            }
        }

        return retval;
    }
    /// <summary>
    /// Checks if the first winter manure frac leach key is valid
    /// </summary>
    /// <param name="key">First winter manure frac leach key object</param>
    /// <returns>string containtin errors if found</returns>
    public static string CheckValidFirstWinterManureFracLeachKey(FirstWinterManureFracLeachKey key)
    {
        string retval = "";

        if(!CoreValidityChecks.CheckValidGridSquare(key.CellID))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid UK 10km grid square ID was provided,no calculations could be done.\n" +
                    "Please provide a valid UK 10km grid square ID.";
            }
            else
            {
                retval += "\nAn invalid UK 10km grid square ID  was provided,no calculations could be done.\n" +
                    "Please provide a valid UK 10km grid square ID.";
            }
        }

        if(!CoreValidityChecks.CheckValidSoilType(key.SoilType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid soil type was provided,no calculations could be done.\n" +
                    "Please provide a valid soil type.";
            }
            else
            {
                retval += "\nAn invalid soiltype was provided,no calculations could be done.\n" +
                    "Please provide a valid soil type.";
            }
        }

        return retval;
    }
    /// <summary>
    /// Check if grass coefficient key is valid
    /// </summary>
    /// <param name="key">Grass coefficient key object</param>
    /// <returns>string with error messages if errors found</returns>
    public static string CheckValidGrassCoefficientKey(GrassCoefficientKey key)
    {
        string retval = "";

        if(!CoreValidityChecks.CheckValidGrassType(key.GrassType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid grass type was provided,no calculations could be done.\n" +
                    "Please provide a valid grass type.";
            }
            else
            {
                retval += "\nAn invalid grass type was provided,no calculations could be done.\n" +
                    "Please provide a valid grass type.";
            }
        }

        if(!CoreValidityChecks.CheckValidGrassUseType(key.GrassUseType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid grass use type was provided,no calculations could be done.\n" +
                    "Please provide a valid grass type.";
            }
            else
            {
                retval += "\nAn invalid grass use type was provided,no calculations could be done.\n" +
                    "Please provide a valid grass type.";
            }
        }

        if(!CoreValidityChecks.CheckValidSoilType(key.SoilTextureType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid soil type was provided,no calculations could be done.\n" +
                    "Please provide a valid soil type.";
            }
            else
            {
                retval += "\nAn invalid soiltype was provided,no calculations could be done.\n" +
                    "Please provide a valid soil type.";
            }
        }

        if(!CoreValidityChecks.CheckValidClimateRegion(key.ClimateRegion))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid climate region was provided,no calculations could be done.\n" +
                    "Please provide a valid climate region .";
            }
            else
            {
                retval += "\nAn invalid climate region  was provided,no calculations could be done.\n" +
                    "Please provide a valid climate region .";
            }
        }

        if(!CoreValidityChecks.CheckValidGrassCoefficient(key.GrassCoefficientRequired))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid grass coefficient type was provided,no calculations could be done.\n" +
                    "Please provide a valid grass coefficient type.";
            }
            else
            {
                retval += "\nAn invalid grass coefficient type was provided,no calculations could be done.\n" +
                    "Please provide a valid grass coefficient type.";
            }
        }

        return retval;
    }
    /// <summary>
    /// Checks if the grass type can be converted into an enum and that the fertiliser rate is not negative or NaN
    /// </summary>
    /// <param name="jsonInputObject"></param>
    /// <returns></returns>
    public static string CheckValidGrassGeneticGainInputs(GrassGeneticGainInputs jsonInputObject)
    {
        string retval = "";
        if(!Enum.IsDefined(typeof(GrassType), jsonInputObject.GrassType))
        {
            retval = "An invlaid grass type integer value was provided.\n" + "Please provide a valid grass type.";
        }

        if(jsonInputObject.FertiliserRate < 0.0 || double.IsNaN(jsonInputObject.FertiliserRate))
        {
            if(retval.Equals(""))
            {
                retval = "Invalid fertiliser rate.\nFertiliser rate must be greater than or equal to zero.";
            }
            else
            {
                retval += "\nInvalid fertiliser rate.\nFertiliser rate must be greater than or equal to zero.";
            }
        }

        return retval;
    }
}
