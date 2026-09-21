using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class BeefDefaultKey
{
    public BeefCattleType CattleType { get; set; }
    public BeefCattleBreed CattleBreed { get; set; }
    public int CattleAge { get; set; }
}
