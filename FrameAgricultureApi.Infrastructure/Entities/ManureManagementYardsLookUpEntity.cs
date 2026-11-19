using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the ManureManagement_Yards collection.
/// </summary>
[BsonCollection("ManureManagement_Yards")]
public class ManureManagementYardsLookUpEntity : MongoDocument
{
    [BsonElement("AnimalType")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    [BsonElement("EF4")]
    public double EF4 { get; set; }

    [BsonElement("EF5")]
    public double EF5 { get; set; }

    [BsonElement("MCF")]
    public int MCF { get; set; }

    [BsonElement("N2O_N_EmissionFactor")]
    public int N2ONEmissionFactor { get; set; }

    [BsonElement("N2_Ratio")]
    public int N2Ratio { get; set; }

    [BsonElement("NH3_N_EmissionFactor")]
    public int NH3NEmissionFactor { get; set; }

    [BsonElement("NO_N_Ratio")]
    public double NONRatio { get; set; }

    [BsonElement("ScrapingEfficiency")]
    public int ScrapingEfficiency { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("SectorID")]
    public int SectorID { get; set; }

    [BsonElement("YardType")]
    public string YardType { get; set; } = string.Empty;
}