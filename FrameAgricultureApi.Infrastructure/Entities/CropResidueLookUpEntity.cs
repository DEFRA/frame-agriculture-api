using FrameAgricultureApi.Core.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

[BsonCollection("Crop_Residue")]
public class CropResidueLookUpEntity : MongoDocument
{
    public string CropName { get; set; } = string.Empty;
    public CropType CropTypeId { get; set; }

    [BsonElement("AboveGroundResiduesRemainingAfterBalingPercentage")]
    public double AboveGroundResiduesRemainingAfterBalingPercentage { get; set; }

    public double AboveGroundResidueNContent { get; set; }
    public double AboveToBelowGroundResidueRatio { get; set; }
    public double BelowGroundResidueNContent { get; set; }
    public double? CropYieldNContent { get; set; }
    public double DryMatterContentPercentage { get; set; }
    public double Fate1 { get; set; }
    [BsonElement("Fate2Incorporate")]
    public double Fate2Incorporate { get; set; }

    public double HarvestIndex { get; set; }
    public double IPCCIntercept { get; set; }
    public double IPCCSlope { get; set; }
    public double? RAFate1 { get; set; }
    public double? RAFate2 { get; set; }
    public double? RegionalAverageYieldsEngland2019 { get; set; }
    public double ResidueAmmoniaContributingFraction { get; set; }
    public double Yield { get; set; }
}