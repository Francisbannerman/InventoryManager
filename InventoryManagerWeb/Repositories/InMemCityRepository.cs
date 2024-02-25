using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository;

namespace InventoryManagerWeb.Repositories;

public class InMemCityRepository : IInMemCitiesRepository
{
    private readonly List<City> cities = new()
    {
        new City { Id = Guid.NewGuid(), CityName = "North", CityRep = "Nii", CreatedDate = DateTimeOffset.Now, },
        new City { Id = Guid.NewGuid(), CityName = "South", CityRep = "Lantey", CreatedDate = DateTimeOffset.Now, },
        new City { Id = Guid.NewGuid(), CityName = "East", CityRep = "Francis", CreatedDate = DateTimeOffset.Now, },
    };

    public Task<IEnumerable<City>> GetCitiesAsync()
    {
        return Task.FromResult<IEnumerable<City>>(cities);
    }

    public Task<City> GetCityAsync(Guid id)
    {
        return Task.FromResult(cities.SingleOrDefault(city => city.Id == id));
    }

    public Task CreateCityAsync(City city)
    {
        cities.Add(city);
        return Task.CompletedTask;
    }

    public Task UpdateCityAsync(City city)
    {
        var index = cities.FindIndex(existingCity => existingCity.Id == city.Id);
        cities[index] = city;
        return Task.CompletedTask;
    }

    public Task DeleteCityAsync(Guid id)
    {
        var index = cities.FindIndex(existingCity => existingCity.Id == id);
        cities.RemoveAt(index);
        return Task.CompletedTask;
    }

}