using FrameAgricultureApi.ComponentLogic;
using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Core.Inputs;
using FrameAgricultureApi.Core.Outputs;

namespace FrameAgricultureApi.Test.ComponentTests;

/// <summary>
/// Legacy fertiliser application scenarios for the new request-based component.
/// Missing lookup values are deliberately marked with TODOs and 9999 placeholders.
/// These tests are skipped awaiting data licensing for certain lookup tables.
/// </summary>
public class FertiliserApplicationTests
{
    /// <summary>
    /// Three unmitigated scenarios from FLEA.Components.Tests/FertiliserApplicationTests.cs.
    /// Expected output literals are preserved from the legacy tests.
    /// </summary>
    public static TheoryData<FertiliserApplicationUnmitigatedEmissionsFertiliserRequest, FertiliserApplicationEmissions> UnmitigatedCases => new()
    {
        // Legacy scenario: CH4N2O, grid 219.
        {
            CreateUnmitigatedRequest(
                FertiliserType.Urea,
                [0m, 0m, 50m, 50m, 0m, 100m, 0m, 0m, 0m, 0m, 0m, 0m],
                total_N_Applied: 500m, percentageFertiliserType: 40m,
                annualAverageRainfall: 1056m, acidicSoil: true),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 1.064765m },
                Direct_NH3_N = { Value = 36.79613m },
                Direct_NO_N = { Value = 0.425906m },
                Direct_N2_N = { Value = 3.194295m },
                Indirect_N2O_N_Leached = { Value = 0.45m },
                Indirect_N2O_N_Volatilisation = { Value = 0.521109m },
                Direct_NO3_N_Leached = { Value = 60m },
                CH4N2O_N_Applied = { Value = 200m }
            }
        },
        // Legacy scenario: NH4NO3, grid 402.
        {
            CreateUnmitigatedRequest(
                FertiliserType.AmmoniumNitrate,
                [0m, 0m, 10m, 20m, 0m, 100m, 0m, 10m, 100m, 0m, 0m, 0m],
                total_N_Applied: 600m, percentageFertiliserType: 40m,
                annualAverageRainfall: 1063m, acidicSoil: true),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 6.493269m },
                Direct_NH3_N = { Value = 4.0608m },
                Direct_NO_N = { Value = 2.597307m },
                Direct_N2_N = { Value = 19.47981m },
                Indirect_N2O_N_Leached = { Value = 0.54m },
                Indirect_N2O_N_Volatilisation = { Value = 0.093214m },
                Direct_NO3_N_Leached = { Value = 72m },
                NH4NO3_N_Applied = { Value = 240m }
            }
        },
        // Legacy scenario: AS/DAP, grid 5193.
        {
            CreateUnmitigatedRequest(
                FertiliserType.AmmoniumSulphate_DiammoniumPhosphate,
                [150m, 0m, 100m, 50m, 0m, 100m, 0m, 50m, 100m, 0m, 100m, 0m],
                total_N_Applied: 812.5m, percentageFertiliserType: 80m,
                annualAverageRainfall: 662m, acidicSoil: false),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 7.925858m },
                Direct_NH3_N = { Value = 103.4034m },
                Direct_NO_N = { Value = 3.170343m },
                Direct_N2_N = { Value = 23.77757m },
                Indirect_N2O_N_Leached = { Value = 1.4625m },
                Indirect_N2O_N_Volatilisation = { Value = 1.492033m },
                Direct_NO3_N_Leached = { Value = 195m },
                AS_DAP_N_Applied = { Value = 650m }
            }
        }
    };

    /// <summary>
    /// Five mitigated scenarios from FLEA.Components.Tests/FertiliserApplicationTests.cs.
    /// Multipliers are resolved from FRAMESeparatedByLUT.FertiliserMitigation.
    /// </summary>
    public static TheoryData<FertiliserApplicationMitigatedEmissionsFertiliserRequest, FertiliserApplicationEmissions> MitigatedCases => new()
    {
        // Legacy scenario: CH4N2O, grid 219, method 1 (45% leaching reduction).
        {
            CreateMitigatedRequest(
                FertiliserType.Urea,
                [0m, 0m, 50m, 50m, 0m, 100m, 0m, 0m, 0m, 0m, 0m, 0m],
                total_N_Applied: 400m, percentageFertiliserType: 50m,
                annualAverageRainfall: 1056m, acidicSoil: true,
                factor_N2O: 1m, factor_NH3: 1m, factorLeaching: 0.55m),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 0.978514m },
                Direct_NH3_N = { Value = 36.79613m },
                Direct_NO_N = { Value = 0.391406m },
                Direct_N2_N = { Value = 2.935543m },
                Indirect_N2O_N_Leached = { Value = 0.2475m },
                Indirect_N2O_N_Volatilisation = { Value = 0.520626m },
                Direct_NO3_N_Leached = { Value = 33m },
                CH4N2O_N_Applied = { Value = 200m }
            }
        },
        // Legacy scenario: CH4N2O, grid 402, methods 1 and 2 (method 2 has zero reductions).
        {
            CreateMitigatedRequest(
                FertiliserType.Urea,
                [0m, 0m, 10m, 20m, 0m, 100m, 0m, 10m, 100m, 0m, 0m, 0m],
                total_N_Applied: 480m, percentageFertiliserType: 50m,
                annualAverageRainfall: 1063m, acidicSoil: true,
                factor_N2O: 1m, factor_NH3: 1m, factorLeaching: 0.55m),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 1.256124m },
                Direct_NH3_N = { Value = 58.45722m },
                Direct_NO_N = { Value = 0.50245m },
                Direct_N2_N = { Value = 3.768373m },
                Indirect_N2O_N_Leached = { Value = 0.297m },
                Indirect_N2O_N_Volatilisation = { Value = 0.825435m },
                Direct_NO3_N_Leached = { Value = 39.6m },
                CH4N2O_N_Applied = { Value = 240m }
            }
        },
        // Legacy scenario: UAN, grid 863, methods 1 and 3 (45% leaching and 44% NH3 reductions).
        {
            CreateMitigatedRequest(
                FertiliserType.UreaAmmoniumNitrate,
                [150m, 0m, 100m, 50m, 0m, 100m, 0m, 50m, 100m, 0m, 100m, 0m],
                total_N_Applied: 812.5m, percentageFertiliserType: 80m,
                annualAverageRainfall: 1057m, acidicSoil: false,
                factor_N2O: 1m, factor_NH3: 0.56m, factorLeaching: 0.55m),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 4.559927m },
                Direct_NH3_N = { Value = 30.53312m },
                Direct_NO_N = { Value = 1.823971m },
                Direct_N2_N = { Value = 13.67978m },
                Indirect_N2O_N_Leached = { Value = 0.804375m },
                Indirect_N2O_N_Volatilisation = { Value = 0.452999m },
                Direct_NO3_N_Leached = { Value = 107.25m },
                UAN_N_Applied = { Value = 650m }
            }
        },
        // Legacy scenario: UAN, grid 863, methods 1, 2 and 3 (method 2 has zero reductions).
        {
            CreateMitigatedRequest(
                FertiliserType.UreaAmmoniumNitrate,
                [150m, 0m, 100m, 50m, 0m, 100m, 0m, 50m, 100m, 0m, 100m, 0m],
                total_N_Applied: 812.5m, percentageFertiliserType: 80m,
                annualAverageRainfall: 1057m, acidicSoil: false,
                factor_N2O: 1m, factor_NH3: 0.56m, factorLeaching: 0.55m),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 4.559927m },
                Direct_NH3_N = { Value = 30.53312m },
                Direct_NO_N = { Value = 1.823971m },
                Direct_N2_N = { Value = 13.67978m },
                Indirect_N2O_N_Leached = { Value = 0.804375m },
                Indirect_N2O_N_Volatilisation = { Value = 0.452999m },
                Direct_NO3_N_Leached = { Value = 107.25m },
                UAN_N_Applied = { Value = 650m }
            }
        },
        // Legacy scenario: CAN, grid 6157, method 4 (80% NH3 reduction).
        {
            CreateMitigatedRequest(
                FertiliserType.CalciumAmmoniumNitrate,
                [0m, 10m, 100m, 50m, 0m, 50m, 0m, 50m, 100m, 0m, 100m, 0m],
                total_N_Applied: 1150m, percentageFertiliserType: 40m,
                annualAverageRainfall: 1647m, acidicSoil: true,
                factor_N2O: 1m, factor_NH3: 0.2m, factorLeaching: 1m),
            new FertiliserApplicationEmissions
            {
                Direct_N2O_N = { Value = 122.8457m },
                Direct_NH3_N = { Value = 1.55664m },
                Direct_NO_N = { Value = 49.13827m },
                Direct_N2_N = { Value = 368.5371m },
                Indirect_N2O_N_Leached = { Value = 1.035m },
                Indirect_N2O_N_Volatilisation = { Value = 0.709729m },
                Direct_NO3_N_Leached = { Value = 138m },
                CAN_N_Applied = { Value = 460m }
            }
        }
    };

    /// <summary>
    /// Checks all output values against the legacy unmitigated results at four decimal places.
    /// </summary>
    [Theory(Skip = "Awaiting data licensing for certain lookup tables.")]
    [MemberData(nameof(UnmitigatedCases))]
    public void UnmitigatedEmissionsFertiliser_AllOutputsCorrect(
        FertiliserApplicationUnmitigatedEmissionsFertiliserRequest request,
        FertiliserApplicationEmissions expected)
    {
        var component = new FertiliserApplicationComponent();

        var actual = component.UnmitigatedEmissionsFertiliser(request);

        AssertAllOutputs(expected, actual);
    }

    /// <summary>
    /// Checks all output values against the legacy mitigated results at four decimal places.
    /// </summary>
    [Theory(Skip = "Awaiting data licensing for certain lookup tables.")]
    [MemberData(nameof(MitigatedCases))]
    public void MitigatedEmissionsFertiliser_AllOutputsCorrect(
        FertiliserApplicationMitigatedEmissionsFertiliserRequest request,
        FertiliserApplicationEmissions expected)
    {
        var component = new FertiliserApplicationComponent();

        var actual = component.MitigatedEmissionsFertiliser(request);

        AssertAllOutputs(expected, actual);
    }

    private static FertiliserApplicationUnmitigatedEmissionsFertiliserRequest CreateUnmitigatedRequest(
        FertiliserType fertiliserType, decimal[] applications, decimal total_N_Applied,
        decimal percentageFertiliserType, decimal annualAverageRainfall, bool acidicSoil)
    {
        return new FertiliserApplicationUnmitigatedEmissionsFertiliserRequest
        {
            Inputs = new FertiliserApplicationUnmitigatedEmissionsFertiliserRequestInputs
            {
                FertiliserType = fertiliserType,
                FertiliserApplications = applications,
                Total_N_Applied = total_N_Applied,
                PercentageFertiliserType = percentageFertiliserType,
                AcidicSoil = acidicSoil,
                // FRAMESeparatedByLUT.Generic_CLIMATE REGION_CELL, ANNUAL RAINFALL for the scenario's CELL.
                // TODO: Reconcile these climate values with the different physical lookup used by the legacy component.
                AnnualAverageRainfall = annualAverageRainfall,
                // TODO: Replace every 9999 with the missing monthly mean temperature, January to December.
                MonthlyTemperatures = [9999m, 9999m, 9999m, 9999m, 9999m, 9999m, 9999m, 9999m, 9999m, 9999m, 9999m, 9999m],
                // TODO: Replace every 9999 with the missing rainfall-event probability for days 1 to 6.
                RainfallEventProbabilities = [9999m, 9999m, 9999m, 9999m, 9999m, 9999m]
            },
            EmissionFactors = new FertiliserApplicationUnmitigatedEmissionsFertiliserRequestEmissionFactors
            {
                // TODO: Replace 9999 with the missing N2O-N model coefficient uncertainty for the fertiliser type.
                N2O_N_ModelCoefficientUncertainty = 9999m,
                // TODO: Replace 9999 with the missing NH3 model coefficient uncertainty for the fertiliser type.
                NH3_ModelCoefficientUncertainty = 9999m,
                // FRAMESeparatedByLUT.Fertiliser_RatioEFs: Ratio_NON, Ratio_N2N, FracLeach, IPCCEF4, IPCCEF5.
                NO_N_Ratio = 0.4m,
                N2_N_Ratio = 3m,
                FracLeach = 30m,
                IpccEf4 = 1.4m,
                IpccEf5 = 0.75m
            },
            MitigationFactors = new FertiliserApplicationUnmitigatedEmissionsFertiliserRequestMitigationFactors()
        };
    }

    private static FertiliserApplicationMitigatedEmissionsFertiliserRequest CreateMitigatedRequest(
        FertiliserType fertiliserType, decimal[] applications, decimal total_N_Applied,
        decimal percentageFertiliserType, decimal annualAverageRainfall, bool acidicSoil,
        decimal factor_N2O, decimal factor_NH3, decimal factorLeaching)
    {
        // Both legacy functions consume the same application, weather and emission factor inputs.
        var unmitigated = CreateUnmitigatedRequest(
            fertiliserType, applications, total_N_Applied, percentageFertiliserType, annualAverageRainfall, acidicSoil);

        return new FertiliserApplicationMitigatedEmissionsFertiliserRequest
        {
            Inputs = new FertiliserApplicationMitigatedEmissionsFertiliserRequestInputs
            {
                FertiliserType = unmitigated.Inputs.FertiliserType,
                FertiliserApplications = unmitigated.Inputs.FertiliserApplications,
                Total_N_Applied = unmitigated.Inputs.Total_N_Applied,
                PercentageFertiliserType = unmitigated.Inputs.PercentageFertiliserType,
                AcidicSoil = unmitigated.Inputs.AcidicSoil,
                // TODO: Reconcile the climate rainfall with the legacy physical lookup.
                AnnualAverageRainfall = unmitigated.Inputs.AnnualAverageRainfall,
                // TODO: Replace the 9999 temperature placeholders created above.
                MonthlyTemperatures = unmitigated.Inputs.MonthlyTemperatures,
                // TODO: Replace the 9999 rainfall-event probability placeholders created above.
                RainfallEventProbabilities = unmitigated.Inputs.RainfallEventProbabilities
            },
            EmissionFactors = new FertiliserApplicationMitigatedEmissionsFertiliserRequestEmissionFactors
            {
                // TODO: Replace the 9999 N2O-N uncertainty placeholder created above.
                N2O_N_ModelCoefficientUncertainty = unmitigated.EmissionFactors.N2O_N_ModelCoefficientUncertainty,
                // TODO: Replace the 9999 NH3 uncertainty placeholder created above.
                NH3_ModelCoefficientUncertainty = unmitigated.EmissionFactors.NH3_ModelCoefficientUncertainty,
                NO_N_Ratio = unmitigated.EmissionFactors.NO_N_Ratio,
                N2_N_Ratio = unmitigated.EmissionFactors.N2_N_Ratio,
                FracLeach = unmitigated.EmissionFactors.FracLeach,
                IpccEf4 = unmitigated.EmissionFactors.IpccEf4,
                IpccEf5 = unmitigated.EmissionFactors.IpccEf5
            },
            // Final multipliers from FertiliserMitigation: product of (100 - reduction percentage) / 100.
            // Methods 1 to 4 have no mutual conflicts in the queried MitigationConflicts rows.
            MitigationFactors = new FertiliserApplicationMitigatedEmissionsFertiliserRequestMitigationFactors
            {
                Factor_N2O = factor_N2O,
                Factor_NH3 = factor_NH3,
                FactorLeaching = factorLeaching
            }
        };
    }

    private static void AssertAllOutputs(FertiliserApplicationEmissions expected, FertiliserApplicationEmissions actual)
    {
        // Compare independently of the production RoundEmissions implementation.
        // Unused fertiliser categories retain zero in the current decimal output model;
        // the legacy model used NaN for those entries.
        Assert.Multiple(
            () => AssertEmissionEqual(expected.Direct_N2O_N.Value, actual.Direct_N2O_N.Value),
            () => AssertEmissionEqual(expected.Direct_NH3_N.Value, actual.Direct_NH3_N.Value),
            () => AssertEmissionEqual(expected.Direct_NO_N.Value, actual.Direct_NO_N.Value),
            () => AssertEmissionEqual(expected.Direct_N2_N.Value, actual.Direct_N2_N.Value),
            () => AssertEmissionEqual(expected.Indirect_N2O_N_Leached.Value, actual.Indirect_N2O_N_Leached.Value),
            () => AssertEmissionEqual(expected.Indirect_N2O_N_Volatilisation.Value, actual.Indirect_N2O_N_Volatilisation.Value),
            () => AssertEmissionEqual(expected.Direct_NO3_N_Leached.Value, actual.Direct_NO3_N_Leached.Value),
            () => AssertEmissionEqual(expected.CH4N2O_N_Applied.Value, actual.CH4N2O_N_Applied.Value),
            () => AssertEmissionEqual(expected.NH4NO3_N_Applied.Value, actual.NH4NO3_N_Applied.Value),
            () => AssertEmissionEqual(expected.CAN_N_Applied.Value, actual.CAN_N_Applied.Value),
            () => AssertEmissionEqual(expected.UAN_N_Applied.Value, actual.UAN_N_Applied.Value),
            () => AssertEmissionEqual(expected.AS_DAP_N_Applied.Value, actual.AS_DAP_N_Applied.Value),
            () => AssertEmissionEqual(expected.OtherFertilisers_N_Applied.Value, actual.OtherFertilisers_N_Applied.Value));
    }

    private static void AssertEmissionEqual(decimal expected, decimal actual)
    {
        Assert.Equal(
            decimal.Round(expected, 4, MidpointRounding.AwayFromZero),
            decimal.Round(actual, 4, MidpointRounding.AwayFromZero));
    }
}