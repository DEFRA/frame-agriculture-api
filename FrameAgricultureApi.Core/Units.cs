using System;

namespace FrameAgricultureApi.Core;

public static class Units
{
    public static PhysicalUnit Kilogram { get; } = new()
    {
        Symbol = UnitSymbols.Kg,
        MultipleToBaseFactor = 1m
    };

    public static PhysicalUnit KilogramPerHectare { get; } = new()
    {
        Symbol = UnitSymbols.KgPerHa,
        MultipleToBaseFactor = 0.0001m
    };

    public static PhysicalUnit Ratio { get; } = new()
    {
        Symbol = UnitSymbols.Ratio,
        MultipleToBaseFactor = 1m
    };

    public static PhysicalUnit Proportion { get; } = new()
    {
        Symbol = UnitSymbols.Proportion,
        MultipleToBaseFactor = 1m
    };
}