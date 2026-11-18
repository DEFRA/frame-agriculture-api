using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Libraries.CoverCrops;

/// <summary>
/// Core emissions and additional nitrogen outputs from cover crop calculations.
/// </summary>
public class CoverCropEmissions : BaseEmissions
{
    /// <summary>
    /// Nitrogen returned by cover crops, measured in kilograms of nitrogen.
    /// This is an input to the emission calculations rather than a pollutant emission.
    /// </summary>
    public CoverCropEmission N { get; set; } = new()
    {
        SummarisePollutantType = null,
        SummarisePathwayType = null,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrate leached without the cover crop reduction, measured in kilograms of nitrogen.
    /// </summary>
    public CoverCropEmission UnadjustedN03_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.Nitrate,
        SummarisePathwayType = null,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Indirect nitrous oxide from unadjusted nitrate leaching, measured in kilograms of nitrogen.
    /// </summary>
    public CoverCropEmission UnadjustedIndirectedN20_NLeached { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NitrousOxide,
        SummarisePathwayType = SummarisePathwayType.IndirectLeaching,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };


    /// <summary>
    /// Resets core and additional emission values before calculation.
    /// </summary>
    public void InitialiseCoverCropEmissions()
    {
        InitialiseBaseEmissions();
        foreach (var emission in GetAdditionalEmissions())
        {
            emission.Value = double.NaN;
        }
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

        (CoverCropEmission Emission, string Name, string Units)[] additionalOutputs =
        [
            (N, "Cover crops N return", "kg N"),
            (UnadjustedN03_N, "Unadjusted ", "kg NO3-N"),
            (UnadjustedIndirectedN20_NLeached, "Unadjusted indirect leach N2O-N", "kg N2O-N")
        ];
        foreach (var output in additionalOutputs)
        {
            if (!double.IsNaN(output.Emission.Value))
            {
                retList.Add(new Emission(output.Name, output.Units, output.Emission.Value));
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

        foreach (var emission in GetAdditionalEmissions())
        {
            if (!double.IsNaN(emission.Value))
            {
                emission.Value = Math.Round(emission.Value, decimalPlaces);
            }
        }
    }

    private IEnumerable<CoverCropEmission> GetAdditionalEmissions()
    {
        yield return N;
        yield return UnadjustedN03_N;
        yield return UnadjustedIndirectedN20_NLeached;
    }
}