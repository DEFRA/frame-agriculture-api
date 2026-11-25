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
        ChemicalUnits.N);

    public static Unit Kilogram_N2O_N_PerHectare { get; } = new(
        PhysicalUnits.KilogramPerHectare,
        ChemicalUnits.N2O_N);

    public static Unit Kilogram_NO3_N_PerHectare { get; } = new(PhysicalUnits.KilogramPerHectare, ChemicalUnits.NO3_N);

    public static Unit Kilogram_NO_N_PerHectare { get; } = new(PhysicalUnits.KilogramPerHectare, ChemicalUnits.NO_N);

    public static Unit Kilogram_N2_N_PerHectare { get; } = new(PhysicalUnits.KilogramPerHectare, ChemicalUnits.N2_N);

    public static Unit Kilogram_N { get; } = new(PhysicalUnits.Kilogram, ChemicalUnits.N);

    public static Unit Kilogram_NH3_N { get; } = new(PhysicalUnits.Kilogram, ChemicalUnits.NH3_N);

    public static Unit Kilogram_N2O_N_PerKilogram_N { get; } = new(
        PhysicalUnits.KilogramPerKilogram, ChemicalUnits.N2O_N_Per_N);

    public static Unit Kilogram_N2O_N_PerKilogram_NH3_N_And_NOx_N { get; } = new(
        PhysicalUnits.KilogramPerKilogram, ChemicalUnits.N2O_N_Per_NH3_N_And_NOx_N);

    public static Unit Kilogram_NO3_N_PerKilogram_N { get; } = new(
        PhysicalUnits.KilogramPerKilogram, ChemicalUnits.NO3_N_Per_N);

    public static Unit Kilogram_NO_N_PerKilogram_N2O_N { get; } = new(
        PhysicalUnits.KilogramPerKilogram, ChemicalUnits.NO_N_Per_N2O_N);

    public static Unit Kilogram_N2_N_PerKilogram_N2O_N { get; } = new(
        PhysicalUnits.KilogramPerKilogram, ChemicalUnits.N2_N_Per_N2O_N);

    public static Unit Ratio { get; } = new(PhysicalUnits.Ratio, ChemicalUnits.None);

    public static Unit Proportion { get; } = new(PhysicalUnits.Proportion, ChemicalUnits.None);
}