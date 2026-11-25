using System;

namespace FrameAgricultureApi.Core;

public static class Units
{
    public static Unit Kilogram { get; } = new()
    {
        Symbol = UnitSymbols.Kg,
        MultipleToBaseFactor = 1m
    };

    public static Unit KilogramPerHectare { get; } = new()
    {
        Symbol = UnitSymbols.KgPerHa,
        MultipleToBaseFactor = 0.0001m
    };

    public static Unit Ratio { get; } = new()
    {
        Symbol = UnitSymbols.Ratio,
        MultipleToBaseFactor = 1m
    };

    public static Unit Proportion { get; } = new()
    {
        Symbol = UnitSymbols.Proportion,
        MultipleToBaseFactor = 1m
    };
}