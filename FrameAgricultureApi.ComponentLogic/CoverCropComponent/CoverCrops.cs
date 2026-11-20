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
        myEmissions.InitialiseCoverCropEmissions();

        myEmissions.N.Value = coverCropRequest.CoverCropInputs.NReturn;

        myEmissions.DirectN20_N.Value = GenericEquations.Emission_PercentageEF(coverCropRequest.CoverCropInputs.NReturn, coverCropRequest.CoverCropEmissionFactors.IpccEf1);
        double adjustedFracLeach = coverCropRequest.CoverCropEmissionFactors.FracLeach * ((100 - coverCropRequest.CoverCropMitigationFactors.FracLeachReduction) * HelperFunctions.Percent_to_Proportion);
        myEmissions.DirectNO3_NLeached.Value = GenericEquations.Emission_PercentageEF(coverCropRequest.CoverCropInputs.NReturn, adjustedFracLeach);
        myEmissions.UnadjustedN03_N.Value = GenericEquations.Emission_PercentageEF(coverCropRequest.CoverCropInputs.NReturn, coverCropRequest.CoverCropEmissionFactors.FracLeach);
        myEmissions.IndirectN20_NLeached.Value = GenericEquations.Emission_PercentageEF(myEmissions.DirectNO3_NLeached.Value, coverCropRequest.CoverCropEmissionFactors.IpccEf5);
        myEmissions.UnadjustedIndirectedN20_NLeached.Value = GenericEquations.Emission_PercentageEF(myEmissions.UnadjustedN03_N.Value, coverCropRequest.CoverCropEmissionFactors.IpccEf5);
        myEmissions.DirectNO_N.Value = GenericEquations.Emission_ProportionEF(myEmissions.DirectN20_N.Value, coverCropRequest.CoverCropEmissionFactors.N0_N_ratio);
        myEmissions.DirectN2_N.Value = GenericEquations.Emission_ProportionEF(myEmissions.DirectN20_N.Value, coverCropRequest.CoverCropEmissionFactors.N2_N_ratio);
        myEmissions.IndirectN20_NVolatilisation.Value = GenericEquations.Emission_PercentageEF(myEmissions.DirectNO_N.Value, coverCropRequest.CoverCropEmissionFactors.IpccEf4);

        return myEmissions;
    }
}