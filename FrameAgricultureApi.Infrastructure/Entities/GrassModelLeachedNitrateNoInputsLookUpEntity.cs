using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the GrassModel_LeachedNitrate_NoInputs-Base collection.
/// </summary>
[BsonCollection("GrassModel_LeachedNitrate_NoInputs-Base")]
public class GrassModelLeachedNitrateNoInputsLookUpEntity : MongoDocument
{
    [BsonElement("C")]
    public double C { get; set; }

    [BsonElement("CLIMATEREGION")]
    public int ClimateRegion { get; set; }

    [BsonElement("CLOVERTYPE")]
    public int CloverType { get; set; }

    [BsonElement("COEFF_X")]
    public double CoefficientX { get; set; }

    [BsonElement("COEFF_X2")]
    public double CoefficientX2 { get; set; }

    [BsonElement("COEFF_X3")]
    public double CoefficientX3 { get; set; }

    [BsonElement("COEFF_X4")]
    public double CoefficientX4 { get; set; }

    [BsonElement("GRASSTYPE")]
    public int GrassType { get; set; }

    [BsonElement("GRASSUSETYPE")]
    public int GrassUseType { get; set; }

    [BsonElement("MANURINGTYPE")]
    public int ManuringType { get; set; }

    [BsonElement("SOILTEXTURETYPE")]
    public int SoilTextureType { get; set; }
}