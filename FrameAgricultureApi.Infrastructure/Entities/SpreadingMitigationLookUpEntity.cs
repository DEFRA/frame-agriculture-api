using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the SpreadingMitigation collection.
/// </summary>
[BsonCollection("SpreadingMitigation")]
public class SpreadingMitigationLookUpEntity : MongoDocument
{
    [BsonElement("Ammonia_RF")]
    public int AmmoniaRF { get; set; }

    [BsonElement("Animal Source Enum")]
    public int AnimalSourceEnum { get; set; }

    [BsonElement("Component Enum")]
    public int ComponentEnum { get; set; }

    [BsonElement("FLEA Enumerator")]
    public int FLEAEnumerator { get; set; }

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

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("State enum")]
    public int StateEnum { get; set; }

    [BsonElement("UID")]
    public int UID { get; set; }
}