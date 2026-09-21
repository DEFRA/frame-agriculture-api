using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class LiveWeightOutputsDairy
{
    // Core
    public Dictionary<DairyCattle, double> MeanLiveweightForNExcretion { get; set; } = new Dictionary<DairyCattle, double>();
    public Dictionary<DairyCattle, double> MeanLiveweightForEntericMethane { get; set; } = new Dictionary<DairyCattle, double>();

    public GrowthRates GrowthRates { get; set; } = new GrowthRates();

    // Additional
    public double LiveweightAtBirth { get; set; }
    public double LiveweightAtOneMonth { get; set; }
    public double LiveweightAtOneYear { get; set; }
    public double LiveweightAtFirstConception { get; set; }
    public double LiveweightAtFirstCalving { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        foreach(var key in MeanLiveweightForEntericMethane.Keys)
        {
            MeanLiveweightForEntericMethane[key] = Math.Round(MeanLiveweightForEntericMethane[key], decimalPlaces);
            MeanLiveweightForNExcretion[key] = Math.Round(MeanLiveweightForNExcretion[key], decimalPlaces);
        }

        this.GrowthRates.RoundMembers(decimalPlaces);
        this.LiveweightAtBirth = Math.Round(LiveweightAtBirth, decimalPlaces);
        this.LiveweightAtOneMonth = Math.Round(LiveweightAtOneMonth, decimalPlaces);
        this.LiveweightAtOneYear = Math.Round(LiveweightAtOneYear, decimalPlaces);
        this.LiveweightAtFirstConception = Math.Round(LiveweightAtFirstCalving, decimalPlaces);
        this.LiveweightAtFirstCalving = Math.Round(LiveweightAtFirstCalving, decimalPlaces);

    }
}

public class LiveWeightOutputsBeef
{
    // Core
    public double MeanLiveweight { get; set; }
    //Todo is this needed
    public double MeanLiveweightForEntericMethane { get; set; }
    //Todo is this needed
    public double MeanLiveweightForNitrogenExcretion { get; set; }
    public double MeanGrowthRate { get; set; }

    // Additional
    public double MatureLiveweight { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.MeanLiveweight = Math.Round(MeanLiveweight, decimalPlaces);
        this.MeanLiveweightForEntericMethane = Math.Round(MeanLiveweightForEntericMethane, decimalPlaces);
        this.MeanLiveweightForNitrogenExcretion = Math.Round(MeanLiveweightForNitrogenExcretion, decimalPlaces);
        this.MeanGrowthRate = Math.Round(MeanGrowthRate, decimalPlaces);
        this.MatureLiveweight = Math.Round(MatureLiveweight, decimalPlaces);
    }
}
