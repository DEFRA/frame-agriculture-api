using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class MinorLivestockHousingOutput : MinorLivestockStandardOutput
{
    public override StageType StageType => StageType.Housing;
}