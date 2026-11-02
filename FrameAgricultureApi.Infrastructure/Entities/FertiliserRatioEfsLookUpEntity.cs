namespace FrameAgricultureApi.Infrastructure.Entities;

[BsonCollection("Fertiliser_RatioEFs")]
public class FertiliserRatioEfsLookUpEntity : MongoDocument
{
    public string Parameter { get; set; } = string.Empty;
    public double Value { get; set; }
}