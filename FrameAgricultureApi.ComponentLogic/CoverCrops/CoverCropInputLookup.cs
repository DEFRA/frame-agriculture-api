using FrameAgricultureApi.Core.Enums;
using FrameAgricultureApi.Core.Outputs;
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

    public CoverCropInputLookupOutput CoverCropEmissionInputHelper(CoverCropType coverCropType)
    {
        var output = new CoverCropInputLookupOutput();
        output.CoverCropInputsHelper.N_Return.Value = GetCoverCropValue("N return");

        var factors = output.CoverCropEmissionFactorsHelper;
        factors.IpccEf1.Value = GetEmissionFactor("IPCCEF1");
        factors.IpccEf4.Value = GetEmissionFactor("IPCCEF4");
        factors.IpccEf5.Value = GetEmissionFactor("IPCCEF5");
        factors.FracLeach.Value = GetEmissionFactor("FracLeach");
        factors.NO_N_ratio.Value = GetEmissionFactor("Ratio_NON");
        factors.N2_N_ratio.Value = GetEmissionFactor("Ratio_N2N");

        output.CoverCropMitigationFactorsHelper.FracLeachReduction.Value = GetCoverCropValue("Frac Leach reduction");
        return output;

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