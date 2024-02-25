using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;
using InventoryManagerWeb.Entities;


namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class InventoryItemRepository : Repository<InventoryItem>, IInventoryItemRepository
{
    private readonly ApplicationDbContext _db;

    public InventoryItemRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(InventoryItem obj)
    {
        _db.InventoryItems.Update(obj);
    }
    
    public async Task<InventoryItem> GetByIdAsync(Guid id)
    {
        return await _db.InventoryItems.FindAsync(id);
    }
}