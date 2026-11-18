using System;
using FrameAgricultureApi.Core.Enums;

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
        additionalOutputs.Add(AdditionalCoverCropEmissions.NReturn, new Emission("Cover crops N return", "kg N", double.NaN));
        additionalOutputs.Add(AdditionalCoverCropEmissions.UnadjustedNO3N, new Emission("Unadjusted ", "kg NO3-N", double.NaN));
        additionalOutputs.Add(AdditionalCoverCropEmissions.UnadjustedN2ONLeach, new Emission("Unadjusted indirect leach N2O-N", "kg N2O-N", double.NaN));
    }

    /// <summary>
    /// Export emissions method
    /// </summary>
    /// <returns>List of emission objects</returns>
    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = [];
        (BaseEmission Emission, string Name, string Units)[] coreOutputs =
        [
            (DirectN20_N, "Direct N2O-N", "kg N2O-N"),
            (IndirectN20_NLeached, "Indirect N2O-N (leached)", "kg N2O-N"),
            (IndirectN20_NVolatilisation, "Indirect N2O-N (volatilisation)", "kg N2O-N"),
            (DirectNH3_N, "Direct NH3-N", "kg NH3-N"),
            (DirectNO3_NLeached, "Direct NO3-N (leached)", "kg NO3-N"),
            (DirectNO_N, "Direct NO-N", "kg NO-N"),
            (DirectN2_N, "Direct N2-N", "kg N2-N")
        ];
        foreach (var output in coreOutputs)
        {
            if (!double.IsNaN(output.Emission.Value))
            {
                retList.Add(new Emission(output.Name, output.Units, output.Emission.Value));
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