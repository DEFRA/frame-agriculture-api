using FrameAgricultureApi.Libraries.CoverCrops;
using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

[BsonCollection("Crop_NonGHG")]
public class CropNonGhgLookUpEntity : MongoDocument
{
    public string CropName { get; set; } = string.Empty;
    public CropType CropTypeId { get; set; }

    [BsonElement("EF_NMVOC")]
    public double EfNmvoc { get; set; }

    [BsonElement("EF_PM10_Cleaning")]
    public double EfPm10Cleaning { get; set; }

    [BsonElement("EF_PM10_Cultivation")]
    public double EfPm10Cultivation { get; set; }

    [BsonElement("EF_PM10_Drying")]
    public double EfPm10Drying { get; set; }

    [BsonElement("EF_PM10_Harvesting")]
    public double EfPm10Harvesting { get; set; }

    [BsonElement("EF_PM2_5_Cleaning")]
    public double EfPm2_5Cleaning { get; set; }

    [BsonElement("EF_PM2_5_Cultivation")]
    public double EfPm2_5Cultivation { get; set; }

    [BsonElement("EF_PM2_5_Drying")]
    public double EfPm2_5Drying { get; set; }

    [BsonElement("EF_PM2_5_Harvesting")]
    public double EfPm2_5Harvesting { get; set; }
}