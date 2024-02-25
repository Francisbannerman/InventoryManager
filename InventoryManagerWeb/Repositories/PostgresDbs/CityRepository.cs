using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class CityRepository : Repository<City>, ICityRepository
{
    private readonly ApplicationDbContext _db;

    public CityRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(City obj)
    {
        _db.Cities.Update(obj);
    }
    
    public async Task<City> GetByIdAsync(Guid id)
    {
        return await _db.Cities.FindAsync(id);
    }
}