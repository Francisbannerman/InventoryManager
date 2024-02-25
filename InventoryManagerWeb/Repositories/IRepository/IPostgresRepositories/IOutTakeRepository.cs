using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface IOutTakeRepository : IRepository<OutTake>
{
    void Update(OutTake obj);
    Task<OutTake> GetByIdAsync(Guid id);
}