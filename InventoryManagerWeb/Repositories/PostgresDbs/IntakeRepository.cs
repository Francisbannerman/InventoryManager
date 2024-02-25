using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class IntakeRepository : Repository<Intake>, IIntakeRepository
{
    private readonly ApplicationDbContext _db;

    public IntakeRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Intake obj)
    {
        _db.Intakes.Update(obj);
    }
    
    public async Task<Intake> GetByIdAsync(Guid id)
    {
        return await _db.Intakes.FindAsync(id);
    }
}