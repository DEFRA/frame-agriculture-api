using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using FrameAgricultureApi.Utils.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NSubstitute;

namespace FrameAgricultureApi.Test.ComponentTests;

public class MongoDbSeederTests
{
    [Fact]
    public async Task SeedAsync_ClearsAllSeededCollectionsBeforeLoadingDataWithStableIds()
    {
        MongoConventions.Register();
        var database = Substitute.For<IMongoDatabase>();
        var operations = new List<string>();
        var coverCrops = RegisterCollection<CoverCropLookUpEntity>(database, "CoverCrops", operations);
        var nonGhg = RegisterCollection<CropNonGhgLookUpEntity>(database, "Crop_NonGHG", operations);
        var residues = RegisterCollection<CropResidueLookUpEntity>(database, "Crop_Residue", operations);
        var fertilisers = RegisterCollection<FertiliserRatioEfsLookUpEntity>(database, "Fertiliser_RatioEFs", operations);
        var seeder = new MongoDbSeeder(database);

        await seeder.SeedAsync();

        AssertDeleteBeforeSeed(operations);
        Assert.Equal(2, GetSeedUpdates(coverCrops).Count);
        var nonGhgUpdates = GetSeedUpdates(nonGhg);
        Assert.Equal(48, nonGhgUpdates.Count);
        Assert.Equal(ObjectId.Parse("6ae4ca7efe2f84e05c3ec6b2"), nonGhgUpdates[0].Id);
        Assert.Equal("Oats", nonGhgUpdates[0].Values["cropName"].AsString);
        Assert.Equal(1, nonGhgUpdates[0].Values["cropTypeId"].AsInt32);
        Assert.Equal(0.86, nonGhgUpdates[0].Values["EF_NMVOC"].AsDouble);
        Assert.Equal(0.0125, nonGhgUpdates[0].Values["EF_PM2_5_Cleaning"].AsDouble);

        var residueUpdates = GetSeedUpdates(residues);
        Assert.Equal(48, residueUpdates.Count);
        Assert.Equal(50, residueUpdates[0].Values["AboveGroundResiduesRemainingAfterBalingPercentage"].AsDouble);
        Assert.Equal(38.8, residueUpdates[0].Values["Fate2Incorporate"].AsDouble);
        Assert.True(residueUpdates[1].Values["rAFate1"].IsBsonNull);
        Assert.Contains(residueUpdates, update => update.Values["cropYieldNContent"].IsBsonNull);

        var fertiliserUpdates = GetSeedUpdates(fertilisers);
        Assert.Equal(6, fertiliserUpdates.Count);
        Assert.Equal("Ratio_NON", fertiliserUpdates[0].Values["parameter"].AsString);
        Assert.Equal(0.4, fertiliserUpdates[0].Values["value"].AsDouble);
        Assert.Equal(3, fertiliserUpdates[1].Values["value"].AsDouble);

        operations.Clear();
        await seeder.SeedAsync();

        AssertDeleteBeforeSeed(operations);
        AssertRepeatedUpdates(coverCrops, 2);
        AssertRepeatedUpdates(nonGhg, 48);
        AssertRepeatedUpdates(residues, 48);
        AssertRepeatedUpdates(fertilisers, 6);
    }

    private static IMongoCollection<TDocument> RegisterCollection<TDocument>(
        IMongoDatabase database, string name, List<string> operations)
    {
        var collection = Substitute.For<IMongoCollection<TDocument>>();
        database.GetCollection<TDocument>(name).Returns(collection);
        collection.DeleteManyAsync(Arg.Any<FilterDefinition<TDocument>>()).Returns(call =>
        {
            var registry = BsonSerializer.SerializerRegistry;
            var filter = call.Arg<FilterDefinition<TDocument>>().Render(
                new RenderArgs<TDocument>(registry.GetSerializer<TDocument>(), registry));
            Assert.Empty(filter);
            operations.Add($"delete:{name}");
            return Task.FromResult<DeleteResult>(new DeleteResult.Acknowledged(0));
        });
        collection.UpdateOneAsync(Arg.Any<FilterDefinition<TDocument>>(),
            Arg.Any<UpdateDefinition<TDocument>>(), Arg.Any<UpdateOptions>()).Returns(_ =>
        {
            operations.Add($"seed:{name}");
            return Task.FromResult<UpdateResult>(new UpdateResult.Acknowledged(0, 0, null));
        });
        return collection;
    }

    private static void AssertDeleteBeforeSeed(List<string> operations)
    {
        Assert.Equal(new[]
        {
            "delete:CoverCrops", "delete:Crop_NonGHG", "delete:Crop_Residue", "delete:Fertiliser_RatioEFs"
        }, operations.Take(4));
        Assert.Equal(108, operations.Count);
        Assert.All(operations.Skip(4), operation => Assert.StartsWith("seed:", operation));
    }

    private static List<(ObjectId Id, BsonDocument Values)> GetSeedUpdates<TDocument>(IMongoCollection<TDocument> collection)
    {
        var registry = BsonSerializer.SerializerRegistry;
        var renderArgs = new RenderArgs<TDocument>(registry.GetSerializer<TDocument>(), registry);
        var updates = new List<(ObjectId Id, BsonDocument Values)>();
        foreach (var call in collection.ReceivedCalls().Where(call => call.GetMethodInfo().Name == "UpdateOneAsync"))
        {
            var arguments = call.GetArguments();
            var filter = ((FilterDefinition<TDocument>)arguments[0]!).Render(renderArgs);
            var update = ((UpdateDefinition<TDocument>)arguments[1]!).Render(renderArgs).AsBsonDocument;
            Assert.True(((UpdateOptions)arguments[2]!).IsUpsert);
            Assert.Equal("$setOnInsert", Assert.Single(update.Elements).Name);
            Assert.False(update["$setOnInsert"].AsBsonDocument.Contains("_id"));
            updates.Add((filter["_id"].AsObjectId, update["$setOnInsert"].AsBsonDocument));
        }
        return updates;
    }

    private static void AssertRepeatedUpdates<TDocument>(IMongoCollection<TDocument> collection, int count)
    {
        var updates = GetSeedUpdates(collection);
        Assert.Equal(count * 2, updates.Count);
        Assert.Equal(count, updates.Select(update => update.Id).Distinct().Count());
        for (var index = 0; index < count; index++)
        {
            Assert.Equal(updates[index], updates[index + count]);
        }
    }
}