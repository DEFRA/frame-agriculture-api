using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Core.Outputs;

namespace FrameAgricultureApi.Libraries.CoverCrops.Outputs;

/// <summary>
/// Crop cultivation outputs from cover crop emission calculations.
/// </summary>
public class CoverCropOutput
{
    /// <summary>
    /// The agricultural sector associated with cover crops.
    /// </summary>
    public SectorType SectorType => SectorType.Crop;

    /// <summary>
    /// The crop associated with the calculation.
    /// </summary>
    public CropType CropType { get; set; }

    /// <summary>
    /// The cover crop associated with the calculation.
    /// </summary>
    public CoverCropType CoverCropType { get; set; }

    /// <summary>
    /// Calculated emissions and additional nitrogen outputs.
    /// </summary>
    public CoverCropEmissions Emissions { get; set; } = new();
}