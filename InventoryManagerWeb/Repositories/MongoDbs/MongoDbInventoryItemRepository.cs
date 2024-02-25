using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InventoryManagerWeb.Repositories;

public class MongoDbInventoryItemRepository :IInventoryItemsRepository
{
    private const string databaseName = "Inventory";
    private const string collectionName = "inventoryItem";
    private readonly IMongoCollection<InventoryItem> _inventoryItemCollection;
    private readonly FilterDefinitionBuilder<InventoryItem> _filterBuilder = Builders<InventoryItem>.Filter;

    public MongoDbInventoryItemRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        _inventoryItemCollection = database.GetCollection<InventoryItem>(collectionName);

    }
    public async Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync()
    {
        return await _inventoryItemCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<InventoryItem> GetInventoryItemAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(inventoryItem => inventoryItem.Id, id);
        return await _inventoryItemCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateInventoryItemAsync(InventoryItem inventoryItem)
    {
        await _inventoryItemCollection.InsertOneAsync(inventoryItem);
    }

    public async Task  UpdateInventoryItemAsync(InventoryItem inventoryItem)
    {
        var filter = _filterBuilder.Eq(existingInventoryItem => existingInventoryItem.Id, inventoryItem.Id);
        await _inventoryItemCollection.ReplaceOneAsync(filter, inventoryItem);
    }

    public async Task DeleteInventoryItemAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(inventoryItem => inventoryItem.Id, id);
        await _inventoryItemCollection.DeleteOneAsync(filter);
    }
}