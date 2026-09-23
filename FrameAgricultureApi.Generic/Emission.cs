/// <summary>
/// Base Emission class. Emission defined to have a name, units and a value.
/// </summary>
public class Emission
{
    /// <summary>
    /// Name of the emission
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The units of the emission
    /// </summary>
    public string Units { get; }

    /// <summary>
    /// The value of the emission
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Emissions base construnctor
    /// </summary>
    public Emission()
    {
        Name = "";
        Units = "";
        Value = double.NaN;
    }
    /// <summary>
    /// Overloaded constructo rto set values at instantiation
    /// </summary>
    /// <param name="name">Emission name</param>
    /// <param name="units">Emission units</param>
    /// <param name="value">Emission value</param>
    public Emission(string name, string units, double value)
    {
        Name = name;
        Units = units;
        Value = value;
    }
}
