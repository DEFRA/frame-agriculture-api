using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the CropBurning collection.
/// </summary>
[BsonCollection("CropBurning")]
public class CropBurningLookUpEntity : MongoDocument
{
    [BsonElement("BurningEfficiency")]
    public double BurningEfficiency { get; set; }

    [BsonElement("CropName")]
    public string CropName { get; set; } = string.Empty;

    [BsonElement("CropTypeId")]
    public int CropTypeId { get; set; }

    [BsonElement("EF_CH4")]
    public double EFCH4 { get; set; }

    [BsonElement("EF_CO")]
    public double EFCO { get; set; }

    [BsonElement("EF_N2ON")]
    public double EFN2ON { get; set; }

    [BsonElement("EF_NH3N")]
    public double EFNH3N { get; set; }

    [BsonElement("EF_NMVOC")]
    public double EFNMVOC { get; set; }

    [BsonElement("EF_NOxN")]
    public double EFNOxN { get; set; }

    [BsonElement("EF_PM10")]
    public double EFPM10 { get; set; }

    [BsonElement("EF_PM2_5")]
    public double EFPM25 { get; set; }

    [BsonElement("EF_SO2")]
    public double EFSO2 { get; set; }

    [BsonElement("EF_TSP")]
    public double EFTSP { get; set; }
}