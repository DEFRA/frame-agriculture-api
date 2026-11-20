using System;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core;

/// <summary>
/// An additional cover crop nitrogen output with optional pollutant and pathway metadata.
/// </summary>
public class CoverCropEmission
{

    public SummarisePollutantType SummarisePollutantType { get; set; }
    public SummarisePathwayType SummarisePathwayType { get; set; }
    public Unit Unit { get; set; } = new();
    /// <summary>
    /// Calculated emission value, or null when no value has been calculated.
    /// </summary>
    public decimal? Value { get; set; }
}