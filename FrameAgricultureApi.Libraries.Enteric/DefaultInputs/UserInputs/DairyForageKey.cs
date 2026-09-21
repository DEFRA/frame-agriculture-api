using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.DefaultInputs.UserInputs;

public class DairyForageKey
{
    public Country Country { get; set; }
    public DairyCattle CattleType { get; set; }
    public ManagementRegime ManagementRegime { get; set; }
    public Month Month { get; set; }
}
