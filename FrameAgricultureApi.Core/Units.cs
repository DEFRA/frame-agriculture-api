namespace FrameAgricultureApi.Core;

/// <summary>
/// Reusable combined physical and chemical units used by the API.
/// </summary>
public static class Units
{
    /// <summary>
    /// Kilograms of nitrogen per hectare, displayed as KgN/ha.
    /// </summary>
    public static Unit Kilogram_N_PerHectare { get; } = new(
        PhysicalUnits.KilogramPerHectare,
        new ChemicalUnit(
        [
            new BaseChemicalUnit
            {
                Symbol = "N",
                Exponent = 1
            }
        ]));

    public static Unit Ratio { get; } = new(PhysicalUnits.Ratio, new ChemicalUnit([]));

    public static Unit Proportion { get; } = new(PhysicalUnits.Proportion, new ChemicalUnit([]));
}