using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

/// <summary>
/// An emission with pollutant, pathway, units, value and fertilising location metadata.
/// </summary>
public class FertiliserApplicationEmission : BaseEmission
{
    /// <summary>
    /// The crop and grass fertiliser category associated with the emission.
    /// </summary>
    public CropAndGrassFertilisingLocationType CropAndGrassFertilisingLocationType { get; set; }
}