using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the MMV_MMChain collection.
/// </summary>
[BsonCollection("MMV_MMChain")]
public class ManureMassVolumeChainLookUpEntity : MongoDocument
{
    [BsonElement("Animal Type code")]
    public int AnimalTypeCode { get; set; }

    [BsonElement("Animal type")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("Housing mass arriving")]
    public double HousingMassArriving { get; set; }

    [BsonElement("Organic Matter type")]
    public string OrganicMatterType { get; set; } = string.Empty;

    [BsonElement("Organic mattter source type code")]
    public int OrganicMatterSourceTypeCode { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("Sector code")]
    public int SectorCode { get; set; }

    [BsonElement("Spreading mass arirving")]
    public double SpreadingMassArriving { get; set; }

    [BsonElement("housing mass leaving")]
    public double HousingMassLeaving { get; set; }

    [BsonElement("housing violume leaving")]
    public double HousingVolumeLeaving { get; set; }

    [BsonElement("housing volume arriving")]
    public double HousingVolumeArriving { get; set; }

    [BsonElement("spreading mass leaving")]
    public double SpreadingMassLeaving { get; set; }

    [BsonElement("spreading volume arriving")]
    public double SpreadingVolumeArriving { get; set; }

    [BsonElement("spreading volume leaving")]
    public double SpreadingVolumeLeaving { get; set; }

    [BsonElement("storage mas leaving")]
    public double StorageMassLeaving { get; set; }

    [BsonElement("storage mass arriving")]
    public double StorageMassArriving { get; set; }

    [BsonElement("storage volume arriving")]
    public double StorageVolumeArriving { get; set; }

    [BsonElement("storage volume leaving")]
    public double StorageVolumeLeaving { get; set; }
}