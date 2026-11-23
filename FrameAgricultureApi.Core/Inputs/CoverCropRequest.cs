using System.Text.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.ComponentLogic.CoverCropComponent;

/// <summary>
/// Inputs for calculation of emissions from cover crop use
/// </summary>
/// /// <DocumentationType>
/// Request
/// </DocumentationType>
public class CoverCropRequest
{
    public CoverCropInputs CoverCropInputs { get; set; }
    public CoverCropEmissionFactors CoverCropEmissionFactors { get; set; }
    public CoverCropMitigationFactors CoverCropMitigationFactors { get; set; }
}

/// <summary>
/// summary
/// </summary>
/// <DocumentationType>
/// Inputs
/// </DocumentationType>
public class CoverCropInputs
{
    /// <summary>
    /// Additional nitrogen returned to soil from cover crop residues
    /// </summary>
    /// <units>
    /// kg N/ha
    /// </units>
    [Required]
    [JsonPropertyName("nReturn")]
    public required decimal N_Return { get; set; }
}

/// <summary>
/// summary
/// </summary>
/// <DocumentationType>
/// EmissionFactors
/// </DocumentationType>
public class CoverCropEmissionFactors
{
    /// <summary>
    /// Nitrous oxide emissions from nitrogen inputs
    /// </summary>
    /// <units>
    /// kg N2O-N / kg N
    /// </units>
    [Required]
    public required decimal IpccEf1 { get; set; }
    /// <summary>
    /// Nitrous Oxide emissions from  atmospheric deposition of N on soils and water surfaces
    /// </summary>
    /// <units>
    /// kg N2O-N / (kg NH3-N +Nox-N volatilised)
    /// </units>
    [Required]
    public required decimal IpccEf4 { get; set; }
    /// <summary>
    /// Nitrous oxide emission from N leaching and runoff
    /// </summary>
    /// <units>
    /// kg N2O-N / kg N leached and runoff
    /// </units>
    [Required]
    public required decimal IpccEf5 { get; set; }
    /// <summary>
    /// fraction of all N that is lost through leaching and runoff
    /// </summary>
    /// <units>
    /// kg NO3-N leached / Kg N
    /// </units>
    [Required]
    public required decimal FracLeach { get; set; }
    /// <summary>
    /// NO-N emissions as a ratio of direct nitrous oxide emissions
    /// </summary>
    /// <units>
    /// kg NO-N / Kg N2O-N
    /// </units>
    [Required]
    [JsonPropertyName("n0_N_ratio")]
    public required decimal NO_N_ratio { get; set; }
    /// <summary>
    /// N2-N emissions as a ratio of direct nitrous oxide emissions
    /// </summary>
    /// <units>
    /// kg N2-N / Kg N2O-N
    /// </units>
    [Required]
    public required decimal N2_N_ratio { get; set; }
}

/// <summary>
/// summary
/// </summary>
/// <DocumentationType>
/// MitigationFactors
/// </DocumentationType>
public class CoverCropMitigationFactors
{
    /// <summary>
    /// Proportional reduction in default FRAC LEACH
    /// </summary>
    /// <units>
    /// Proportion
    /// </units>
    [Required]
    public required decimal FracLeachReduction { get; set; }
}