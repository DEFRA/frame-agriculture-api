using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class PigStorageOutput : PigStandardOutput
{
    public override StageType StageType => StageType.Storing;
}