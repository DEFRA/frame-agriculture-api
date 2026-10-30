using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure;

public interface IMongoDocument
{
    [BsonId]
    [BsonRequired]
    ObjectId Id { get; set; }
    DateTime CreatedAt { get; }
}