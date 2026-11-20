using System.Text.Json.Serialization;
using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

/// <summary>
/// Standard nitrogen emissions, identified by pollutant and pathway.
/// </summary>
public class BaseEmissions
{
    /// <summary>
    /// Direct N2O-N, measured as nitrogen in kilograms.
    /// </summary>
    [JsonPropertyName("directN20_N")]
    public BaseEmission Direct_N2O_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2O,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Indirect N2O-N (leached), measured as nitrogen in kilograms.
    /// </summary>
    [JsonPropertyName("indirectN20_NLeached")]
    public BaseEmission Indirect_N2O_N_Leached { get; set; } = new()
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
    /// Indirect N2O-N (volatilisation), measured as nitrogen in kilograms.
    /// </summary>
    [JsonPropertyName("indirectN20_NVolatilisation")]
    public BaseEmission Indirect_N2O_N_Volatilisation { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2O,
        SummarisePathwayType = SummarisePathwayType.IndirectDeposition,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Direct NO3-N (leached), measured as nitrogen in kilograms.
    /// </summary>
    [JsonPropertyName("directNO3_NLeached")]
    public BaseEmission Direct_NO3_N_Leached { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NO3,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Direct NO-N, measured as nitrogen in kilograms.
    /// </summary>
    [JsonPropertyName("directNO_N")]
    public BaseEmission Direct_NO_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NO,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Direct N2-N, measured as nitrogen in kilograms.
    /// </summary>
    [JsonPropertyName("directN2_N")]
    public BaseEmission Direct_N2_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };


    /// <summary>
    /// Rounds all calculated core emission values.
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to use.</param>
    public void RoundCoreEmissions(int decimalPlaces)
    {
        foreach (var emission in GetCoreEmissions())
        {
            if (emission.Value is decimal value)
            {
                emission.Value = decimal.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
            }
        }
    }

    /// <summary>
    /// Enumerates the core emission properties for shared processing.
    /// </summary>
    /// <returns>The seven core nitrogen emissions.</returns>
    protected IEnumerable<BaseEmission> GetCoreEmissions()
    {
        yield return Direct_N2O_N;
        yield return Indirect_N2O_N_Leached;
        yield return Indirect_N2O_N_Volatilisation;
        yield return Direct_NO3_N_Leached;
        yield return Direct_NO_N;
        yield return Direct_N2_N;
    }
}