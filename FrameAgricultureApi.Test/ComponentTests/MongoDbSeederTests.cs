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
    public async Task SeedAsync_ClearsAllSeededCollectionsBeforeLoadingDataWithoutExplicitIds()
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
        var coverCropDocuments = GetSeedInserts(coverCrops);
        Assert.Equal(2, coverCropDocuments.Count);
        Assert.Equal(0, coverCropDocuments[0]["coverCropType"].AsInt32);
        Assert.Equal("N return", coverCropDocuments[0]["item"].AsString);
        Assert.Equal(77, coverCropDocuments[0]["value"].AsDouble);
        Assert.Equal(45, coverCropDocuments[1]["value"].AsDouble);
        var nonGhgDocuments = GetSeedInserts(nonGhg);
        Assert.Equal(48, nonGhgDocuments.Count);
        Assert.Equal("Oats", nonGhgDocuments[0]["cropName"].AsString);
        Assert.Equal(1, nonGhgDocuments[0]["cropTypeId"].AsInt32);
        Assert.Equal(0.86, nonGhgDocuments[0]["EF_NMVOC"].AsDouble);
        Assert.Equal(0.0125, nonGhgDocuments[0]["EF_PM2_5_Cleaning"].AsDouble);

        var residueDocuments = GetSeedInserts(residues);
        Assert.Equal(48, residueDocuments.Count);
        Assert.Equal(50, residueDocuments[0]["AboveGroundResiduesRemainingAfterBalingPercentage"].AsDouble);
        Assert.Equal(38.8, residueDocuments[0]["Fate2Incorporate"].AsDouble);
        Assert.True(residueDocuments[1]["rAFate1"].IsBsonNull);
        Assert.Contains(residueDocuments, document => document["cropYieldNContent"].IsBsonNull);

        var fertiliserDocuments = GetSeedInserts(fertilisers);
        Assert.Equal(6, fertiliserDocuments.Count);
        Assert.Equal("Ratio_NON", fertiliserDocuments[0]["parameter"].AsString);
        Assert.Equal(0.4, fertiliserDocuments[0]["value"].AsDouble);
        Assert.Equal(3, fertiliserDocuments[1]["value"].AsDouble);

        operations.Clear();
        await seeder.SeedAsync();

        AssertDeleteBeforeSeed(operations);
        AssertRepeatedInserts(coverCrops, 2);
        AssertRepeatedInserts(nonGhg, 48);
        AssertRepeatedInserts(residues, 48);
        AssertRepeatedInserts(fertilisers, 6);
    }

    private static IMongoCollection<TDocument> RegisterCollection<TDocument>(
        IMongoDatabase database, string name, List<string> operations)
        where TDocument : MongoDocument
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
        collection.InsertOneAsync(Arg.Any<TDocument>(), Arg.Any<InsertOneOptions>(),
            Arg.Any<CancellationToken>()).Returns(call =>
        {
            var entity = call.Arg<TDocument>();
            // The real MongoDB driver assigns the ID when inserting this document.
            Assert.Equal(ObjectId.Empty, entity.Id);
            operations.Add($"seed:{name}");
            return Task.CompletedTask;
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

    private static List<BsonDocument> GetSeedInserts<TDocument>(IMongoCollection<TDocument> collection)
        where TDocument : MongoDocument
    {
        return collection.ReceivedCalls()
            .Where(call => call.GetMethodInfo().Name == "InsertOneAsync")
            .Select(call => ((TDocument)call.GetArguments()[0]!).ToBsonDocument())
            .ToList();
    }

    private static void AssertRepeatedInserts<TDocument>(IMongoCollection<TDocument> collection, int count)
        where TDocument : MongoDocument
    {
        var documents = GetSeedInserts(collection);
        Assert.Equal(count * 2, documents.Count);
        for (var index = 0; index < count; index++)
        {
            Assert.Equal(documents[index], documents[index + count]);
        }
    }
}