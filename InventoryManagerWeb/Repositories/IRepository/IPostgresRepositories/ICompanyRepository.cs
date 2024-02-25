using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company obj);
    Task<Company> GetByIdAsync(Guid id);
}