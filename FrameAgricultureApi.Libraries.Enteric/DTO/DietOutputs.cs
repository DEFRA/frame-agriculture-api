namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class DietOutputs
{
    // Core
    public double MEGERatioWholeDiet { get; set; }
    public double UEMEMaintenance { get; set; }
    public double UEMEWeightGainNonLactating { get; set; }
    public double DryMatterDigestibility { get; set; }
    public double DryMatterContent { get; set; }

    // Additional
    public double WeightedMEGERatioConcentrate { get; set; }
    public double WeightedMEConcentrate { get; set; }
    public double WeightedGEConcentrate { get; set; }
    public double WeightedCPConcentrate { get; set; }
    public double WeightedDryMatterContentConcentrate { get; set; }
    public double WeightedDryMatterDigestibilityConcentrate { get; set; }
    public double WeightedMEGERatioForage { get; set; }
    public double WeightedMEForage { get; set; }
    public double WeightedGEForage { get; set; }
    public double WeightedCPForage { get; set; }
    public double WeightedDryMatterContentForage { get; set; }
    public double WeightedDryMatterDigestibilityForage { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        var properties = typeof(DietOutputs).GetProperties();
        foreach(var property in properties)
        {
            property.SetValue(this, Math.Round((double)property.GetValue(this)));
        }
    }
}
