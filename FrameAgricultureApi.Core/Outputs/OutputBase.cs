using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Core.Outputs;

public abstract class OutputBase
{
    public abstract SectorType SectorType { get; }
}