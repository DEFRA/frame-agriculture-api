using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the PigManureSpreading collection.
/// </summary>
[BsonCollection("PigManureSpreading")]
public class PigManureSpreadingLookUpEntity : MongoDocument
{
    [BsonElement("FractionLeach")]
    public int FractionLeach { get; set; }

    [BsonElement("LandUse")]
    public string LandUse { get; set; } = string.Empty;

    [BsonElement("LandUseId")]
    public int LandUseId { get; set; }

    [BsonElement("MatterType")]
    public string MatterType { get; set; } = string.Empty;

    [BsonElement("MatterTypeId")]
    public int MatterTypeId { get; set; }

    [BsonElement("N2O_N_EmissionFactor")]
    public double N2O_N_EmissionFactor { get; set; }

    [BsonElement("N2_N_Ratio")]
    public int N2_N_Ratio { get; set; }

    [BsonElement("NH3_N_EmissionFactor")]
    public double NH3_N_EmissionFactor { get; set; }

    [BsonElement("NO_N_Ratio")]
    public double NO_N_Ratio { get; set; }
}