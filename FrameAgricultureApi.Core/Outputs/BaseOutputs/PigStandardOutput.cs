using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public abstract class PigStandardOutput : OutputBase
{
    public override SectorType SectorType => SectorType.Pig;
    public PigItemType PigItemType { get; set; }
}