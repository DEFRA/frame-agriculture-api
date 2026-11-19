using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the ManureManagement_Storage collection.
/// </summary>
[BsonCollection("ManureManagement_Storage")]
public class ManureManagementStorageLookUpEntity : MongoDocument
{
    [BsonElement("AnimalType")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    [BsonElement("CH4_CF")]
    public double CH4CF { get; set; }

    [BsonElement("Column8")]
    public int Column8 { get; set; }

    [BsonElement("FractionLeach")]
    public double FractionLeach { get; set; }

    [BsonElement("ManureType")]
    public string ManureType { get; set; } = string.Empty;

    [BsonElement("MineralisationFactor")]
    public int MineralisationFactor { get; set; }

    [BsonElement("N2O-N_EmissionFactor")]
    public double N2ONEmissionFactor { get; set; }

    [BsonElement("N2_N_Ratio")]
    public int N2NRatio { get; set; }

    [BsonElement("NH3_N_EmissionFactor")]
    public double NH3NEmissionFactor { get; set; }

    [BsonElement("NO_N_Ratio")]
    public double NONRatio { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("SectorId")]
    public int SectorId { get; set; }

    [BsonElement("StorageId")]
    public int StorageId { get; set; }

    [BsonElement("StorageSystem")]
    public string StorageSystem { get; set; } = string.Empty;
}