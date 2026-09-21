using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class DairyEnergyBalanceInputs
{
    public DairyCattle CattleType { get; set; }
    public double MeanLiveweightEnteric { get; set; }
    public double GrowthRateEnteric { get; set; }
    public double GrowthRateExcretion { get; set; }
    public double AverageAnnualMilkYield { get; set; }
    public double MilkFatContent { get; set; }
    public double MilkProteinContent { get; set; }
    public double CalvingInterval { get; set; }
    public double MEGERatioWholeDiet { get; set; }
    public double UEMEMaintenance { get; set; }
    public double UEMEWeightGainNonLactating { get; set; }
    public double DryMatterDigestibility { get; set; }
    public double DryMatterContent { get; set; }
    public double ConcentrateIntake { get; set; }
    public double MEContentOfConcentrate { get; set; }
    public double MEContentOfForage { get; set; }
    public double GEContentOfForage { get; set; }
    public double GEContentOfConcentrate { get; set; }
    // Todo check these are needed
    public double CPContentOfForage { get; set; }
    public double CPContentOfConcentrate { get; set; }
    public double BirthWeight { get; set; }
}

public class BeefEnergyBalanceInputs
{
    public BeefCattleType CattleType { get; set; }
    public BeefCattleBreed CattleBreed { get; set; }
    public double DietCrudeProteinContent { get; set; }
    public double DietGrossEnergyContent { get; set; }
    public double DietMetabolizableEnergyContent { get; set; }
    public double MeanLiveweight { get; set; }
    //todo is this needed?
    public double MeanLiveweightForEntericMethane { get; set; }
    //todo is this needed?
    public double MeanLiveweightForNitrogenExcretion { get; set; }
    public double MeanGrowthRate { get; set; }
    public double BirthWeight { get; set; }
    public double TimePeriodScalar { get; set; }
    public double PercentCattleTypeGestating { get; set; }
    public double PercentCattleTypeLactating { get; set; }
    public double LactationLength { get; set; }
    public double MilkYield { get; set; }
    public double MilkFat { get; set; }
    public double MilkProtein { get; set; }
    public double LactationScalar { get; set; }
}

public class IrishBeefEnergyBalanceInputs : BeefEnergyBalanceInputs
{
    public int AgeIndex { get; set; }
    public double MatureLiveweight { get; set; }
}
