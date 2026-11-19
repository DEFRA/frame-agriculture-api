using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the YardMitigation collection.
/// </summary>
[BsonCollection("YardMitigation")]
public class YardMitigationLookUpEntity : MongoDocument
{
    [BsonElement("Ammonia_RF")]
    public int AmmoniaRF { get; set; }

    [BsonElement("Component Enum")]
    public int ComponentEnum { get; set; }

    [BsonElement("Leaching_RF")]
    public int LeachingRF { get; set; }

    [BsonElement("Methane_RF")]
    public int MethaneRF { get; set; }

    [BsonElement("Method_ID")]
    public int MethodID { get; set; }

    [BsonElement("Mineralisation_RF")]
    public int MineralisationRF { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("NitrousOxide_RF")]
    public int NitrousOxideRF { get; set; }

    [BsonElement("UID")]
    public int UID { get; set; }
}