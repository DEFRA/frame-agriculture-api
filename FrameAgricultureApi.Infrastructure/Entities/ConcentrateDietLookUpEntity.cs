using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the ConcentrateDiet collection.
/// </summary>
[BsonCollection("ConcentrateDiet")]
public class ConcentrateDietLookUpEntity : MongoDocument
{
    [BsonElement("CATTLEBREED")]
    public int CattleBreed { get; set; }

    [BsonElement("CATTLETYPE")]
    public int CattleType { get; set; }

    [BsonElement("COUNTRY")]
    public int Country { get; set; }

    [BsonElement("CP")]
    public double CP { get; set; }

    [BsonElement("ConcentrateUse")]
    public double ConcentrateUse { get; set; }

    [BsonElement("DMC")]
    public int DMC { get; set; }

    [BsonElement("DMD")]
    public double DMD { get; set; }

    [BsonElement("GE")]
    public double GE { get; set; }

    [BsonElement("Key")]
    public string Key { get; set; } = string.Empty;

    [BsonElement("ME")]
    public double ME { get; set; }

    [BsonElement("YEAR")]
    public int Year { get; set; }
}