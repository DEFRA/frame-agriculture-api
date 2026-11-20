using FrameAgricultureApi.Core.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

[BsonCollection("Crop_Residue")]
public class CropResidueLookUpEntity : MongoDocument
{
    [BsonElement("CropName")]
    public string CropName { get; set; } = string.Empty;
    [BsonElement("CropTypeId")]
    public CropType CropTypeId { get; set; }

    [BsonElement("AboveGroundResiduesRemainingAfterBalingPercentage")]
    public double AboveGroundResiduesRemainingAfterBalingPercentage { get; set; }

    [BsonElement("AboveGroundResidueNContent")]
    public double AboveGroundResidue_N_Content { get; set; }
    [BsonElement("AboveToBelowGroundResidueRatio")]
    public double AboveToBelowGroundResidueRatio { get; set; }
    [BsonElement("BelowGroundResidueNContent")]
    public double BelowGroundResidue_N_Content { get; set; }
    [BsonElement("CropYieldNContent")]
    public double? CropYield_N_Content { get; set; }
    [BsonElement("DryMatterContentPercentage")]
    public double DryMatterContentPercentage { get; set; }
    [BsonElement("Fate1")]
    public double Fate1 { get; set; }
    [BsonElement("Fate2Incorporate")]
    public double Fate2Incorporate { get; set; }

    [BsonElement("HarvestIndex")]
    public double HarvestIndex { get; set; }
    [BsonElement("IPCCIntercept")]
    public double IPCCIntercept { get; set; }
    [BsonElement("IPCCSlope")]
    public double IPCCSlope { get; set; }
    [BsonElement("RAFate1")]
    public double? RAFate1 { get; set; }
    [BsonElement("RAFate2")]
    public double? RAFate2 { get; set; }
    [BsonElement("RegionalAverageYieldsEngland2019")]
    public double? RegionalAverageYieldsEngland2019 { get; set; }
    [BsonElement("ResidueAmmoniaContributingFraction")]
    public double Residue_NH3_ContributingFraction { get; set; }
    [BsonElement("Yield")]
    public double Yield { get; set; }
}