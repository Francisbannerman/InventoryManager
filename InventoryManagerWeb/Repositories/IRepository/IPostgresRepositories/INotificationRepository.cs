using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface INotificationRepository : IRepository<Notification>
{
    void Update(Notification obj);
    Task<Notification> GetByIdAsync(Guid id);
}