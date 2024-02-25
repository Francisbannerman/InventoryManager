using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface IInventoryItemRepository : IRepository<InventoryItem>
{
    void Update(InventoryItem obj);
    Task<InventoryItem> GetByIdAsync(Guid id);
}