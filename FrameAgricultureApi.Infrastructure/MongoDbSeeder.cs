using FrameAgricultureApi.Infrastructure.Entities;
using FrameAgricultureApi.Libraries.CoverCrops;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FrameAgricultureApi.Infrastructure;

public class MongoDbSeeder(IMongoDatabase database)
{
    public async Task SeedAsync()
    {
        var collection = database.GetCollection<CoverCropLookUpEntity>("CoverCrops");

        List<CoverCropLookUpEntity> coverCropLookUpEntities = new()
        {
            new()
            {
                Id = ObjectId.Parse("6a9f0cd7a6c94fe61cdcfb48"),
                CoverCropType = CoverCropType.All,
                Item = "N return",
                Value = 77
            },
            new()
            {
                CoverCropType = CoverCropType.All,
                Item = "Frac Leach Reduction",
                Value = 45
            }
        };

        foreach (var coverCropLookUpEntity in coverCropLookUpEntities)
        {
            var filter = Builders<CoverCropLookUpEntity>.Filter
                .Eq(x => x.Id, coverCropLookUpEntity.Id);

            var update = Builders<CoverCropLookUpEntity>.Update
                .SetOnInsert(x => x.CoverCropType, coverCropLookUpEntity.CoverCropType)
                .SetOnInsert(x => x.Item, coverCropLookUpEntity.Item)
                .SetOnInsert(x => x.Value, coverCropLookUpEntity.Value);

            await collection.UpdateOneAsync(
                filter,
                update,
                new UpdateOptions { IsUpsert = true }
            );
        }
    }
}