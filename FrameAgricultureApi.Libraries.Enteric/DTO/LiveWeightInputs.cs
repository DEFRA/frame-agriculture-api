using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class DairyLiveWeightInputs
{
    public Country Country { get; set; }
    public DairyBreedSize CattleSize { get; set; }
    public double AgeFirstConception { get; set; }
    public double AgeFirstCalving { get; set; }
    public double AgeAtDeath { get; set; }
    public double MatureWeight { get; set; }
}

public class BeefLiveWeightInputs
{
    public Country Country { get; set; }
    public BeefCattleType CattleType { get; set; }
    public BeefCattleBreed CattleBreed { get; set; }
    public double LowerBoundaryAge { get; set; }
    public double UpperBoundaryAge { get; set; }
    public double MatureAge { get; set; }
    public double CalfBirthWeight { get; set; }
    public double MatureWeight { get; set; }
}
