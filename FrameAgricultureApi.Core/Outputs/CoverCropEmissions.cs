using System.Text.Json.Serialization;
using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

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
        // Todo I have just assumed N is nitrogen 
        SummarisePollutantType = SummarisePollutantType.N,
        // Todo I have assumed this is direct
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrate leached without the cover crop reduction, measured in kilograms of nitrogen.
    /// </summary>
    [JsonPropertyName("unadjustedN03_N")]
    public CoverCropEmission Unadjusted_NO3_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NO3_N,
        // Todo I have assumed this is direct
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Indirect nitrous oxide from unadjusted nitrate leaching, measured in kilograms of nitrogen.
    /// </summary>
    [JsonPropertyName("unadjustedIndirectedN20_NLeached")]
    public CoverCropEmission UnadjustedIndirected_N2O_N_Leached { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2O,
        SummarisePathwayType = SummarisePathwayType.IndirectLeaching,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Method to round emissions to a fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to be rounded to</param>
    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach (var emission in GetAdditionalEmissions())
        {
            if (emission.Value is decimal value)
            {
                emission.Value = decimal.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
            }
        }
    }

    private IEnumerable<CoverCropEmission> GetAdditionalEmissions()
    {
        yield return N;
        yield return Unadjusted_NO3_N;
        yield return UnadjustedIndirected_N2O_N_Leached;
    }
}