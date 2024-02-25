using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface IPackagingRepository : IRepository<Packaging>
{
    void Update(Packaging obj);
    Task<Packaging> GetByIdAsync(Guid id);
}