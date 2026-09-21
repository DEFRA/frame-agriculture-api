namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class DairyKeyPerformanceIndicators
{
    public double CalvingInterval { get; set; }
    public double GestationPeriod { get; set; }
    public double MatureWeight { get; set; }
    public double AgeAtConception { get; set; }
    public double AgeAtCalving { get; set; }
    public double AgeAtDeath { get; set; }
    /// <summary>
    /// Liveweight gain in kg (per unit time - default month)
    /// </summary>
    public double LiveweightGain { get; set; }
}
