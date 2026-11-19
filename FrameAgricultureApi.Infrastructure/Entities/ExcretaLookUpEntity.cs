using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the Excreta collection.
/// </summary>
[BsonCollection("Excreta")]
public class ExcretaLookUpEntity : MongoDocument
{
    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    [BsonElement("N_Excretion")]
    public double NExcretion { get; set; }

    [BsonElement("Percent_TAN")]
    public int PercentTAN { get; set; }

    [BsonElement("SectorId")]
    public int SectorId { get; set; }

    [BsonElement("Volatile_Solids")]
    public double VolatileSolids { get; set; }
}