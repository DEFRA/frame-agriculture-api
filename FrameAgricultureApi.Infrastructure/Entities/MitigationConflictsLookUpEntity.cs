using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the MitigationConflicts collection.
/// </summary>
[BsonCollection("MitigationConflicts")]
public class MitigationConflictsLookUpEntity : MongoDocument
{
    [BsonElement("1")]
    public string Method1 { get; set; } = string.Empty;

    [BsonElement("10")]
    public string Method10 { get; set; } = string.Empty;

    [BsonElement("11")]
    public string Method11 { get; set; } = string.Empty;

    [BsonElement("12")]
    public string Method12 { get; set; } = string.Empty;

    [BsonElement("13")]
    public string Method13 { get; set; } = string.Empty;

    [BsonElement("14")]
    public string Method14 { get; set; } = string.Empty;

    [BsonElement("15")]
    public string Method15 { get; set; } = string.Empty;

    [BsonElement("16")]
    public string Method16 { get; set; } = string.Empty;

    [BsonElement("17")]
    public string Method17 { get; set; } = string.Empty;

    [BsonElement("18")]
    public string Method18 { get; set; } = string.Empty;

    [BsonElement("19")]
    public string Method19 { get; set; } = string.Empty;

    [BsonElement("2")]
    public string Method2 { get; set; } = string.Empty;

    [BsonElement("20")]
    public string Method20 { get; set; } = string.Empty;

    [BsonElement("21")]
    public string Method21 { get; set; } = string.Empty;

    [BsonElement("22")]
    public string Method22 { get; set; } = string.Empty;

    [BsonElement("23")]
    public string Method23 { get; set; } = string.Empty;

    [BsonElement("24")]
    public string Method24 { get; set; } = string.Empty;

    [BsonElement("25")]
    public string Method25 { get; set; } = string.Empty;

    [BsonElement("26")]
    public string Method26 { get; set; } = string.Empty;

    [BsonElement("27")]
    public string Method27 { get; set; } = string.Empty;

    [BsonElement("3")]
    public string Method3 { get; set; } = string.Empty;

    [BsonElement("4")]
    public string Method4 { get; set; } = string.Empty;

    [BsonElement("5")]
    public string Method5 { get; set; } = string.Empty;

    [BsonElement("6")]
    public string Method6 { get; set; } = string.Empty;

    [BsonElement("7")]
    public string Method7 { get; set; } = string.Empty;

    [BsonElement("8")]
    public string Method8 { get; set; } = string.Empty;

    [BsonElement("9")]
    public string Method9 { get; set; } = string.Empty;

    [BsonElement("Column1")]
    public int Column1 { get; set; }
}