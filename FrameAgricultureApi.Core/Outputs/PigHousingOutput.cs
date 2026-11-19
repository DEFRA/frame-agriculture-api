using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class PigHousingOutput : PigStandardOutput
{
    public override StageType StageType => StageType.Housing;
    public PigHousingLocationType PigHousingLocationType { get; set; }
}