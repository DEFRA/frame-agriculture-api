namespace FrameAgricultureApi.Core.Enums;

/// <summary>
/// Type of housing for beef and dairy cattle.
/// </summary>
public enum CattleHousingLocationType
{
    /// <summary>
    /// All.
    /// </summary>
    All = 0,
    /// <summary>
    /// Liquid Manure Cubicle Solid Floor.
    /// </summary>
    LiquidManureCubicleSolidFloor = 1,
    /// <summary>
    /// Liquid Manure Cubicle Slatted Floor.
    /// </summary>
    LiquidManureCubicleSlattedFloor = 2,
    /// <summary>
    /// Liquid Manure Loose Housing Slatted Floor.
    /// </summary>
    LiquidManureLooseHousingSlattedFloor = 3,
    /// <summary>
    /// Solid Manure Deep Bedding Solid Floor.
    /// </summary>
    SolidManureDeepBeddingSolidFloor = 4
}