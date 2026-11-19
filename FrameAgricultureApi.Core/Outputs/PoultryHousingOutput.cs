using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class PoultryHousingOutput : PoultryStandardOutput
{
    public override StageType StageType => StageType.Housing;
}