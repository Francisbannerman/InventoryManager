using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InventoryManagerWeb.Repositories;

public class MongoDbCityRepository :ICitiesRepository
{
    private const string databaseName = "Inventory";
    private const string collectionName = "city";
    private readonly IMongoCollection<City> _cityCollection;
    private readonly FilterDefinitionBuilder<City> _filterBuilder = Builders<City>.Filter;

    public MongoDbCityRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        _cityCollection = database.GetCollection<City>(collectionName);

    }
    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _cityCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<City> GetCityAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(city => city.Id, id);
        return await _cityCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateCityAsync(City city)
    {
        await _cityCollection.InsertOneAsync(city);
    }

    public async Task  UpdateCityAsync(City city)
    {
        var filter = _filterBuilder.Eq(existingCity => existingCity.Id, city.Id);
        await _cityCollection.ReplaceOneAsync(filter, city);
    }

    public async Task DeleteCityAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(city => city.Id, id);
        await _cityCollection.DeleteOneAsync(filter);
    }
}