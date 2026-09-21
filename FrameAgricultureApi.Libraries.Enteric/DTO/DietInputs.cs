namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class DietInputs
{
    public double ConcentrateIntake { get; set; }
    public double DMIDaily { get; set; }
    public List<ForageComponent> ForageComponents { get; set; } = new List<ForageComponent>();
    public List<ConcentrateComponent> ConcentrateComponents { get; set; } = new List<ConcentrateComponent>();
}

public class ConcentrateComponent
{
    public double ConcentrateComponentPercent { get; set; }
    public double ConcentrateMEContent { get; set; }
    public double ConcentrateGEContent { get; set; }
    public double ConcentrateCPContent { get; set; }
    public double ConcentrateDryMatterContent { get; set; }
}

public class ForageComponent
{
    public double ForageComponentPercent { get; set; }
    public double ForageMEContent { get; set; }
    public double ForageGEContent { get; set; }
    public double ForageCPContent { get; set; }
    public double ForageDryMatterContent { get; set; }
}
