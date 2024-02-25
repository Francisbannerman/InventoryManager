using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository;

public interface IInventoryItemsRepository
{
    Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync();
    Task<InventoryItem> GetInventoryItemAsync(Guid id);
    Task CreateInventoryItemAsync(InventoryItem inventoryItem);
    Task UpdateInventoryItemAsync(InventoryItem inventoryItem);
    Task DeleteInventoryItemAsync(Guid id);
}