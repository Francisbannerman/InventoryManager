using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface IDistributionCenterRepository : IRepository<DistributionCenter>
{
    void Update(DistributionCenter obj);
    Task<DistributionCenter> GetByIdAsync(Guid id);
}