using FrameAgricultureApi.IOClasses;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Grass residue input validity checks
/// </summary>
public static class GrassResidueValidityChecks
{
    /// <summary>
    /// Checks if grass residue inputs are valid
    /// </summary>
    /// <param name="residueInputs">A grass residue inputs object</param>
    /// <param name="mitigated">Boolean out variable tha is set to true if mitigatiion methofs have been included</param>
    /// <returns>errors as string</returns>
    public static string CheckValidInputs(GrassProductionandRenewalInput residueInputs, out bool mitigated)
    {
        string retval = "";
        if(!CoreValidityChecks.CheckValidGridSquare(residueInputs.GridSquare))
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
        if(!CoreValidityChecks.CheckValidSoilType(residueInputs.SoilType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid soil type was provided, no calcualtions could be done.\n" +
                    "Please provide a valid soil type.";
            }
            else
            {
                retval += "\nAn invalid soil type was provided, no calcualtions could be done.\n" +
                   "Please provide a valid soil type.";
            }
        }
        if(!CoreValidityChecks.CheckValidGrassType(residueInputs.GrassType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid grass type was provided, no calcualtions could be done.\n" +
                    "Please provide a valid grass type.";
            }
            else
            {
                retval += "\nAn invalid grass type was provided, no calcualtions could be done.\n" +
                   "Please provide a valid grass type.";
            }
        }
        if(!CoreValidityChecks.CheckValidGrassUseType(residueInputs.GrassUseType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid grass use type was provided, no calcualtions could be done.\n" +
                    "Please provide a valid grass use type.";
            }
            else
            {
                retval += "\nAn invalid grass use type was provided, no calcualtions could be done.\n" +
                   "Please provide a valid grass use type.";
            }
        }
        if(residueInputs.GeneticGainScalars == null)
        {
            if(retval.Equals(""))
            {
                retval = "An invalid genetic gain scalar object was provided, no calcualtions could be done.\n" +
                    "Please provide a valid grass genetic gain scalar object.";
            }
            else
            {
                retval += "\nAn invalid genetic gain scalar object was provided, no calcualtions could be done.\n" +
                    "Please provide a valid grass genetic gain scalar object.";
            }
        }
        if(residueInputs.MitigationMethods == null || residueInputs.MitigationMethods.Length <= 0)
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

