using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta.DTO;

public class InitialNitrogenFreeRangePoultryUserInput
{
    public PoultryType PoultryType { get; set; }
    public double PercentageIndoors { get; set; }
}
