using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta.DTO;

// Todo separate these out

public class BeefEntericMethaneInputsDto : EntericMethaneInputsDto
{
    public BeefCattleType BeefCattleType { get; set; }
}
public class DairyEntericMethaneInputsDto : EntericMethaneInputsDto
{
    public DairyCattle CattleType { get; set; }
}
public class EntericMethaneInputsDto
{
    public double DailyDMIIntake { get; set; }
}

public class DairyNitrogenInputsDto : NitrogenInputsDto
{
    public DairyCattle CattleType { get; set; }
}

public class BeefNitrogenInputsDto : NitrogenInputsDto
{
    public BeefCattleType CattleType { get; set; }
}

public class NitrogenInputsDto
{
    public NitrogenInputs NitrogenInputs { get; set; } = new();
    public double TimeScalar { get; set; }
}
public class NitrogenInputs
{
    public double DailyNExcretion { get; set; }
    public double DailyNIntake { get; set; }
}

public class VolatileSolidsInputsDto
{
    public VolatileSolidsInputs VolatileSolidsInputs { get; set; } = new();
    public double TimeScalar { get; set; }
}

public class VolatileSolidsInputs
{
    public double DailyGEIntake { get; set; }
    public double DailyDMIIntake { get; set; }
    public double DailyMERequirement { get; set; }
}

public class NitrogenAndEntericMethaneInputs
{
    public NitrogenInputs NitrogenInputs { get; set; } = new();
    public VolatileSolidsInputs VolatileSolidsInputs { get; set; } = new();
    public double TimeScalar { get; set; }
}

public class DairyNitrogenAndEntericMethaneInputs : NitrogenAndEntericMethaneInputs
{
    public DairyCattle CattleType { get; set; }
}

public class BeefNitrogenAndEntericMethaneInputs : NitrogenAndEntericMethaneInputs
{
    public BeefCattleType CattleType { get; set; }
}
