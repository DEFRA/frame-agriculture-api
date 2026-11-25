using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class SheepStorageOutput : SheepStandardOutput
{
    public override StageType StageType => StageType.Storing;
}