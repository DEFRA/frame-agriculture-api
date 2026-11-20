using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the ManureManagement_Housing collection.
/// </summary>
[BsonCollection("ManureManagement_Housing")]
public class ManureManagementHousingLookUpEntity : MongoDocument
{
    [BsonElement("AnimalType")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    [BsonElement("CH4 CF")]
    public double CH4_CF { get; set; }

    [BsonElement("HousingIndex")]
    public int HousingIndex { get; set; }

    [BsonElement("HousingSystem")]
    public string HousingSystem { get; set; } = string.Empty;

    [BsonElement("Immobilisation Factor")]
    public int ImmobilisationFactor { get; set; }

    [BsonElement("Manure Type")]
    public string ManureType { get; set; } = string.Empty;

    [BsonElement("Manure type ID")]
    public int ManureTypeID { get; set; }

    [BsonElement("N2-N Ratio")]
    public int N2_N_Ratio { get; set; }

    [BsonElement("N2O-N EF")]
    public double N2O_N_EF { get; set; }

    [BsonElement("NH3_N EF")]
    public double NH3_N_EF { get; set; }

    [BsonElement("NO-N Ratio")]
    public double NO_N_Ratio { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("SectorId")]
    public int SectorId { get; set; }
}