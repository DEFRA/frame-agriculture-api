using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the BeefCattleLiveWeightParameters collection.
/// </summary>
[BsonCollection("BeefCattleLiveWeightParameters")]
public class BeefCattleLiveWeightParametersLookUpEntity : MongoDocument
{
    [BsonElement("BeefCattleBreed")]
    public int BeefCattleBreed { get; set; }

    [BsonElement("BeefCattleRole")]
    public int BeefCattleRole { get; set; }

    [BsonElement("K")]
    public double K { get; set; }

    [BsonElement("c")]
    public double C { get; set; }
}