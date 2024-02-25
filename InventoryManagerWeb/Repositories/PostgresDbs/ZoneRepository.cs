using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class ZoneRepository : Repository<Zone>, IZoneRepository
{
    private readonly ApplicationDbContext _db;

    public ZoneRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Zone obj)
    {
        _db.Zones.Update(obj);
    }
    
    public async Task<Zone> GetByIdAsync(Guid id)
    {
        return await _db.Zones.FindAsync(id);
    }
}