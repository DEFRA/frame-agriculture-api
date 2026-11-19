using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class PoultryStorageOutput : PoultryStandardOutput
{
    public override StageType StageType => StageType.Storing;
}