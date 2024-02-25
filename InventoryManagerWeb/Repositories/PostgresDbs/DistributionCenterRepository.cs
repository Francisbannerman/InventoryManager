using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class DistributionCenterRepository : Repository<DistributionCenter>, IDistributionCenterRepository
{
    private readonly ApplicationDbContext _db;

    public DistributionCenterRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(DistributionCenter obj)
    {
        _db.DistributionCenters.Update(obj);
    }
    
    public async Task<DistributionCenter> GetByIdAsync(Guid id)
    {
        return await _db.DistributionCenters.FindAsync(id);
    }
}