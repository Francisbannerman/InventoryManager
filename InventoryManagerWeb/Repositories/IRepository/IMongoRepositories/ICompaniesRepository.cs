using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository;

public interface ICompaniesRepository
{
    Task<IEnumerable<Company>> GetCompaniesAsync();
    Task<Company> GetCompanyAsync(Guid id);
    Task CreateCompanyAsync(Company company);
    Task UpdateCompanyAsync(Company company);
    Task DeleteCompanyAsync(Guid id);
}