using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class SheepHousingOutput : SheepStandardOutput
{
    public override StageType StageType => StageType.Housing;
}