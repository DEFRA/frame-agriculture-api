using System.Text.Json;
using FrameAgricultureApi.Infrastructure.Entities;
using FrameAgricultureApi.Libraries.CoverCrops;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace FrameAgricultureApi.Infrastructure;

public class MongoDbSeeder(IMongoDatabase database)
{
    public async Task SeedAsync()
    {
        await DeleteCollectionDataAsync<CoverCropLookUpEntity>("CoverCrops");
        await DeleteCollectionDataAsync<CropNonGhgLookUpEntity>("Crop_NonGHG");
        await DeleteCollectionDataAsync<CropResidueLookUpEntity>("Crop_Residue");
        await DeleteCollectionDataAsync<FertiliserRatioEfsLookUpEntity>("Fertiliser_RatioEFs");

        await SeedCoverCropsAsync();
        await SeedCollectionAsync<CropNonGhgLookUpEntity>("CropNonGHG.json", "Crop_NonGHG");
        await SeedCollectionAsync<CropResidueLookUpEntity>("CropResidue.json", "Crop_Residue");
        await SeedCollectionAsync<FertiliserRatioEfsLookUpEntity>("FertiliserRatioEFs.json", "Fertiliser_RatioEFs");
    }

    private async Task DeleteCollectionDataAsync<TDocument>(string collectionName)
        where TDocument : MongoDocument
    {
        var collection = database.GetCollection<TDocument>(collectionName);
        await collection.DeleteManyAsync(Builders<TDocument>.Filter.Empty);
    }

    private async Task SeedCoverCropsAsync()
    {
        var collection = database.GetCollection<CoverCropLookUpEntity>("CoverCrops");
        using var stream = OpenSeedData("CoverCrops.json");
        var seedData = await JsonSerializer.DeserializeAsync<List<CoverCropSeedData>>(stream)
            ?? throw new InvalidOperationException("CoverCrops seed data must be a JSON array.");

        foreach (var seed in seedData)
        {
            var filter = Builders<CoverCropLookUpEntity>.Filter
                .Eq(x => x.Id, ObjectId.Parse(seed.Id));

            var update = Builders<CoverCropLookUpEntity>.Update
                .SetOnInsert(x => x.CoverCropType, seed.CoverCropType)
                .SetOnInsert(x => x.Item, seed.Item)
                .SetOnInsert(x => x.Value, seed.Value);

            await collection.UpdateOneAsync(
                filter,
                update,
                new UpdateOptions { IsUpsert = true }
            );
        }
    }

    private async Task SeedCollectionAsync<TDocument>(string fileName, string collectionName)
        where TDocument : MongoDocument
    {
        using var stream = OpenSeedData(fileName);
        using var reader = new StreamReader(stream);
        var documents = BsonSerializer.Deserialize<BsonArray>(await reader.ReadToEndAsync());
        var classMap = BsonClassMap.LookupClassMap(typeof(TDocument));
        var collection = database.GetCollection<TDocument>(collectionName);

        foreach (var document in documents)
        {
            var mappedDocument = new BsonDocument();
            foreach (var element in document.AsBsonDocument)
            {
                var memberMap = classMap.AllMemberMaps.FirstOrDefault(member =>
                    member.ElementName == element.Name || member.MemberName == element.Name)
                    ?? throw new InvalidOperationException($"Unknown field '{element.Name}' in {fileName}.");
                mappedDocument.Add(memberMap.ElementName, element.Value);
            }

            var entity = BsonSerializer.Deserialize<TDocument>(mappedDocument);
            if (entity.Id == ObjectId.Empty)
            {
                throw new InvalidOperationException($"Each record in {fileName} must have a non-empty _id.");
            }

            var values = entity.ToBsonDocument();
            values.Remove("_id");
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, entity.Id);
            var update = new BsonDocumentUpdateDefinition<TDocument>(new BsonDocument("$setOnInsert", values));

            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }

    private static Stream OpenSeedData(string fileName)
    {
        return typeof(MongoDbSeeder).Assembly.GetManifestResourceStream(
            $"FrameAgricultureApi.Infrastructure.SeedData.{fileName}")
            ?? throw new InvalidOperationException($"Seed data resource '{fileName}' was not found.");
    }

    private sealed record CoverCropSeedData(string Id, CoverCropType CoverCropType, string Item, double Value);
}