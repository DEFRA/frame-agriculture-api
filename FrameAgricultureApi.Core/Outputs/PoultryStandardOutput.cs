using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public class PoultryStandardOutput : OutputBase
{
    public override SectorType SectorType => SectorType.Poultry;
    public PoultryItemType PoultryItemType { get; set; }
}