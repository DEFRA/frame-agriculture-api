using System.Text.Json.Serialization;
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
    [JsonPropertyName("nReturn")]
    public required decimal N_Return { get; set; }
}

public class CoverCropEmissionFactors
{
    [Required]
    public required decimal IpccEf1 { get; set; }
    [Required]
    public required decimal IpccEf4 { get; set; }
    [Required]
    public required decimal IpccEf5 { get; set; }
    [Required]
    public required decimal FracLeach { get; set; }
    [Required]
    [JsonPropertyName("n0_N_ratio")]
    public required decimal NO_N_ratio { get; set; }
    [Required]
    public required decimal N2_N_ratio { get; set; }
}

public class CoverCropMitigationFactors
{
    [Required]
    public required decimal FracLeachReduction { get; set; }
}