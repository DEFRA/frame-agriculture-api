using FrameAgricultureApi.Core.Outputs;
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
        // Todo I have just assumed N is nitrogen 
        SummarisePollutantType = SummarisePollutantType.Nitrogen,
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
    public CoverCropEmission UnadjustedN03_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NitrateNitrogen,
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