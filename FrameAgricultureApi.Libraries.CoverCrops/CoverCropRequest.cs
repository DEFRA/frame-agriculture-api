using System;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Libraries.CoverCrops;

/// <summary>
/// Inputs for calculation of emissions from cover crop use
/// </summary>
public class CoverCropRequest
{
    /// <summary>
    /// The crop type (integer value of enumerator)
    /// </summary>
    public CropType CropType { get; set; }

    /// <summary>
    /// Cover Crop Type (integer value of enumerator)
    /// </summary>
    public CoverCropType CoverCropType { get; set; }
}