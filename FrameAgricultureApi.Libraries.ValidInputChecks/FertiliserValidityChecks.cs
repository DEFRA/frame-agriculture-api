using FrameAgricultureApi.IOClasses;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Validity checks for Fertiliser endpoint
/// </summary>
public static class FertiliserValidityChecks
{
    /// <summary>
    /// Checks whether the grid square, fertiliser type, fertiliser array inputs are vaid for fertiliser endpoint
    /// </summary>
    /// <param name="fertiliserInputs"></param>
    /// <param name="mitigated">Mitigated boolean - set to true if mitigaiton method list is valid</param>
    /// <returns>Error messages as string, emptyr string if no errors</returns>
    public static string CheckValidInputs(FertiliserInput fertiliserInputs, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidGridSquare(fertiliserInputs.GridSquare))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid Grid Square ID was provided,no calculations could be done.\n" +
                    "Please provide a valid UK 10km grid square ID.";
            }
            else
            {
                retval += "\nAn invalid Grid Square ID was provided,no calculations could be done.\n" +
                    "Please provide a valid UK 10km grid square ID.";
            }
        }
        if(!CoreValidityChecks.CheckValidFertiliserType((int)fertiliserInputs.FertiliserType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid fertiliser type was provided,no calculations could be done.\n" +
                    "Please provide a valid fertiliser types.";
            }
            else
            {
                retval += "\nAn invalid fertiliser type was provided,no calculations could be done.\n" +
                    "Please provide a valid fertiliser types.";

            }
        }
        if(!CoreValidityChecks.CheckFertiliserApplicationArrayLength(fertiliserInputs.FertiliserApplications))
        {
            if(retval.Equals(""))
            {
                retval = "The fertiliser applications array was not of length 12.\n Please provide an array of length 12.";
            }
            else
            {
                retval += "\nThe fertiliser applications array was not of length 12.\n Please provide an array of length 12.";

            }
        }
        if(!CoreValidityChecks.CheckFertiliserAppliedNonZero(fertiliserInputs.FertiliserApplications))
        {
            if(retval.Equals(""))
            {
                retval = "Zero fertiliser applied.\n The amount of fertiliser applied must be greater than zero.";
            }
            else
            {
                retval += "\nZero fertiliser applied.\n The amount of fertiliser applied must be greater than zero.";
            }
        }
        if(fertiliserInputs.MitigationMethods == null || fertiliserInputs.MitigationMethods.Length <= 0)
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
    /// Checks whether grass fertiliser inputs are valid
    /// </summary>
    /// <param name="fertiliserInputs">a Grass fertiliser input object</param>
    /// <param name="mitigated">return parameter that is set to true if list of mitigation methods provided</param>
    /// <returns>errors as string</returns>
    public static string CheckValidInputs(GrassFertiliserInput fertiliserInputs, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidGridSquare(fertiliserInputs.GridSquare))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid Grid Square ID was provided,no calculations could be done.\n" +
                    "Please provide a valid UK 10km grid square ID.";
            }
            else
            {
                retval += "\nAn invalid Grid Square ID was provided,no calculations could be done.\n" +
                    "Please provide a valid UK 10km grid square ID.";
            }
        }
        if(!CoreValidityChecks.CheckValidFertiliserType((int)fertiliserInputs.FertiliserType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid fertiliser type was provided,no calculations could be done.\n" +
                    "Please provide a valid fertiliser types.";
            }
            else
            {
                retval += "\nAn invalid fertiliser type was provided,no calculations could be done.\n" +
                    "Please provide a valid fertiliser types.";

            }
        }
        if(!CoreValidityChecks.CheckValidSoilType(fertiliserInputs.SoilType))
        {
            if(retval.Equals(""))
            {
                retval = "An invlaid soil type was provided, no calcualtions could be done.\n" +
                    "Please provide a valid soil type.";
            }
            else
            {
                retval += "An invlaid soil type was provided, no calcualtions could be done.\n" +
                   "Please provide a valid soil type.";
            }
        }
        if(!CoreValidityChecks.CheckValidGrassType(fertiliserInputs.GrassType))
        {
            if(retval.Equals(""))
            {
                retval = "An invlaid grass type was provided, no calcualtions could be done.\n" +
                    "Please provide a valid grass type.";
            }
            else
            {
                retval += "An invlaid grass type was provided, no calcualtions could be done.\n" +
                   "Please provide a valid grass type.";
            }
        }
        if(!CoreValidityChecks.CheckValidGrassUseType(fertiliserInputs.GrassUseType))
        {
            if(retval.Equals(""))
            {
                retval = "An invlaid grass use type was provided, no calcualtions could be done.\n" +
                    "Please provide a valid grass use type.";
            }
            else
            {
                retval += "An invlaid grass use type was provided, no calcualtions could be done.\n" +
                   "Please provide a valid grass use type.";
            }
        }
        if(!CoreValidityChecks.CheckFertiliserApplicationArrayLength(fertiliserInputs.FertiliserApplications))
        {
            if(retval.Equals(""))
            {
                retval = "The fertiliser applications array was not of length 12.\n Please provide an array of length 12.";
            }
            else
            {
                retval += "\nThe fertiliser applications array was not of length 12.\n Please provide an array of length 12.";

            }
        }
        if(!CoreValidityChecks.CheckFertiliserAppliedNonZero(fertiliserInputs.FertiliserApplications))
        {
            if(retval.Equals(""))
            {
                retval = "Zero fertiliser applied.\n The amount of fertiliser applied must be greater than zero.";
            }
            else
            {
                retval += "\nZero fertiliser applied.\n The amount of fertiliser applied must be greater than zero.";
            }
        }
        if(fertiliserInputs.MitigationMethods == null || fertiliserInputs.MitigationMethods.Length <= 0)
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

