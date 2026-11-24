using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public abstract class OutputBase
{
    public abstract SectorType SectorType { get; }
    public abstract StageType StageType { get; }
    public ComponentTrace ComponentTrace { get; set; }
}