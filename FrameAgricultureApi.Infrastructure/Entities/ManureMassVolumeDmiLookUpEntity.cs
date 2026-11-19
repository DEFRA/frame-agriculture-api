using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the MMV_DMI collection.
/// </summary>
[BsonCollection("MMV_DMI")]
public class ManureMassVolumeDmiLookUpEntity : MongoDocument
{
    [BsonElement("Animal Type")]
    public int AnimalType { get; set; }

    [BsonElement("Animal type Name")]
    public string AnimalTypeName { get; set; } = string.Empty;

    [BsonElement("DMI")]
    public double DMI { get; set; }

    [BsonElement("Location")]
    public string Location { get; set; } = string.Empty;

    [BsonElement("LocationID")]
    public int LocationID { get; set; }

    [BsonElement("Mass Coeff")]
    public double MassCoeff { get; set; }

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("Sector Code")]
    public int SectorCode { get; set; }

    [BsonElement("Volume Coefficient")]
    public double VolumeCoefficient { get; set; }
}