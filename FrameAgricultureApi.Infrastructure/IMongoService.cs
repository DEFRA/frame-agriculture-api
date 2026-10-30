using MongoDB.Driver;
using MongoDB.Bson;

namespace FrameAgricultureApi.Infrastructure;


public interface IMongoService<TDocument> where TDocument : IMongoDocument
{
    IQueryable<TDocument> AsQueryable();
    IEnumerable<TDocument> FilterBy(
        FilterDefinition<TDocument> filterExpression);
    Task<IEnumerable<TDocument>> FilterByAsync(
        FilterDefinition<TDocument> filterExpression);
    IEnumerable<TProjected> FilterBy<TProjected>(
        FilterDefinition<TDocument> filterExpression,
        ProjectionDefinition<TDocument, TProjected> projectionExpression);
    Task<IEnumerable<TProjected>> FilterByAsync<TProjected>(
        FilterDefinition<TDocument> filterExpression,
        ProjectionDefinition<TDocument, TProjected> projectionExpression);
    TDocument FindOne(FilterDefinition<TDocument> filterExpression);
    Task<TDocument> FindOneAsync(FilterDefinition<TDocument> filterExpression);
    TDocument FindById(string id);
    Task<TDocument> FindByIdAsync(string id);
    void InsertOne(TDocument document);
    Task InsertOneAsync(TDocument document);
    void InsertMany(ICollection<TDocument> documents);
    Task InsertManyAsync(ICollection<TDocument> documents);
    void ReplaceOne(TDocument document);
    Task ReplaceOneAsync(TDocument document);
    void UpdateMany(UpdateDefinition<TDocument> updateExpression, FilterDefinition<TDocument> filterExpression);
    Task UpdateManyAsync(UpdateDefinition<TDocument> updateExpression, FilterDefinition<TDocument> filterExpression);
    void UpdateOne(UpdateDefinition<TDocument> updateExpression, TDocument document);
    Task UpdateOneAsync(UpdateDefinition<TDocument> updateExpression, TDocument document);
    void UpdateOneById(UpdateDefinition<TDocument> updateExpression, string id);
    Task UpdateOneByIdAsync(UpdateDefinition<TDocument> updateExpression, string id);
    void UpsertOne(FilterDefinition<TDocument> filterExpression, TDocument document);
    Task UpsertOneAsync(FilterDefinition<TDocument> filterExpression, TDocument document);
    void DeleteOne(FilterDefinition<TDocument> filterExpression);
    Task DeleteOneAsync(FilterDefinition<TDocument> filterExpression);
    void DeleteById(string id);
    Task DeleteByIdAsync(string id);
    void DeleteMany(FilterDefinition<TDocument> filterExpression);
    Task DeleteManyAsync(FilterDefinition<TDocument> filterExpression);

    Task<List<BsonDocument>> AggregateAsync(List<BsonDocument> pipeline);
    public IAggregateFluent<TDocument> AggregateFluent { get; }

}