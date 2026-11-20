using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Inputs;

/// <summary>
/// Request for FertiliserApplication.UnmitigatedEmissionsFertiliser with lookup values supplied directly.
/// </summary>
public class FertiliserApplicationUnmitigatedEmissionsFertiliserRequest
{
    /// <summary>
    /// Application and environmental inputs for the calculation.
    /// </summary>
    [Required]
    public required FertiliserApplicationUnmitigatedEmissionsFertiliserRequestInputs Inputs { get; set; }

    /// <summary>
    /// Resolved emission factors and model uncertainties.
    /// </summary>
    [Required]
    public required FertiliserApplicationUnmitigatedEmissionsFertiliserRequestEmissionFactors EmissionFactors { get; set; }

    /// <summary>
    /// Empty mitigation factors section; the unmitigated calculation requires no mitigation.
    /// </summary>
    [Required]
    public required FertiliserApplicationUnmitigatedEmissionsFertiliserRequestMitigationFactors MitigationFactors { get; set; }
}

/// <summary>
/// Application inputs and environmental values required by the legacy calculation and its called functions.
/// Grid-square identifiers are omitted because their lookup results are supplied directly.
/// </summary>
public class FertiliserApplicationUnmitigatedEmissionsFertiliserRequestInputs
{
    /// <summary>
    /// Fertiliser type used to select the nitrous oxide model and ammonia modifications.
    /// </summary>
    [Required]
    public required FertiliserType FertiliserType { get; set; }

    /// <summary>
    /// Monthly applications of this fertiliser, in kg N/ha, ordered January to December.
    /// </summary>
    [Required]
    [MinLength(12)]
    [MaxLength(12)]
    public required decimal[] FertiliserApplications { get; set; }

    /// <summary>
    /// Total annual nitrogen applied across all fertiliser types, in kg N/ha.
    /// </summary>
    [Required]
    [JsonPropertyName("totalNApplied")]
    public required decimal Total_N_Applied { get; set; }

    /// <summary>
    /// Share of total nitrogen supplied by this fertiliser type, as a percentage (0 to 100).
    /// </summary>
    [Required]
    public required decimal PercentageFertiliserType { get; set; }

    /// <summary>
    /// Whether soil pH is below 7, used in the ammonium sulphate/diammonium phosphate ammonia modification.
    /// </summary>
    [Required]
    public required bool AcidicSoil { get; set; }

    /// <summary>
    /// Annual average rainfall in mm, resolved from PhysicalLookup.RetrieveAnnualAverageRainfall; used by the non-urea model.
    /// </summary>
    [Required]
    public required decimal AnnualAverageRainfall { get; set; }

    /// <summary>
    /// Monthly mean temperatures in degrees Celsius, ordered January to December, resolved from PhysicalLookup.RetrieveMonthlyTemperature.
    /// </summary>
    [Required]
    [MinLength(12)]
    [MaxLength(12)]
    public required decimal[] MonthlyTemperatures { get; set; }

    /// <summary>
    /// Rainfall-event probabilities for days 1 to 6 after application, in that order, resolved from PhysicalLookup.RetrieveRainfallEventProbability.
    /// </summary>
    [Required]
    [MinLength(6)]
    [MaxLength(6)]
    public required decimal[] RainfallEventProbabilities { get; set; }
}

/// <summary>
/// Values resolved from CropLookup for the selected fertiliser.
/// Fixed FertiliserEquationParameters constants remain part of the equations.
/// </summary>
public class FertiliserApplicationUnmitigatedEmissionsFertiliserRequestEmissionFactors
{
    /// <summary>
    /// Additive nitrous oxide model coefficient uncertainty, resolved from CropLookup.Uncertainty_N2ON_FertiliserModelCoefficient; not a percentage multiplier.
    /// </summary>
    [Required]
    [JsonPropertyName("n2ONModelCoefficientUncertainty")]
    public required decimal N2O_N_ModelCoefficientUncertainty { get; set; }

    /// <summary>
    /// Additive ammonia emission factor uncertainty in percentage points, resolved from CropLookup.Uncertainty_AmmoniaFertiliserModelCoefficient.
    /// </summary>
    [Required]
    [JsonPropertyName("ammoniaModelCoefficientUncertainty")]
    public required decimal NH3_ModelCoefficientUncertainty { get; set; }

    /// <summary>
    /// Ratio of NO-N to direct N2O-N, resolved from CropLookup.RetrieveRatioNON.
    /// </summary>
    [Required]
    [JsonPropertyName("noNRatio")]
    public required decimal NO_N_Ratio { get; set; }

    /// <summary>
    /// Ratio of N2-N to direct N2O-N, resolved from CropLookup.RetrieveRatioN2N.
    /// </summary>
    [Required]
    [JsonPropertyName("n2NRatio")]
    public required decimal N2_N_Ratio { get; set; }

    /// <summary>
    /// Nitrogen leached as nitrate, as a percentage, resolved from CropLookup.RetrieveFracLeach.
    /// </summary>
    [Required]
    public required decimal FracLeach { get; set; }

    /// <summary>
    /// Indirect N2O-N emission factor for volatilisation, as a percentage, resolved from CropLookup.RetrieveIPCCEmissionFactor4.
    /// </summary>
    [Required]
    public required decimal IpccEf4 { get; set; }

    /// <summary>
    /// Indirect N2O-N emission factor for leaching, as a percentage, resolved from CropLookup.RetrieveIPCCEmissionFactor5.
    /// </summary>
    [Required]
    public required decimal IpccEf5 { get; set; }
}

/// <summary>
/// No mitigation factors are required for unmitigated emissions.
/// </summary>
public class FertiliserApplicationUnmitigatedEmissionsFertiliserRequestMitigationFactors
{
}