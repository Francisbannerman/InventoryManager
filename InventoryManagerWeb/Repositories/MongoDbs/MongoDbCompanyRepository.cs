using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InventoryManagerWeb.Repositories;

public class MongoDbCompanyRepository :ICompaniesRepository
{
    private const string databaseName = "Inventory";
    private const string collectionName = "company";
    private readonly IMongoCollection<Company> _companyCollection;
    private readonly FilterDefinitionBuilder<Company> _filterBuilder = Builders<Company>.Filter;

    public MongoDbCompanyRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        _companyCollection = database.GetCollection<Company>(collectionName);
    }
    
    public async Task<IEnumerable<Company>> GetCompaniesAsync()
    {
        return await _companyCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Company> GetCompanyAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(company => company.Id, id);
        return await _companyCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateCompanyAsync(Company company)
    {
        await _companyCollection.InsertOneAsync(company);
    }

    public async Task  UpdateCompanyAsync(Company company)
    {
        var filter = _filterBuilder.Eq(existingCompany => existingCompany.Id, company.Id);
        await _companyCollection.ReplaceOneAsync(filter, company);
    }

    public async Task DeleteCompanyAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(company => company.Id, id);
        await _companyCollection.DeleteOneAsync(filter);
    }
}