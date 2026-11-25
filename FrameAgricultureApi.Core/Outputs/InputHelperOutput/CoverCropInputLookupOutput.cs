namespace FrameAgricultureApi.Core.Outputs;

/// <summary>
/// Cover crop lookup inputs, emission factors and mitigation factors with their units.
/// </summary>
public class CoverCropInputLookupOutput
{
    public CoverCropInputsHelper CoverCropInputsHelper { get; set; } = new();

    public CoverCropEmissionFactorsHelper CoverCropEmissionFactorsHelper { get; set; } = new();

    public CoverCropMitigationFactorsHelper CoverCropMitigationFactorsHelper { get; set; } = new();
}

/// <summary>
/// Additional nitrogen returned to soil from cover crop residues.
/// </summary>
public class CoverCropInputsHelper
{
    public InputHelper N_Return { get; set; } = new()
    {
        Unit = Units.Kilogram_N_PerHectare,
    };
}

/// <summary>
/// Emission factors and ratios used to calculate cover crop emissions.
/// </summary>
public class CoverCropEmissionFactorsHelper
{
    /// <summary>
    /// Direct N2O-N emissions per unit of nitrogen returned.
    /// </summary>
    public InputHelper IpccEf1 { get; set; } = new()
    {
        Unit = Units.Kilogram_N2O_N_PerKilogram_N,
    };

    /// <summary>
    /// Indirect N2O-N emissions per unit of volatilised nitrogen.
    /// </summary>
    public InputHelper IpccEf4 { get; set; } = new()
    {
        Unit = Units.Kilogram_N2O_N_PerKilogram_NH3_N_And_NOx_N,
    };

    /// <summary>
    /// Indirect N2O-N emissions per unit of nitrogen leached or lost in runoff.
    /// </summary>
    public InputHelper IpccEf5 { get; set; } = new()
    {
        Unit = Units.Kilogram_N2O_N_PerKilogram_N,
    };

    /// <summary>
    /// Fraction of nitrogen lost through leaching and runoff.
    /// </summary>
    public InputHelper FracLeach { get; set; } = new()
    {
        Unit = Units.Kilogram_NO3_N_PerKilogram_N
    };

    /// <summary>
    /// NO-N emissions relative to direct N2O-N emissions.
    /// </summary>
    public InputHelper NO_N_ratio { get; set; } = new()
    {
        Unit = Units.Kilogram_NO_N_PerKilogram_N2O_N
    };

    /// <summary>
    /// N2-N emissions relative to direct N2O-N emissions.
    /// </summary>
    public InputHelper N2_N_ratio { get; set; } = new()
    {
        Unit = Units.Kilogram_N2_N_PerKilogram_N2O_N
    };
}

/// <summary>
/// Mitigation factors applied to cover crop emissions.
/// </summary>
public class CoverCropMitigationFactorsHelper
{
    /// <summary>
    /// Proportional reduction in the default leaching fraction, expressed from zero to one.
    /// </summary>
    public InputHelper FracLeachReduction { get; set; } = new()
    {
        Unit = Units.Proportion
    };
}

/// <summary>
/// A resolved lookup value and its unit; the symbol 1 represents a dimensionless proportion.
/// </summary>
public class InputHelper
{
    public Unit Unit { get; set; }

    public decimal Value { get; set; }
}