using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the Generic collection.
/// </summary>
[BsonCollection("Generic")]
public class GenericLookUpEntity : MongoDocument
{
    [BsonElement("Description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("Item")]
    public string Item { get; set; } = string.Empty;

    [BsonElement("Value")]
    public double Value { get; set; }
}