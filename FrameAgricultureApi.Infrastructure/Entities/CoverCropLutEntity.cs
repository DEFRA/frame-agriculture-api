using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the CoverCrop collection.
/// </summary>
[BsonCollection("CoverCrop")]
public class CoverCropLutEntity : MongoDocument
{
    [BsonElement("Cover Crop Type")]
    public string CoverCropTypeName { get; set; } = string.Empty;

    [BsonElement("Item")]
    public string Item { get; set; } = string.Empty;

    [BsonElement("Value")]
    public int Value { get; set; }
}