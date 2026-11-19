using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the Sheep_GeneralCoefficients collection.
/// </summary>
[BsonCollection("Sheep_GeneralCoefficients")]
public class SheepGeneralCoefficientsLookUpEntity : MongoDocument
{
    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("Value")]
    public double Value { get; set; }
}