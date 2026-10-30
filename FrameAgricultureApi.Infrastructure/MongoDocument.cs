using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure;

public abstract class MongoDocument : IMongoDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt => Id.CreationTime;
}