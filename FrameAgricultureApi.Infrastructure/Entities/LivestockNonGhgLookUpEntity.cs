using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the Livestock_NonGHG collection.
/// </summary>
[BsonCollection("Livestock_NonGHG")]
public class LivestockNonGhgLookUpEntity : MongoDocument
{
    [BsonElement("AnimalType")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    [BsonElement("EF Grazing")]
    public double EFGrazing { get; set; }

    [BsonElement("EF Housing")]
    public double EFHousing { get; set; }

    [BsonElement("EF Silage Feed")]
    public double EFSilageFeed { get; set; }

    [BsonElement("Fraction Silage")]
    public double FractionSilage { get; set; }

    [BsonElement("Fraction Silage Store")]
    public double FractionSilageStore { get; set; }

    [BsonElement("PM10")]
    public double PM10 { get; set; }

    [BsonElement("PM25")]
    public double PM25 { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("SectorId")]
    public int SectorId { get; set; }

    [BsonElement("TSP")]
    public double TSP { get; set; }
}