using System;
using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using FrameAgricultureApi.Libraries.LookUps;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace FrameAgricultureApi.Libraries.CoverCrops;


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

    /// <summary>
    /// Calculates emissions from cover crops
    /// </summary>
    /// <param name="coverCropType">Enumerator indicating the cover crop type</param>
    /// <returns>An object of type CoverCropEmissions</returns>
    public CoverCropEmissions CoverCropEmissions(CoverCropType coverCropType)
    {
        _logger.LogInformation("TestLog: This is a test log to see if it appears in the dashboard.");
        CoverCropEmissions myEmissions = new();
        myEmissions.InitialiseCoverCropEmissions();

        var filterBuilder = Builders<CoverCropLookUpEntity>.Filter;
        var coverCropsFilter = filterBuilder.Eq(x => x.Item, "N return");
        var additionalNReturn = _coverCropsCollection.FindOne(coverCropsFilter).Value;
        myEmissions.N.Value = additionalNReturn;

        myEmissions.DirectN20_N.Value = GenericEquations.Emission_PercentageEF(additionalNReturn, CropLookup.RetrieveIPCCEmissionFactor1());
        double adjustedFracLeach = CropLookup.RetrieveFracLeach() * ((100 - CropLookup.RetrievedCoverCropFracLeachReduction()) * HelperFunctions.Percent_to_Proportion);
        myEmissions.DirectNO3_NLeached.Value = GenericEquations.Emission_PercentageEF(additionalNReturn, adjustedFracLeach);
        myEmissions.UnadjustedN03_N.Value = GenericEquations.Emission_PercentageEF(additionalNReturn, CropLookup.RetrieveFracLeach());
        myEmissions.IndirectN20_NLeached.Value = GenericEquations.Emission_PercentageEF(myEmissions.DirectNO3_NLeached.Value, CropLookup.RetrieveIPCCEmissionFactor5());
        myEmissions.UnadjustedIndirectedN20_NLeached.Value = GenericEquations.Emission_PercentageEF(myEmissions.UnadjustedN03_N.Value, CropLookup.RetrieveIPCCEmissionFactor5());
        myEmissions.DirectNO_N.Value = GenericEquations.Emission_ProportionEF(myEmissions.DirectN20_N.Value, CropLookup.RetrieveRatioNON());
        myEmissions.DirectN2_N.Value = GenericEquations.Emission_ProportionEF(myEmissions.DirectN20_N.Value, CropLookup.RetrieveRatioN2N());
        myEmissions.IndirectN20_NVolatilisation.Value = GenericEquations.Emission_PercentageEF(myEmissions.DirectNO_N.Value, CropLookup.RetrieveIPCCEmissionFactor4());

        return myEmissions;
    }
}