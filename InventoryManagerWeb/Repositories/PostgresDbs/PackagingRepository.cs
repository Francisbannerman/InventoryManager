using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class PackagingRepository : Repository<Packaging>, IPackagingRepository
{
    private readonly ApplicationDbContext _db;

    public PackagingRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Packaging obj)
    {
        _db.Packagings.Update(obj);
    }
    
    public async Task<Packaging> GetByIdAsync(Guid id)
    {
        return await _db.Packagings.FindAsync(id);
    }
}