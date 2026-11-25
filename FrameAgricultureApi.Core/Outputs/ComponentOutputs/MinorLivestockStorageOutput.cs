using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class MinorLivestockStorageOutput : MinorLivestockStandardOutput
{
    public override StageType StageType => StageType.Storing;
    public MinorStoringLocationType MinorStoringLocationType { get; set; }
}