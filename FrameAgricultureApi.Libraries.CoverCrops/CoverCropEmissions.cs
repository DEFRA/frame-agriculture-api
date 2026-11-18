using System;
using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Libraries.CoverCrops;

public class CoverCropEmissions : BaseEmissions
{
    /// <summary>
    /// Additional outputs dictionary
    /// </summary>
    public Dictionary<AdditionalCoverCropEmissions, Emission> additionalOutputs = [];

    public CoverCropEmission N = new()
    {
        // Todo I have just assumed N is nitrate 
        SummarisePollutantType = SummarisePollutantType.Nitrate,
        SummarisePathwayType = null,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    public CoverCropEmission UnadjustedN03_N = new()
    {
        //Todo N03_N is Ammonia nitrate which isn't in the summaries pollutant type
        SummarisePollutantType = SummarisePollutantType.,
        SummarisePathwayType = null,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    public CoverCropEmission UnadjustedIndirectedN20_NLeached = new()
    {
        // Todo What is N20_N
        SummarisePollutantType = SummarisePollutantType.NitrousOxide,
        SummarisePathwayType = SummarisePathwayType.IndirectLeaching,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };


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