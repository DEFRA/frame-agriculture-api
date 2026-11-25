namespace FrameAgricultureApi.Core;

/// <summary>
/// A chemical symbol and its exponent within a chemical unit.
/// </summary>
public class BaseChemicalUnit
{
    public string Symbol { get; set; } = string.Empty;
    public int Exponent { get; set; }
}