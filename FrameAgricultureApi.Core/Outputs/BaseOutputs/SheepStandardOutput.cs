using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public abstract class SheepStandardOutput : OutputBase
{
    public override SectorType SectorType => SectorType.Sheep;
    public SheepItemType SheepItemType { get; set; }
}