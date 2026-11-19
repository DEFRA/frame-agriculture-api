using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

[BsonCollection("Fertiliser_RatioEFs")]
public class FertiliserRatioEfsLookUpEntity : MongoDocument
{
    [BsonElement("Parameter")]
    public string Parameter { get; set; } = string.Empty;
    [BsonElement("Value")]
    public double Value { get; set; }
}