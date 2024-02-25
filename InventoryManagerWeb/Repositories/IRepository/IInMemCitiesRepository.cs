using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository;

public interface IInMemCitiesRepository
{
    Task<IEnumerable<City>> GetCitiesAsync();
    Task<City> GetCityAsync(Guid id);
    Task CreateCityAsync(City item);
    Task UpdateCityAsync(City item);
    Task DeleteCityAsync(Guid id);

}