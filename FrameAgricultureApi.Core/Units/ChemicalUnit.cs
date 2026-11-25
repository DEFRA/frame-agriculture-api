namespace FrameAgricultureApi.Core;

/// <summary>
/// A chemical unit composed of symbols grouped by exponent sign.
/// </summary>
public class ChemicalUnit
{
    public ChemicalUnit(List<BaseChemicalUnit> chemicalUnitComponents)
    {
        ArgumentNullException.ThrowIfNull(chemicalUnitComponents);
        if (chemicalUnitComponents.Any(component => component is null))
        {
            throw new ArgumentException("Unit components cannot contain null entries.", nameof(chemicalUnitComponents));
        }

        ChemicalUnitComponents = chemicalUnitComponents;
    }

    public List<BaseChemicalUnit> ChemicalUnitComponents { get; }

    public string Symbol
    {
        get
        {
            var numerator = string.Concat(ChemicalUnitComponents
                .Where(component => component.Exponent > 0)
                .Select(FormatComponentSymbol));
            var denominator = string.Concat(ChemicalUnitComponents
                .Where(component => component.Exponent < 0)
                .Select(FormatComponentSymbol));

            if (denominator.Length == 0)
            {
                return numerator;
            }

            return $"{(numerator.Length == 0 ? "1" : numerator)}/{denominator}";
        }
    }

    private static string FormatComponentSymbol(BaseChemicalUnit component)
    {
        var magnitude = Math.Abs((long)component.Exponent);
        return magnitude > 1 ? $"{component.Symbol}{magnitude}" : component.Symbol;
    }
}