namespace FrameAgricultureApi.Core;

/// <summary>
/// A unit combining physical and chemical components.
/// </summary>
public class Unit
{
    public Unit(PhysicalUnit physicalUnit, ChemicalUnit chemicalUnit)
    {
        ArgumentNullException.ThrowIfNull(physicalUnit);
        ArgumentNullException.ThrowIfNull(chemicalUnit);

        PhysicalUnit = physicalUnit;
        ChemicalUnit = chemicalUnit;
    }

    public PhysicalUnit PhysicalUnit { get; }
    public ChemicalUnit ChemicalUnit { get; }

    /// <summary>
    /// Component symbols grouped by exponent sign, with physical symbols first in each group.
    /// </summary>
    public string Symbol
    {
        get
        {
            var components = PhysicalUnit.PhysicalUnitComponents
                .Select(component => (component.Symbol, component.Exponent))
                .Concat(ChemicalUnit.ChemicalUnitComponents
                    .Select(component => (component.Symbol, component.Exponent)));
            var numerator = string.Concat(components
                .Where(component => component.Exponent > 0)
                .Select(FormatComponentSymbol));
            var denominator = string.Concat(components
                .Where(component => component.Exponent < 0)
                .Select(FormatComponentSymbol));

            if (denominator.Length == 0)
            {
                return numerator;
            }

            return $"{(numerator.Length == 0 ? "1" : numerator)}/{denominator}";
        }
    }

    private static string FormatComponentSymbol((string Symbol, int Exponent) component)
    {
        var magnitude = Math.Abs((long)component.Exponent);
        return magnitude > 1 ? $"{component.Symbol}{magnitude}" : component.Symbol;
    }
}