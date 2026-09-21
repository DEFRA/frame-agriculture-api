using FrameAgricultureApi.IOClasses;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Validity checks for organic matter application endpoint
/// </summary>
public static class OrganicMatterApplicationValidityChecks
{
    /// <summary>
    /// Checks that organic matter type is valid and that a nonzero application has taken place
    /// </summary>
    /// <param name="inputs"></param>
    /// <param name="mitigated">boolwean indicating if mitigaiton is being used</param>
    /// <returns>Error messages as string, emptyr string if no errors</returns>
    public static string CheckValidInputs(OrganicMatterApplicationInputs inputs, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidOrganicMatterType((int)inputs.OrganicMatterType))
        {

            if(retval.Equals(""))
            {
                retval = "An invalid (non-livestock) organic matter type was provided,no calculations could be done.\n" +
                    "Please provide a valid organic matter type.";
            }
            else
            {
                retval += "\nAn invalid (non-livestock) organic matter type was provided,no calculations could be done.\n" +
                    "Please provide a valid organic matter type.";

            }
        }

        if((int)inputs.OrganicMatterType <= 14)
        {
            if(retval.Equals(""))
            {
                retval = "This endpoint should not be used for livestock manures, please use the sepcific livestock or sheep endpoints as appropriate.";
            }
            else
            {

                retval += "T\nhis endpoint should not be used for livestock manures, please use the sepcific livestock or sheep endpoints as appropriate.";
            }
        }

        if(!CoreValidityChecks.CheckValidLandUse((int)inputs.LandAppliedTo))
        {

            if(retval.Equals(""))
            {
                retval = "An invalid Spreading Lnad Use type was provided,no calculations could be done.\n" +
                    "Please provide a valid Spreading Land Use type.";
            }
            else
            {
                retval += "\nAn invalid Spreading Land Use type was provided,no calculations could be done.\n" +
                    "Please provide a valid Spreading Land Use type.";

            }
        }

        if(inputs.QuantityApplied <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The quantity applied was not greater than zero.\n" +
                    "Please provide a quantity applied greater than zero";
            }
            else
            {
                retval += "\nThe quantity applied was not greater than zero.\n" +
                    "Please provide a quantity applied greater than zero";

            }
        }

        if(!Enum.IsDefined(typeof(Month), inputs.Month))
        {
            if(retval.Equals(""))
            {
                retval = "The month enumerator integer provided was not valid.\n" +
                    "Month enumerator integers run form 0 for January to 11 for December, with 12 being used if month is not set.";
            }
            else
            {
                retval += "\nThe month enumerator integer provided was not valid.\n" +
                    "Month enumerator integers run form 0 for January to 11 for December, with 12 being used if month is not set.";

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
    /// ALgorithms to check that livestock manure application inputs are valid.
    /// </summary>
    /// <param name="inputs"></param>
    /// <param name="mitigated"></param>
    /// <returns></returns>
    public static string CheckValidInputsLivestock(LivestockManureApplicationInputs inputs, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidOrganicMatterType((int)inputs.OrganicMatterType) || (int)inputs.OrganicMatterType >= 15)
        {

            if(retval.Equals(""))
            {
                retval = "An invalid livestock organic matter type was provided,no calculations could be done.\n" +
                    "Please provide a valid livestock organic matter type.";
            }
            else
            {
                retval += "\nAn invalid livestock organic matter type was provided,no calculations could be done.\n" +
                    "Please provide a valid livestock organic matter type.";

            }
        }

        if(inputs.Month.Equals(Month.NotSet) || inputs.Month.Equals(null) || !Enum.IsDefined(typeof(Month), inputs.Month))
        {
            if(retval.Equals(""))
            {
                retval = "The month must be provided for all livestock manure applications.\n" +
                    "Please provide the month (note that the enumerator runs form 0 to 11 for January to December)";
            }
            else
            {
                retval += "\nThe month must be provided for all livestock manure applications.\n" +
                    "Please provide the month (note that the enumerator runs form 0 to 11 for January to December)";

            }
        }

        if(!CoreValidityChecks.CheckValidLandUse((int)inputs.LandAppliedTo))
        {

            if(retval.Equals(""))
            {
                retval = "An invalid Spreading Lnad Use type was provided,no calculations could be done.\n" +
                    "Please provide a valid Spreading Land Use type.";
            }
            else
            {
                retval += "\nAn invalid Spreading Land Use type was provided,no calculations could be done.\n" +
                    "Please provide a valid Spreading Land Use type.";

            }
        }

        if(inputs.TotalN <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The total N applied was not greater than zero.\n" +
                    "Please provide a total N applied greater than zero";
            }
            else
            {
                retval += "\nThe total N applied was not greater than zero.\n" +
                    "Please provide a totalN applied greater than zero";

            }
        }

        //if (inputs.TAN <= 0)
        //{
        //    if (retval.Equals(""))
        //    {
        //        retval = "The TAN applied was not greater than zero.\n" +
        //            "Please provide a total N applied greater than zero";
        //    }
        //    else
        //    {
        //        retval += "\nThe TANN applied was not greater than zero.\n" +
        //            "Please provide a totalN applied greater than zero";

        //    }
        //}

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
    /// ALgorithms to check that livestock manure application inputs are valid.
    /// </summary>
    /// <param name="inputs"></param>
    /// <param name="mitigated"></param>
    /// <returns></returns>
    public static string CheckValidInputsSheep(SheepManureApplicationInputs inputs, out bool mitigated)
    {
        string retval = "";

        if(inputs.Month.Equals(Month.NotSet) || inputs.Month.Equals(null) || !Enum.IsDefined(typeof(Month), inputs.Month))
        {
            if(retval.Equals(""))
            {
                retval = "The month must be provided for all livestock manure applications.\n" +
                    "Please provide the month (note that the enumerator runs form 0 to 11 for January to December)";
            }
            else
            {
                retval += "\nThe month must be provided for all livestock manure applications.\n" +
                    "Please provide the month (note that the enumerator runs form 0 to 11 for January to December)";

            }
        }

        if(inputs.TotalN <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The total N applied was not greater than zero.\n" +
                    "Please provide a total N applied greater than zero";
            }
            else
            {
                retval += "\nThe total N applied was not greater than zero.\n" +
                    "Please provide a totalN applied greater than zero";

            }
        }

        //if (inputs.TAN <= 0)
        //{
        //    if (retval.Equals(""))
        //    {
        //        retval = "The TAN applied was not greater than zero.\n" +
        //            "Please provide a total N applied greater than zero";
        //    }
        //    else
        //    {
        //        retval += "\nThe TANN applied was not greater than zero.\n" +
        //            "Please provide a totalN applied greater than zero";

        //    }
        //}

        if(inputs.FristWinterManureFracLeach <= 0)
        {
            if(retval.Equals(""))
            {
                retval = "The first winter manure frac leach percentage was not greater than zero.\n" +
                    "Please provide a first winter manure frac leach percentage greater than zero";
            }
            else
            {
                retval += "\nThe first winter manure frac leach percentage was not greater than zero.\n" +
                    "Please provide a first winter manure frac leach percentagegreater than zero";

            }
        }

        if(inputs.ManureFracLeachCoefficients.Equals(null))
        {
            if(retval.Equals(""))
            {
                retval = "The grass coefficient object is null, please provided a non-null object.";
            }
            else
            {
                retval += "\nThe grass coefficient object is null, please provided a non-null object.";
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
