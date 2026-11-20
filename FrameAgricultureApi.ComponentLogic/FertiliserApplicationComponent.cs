using FrameAgricultureApi.Core.Inputs;
using FrameAgricultureApi.Core.Outputs;

namespace FrameAgricultureApi.ComponentLogic;

/// <summary>
/// Calculates emissions from fertiliser application.
/// </summary>
public class FertiliserApplicationComponent
{
    /// <summary>
    /// Calculates unmitigated emissions from fertiliser application.
    /// </summary>
    /// <param name="request">Application inputs and resolved emission factors.</param>
    /// <returns>Fertiliser application emissions and nitrogen-applied outputs.</returns>
    public FertiliserApplicationEmissions UnmitigatedEmissionsFertiliser(
        FertiliserApplicationUnmitigatedEmissionsFertiliserRequest request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Calculates mitigated emissions from fertiliser application.
    /// </summary>
    /// <param name="request">Application inputs, resolved emission factors and mitigation multipliers.</param>
    /// <returns>Fertiliser application emissions and nitrogen-applied outputs.</returns>
    public FertiliserApplicationEmissions MitigatedEmissionsFertiliser(
        FertiliserApplicationMitigatedEmissionsFertiliserRequest request)
    {
        throw new NotImplementedException();
    }
}