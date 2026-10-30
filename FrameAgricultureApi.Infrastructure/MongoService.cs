using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FrameAgricultureApi.Infrastructure;

public class MongoService<TDocument> : IMongoService<TDocument>
    where TDocument : IMongoDocument
{

    private readonly IMongoDatabase database;
    private readonly IMongoCollection<TDocument> collection;
    private readonly ILogger<TDocument> logger;

    public MongoService(IOptions<DatabaseConfiguration> databaseConfiguration, ILogger<TDocument> logger)
    {
        var client = new MongoClient(databaseConfiguration.Value.ConnectionString);
        database = client.GetDatabase(databaseConfiguration.Value.DatabaseName);
        collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    private protected string? GetCollectionName(Type documentType)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
        .FirstOrDefault())?.CollectionName;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }

    public virtual IQueryable<TDocument> AsQueryable() => collection.AsQueryable();

    public virtual IEnumerable<TDocument> FilterBy(
        FilterDefinition<TDocument> filterExpression)
    {
        return collection.Find(filterExpression).ToEnumerable();
    }


    public virtual async Task<IEnumerable<TDocument>> FilterByAsync(FilterDefinition<TDocument> filterExpression)
    {
        return await collection.Find(filterExpression).ToListAsync();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        FilterDefinition<TDocument> filterExpression,
        ProjectionDefinition<TDocument, TProjected> projectionExpression)
    {
        return collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual async Task<IEnumerable<TProjected>> FilterByAsync<TProjected>(
        FilterDefinition<TDocument> filterExpression,
        ProjectionDefinition<TDocument, TProjected> projectionExpression)
    {
        return await collection.Find(filterExpression).Project(projectionExpression).ToListAsync();
    }

    public virtual TDocument FindOne(FilterDefinition<TDocument> filterExpression)
    {
        return collection.Find(filterExpression).FirstOrDefault();
    }

    public virtual async Task<TDocument> FindOneAsync(FilterDefinition<TDocument> filterExpression)
    {
        return await collection.Find(filterExpression).FirstOrDefaultAsync();
    }

    public virtual TDocument FindById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        return collection.Find(filter).SingleOrDefault();
    }

    public virtual Task<TDocument> FindByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return collection.Find(filter).SingleOrDefaultAsync();
        });
    }

    public virtual void InsertOne(TDocument document) => collection.InsertOne(document);

    public virtual async Task InsertOneAsync(TDocument document) => await collection.InsertOneAsync(document);

    public void InsertMany(ICollection<TDocument> documents) => collection.InsertMany(documents);

    public virtual async Task InsertManyAsync(ICollection<TDocument> documents) => await collection.InsertManyAsync(documents);

    public void ReplaceOne(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        collection.FindOneAndReplace(filter, document);
    }

    public virtual async Task ReplaceOneAsync(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        await collection.FindOneAndReplaceAsync(filter, document);
    }

    public void UpdateMany(UpdateDefinition<TDocument> updateExpression, FilterDefinition<TDocument> filterExpression)
    {
        collection.UpdateMany(filterExpression, updateExpression);
    }

    public virtual async Task UpdateManyAsync(UpdateDefinition<TDocument> updateExpression, FilterDefinition<TDocument> filterExpression)
    {
        await collection.UpdateManyAsync(filterExpression, updateExpression);
    }

    public void UpdateOne(UpdateDefinition<TDocument> updateExpression, TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        collection.UpdateOne(filter, updateExpression);
    }

    public virtual async Task UpdateOneAsync(UpdateDefinition<TDocument> updateExpression, TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        await collection.UpdateOneAsync(filter, updateExpression);
    }

    public void UpdateOneById(UpdateDefinition<TDocument> updateExpression, string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        collection.UpdateOne(filter, updateExpression);
    }

    public virtual async Task UpdateOneByIdAsync(UpdateDefinition<TDocument> updateExpression, string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        await collection.UpdateOneAsync(filter, updateExpression);
    }

    public void UpsertOne(FilterDefinition<TDocument> filterExpression, TDocument document)
    {
        var options = new ReplaceOptions { IsUpsert = true };
        collection.ReplaceOne(filterExpression, document, options);
    }

    public virtual async Task UpsertOneAsync(FilterDefinition<TDocument> filterExpression, TDocument document)
    {
        var options = new ReplaceOptions { IsUpsert = true };
        await collection.ReplaceOneAsync(filterExpression, document, options);
    }

    public void DeleteOne(FilterDefinition<TDocument> filterExpression) => collection.FindOneAndDelete(filterExpression);

    public async Task DeleteOneAsync(FilterDefinition<TDocument> filterExpression) => await collection.FindOneAndDeleteAsync(filterExpression);

    public void DeleteById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        collection.FindOneAndDelete(filter);
    }

    public async Task DeleteByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        await collection.FindOneAndDeleteAsync(filter);
    }

    public void DeleteMany(FilterDefinition<TDocument> filterExpression) => collection.DeleteMany(filterExpression);

    public async Task DeleteManyAsync(FilterDefinition<TDocument> filterExpression) => await collection.DeleteManyAsync(filterExpression);

    public async Task<List<BsonDocument>> AggregateAsync(List<BsonDocument> pipeline)
    {
        List<BsonDocument> Results = await collection.Aggregate<BsonDocument>(pipeline).ToListAsync();

        return Results;
    }

    public double TotalDocumentCount()
    {
        var filter = Builders<TDocument>.Filter.Where(t => true);
        var sizeOfCollection = collection.CountDocuments(filter);

        return sizeOfCollection;
    }

    public IAggregateFluent<TDocument> AggregateFluent { get => collection.Aggregate(); }
}