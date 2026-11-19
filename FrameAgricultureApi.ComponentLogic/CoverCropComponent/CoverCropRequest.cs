using System;
using System.ComponentModel.DataAnnotations;
using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.ComponentLogic.CoverCropComponent;

/// <summary>
/// Inputs for calculation of emissions from cover crop use
/// </summary>
public class CoverCropRequest
{
    public CoverCropInputs CoverCropInputs { get; set; }
    public CoverCropEmissionFactors CoverCropEmissionFactors { get; set; }
    public CoverCropMitigationFactors CoverCropMitigationFactors { get; set; }
}

public class CoverCropInputs
{
    [Required]
    public required double NReturn { get; set; }
}

public class CoverCropEmissionFactors
{
    [Required]
    public required double IpccEf1 { get; set; }
    [Required]
    public required double IpccEf4 { get; set; }
    [Required]
    public required double IpccEf5 { get; set; }
    [Required]
    public required double FracLeach { get; set; }
    [Required]
    public required double N0_N_ratio { get; set; }
    [Required]
    public required double N2_N_ratio { get; set; }
}

public class CoverCropMitigationFactors
{
    [Required]
    public required double FracLeachReduction { get; set; }
}