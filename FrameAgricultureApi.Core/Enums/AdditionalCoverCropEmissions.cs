namespace FrameAgricultureApi.Core.Enums;

/// <summary>
/// Additional outputs from cover crop calculations enumerator
/// </summary>
public enum AdditionalCoverCropEmissions
{
    /// <summary>
    /// Additional N return from cover crops (kg)
    /// </summary>
    N_Return,
    /// <summary>
    /// Leached nitrate if cover crop frac leach reduction is not applied (kg)
    /// </summary>
    Unadjusted_NO3_N,
    /// <summary>
    /// N2ON from leached nitrate without frac leach reduction applied (kg)
    /// </summary>
    Unadjusted_N2O_N_Leach
}