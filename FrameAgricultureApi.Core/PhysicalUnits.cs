using System;

namespace FrameAgricultureApi.Core;

public static class PhysicalUnits
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
                Symbol = UnitSymbols.Kg,
                Exponent = 1
            },
            new BasePhysicalUnit
            {
                Symbol = UnitSymbols.Ha,
                Exponent = -1
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