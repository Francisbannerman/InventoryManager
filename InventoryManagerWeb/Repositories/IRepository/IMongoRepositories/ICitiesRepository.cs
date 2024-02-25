using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Repositories.IRepository;

public interface ICitiesRepository
{
    Task<IEnumerable<City>> GetCitiesAsync();
    Task<City> GetCityAsync(Guid id);
    Task CreateCityAsync(City city);
    Task UpdateCityAsync(City city);
    Task DeleteCityAsync(Guid id);
}