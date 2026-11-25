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
    /// Direct N2O-N, measured as nitrogen in kilograms per hectare.
    /// </summary>
    public BaseEmission Direct_N2O_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2O,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = Units.Kilogram_N2O_N_PerHectare
    };

    /// <summary>
    /// Indirect N2O-N (leached), measured as nitrogen in kilograms per hectare.
    /// </summary>
    public BaseEmission Indirect_N2O_N_Leached { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2O,
        SummarisePathwayType = SummarisePathwayType.IndirectLeaching,
        Unit = Units.Kilogram_N2O_N_PerHectare
    };

    /// <summary>
    /// Indirect N2O-N (Redeposition), measured as nitrogen in kilograms per hectare.
    /// </summary>
    public BaseEmission Indirect_N2O_N_Redeposition { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2O,
        SummarisePathwayType = SummarisePathwayType.IndirectDeposition,
        Unit = Units.Kilogram_N2O_N_PerHectare
    };

    /// <summary>
    /// Direct NO3-N (leached), measured as nitrogen in kilograms per hectare.
    /// </summary>
    public BaseEmission Mitigated_NO3_N_Leached { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NO3,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = Units.Kilogram_NO3_N_PerHectare
    };

    /// <summary>
    /// Direct NO-N, measured as nitrogen in kilograms per hectare.
    /// </summary>
    public BaseEmission Direct_NO_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NO,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = Units.Kilogram_NO_N_PerHectare
    };

    /// <summary>
    /// Direct N2-N, measured as nitrogen in kilograms per hectare.
    /// </summary>
    public BaseEmission Direct_N2_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.N2,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = Units.Kilogram_N2_N_PerHectare
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
    /// <returns>The six core nitrogen emissions.</returns>
    protected IEnumerable<BaseEmission> GetCoreEmissions()
    {
        yield return Direct_N2O_N;
        yield return Indirect_N2O_N_Leached;
        yield return Indirect_N2O_N_Redeposition;
        yield return Mitigated_NO3_N_Leached;
        yield return Direct_NO_N;
        yield return Direct_N2_N;
    }
}