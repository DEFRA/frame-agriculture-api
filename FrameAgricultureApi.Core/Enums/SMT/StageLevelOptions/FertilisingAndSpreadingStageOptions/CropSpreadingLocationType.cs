namespace FrameAgricultureApi.Core.Enums;

/// <summary>
/// Type of Waste to Crop Sector.
/// </summary>
public enum CropSpreadingLocationType
{
    /// <summary>
    /// All.
    /// </summary>
    All = 0,
    /// <summary>
    /// Compost.
    /// </summary>
    Compost = 1,
    /// <summary>
    /// Crop Based Digestate.
    /// </summary>
    CropBasedDigestate = 2,
    /// <summary>
    /// Food Based Digestate.
    /// </summary>
    FoodBasedDigestate = 3,
    /// <summary>
    /// Other Based Digestate.
    /// </summary>
    OtherBasedDigestate = 4,
    /// <summary>
    /// Cake Sewage Sludge.
    /// </summary>
    CakeSewageSludge = 5,
    /// <summary>
    /// Liquid Sewage Sludge.
    /// </summary>
    LiquidSewageSludge = 6
}