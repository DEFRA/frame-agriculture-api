using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta.DTO;

public class ExcretaPigsPoultryMinorLivestockUserInput
{
    public Sector Sector { get; set; }
    public int AnimalType { get; set; }
}
