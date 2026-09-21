namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class EnergyBalanceOutputsDairy
{
    // Core
    public double DailyMERequirement { get; set; }
    public double DailyDryMatterIntake { get; set; }
    public double DailyGEIntake { get; set; }
    public double DailyNIntake { get; set; }
    public double DailyNExcretion { get; set; }

    // Additional
    public double RationMod { get; set; }
    public double MEMaintenanceLactation { get; set; }
    // This is called in the test sheet ER_AP
    public double MEActivity { get; set; }
    public double MELiveWeightGain { get; set; }
    public double MEPregnancy { get; set; }
    public double MEUncertaintyScalar { get; set; }
    public double DMIAsPercentageOfLiveweight { get; set; }
    public double NitrogenRetentionInMilk { get; set; }
    public double NitrogenRetentionInPregnancy { get; set; }
    public double NitrogenRetentionInLiveweightGain { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.DailyMERequirement = Math.Round(this.DailyMERequirement, decimalPlaces);
        this.DailyDryMatterIntake = Math.Round(this.DailyDryMatterIntake, decimalPlaces);
        this.DailyGEIntake = Math.Round(this.DailyGEIntake, decimalPlaces);
        this.DailyNIntake = Math.Round(this.DailyNIntake, decimalPlaces);
        this.DailyNExcretion = Math.Round(this.DailyNExcretion, decimalPlaces);

        this.MEMaintenanceLactation = Math.Round(MEMaintenanceLactation, decimalPlaces);
        this.MEActivity = Math.Round(MEActivity, decimalPlaces);
        this.MELiveWeightGain = Math.Round(MELiveWeightGain, decimalPlaces);
        this.RationMod = Math.Round(RationMod, decimalPlaces);
        this.MEPregnancy = Math.Round(MEPregnancy, decimalPlaces);
        this.MEUncertaintyScalar = Math.Round(MEUncertaintyScalar, decimalPlaces);
        this.DMIAsPercentageOfLiveweight = Math.Round(DMIAsPercentageOfLiveweight, decimalPlaces);
        this.NitrogenRetentionInMilk = Math.Round(NitrogenRetentionInMilk, decimalPlaces);
        this.NitrogenRetentionInPregnancy = Math.Round(NitrogenRetentionInPregnancy, decimalPlaces);
        this.NitrogenRetentionInLiveweightGain = Math.Round(NitrogenRetentionInLiveweightGain, decimalPlaces);
    }
}

public class EnergyBalanceOutputsBeef
{
    // Core
    public double DailyGEIntake { get; set; }
    public double DailyCPIntake { get; set; }
    public double DailyDryMatterIntake { get; set; }
    public double DailyNIntake { get; set; }
    public double DailyNExcretion { get; set; }

    // Additional
    public double DailyMEMaintenance { get; set; }
    public double DailyMELiveweightGain { get; set; }
    public double DailyMEPregnancy { get; set; }
    public double DailyMELactation { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.DailyGEIntake = Math.Round(DailyGEIntake, decimalPlaces);
        this.DailyCPIntake = Math.Round(DailyCPIntake, decimalPlaces);
        this.DailyDryMatterIntake = Math.Round(DailyDryMatterIntake, decimalPlaces);
        this.DailyNIntake = Math.Round(DailyNIntake, decimalPlaces);
        this.DailyNExcretion = Math.Round(DailyNExcretion, decimalPlaces);
        this.DailyMEMaintenance = Math.Round(DailyMEMaintenance, decimalPlaces);
        this.DailyMELiveweightGain = Math.Round(DailyMELiveweightGain, decimalPlaces);
        this.DailyMEPregnancy = Math.Round(DailyMEPregnancy, decimalPlaces);
        this.DailyMELactation = Math.Round(DailyMELactation, decimalPlaces);
    }
}
