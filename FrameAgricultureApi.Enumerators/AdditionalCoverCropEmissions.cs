using System;

namespace FrameAgricultureApi.Libraries.CoverCrops;

/// <summary>
/// Additional outputs from cover crop calculations enumerator
/// </summary>
public enum AdditionalCoverCropEmissions
{
    /// <summary>
    /// Additional N return from cover crops (kg)
    /// </summary>
    NReturn,
    /// <summary>
    /// Leached nitrate if cover crop frac leach reduction is not applied (kg)
    /// </summary>
    UnadjustedNO3N,
    /// <summary>
    /// N2ON from leached nitrate without frac leach reduction applied (kg)
    /// </summary>
    UnadjustedN2ONLeach
}