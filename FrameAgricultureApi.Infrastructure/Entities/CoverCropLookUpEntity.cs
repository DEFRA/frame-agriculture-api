using System;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Infrastructure.Entities;

[BsonCollection("CoverCrops")]
public class CoverCropLookUpEntity : MongoDocument
{
    public CoverCropType CoverCropType { get; set; }
    public string Item { get; set; }
    public double Value { get; set; }
}