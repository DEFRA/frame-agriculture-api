using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the Grass_NonGHG collection.
/// </summary>
[BsonCollection("Grass_NonGHG")]
public class GrassNonGhgLookUpEntity : MongoDocument
{
    [BsonElement("EF_NMVOC")]
    public double EFNMVOC { get; set; }

    [BsonElement("EF_PM10_Cleaning")]
    public int EFPM10Cleaning { get; set; }

    [BsonElement("EF_PM10_Cultivation")]
    public double EFPM10Cultivation { get; set; }

    [BsonElement("EF_PM10_Drying")]
    public int EFPM10Drying { get; set; }

    [BsonElement("EF_PM10_Harvesting")]
    public double EFPM10Harvesting { get; set; }

    [BsonElement("EF_PM2_5_Cleaning")]
    public int EFPM25Cleaning { get; set; }

    [BsonElement("EF_PM2_5_Cultivation")]
    public double EFPM25Cultivation { get; set; }

    [BsonElement("EF_PM2_5_Drying")]
    public int EFPM25Drying { get; set; }

    [BsonElement("EF_PM2_5_Harvesting")]
    public double EFPM25Harvesting { get; set; }

    [BsonElement("Grass Type")]
    public string GrassType { get; set; } = string.Empty;

    [BsonElement("Grass Type Enum")]
    public int GrassTypeEnum { get; set; }
}