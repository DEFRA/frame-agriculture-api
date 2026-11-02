using System;
using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using FrameAgricultureApi.Libraries.LookUps;
using MongoDB.Driver;

namespace FrameAgricultureApi.Libraries.CoverCrops;


/// <summary>
/// The Cover Crops Component class
/// </summary>
public class CoverCrops
{
    private readonly IMongoService<CoverCropLookUpEntity> _coverCropsCollection;
    private readonly IMongoService<CropNonGhgLookUpEntity> _cropNonGhgCollection;
    private readonly IMongoService<CropResidueLookUpEntity> _cropResidueCollection;
    private readonly IMongoService<FertiliserRatioEfsLookUpEntity> _fertiliserRatioEfsCollection;

    public CoverCrops(
        IMongoService<CoverCropLookUpEntity> coverCropsCollection,
        IMongoService<CropNonGhgLookUpEntity> cropNonGhgCollection,
        IMongoService<CropResidueLookUpEntity> cropResidueCollection,
        IMongoService<FertiliserRatioEfsLookUpEntity> fertiliserRatioEfsCollection)
    {
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
        CoverCropEmissions myEmissions = new();
        myEmissions.InitialiseCoverCropEmissions();

        var filterBuilder = Builders<CoverCropLookUpEntity>.Filter;
        var coverCropsFilter = filterBuilder.Eq(x => x.Item, "N return");
        var additionalNReturn = _coverCropsCollection.FindOne(coverCropsFilter).Value;
        myEmissions.additionalOutputs[AdditionalCoverCropEmissions.NReturn].Value = additionalNReturn;

        myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(additionalNReturn, CropLookup.RetrieveIPCCEmissionFactor1());
        double adjustedFracLeach = CropLookup.RetrieveFracLeach() * ((100 - CropLookup.RetrievedCoverCropFracLeachReduction()) * HelperFunctions.Percent_to_Proportion);
        myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = GenericEquations.Emission_PercentageEF(additionalNReturn, adjustedFracLeach);
        myEmissions.additionalOutputs[AdditionalCoverCropEmissions.UnadjustedNO3N].Value = GenericEquations.Emission_PercentageEF(additionalNReturn, CropLookup.RetrieveFracLeach());
        myEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5());
        myEmissions.additionalOutputs[AdditionalCoverCropEmissions.UnadjustedN2ONLeach].Value = GenericEquations.Emission_PercentageEF(myEmissions.additionalOutputs[AdditionalCoverCropEmissions.UnadjustedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5());
        myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());
        myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());
        myEmissions.EmissionsCore[CoreEmissions.N2ONVolatilised].Value = GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, CropLookup.RetrieveIPCCEmissionFactor4());

        return myEmissions;
    }
}