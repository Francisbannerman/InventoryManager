using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository;

public interface IPackagingsRepository
{
    Task<IEnumerable<Packaging>> GetPackagingsAsync();
    Task<Packaging> GetPackagingAsync(Guid id);
    Task CreatePackagingAsync(Packaging packaging);
    Task UpdatePackagingAsync(Packaging packaging);
    Task DeletePackagingAsync(Guid id);
}