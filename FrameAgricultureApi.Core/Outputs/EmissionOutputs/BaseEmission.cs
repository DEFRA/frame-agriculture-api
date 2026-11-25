using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;


/// <summary>
/// Base Emission class. Emission defined to have a name, units and a value.
/// </summary>
public class BaseEmission
{

    public SummarisePollutantType SummarisePollutantType { get; set; }
    public SummarisePathwayType SummarisePathwayType { get; set; }
    public PhysicalUnit Unit { get; set; } = new();
    public decimal Value { get; set; }
}