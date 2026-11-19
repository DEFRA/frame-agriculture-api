using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the ManureManagement_Bedding collection.
/// </summary>
[BsonCollection("ManureManagement_Bedding")]
public class ManureManagementBeddingLookUpEntity : MongoDocument
{
    [BsonElement("AnimalAge")]
    public string AnimalAge { get; set; } = string.Empty;

    [BsonElement("AnimalAgeId")]
    public int AnimalAgeId { get; set; }

    [BsonElement("AnimalType")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    [BsonElement("BeddingAmount")]
    public double BeddingAmount { get; set; }

    [BsonElement("BeddingNContent")]
    public double BeddingNContent { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("SectorID")]
    public int SectorID { get; set; }
}