using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InventoryManagerWeb.Repositories;

public class MongoDbPackagingRepository :IPackagingsRepository
{
    private const string databaseName = "Inventory";
    private const string collectionName = "packaging";
    private readonly IMongoCollection<Packaging> _packagingCollection;
    private readonly FilterDefinitionBuilder<Packaging> _filterBuilder = Builders<Packaging>.Filter;

    public MongoDbPackagingRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        _packagingCollection = database.GetCollection<Packaging>(collectionName);
    }
    
    public async Task<IEnumerable<Packaging>> GetPackagingsAsync()
    {
        return await _packagingCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Packaging> GetPackagingAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(packaging => packaging.Id, id);
        return await _packagingCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreatePackagingAsync(Packaging packaging)
    {
        await _packagingCollection.InsertOneAsync(packaging);
    }

    public async Task  UpdatePackagingAsync(Packaging packaging)
    {
        var filter = _filterBuilder.Eq(existingPackaging => existingPackaging.Id, packaging.Id);
        await _packagingCollection.ReplaceOneAsync(filter, packaging);
    }

    public async Task DeletePackagingAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(packaging => packaging.Id, id);
        await _packagingCollection.DeleteOneAsync(filter);
    }
}