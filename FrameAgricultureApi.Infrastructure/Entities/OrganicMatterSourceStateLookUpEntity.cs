using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the OrganicMatterSourceState collection.
/// </summary>
[BsonCollection("OrganicMatterSourceState")]
public class OrganicMatterSourceStateLookUpEntity : MongoDocument
{
    [BsonElement("Organic Matter Source Type")]
    public string OrganicMatterSourceType { get; set; } = string.Empty;

    [BsonElement("Organic Matter Source Type Enumerator")]
    public int OrganicMatterSourceTypeEnumerator { get; set; }

    [BsonElement("Organic Matter State Type")]
    public string OrganicMatterStateType { get; set; } = string.Empty;

    [BsonElement("Organic Matter State Type Enumerator")]
    public int OrganicMatterStateTypeEnumerator { get; set; }

    [BsonElement("Organic Matter Type")]
    public string OrganicMatterType { get; set; } = string.Empty;

    [BsonElement("Organic Matter Type Enumerator")]
    public int OrganicMatterTypeEnumerator { get; set; }
}