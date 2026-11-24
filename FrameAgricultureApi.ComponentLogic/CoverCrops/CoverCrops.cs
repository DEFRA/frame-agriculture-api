using FrameAgricultureApi.Core.Outputs;
using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.ComponentLogic.CoverCropComponent;

/// <summary>
/// Calculates emissions from cover crops.
/// </summary>
public class CoverCrops
{
    public CoverCropEmissions CoverCropEmissions(CoverCropRequest coverCropRequest)
    {
        CoverCropEmissions myEmissions = new();

        var N_Return = coverCropRequest.CoverCropInputs.N_Return;
        var factors = coverCropRequest.CoverCropEmissionFactors;
        var adjustedFracLeach = factors.FracLeach * ((100m - coverCropRequest.CoverCropMitigationFactors.FracLeachReduction) * 0.01m);
        var direct_N2O_N = GenericEquations.Emission_PercentageEF(N_Return, factors.IpccEf1);
        var direct_NO3_N_Leached = GenericEquations.Emission_PercentageEF(N_Return, adjustedFracLeach);
        var unadjusted_NO3_N = GenericEquations.Emission_PercentageEF(N_Return, factors.FracLeach);
        var direct_NO_N = GenericEquations.Emission_ProportionEF(direct_N2O_N, factors.NO_N_ratio);

        myEmissions.N.Value = N_Return;
        myEmissions.Direct_N2O_N.Value = direct_N2O_N;
        myEmissions.Direct_NO3_N_Leached.Value = direct_NO3_N_Leached;
        myEmissions.Unadjusted_NO3_N.Value = unadjusted_NO3_N;
        myEmissions.Indirect_N2O_N_Leached.Value = GenericEquations.Emission_PercentageEF(direct_NO3_N_Leached, factors.IpccEf5);
        myEmissions.UnadjustedIndirected_N2O_N_Leached.Value = GenericEquations.Emission_PercentageEF(unadjusted_NO3_N, factors.IpccEf5);
        myEmissions.Direct_NO_N.Value = direct_NO_N;
        myEmissions.Direct_N2_N.Value = GenericEquations.Emission_ProportionEF(direct_N2O_N, factors.N2_N_ratio);
        myEmissions.Indirect_N2O_N_Volatilisation.Value = GenericEquations.Emission_PercentageEF(direct_NO_N, factors.IpccEf4);

        return myEmissions;
    }
}