namespace FrameAgricultureApi.Core;

/// <summary>
/// Reusable chemical units used by the API.
/// </summary>
public static class ChemicalUnits
{
    public static ChemicalUnit None { get; } = new([]);

    public static ChemicalUnit N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = "N",
            Exponent = 1
        }
    ]);

    public static ChemicalUnit N2O_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = "N2O-N",
            Exponent = 1
        }
    ]);

    public static ChemicalUnit NO3_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = "NO3-N",
            Exponent = 1
        }
    ]);

    public static ChemicalUnit NO_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = "NO-N",
            Exponent = 1
        }
    ]);

    public static ChemicalUnit N2_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = "N2-N",
            Exponent = 1
        }
    ]);

    public static ChemicalUnit NH3_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = "NH3-N",
            Exponent = 1
        }
    ]);
}