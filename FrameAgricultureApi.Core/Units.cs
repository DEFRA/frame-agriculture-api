using System;

namespace FrameAgricultureApi.Core;

public static class Units
{
    public static PhysicalUnit Kilogram { get; } = new(
        [
            new BasePhysicalUnit
            {
                Symbol = UnitSymbols.Kg,
                Exponent = 1
            }
        ])
    {
        MultipleToBaseFactor = 1m
    };

    public static PhysicalUnit KilogramPerHectare { get; } = new(
        [
            new BasePhysicalUnit
            {
                Symbol = UnitSymbols.KgPerHa,
                Exponent = 1
            }
        ])
    {
        MultipleToBaseFactor = 0.0001m
    };

    public static PhysicalUnit Ratio { get; } = new(
        [
            new BasePhysicalUnit
            {
                Symbol = UnitSymbols.Ratio,
                Exponent = 1
            }
        ])
    {
        MultipleToBaseFactor = 1m
    };

    public static PhysicalUnit Proportion { get; } = new(
        [
            new BasePhysicalUnit
            {
                Symbol = UnitSymbols.Proportion,
                Exponent = 1
            }
        ])
    {
        MultipleToBaseFactor = 1m
    };
}