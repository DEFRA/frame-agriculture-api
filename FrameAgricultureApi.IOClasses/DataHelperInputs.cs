using FrameAgricultureApi.Libraries.Enteric.DTO;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.IOClasses;

/// <summary>
/// Key to access sheep energy balance outputs
/// </summary>
public class SheepEnergyBalanceKey
{
    public int Country { get; set; }
    public int SheepType { get; set; }
    public int SheepSubType { get; set; }
    public int SheepSystemType { get; set; }
}

public class FirstWinterManureFracLeachKey
{
    public int CellID { get; set; }
    public int SoilType { get; set; }
}

public class GrassCoefficientKey
{
    public int ClimateRegion { get; set; }
    public int SoilTextureType { get; set; }
    public int GrassType { get; set; }
    public int GrassUseType { get; set; }
    public bool WhiteCloverSown { get; set; }
    public bool RecievesManagedManure { get; set; }
    public int GrassCoefficientRequired { get; set; }
}
/// <summary>
/// Inputs for grass genetic gain scalar calculation
/// </summary>
public class GrassGeneticGainInputs
{
    public double FertiliserRate { get; set; }
    public int GrassType { get; set; }
}
public class DairyLiveweightGainInputs
{
    public DairyCattle CattleType { get; set; }
    public required LiveWeightOutputsDairy LiveweightData { get; set; }
    public int TimePeriodsPerYear { get; set; }
}

public class BeefLiveweightGainInputs
{
    public required LiveWeightOutputsBeef LiveweightData { get; set; }
    public double TimePeriodsPerYear { get; set; }
}
