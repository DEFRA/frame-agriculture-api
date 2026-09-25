using System;

namespace FrameAgricultureApi.Libraries.CoverCrops;

public class CoverCropEmissions : BaseEmissions
{
    /// <summary>
    /// Additional outputs dictionary
    /// </summary>
    public Dictionary<AdditionalCoverCropEmissions, Emission> additionalOutputs = [];

    /// <summary>
    /// Initialisation method
    /// </summary>
    public void InitialiseCoverCropEmissions()
    {
        InitialiseBaseEmissions();
        additionalOutputs = [];
        additionalOutputs.Clear();
        additionalOutputs.Add(AdditionalCoverCropEmissions.Nreturn, new Emission("Cover crops N return", "kg N", double.NaN));
        additionalOutputs.Add(AdditionalCoverCropEmissions.unadjustedNO3N, new Emission("Unadjusted ", "kg NO3-N", double.NaN));
        additionalOutputs.Add(AdditionalCoverCropEmissions.unadjustedN2ONLeach, new Emission("Unadjusted indirect leach N2O-N", "kg N2O-N", double.NaN));
    }

    /// <summary>
    /// Export emissions method
    /// </summary>
    /// <returns>List of emission objects</returns>
    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = [];
        foreach (CoreEmissions key in EmissionsCore.Keys)
        {
            if (!double.IsNaN(EmissionsCore[key].Value))
            {
                retList.Add(EmissionsCore[key]);
            }
        }

        foreach (AdditionalCoverCropEmissions key in additionalOutputs.Keys)
        {
            if (!double.IsNaN(additionalOutputs[key].Value))
            {
                retList.Add(additionalOutputs[key]);
            }
        }
        return retList;
    }
    /// <summary>
    /// Method to round emissions to a fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to be rounded to</param>
    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach (AdditionalCoverCropEmissions key in additionalOutputs.Keys)
        {
            if (!double.IsNaN(additionalOutputs[key].Value))
            {
                additionalOutputs[key].Value = Math.Round(additionalOutputs[key].Value, decimalPlaces);
            }
        }
    }
}