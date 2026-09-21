using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta.DTO;

public class ExcretalPoolsInput
{
    public double PercentOfTimeSpentAtGrazing { get; set; }
    public double PercentOfTimeSpentAtYardCollecting { get; set; }
    public double PercentOfTimeSpentAtYardFeeding { get; set; }
    public double PercentOfTimeSpentAtHousing { get; set; }
}

public class ExcretalPoolsDairyInput : ExcretalPoolsInput
{
    public DairyCattle CattleType { get; set; }
    public double AverageGestationPeriodForDairyCow { get; set; }
    public double CalvingInterval { get; set; }
}

public class ExcretalPoolsBeefInput : ExcretalPoolsInput
{
    public BeefCattleType CattleType { get; set; }
    // Todo this could be in the ExcretalPoolsInput class
    public double AverageGestationPeriod { get; set; }
    public double CorrectionFactorForNRetention { get; set; }
    public double PercentageOfCattleGestating { get; set; }
    public double LactationLength { get; set; }
    public double ScalingFactorOfCattleLactating { get; set; }
}

