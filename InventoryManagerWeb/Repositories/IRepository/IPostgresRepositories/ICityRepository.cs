using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface ICityRepository : IRepository<City>
{
    void Update(City obj);
    Task<City> GetByIdAsync(Guid id);
}