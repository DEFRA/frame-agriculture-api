using System;

namespace FrameAgricultureApi.Core;

public class PhysicalUnit
{
    public PhysicalUnit(List<BasePhysicalUnit> physicalUnitComponents)
    {
        ArgumentNullException.ThrowIfNull(physicalUnitComponents);
        if (physicalUnitComponents.Any(component => component is null))
        {
            throw new ArgumentException("Unit components cannot contain null entries.", nameof(physicalUnitComponents));
        }

        PhysicalUnitComponents = physicalUnitComponents;
    }

    public List<BasePhysicalUnit> PhysicalUnitComponents { get; }
    public string Symbol
    {
        get
        {
            var numerator = string.Concat(PhysicalUnitComponents
                .Where(component => component.Exponent > 0)
                .Select(FormatComponentSymbol));
            var denominator = string.Concat(PhysicalUnitComponents
                .Where(component => component.Exponent < 0)
                .Select(FormatComponentSymbol));

            if (denominator.Length == 0)
            {
                return numerator;
            }

            return $"{(numerator.Length == 0 ? "1" : numerator)}/{denominator}";
        }
    }
    public decimal MultipleToBaseFactor { get; set; }

    private static string FormatComponentSymbol(BasePhysicalUnit component)
    {
        var magnitude = Math.Abs((long)component.Exponent);
        return magnitude > 1 ? $"{component.Symbol}{magnitude}" : component.Symbol;
    }

}