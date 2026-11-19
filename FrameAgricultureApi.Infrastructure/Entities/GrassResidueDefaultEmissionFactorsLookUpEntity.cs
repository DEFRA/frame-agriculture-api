using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the GrassResidue_DefaultEmissionFactors collection.
/// </summary>
[BsonCollection("GrassResidue_DefaultEmissionFactors")]
public class GrassResidueDefaultEmissionFactorsLookUpEntity : MongoDocument
{
    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("Value (%)")]
    public double ValuePercentage { get; set; }
}