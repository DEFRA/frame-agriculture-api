using System;
using FrameAgricultureApi.Libraries.CoverCrops;

namespace FrameAgricultureApi.Infrastructure.Entities;

[BsonCollection("CoverCrops")]
public class CoverCropLookUpEntity : MongoDocument
{
    public CoverCropType CoverCropType { get; set; }
    public string Item { get; set; }
    public double Value { get; set; }
}