using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using MongoDB.Driver;

namespace FrameAgricultureApi.ComponentLogic.CoverCropComponent;

/// <summary>
/// Resolves cover crop calculation inputs from MongoDB lookup collections.
/// </summary>
public class CoverCropInputLookup
{
    private readonly IMongoService<CoverCropLookUpEntity> _coverCropsCollection;
    private readonly IMongoService<FertiliserRatioEfsLookUpEntity> _fertiliserRatioEfsCollection;

    public CoverCropInputLookup(
        IMongoService<CoverCropLookUpEntity> coverCropsCollection,
        IMongoService<FertiliserRatioEfsLookUpEntity> fertiliserRatioEfsCollection)
    {
        _coverCropsCollection = coverCropsCollection ?? throw new ArgumentNullException(nameof(coverCropsCollection));
        _fertiliserRatioEfsCollection = fertiliserRatioEfsCollection ?? throw new ArgumentNullException(nameof(fertiliserRatioEfsCollection));
    }

    public CoverCropRequest CoverCropEmissionInputHelper(CoverCropType coverCropType)
    {
        return new CoverCropRequest
        {
            CoverCropInputs = new CoverCropInputs
            {
                N_Return = GetCoverCropValue("N return")
            },
            CoverCropEmissionFactors = new CoverCropEmissionFactors
            {
                IpccEf1 = GetEmissionFactor("IPCCEF1"),
                IpccEf4 = GetEmissionFactor("IPCCEF4"),
                IpccEf5 = GetEmissionFactor("IPCCEF5"),
                FracLeach = GetEmissionFactor("FracLeach"),
                NO_N_ratio = GetEmissionFactor("Ratio_NON"),
                N2_N_ratio = GetEmissionFactor("Ratio_N2N")
            },
            CoverCropMitigationFactors = new CoverCropMitigationFactors
            {
                FracLeachReduction = GetCoverCropValue("Frac Leach reduction")
            }
        };

        decimal GetCoverCropValue(string item)
        {
            var filter = Builders<CoverCropLookUpEntity>.Filter.Eq(crop => crop.CoverCropType, coverCropType)
                & Builders<CoverCropLookUpEntity>.Filter.Eq(crop => crop.Item, item);
            var lookup = _coverCropsCollection.FindOne(filter)
                ?? throw new InvalidOperationException($"Cover crop lookup '{item}' was not found for cover crop type '{coverCropType}'.");

            return (decimal)lookup.Value;
        }

        decimal GetEmissionFactor(string parameter)
        {
            var filter = Builders<FertiliserRatioEfsLookUpEntity>.Filter.Eq(factor => factor.Parameter, parameter);
            var lookup = _fertiliserRatioEfsCollection.FindOne(filter)
                ?? throw new InvalidOperationException($"Fertiliser emission factor '{parameter}' was not found.");

            return (decimal)lookup.Value;
        }
    }
}