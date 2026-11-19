using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the OrganicMatterSpreading collection.
/// </summary>
[BsonCollection("OrganicMatterSpreading")]
public class OrganicMatterSpreadingLookUpEntity : MongoDocument
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
    public int N2ONEmissionFactor { get; set; }

    [BsonElement("N2_N_Ratio")]
    public int N2NRatio { get; set; }

    [BsonElement("NH3_N_EmissionFactor")]
    public int NH3NEmissionFactor { get; set; }

    [BsonElement("NO_N_Ratio")]
    public double NONRatio { get; set; }

    [BsonElement("N_Content")]
    public double NContent { get; set; }

    [BsonElement("PercentageTAN")]
    public int PercentageTAN { get; set; }
}