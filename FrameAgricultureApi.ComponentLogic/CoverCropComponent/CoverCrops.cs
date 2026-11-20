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

        var nReturn = coverCropRequest.CoverCropInputs.NReturn;
        var factors = coverCropRequest.CoverCropEmissionFactors;
        var adjustedFracLeach = factors.FracLeach * ((100m - coverCropRequest.CoverCropMitigationFactors.FracLeachReduction) * 0.01m);
        var directN2ON = GenericEquations.Emission_PercentageEF(nReturn, factors.IpccEf1);
        var directNO3NLeached = GenericEquations.Emission_PercentageEF(nReturn, adjustedFracLeach);
        var unadjustedNO3N = GenericEquations.Emission_PercentageEF(nReturn, factors.FracLeach);
        var directNON = GenericEquations.Emission_ProportionEF(directN2ON, factors.N0_N_ratio);

        myEmissions.N.Value = nReturn;
        myEmissions.DirectN20_N.Value = directN2ON;
        myEmissions.DirectNO3_NLeached.Value = directNO3NLeached;
        myEmissions.UnadjustedN03_N.Value = unadjustedNO3N;
        myEmissions.IndirectN20_NLeached.Value = GenericEquations.Emission_PercentageEF(directNO3NLeached, factors.IpccEf5);
        myEmissions.UnadjustedIndirectedN20_NLeached.Value = GenericEquations.Emission_PercentageEF(unadjustedNO3N, factors.IpccEf5);
        myEmissions.DirectNO_N.Value = directNON;
        myEmissions.DirectN2_N.Value = GenericEquations.Emission_ProportionEF(directN2ON, factors.N2_N_ratio);
        myEmissions.IndirectN20_NVolatilisation.Value = GenericEquations.Emission_PercentageEF(directNON, factors.IpccEf4);

        return myEmissions;
    }
}