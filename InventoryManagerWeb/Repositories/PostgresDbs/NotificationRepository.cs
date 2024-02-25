using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    private readonly ApplicationDbContext _db;

    public NotificationRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Notification obj)
    {
        _db.Notifications.Update(obj);
    }

    public async Task<Notification> GetByIdAsync(Guid id)
    {
        return await _db.Notifications.FindAsync(id);
    }
}