namespace FrameAgricultureApi.Core.Enums;

/// <summary>
/// Type of Fertiliser to Crop Sector.
/// </summary>
public enum CropFertilisingLocationType
{
    /// <summary>
    /// All.
    /// </summary>
    All = 0,
    /// <summary>
    /// Urea.
    /// </summary>
    Urea = 1,
    /// <summary>
    /// Ammonium Nitrate.
    /// </summary>
    AmmoniumNitrate = 2,
    /// <summary>
    /// Urea Ammonium Nitrate.
    /// </summary>
    UreaAmmoniumNitrate = 3,
    /// <summary>
    /// Calcium Ammonium Nitrate.
    /// </summary>
    CalciumAmmoniumNitrate = 4,
    /// <summary>
    /// Ammonium Sulphate.
    /// </summary>
    AmmoniumSulphate = 5,
    /// <summary>
    /// Other Nitrogen.
    /// </summary>
    OtherNitrogen = 6,
    /// <summary>
    /// No Fertiliser.
    /// </summary>
    NoFertiliser = 7
}