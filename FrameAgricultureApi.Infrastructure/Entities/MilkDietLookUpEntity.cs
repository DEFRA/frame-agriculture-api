using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the MilkDiet collection.
/// </summary>
[BsonCollection("MilkDiet")]
public class MilkDietLookUpEntity : MongoDocument
{
    [BsonElement("CATTLEBREED")]
    public int CattleBreed { get; set; }

    [BsonElement("CATTLETYPE")]
    public int CattleType { get; set; }

    [BsonElement("COUNTRY")]
    public int Country { get; set; }

    [BsonElement("Key")]
    public string Key { get; set; } = string.Empty;

    [BsonElement("MILKYIELD")]
    public double MilkYield { get; set; }

    [BsonElement("Milk Protein")]
    public double MilkProtein { get; set; }

    [BsonElement("MilkFat")]
    public double MilkFat { get; set; }
}