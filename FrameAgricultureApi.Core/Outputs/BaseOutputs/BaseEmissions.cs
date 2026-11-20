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
    public BaseEmission DirectN20_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NitrousOxide,
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
    public BaseEmission IndirectN20_NLeached { get; set; } = new()
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
    /// Indirect N2O-N (volatilisation), measured as nitrogen in kilograms.
    /// </summary>
    public BaseEmission IndirectN20_NVolatilisation { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NitrousOxide,
        SummarisePathwayType = SummarisePathwayType.IndirectDeposition,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Direct NH3-N, measured as nitrogen in kilograms.
    /// </summary>
    public BaseEmission DirectNH3_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.Ammonia,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Direct NO3-N (leached), measured as nitrogen in kilograms.
    /// </summary>
    public BaseEmission DirectNO3_NLeached { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.Nitrate,
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
    public BaseEmission DirectNO_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NitricOxide,
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
    public BaseEmission DirectN2_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.Dinitrogen,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Resets all core emission values before calculation.
    /// </summary>
    public void InitialiseBaseEmissions()
    {
        foreach (var emission in GetCoreEmissions())
        {
            emission.Value = null;
        }
    }

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
        yield return DirectN20_N;
        yield return IndirectN20_NLeached;
        yield return IndirectN20_NVolatilisation;
        yield return DirectNH3_N;
        yield return DirectNO3_NLeached;
        yield return DirectNO_N;
        yield return DirectN2_N;
    }
}