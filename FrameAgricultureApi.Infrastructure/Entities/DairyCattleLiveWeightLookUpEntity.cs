using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the DairyCattleLiveWeight collection.
/// </summary>
[BsonCollection("DairyCattleLiveWeight")]
public class DairyCattleLiveWeightLookUpEntity : MongoDocument
{
    [BsonElement("Age Death")]
    public int AgeDeath { get; set; }

    [BsonElement("Age First Calving")]
    public int AgeFirstCalving { get; set; }

    [BsonElement("Age first conception")]
    public int AgeFirstConception { get; set; }

    [BsonElement("Breed")]
    public int Breed { get; set; }

    [BsonElement("Cattle Mature Weight")]
    public double CattleMatureWeight { get; set; }

    [BsonElement("Country")]
    public int Country { get; set; }
}