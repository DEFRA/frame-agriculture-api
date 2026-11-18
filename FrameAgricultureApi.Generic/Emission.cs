using FrameAgricultureApi.Core;
using FrameAgricultureApi.Core.Enums;


/// <summary>
/// Base Emission class. Emission defined to have a name, units and a value.
/// </summary>
public class BaseEmission
{

    public SummarisePollutantType SummarisePollutantType { get; set; }
    public SummarisePathwayType SummarisePathwayType { get; set; }
    public Unit Unit { get; set; } = new();
    public double Value { get; set; } = double.NaN;
}

/// <summary>
/// A named emission value in the standard API output.
/// </summary>
public class Emission
{
    /// <summary>
    /// Display name of the emission.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Display units, including the substance measured.
    /// </summary>
    public string Units { get; }

    /// <summary>
    /// Calculated emission value.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Creates an empty output emission.
    /// </summary>
    public Emission() : this("", "", double.NaN)
    {
    }

    /// <summary>
    /// Creates a named output emission.
    /// </summary>
    /// <param name="name">Display name.</param>
    /// <param name="units">Display units.</param>
    /// <param name="value">Calculated value.</param>
    public Emission(string name, string units, double value)
    {
        Name = name;
        Units = units;
        Value = value;
    }
}