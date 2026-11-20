using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the YardMitigation collection.
/// </summary>
[BsonCollection("YardMitigation")]
public class YardMitigationLookUpEntity : MongoDocument
{
    [BsonElement("Ammonia_RF")]
    public int NH3_RF { get; set; }

    [BsonElement("Component Enum")]
    public int ComponentEnum { get; set; }

    [BsonElement("Leaching_RF")]
    public int LeachingRF { get; set; }

    [BsonElement("Methane_RF")]
    public int CH4_RF { get; set; }

    [BsonElement("Method_ID")]
    public int MethodID { get; set; }

    [BsonElement("Mineralisation_RF")]
    public int MineralisationRF { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("NitrousOxide_RF")]
    public int N2O_RF { get; set; }

    [BsonElement("UID")]
    public int UID { get; set; }
}