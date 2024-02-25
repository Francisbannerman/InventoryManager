using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class OutTakeRepository : Repository<OutTake>, IOutTakeRepository
{
    private readonly ApplicationDbContext _db;

    public OutTakeRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(OutTake obj)
    {
        _db.OutTakes.Update(obj);
    }
    
    public async Task<OutTake> GetByIdAsync(Guid id)
    {
        return await _db.OutTakes.FindAsync(id);
    }
}