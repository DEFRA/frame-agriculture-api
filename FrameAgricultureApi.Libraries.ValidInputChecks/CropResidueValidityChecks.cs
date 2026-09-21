using FrameAgricultureApi.IOClasses;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Validity checks specific to crop residues
/// </summary>
public static class CropResidueValidityChecks
{
    /// <summary>
    /// Checks that valid grid square ID and valid crop type are provided
    /// </summary>
    /// <param name="cropResidueInputs"></param>
    /// <param name="mitigated"></param>
    /// <returns>String containing error messages, empty string if no errors</returns>
    public static string CheckValidInputs(CropResidueInputs_IPCC cropResidueInputs, out bool mitigated)
    {
        string retval = "";

        if(!CoreValidityChecks.CheckValidCropType((int)cropResidueInputs.CropType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid crop type was provided,no calculations could be done.\n" +
                    "Please provide a valid crop type.";
            }
            else
            {
                retval += "\nAn invalid crop type was provided,no calculations could be done.\n" +
                    "Please provide a valid crop type.";

            }
        }
        if(double.IsNaN((double)cropResidueInputs.SlopeIPCC))
        {
            retval = "Slope for IPCC equation is not defined, no calculations could be done.\n" +
                "Please provide a value for the slope of the IPCC equation.";
        }
        if(double.IsNaN((double)cropResidueInputs.InterceptIPCC))
        {
            retval = "Intercept for IPCC equation is not defined, no calculations could be done.\n" +
                "Please provide a value for the intercept of the IPCC equation.";
        }
        if(double.IsNaN((double)cropResidueInputs.CropDryMatterContent) || cropResidueInputs.CropDryMatterContent.Equals(0.0))
        {
            retval = "Invalid value for crop dry matter content, no calculations could be done.\n" +
                "Please provide a value for the dry matter content of the crop.";
        }
        if(cropResidueInputs.MitigationMethods == null || cropResidueInputs.MitigationMethods.Count() <= 0)
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
    /// Checks that valid grid square ID and valid crop type are provided
    /// </summary>
    /// <param name="cropResidueInputs"></param>
    /// <param name="mitigated"></param>
    /// <returns>String containing error messages, empty string if no errors</returns>
    public static string CheckValidInputs_HarvestIndex(CropResidueInputs_HarvestIndex cropResidueInputs, out bool mitigated)
    {
        string retval = "";

        if(!CoreValidityChecks.CheckValidCropType((int)cropResidueInputs.CropType))
        {
            if(retval.Equals(""))
            {
                retval = "An invalid crop type was provided,no calculations could be done.\n" +
                    "Please provide a valid crop type.";
            }
            else
            {
                retval += "\nAn invalid crop type was provided,no calculations could be done.\n" +
                    "Please provide a valid crop type.";

            }
        }
        if(double.IsNaN((double)cropResidueInputs.CropHarvestIndex) || cropResidueInputs.CropHarvestIndex.Equals(0.0))
        {
            retval = "Crop harvest index is not defined, no calculations could be done.\n" +
                "Please provide a non-zero value for the crop harvest index.";
        }
        if(double.IsNaN((double)cropResidueInputs.CropDryMatterContent) || cropResidueInputs.CropDryMatterContent.Equals(0.0))
        {
            retval = "Invalid value for crop dry matter content, no calculations could be done.\n" +
                "Please provide a value for the dry matter content of the crop.";
        }
        if(cropResidueInputs.MitigationMethods == null || cropResidueInputs.MitigationMethods.Count() <= 0)
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

