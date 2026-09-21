using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.DefaultInputs.UserInputs;

public class DairyConcentrateKey
{
    public Country Country { get; set; }
    public DairyCattle CattleType { get; set; }
    public DairyBreedSize BreedSize { get; set; }
}
