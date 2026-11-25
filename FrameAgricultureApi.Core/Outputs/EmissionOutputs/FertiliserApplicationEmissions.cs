using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

/// <summary>
/// Core fertiliser application emissions and nitrogen applied by fertiliser type.
/// Used for both mitigated and unmitigated fertiliser application calculations.
/// Additional outputs identify nitrogen supplied by each fertiliser type;
/// they represent applied nitrogen rather than pollutant emissions.
/// </summary>
public class FertiliserApplicationEmissions : BaseEmissions
{
    /// <summary>
    /// Direct NH3-N, measured as nitrogen in kilograms.
    /// </summary>
    public FertiliserApplicationEmission Direct_NH3_N { get; set; } = new()
    {
        SummarisePollutantType = SummarisePollutantType.NH3,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrogen applied as urea (CH4N2O), in kilograms of nitrogen.
    /// </summary>
    public FertiliserApplicationEmission CH4N2O_N_Applied { get; set; } = new()
    {
        CropAndGrassFertilisingLocationType = CropAndGrassFertilisingLocationType.Urea,
        SummarisePollutantType = SummarisePollutantType.N,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrogen applied as ammonium nitrate (NH4NO3), in kilograms of nitrogen.
    /// </summary>
    public FertiliserApplicationEmission NH4NO3_N_Applied { get; set; } = new()
    {
        CropAndGrassFertilisingLocationType = CropAndGrassFertilisingLocationType.AmmoniumNitrate,
        SummarisePollutantType = SummarisePollutantType.N,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrogen applied as calcium ammonium nitrate (CAN), in kilograms of nitrogen.
    /// </summary>
    public FertiliserApplicationEmission CAN_N_Applied { get; set; } = new()
    {
        CropAndGrassFertilisingLocationType = CropAndGrassFertilisingLocationType.CalciumAmmoniumNitrate,
        SummarisePollutantType = SummarisePollutantType.N,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrogen applied as urea ammonium nitrate (UAN), in kilograms of nitrogen.
    /// </summary>
    public FertiliserApplicationEmission UAN_N_Applied { get; set; } = new()
    {
        CropAndGrassFertilisingLocationType = CropAndGrassFertilisingLocationType.UreaAmmoniumNitrate,
        SummarisePollutantType = SummarisePollutantType.N,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrogen applied as ammonium sulphate or diammonium phosphate (AS/DAP), in kilograms of nitrogen.
    /// </summary>
    public FertiliserApplicationEmission AS_DAP_N_Applied { get; set; } = new()
    {
        CropAndGrassFertilisingLocationType = CropAndGrassFertilisingLocationType.AmmoniumSulphate,
        SummarisePollutantType = SummarisePollutantType.N,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Nitrogen applied as other nitrogen fertilisers, including compound blends, in kilograms of nitrogen.
    /// </summary>
    public FertiliserApplicationEmission OtherFertilisers_N_Applied { get; set; } = new()
    {
        CropAndGrassFertilisingLocationType = CropAndGrassFertilisingLocationType.Other_N,
        SummarisePollutantType = SummarisePollutantType.N,
        SummarisePathwayType = SummarisePathwayType.Direct,
        Unit = new()
        {
            Symbol = UnitSymbols.Kg,
            MultipleToBaseFactor = 1
        }
    };

    /// <summary>
    /// Rounds core emissions and nitrogen-applied outputs to a fixed number of decimal places.
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to use.</param>
    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach (var emission in GetAdditionalEmissions())
        {
            emission.Value = decimal.Round(emission.Value, decimalPlaces, MidpointRounding.AwayFromZero);
        }
    }

    private IEnumerable<FertiliserApplicationEmission> GetAdditionalEmissions()
    {
        yield return Direct_NH3_N;
        yield return CH4N2O_N_Applied;
        yield return NH4NO3_N_Applied;
        yield return CAN_N_Applied;
        yield return UAN_N_Applied;
        yield return AS_DAP_N_Applied;
        yield return OtherFertilisers_N_Applied;
    }
}