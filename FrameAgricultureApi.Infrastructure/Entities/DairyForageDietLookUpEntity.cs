using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the DairyForageDiet collection.
/// </summary>
[BsonCollection("DairyForageDiet")]
public class DairyForageDietLookUpEntity : MongoDocument
{
    [BsonElement("CATTLETYPE")]
    public int CattleType { get; set; }

    [BsonElement("COUNTRY")]
    public int Country { get; set; }

    [BsonElement("CP")]
    public double CP { get; set; }

    [BsonElement("DMC")]
    public double DMC { get; set; }

    [BsonElement("DMD")]
    public double DMD { get; set; }

    [BsonElement("GE")]
    public double GE { get; set; }

    [BsonElement("MANAGEMENTREGIME")]
    public int ManagementRegime { get; set; }

    [BsonElement("ME")]
    public double ME { get; set; }

    [BsonElement("MONTH")]
    public int Month { get; set; }
}