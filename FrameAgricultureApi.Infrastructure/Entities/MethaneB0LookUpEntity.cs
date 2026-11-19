using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the MethaneB0 collection.
/// </summary>
[BsonCollection("MethaneB0")]
public class MethaneB0LookUpEntity : MongoDocument
{
    [BsonElement("AnimalType")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    [BsonElement("B0")]
    public double B0 { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("SectorId")]
    public int SectorId { get; set; }
}