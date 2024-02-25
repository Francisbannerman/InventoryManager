using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface IIntakeRepository : IRepository<Intake>
{
    void Update(Intake obj);
    Task<Intake> GetByIdAsync(Guid id);
}