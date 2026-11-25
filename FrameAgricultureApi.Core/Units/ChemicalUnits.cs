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
            Symbol = ChemicalUnitSymbols.N,
            Exponent = 1
        }
    ]);

    public static ChemicalUnit N2O_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N2O_N,
            Exponent = 1
        }
    ]);

    public static ChemicalUnit NO3_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.NO3_N,
            Exponent = 1
        }
    ]);

    public static ChemicalUnit NO_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.NO_N,
            Exponent = 1
        }
    ]);

    public static ChemicalUnit N2_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N2_N,
            Exponent = 1
        }
    ]);

    public static ChemicalUnit N2O_N_Per_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N2O_N,
            Exponent = 1
        },
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N,
            Exponent = -1
        }
    ]);

    /// <summary>
    /// N2O-N relative to the sum of volatilised NH3-N and NOx-N.
    /// The denominator is a grouped label representing addition rather than multiplication.
    /// </summary>
    public static ChemicalUnit N2O_N_Per_NH3_N_And_NOx_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N2O_N,
            Exponent = 1
        },
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.NH3_N_And_NOx_N,
            Exponent = -1
        }
    ]);

    public static ChemicalUnit NO3_N_Per_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.NO3_N,
            Exponent = 1
        },
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N,
            Exponent = -1
        }
    ]);

    public static ChemicalUnit NO_N_Per_N2O_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.NO_N,
            Exponent = 1
        },
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N2O_N,
            Exponent = -1
        }
    ]);

    public static ChemicalUnit N2_N_Per_N2O_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N2_N,
            Exponent = 1
        },
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.N2O_N,
            Exponent = -1
        }
    ]);

    public static ChemicalUnit NH3_N { get; } = new(
    [
        new BaseChemicalUnit
        {
            Symbol = ChemicalUnitSymbols.NH3_N,
            Exponent = 1
        }
    ]);
}