using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the OutdoorExcretion collection.
/// </summary>
[BsonCollection("OutdoorExcretion")]
public class OutdoorExcretionLookUpEntity : MongoDocument
{
    [BsonElement("AnimalType")]
    public string AnimalType { get; set; } = string.Empty;

    [BsonElement("AnimalTypeId")]
    public int AnimalTypeId { get; set; }

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("Dung N2ON EF")]
    public BsonValue Dung_N2O_N_EF { get; set; } = BsonNull.Value;

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("Dung NH3N EF")]
    public BsonValue Dung_NH3_N_EF { get; set; } = BsonNull.Value;

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("Excreta N2ON EF")]
    public BsonValue Excreta_N2O_N_EF { get; set; } = BsonNull.Value;

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("Excreta NH3N EF")]
    public BsonValue Excreta_NH3_N_EF { get; set; } = BsonNull.Value;

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("FracLeach")]
    public BsonValue FracLeach { get; set; } = BsonNull.Value;

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("FracLeach Uncertainty")]
    public BsonValue FracLeachUncertainty { get; set; } = BsonNull.Value;

    [BsonElement("Methane Conversion Factor")]
    public int CH4_ConversionFactor { get; set; }

    [BsonElement("N2N Ratio")]
    public int N2_N_Ratio { get; set; }

    [BsonElement("NON ratio")]
    public double NO_N_Ratio { get; set; }

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("Nitrogen Coefficient For Free Range Poultry")]
    public BsonValue N_CoefficientForFreeRangePoultry { get; set; } = BsonNull.Value;

    [BsonElement("Sector")]
    public string Sector { get; set; } = string.Empty;

    [BsonElement("SectorId")]
    public int SectorId { get; set; }

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("Urine N2ON EF")]
    public BsonValue Urine_N2O_N_EF { get; set; } = BsonNull.Value;

    /// <summary>Stored as either numeric or text values in this collection.</summary>
    [BsonElement("Urine NH3N EF")]
    public BsonValue Urine_NH3_N_EF { get; set; } = BsonNull.Value;
}