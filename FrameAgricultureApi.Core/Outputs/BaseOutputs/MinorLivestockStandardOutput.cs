using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public abstract class MinorLivestockStandardOutput : OutputBase
{
    public override SectorType SectorType => SectorType.Minor;
    public MinorItemType MinorItemType { get; set; }
}