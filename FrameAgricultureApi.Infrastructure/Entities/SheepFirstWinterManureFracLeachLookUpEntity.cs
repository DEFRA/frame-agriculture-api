using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the Sheep_FirstWinterManureFracLeach collection.
/// </summary>
[BsonCollection("Sheep_FirstWinterManureFracLeach")]
public class SheepFirstWinterManureFracLeachLookUpEntity : MongoDocument
{
    [BsonElement("AAR")]
    public double AverageAnnualRainfall { get; set; }

    [BsonElement("CELL")]
    public int Cell { get; set; }

    [BsonElement("CLIMATEREGION")]
    public int ClimateRegion { get; set; }

    [BsonElement("FIRST_YEAR_NITROGEN_LOSS_PERCENT")]
    public double FirstYearNitrogenLossPercent { get; set; }

    [BsonElement("SOILTYPE")]
    public int SoilType { get; set; }
}