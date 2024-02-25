using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface IZoneRepository : IRepository<Zone>
{
    void Update(Zone obj);
    Task<Zone> GetByIdAsync(Guid id);
}