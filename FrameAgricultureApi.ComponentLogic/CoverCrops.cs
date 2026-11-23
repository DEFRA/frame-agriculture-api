using FrameAgricultureApi.Core.Outputs;
using System;
using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace FrameAgricultureApi.ComponentLogic.CoverCropComponent;


/// <summary>
/// The Cover Crops Component class
/// </summary>
public class CoverCrops
{
    private readonly ILogger<CoverCrops> _logger;
    private readonly IMongoService<CoverCropLookUpEntity> _coverCropsCollection;
    private readonly IMongoService<CropNonGhgLookUpEntity> _cropNonGhgCollection;
    private readonly IMongoService<CropResidueLookUpEntity> _cropResidueCollection;
    private readonly IMongoService<FertiliserRatioEfsLookUpEntity> _fertiliserRatioEfsCollection;

    public CoverCrops(
        IMongoService<CoverCropLookUpEntity> coverCropsCollection,
        IMongoService<CropNonGhgLookUpEntity> cropNonGhgCollection,
        IMongoService<CropResidueLookUpEntity> cropResidueCollection,
        IMongoService<FertiliserRatioEfsLookUpEntity> fertiliserRatioEfsCollection,
        ILogger<CoverCrops> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _coverCropsCollection = coverCropsCollection ?? throw new ArgumentNullException(nameof(coverCropsCollection));
        _cropNonGhgCollection = cropNonGhgCollection ?? throw new ArgumentNullException(nameof(cropNonGhgCollection));
        _cropResidueCollection = cropResidueCollection ?? throw new ArgumentNullException(nameof(cropResidueCollection));
        _fertiliserRatioEfsCollection = fertiliserRatioEfsCollection ?? throw new ArgumentNullException(nameof(fertiliserRatioEfsCollection));
    }

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