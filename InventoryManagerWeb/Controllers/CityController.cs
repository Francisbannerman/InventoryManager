using InventoryManagerWeb.Dtos;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("City")]
public class CityController : ControllerBase
{
    ////Mongo Repo
     // private readonly ICitiesRepository _repository;
     // public CityController(ICitiesRepository repository)
     // {
     //     _repository = repository;
     // }
     
     ////In Mem Repo
     // private readonly IInMemCitiesRepository _repository;
     // public CityController(IInMemCitiesRepository repository)
     // {
     //     _repository = repository;
     // }
     
     //IUnitOfWork
     private readonly IUnitOfWork _unitOfWork;
     private readonly NotificationService _notificationService;
     private readonly ILogger<CityController> _logger;
     public CityController(IUnitOfWork unitOfWork, NotificationService notificationService,
         ILogger<CityController> logger)
     {
         _unitOfWork = unitOfWork;
         _notificationService = notificationService;
         _logger = logger;
     }
     
     [HttpGet]
    public async Task<IEnumerable<CityDto>> GetCitiesAsync()
    {
        var cities = (await _unitOfWork.City.GetAllAsync()).Select(city => city.AsDto());
        _logger.LogInformation("All Cities have been searched for");
        return cities;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CityDto>> GetCityAsync(Guid id)
    {
        var city = await _unitOfWork.City.GetAsync(id);
        if (city is null)
        {
            return NotFound();
        }
        _logger.LogInformation($"City With name {city.CityName} have been searched for");
        return city.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<CityDto>> CreateCityAsync(CreateCityDto cityDto)
    {
        var msg = $"A new city({cityDto.CityName}), has been created";
        City city = new()
        {
            Id = Guid.NewGuid(), CityName = cityDto.CityName,
            CityRep = cityDto.CityRep, CreatedDate = DateTimeOffset.Now
        };
        await _unitOfWork.City.AddAsync(city);
        _notificationService.AddNotification(msg);
        _logger.LogInformation($"City With name {city.CityName} have been created");
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetCityAsync), new { id = city.Id }, city.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCityAsync(Guid id, UpdateCityDto cityDto)
    {
        var existingCity = await _unitOfWork.City.GetAsync(id);
        if (existingCity is null)
        {
            return NotFound();
        }
        existingCity.CityName = cityDto.CityName;
        existingCity.CityRep = cityDto.CityRep;

        await _unitOfWork.City.EditAsync(existingCity);
        _logger.LogInformation($"City With name {existingCity.CityName} have been updated");
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCityAsync(Guid id)
    {
        var existingCity = await _unitOfWork.City.GetAsync(id);
        if (existingCity is null)
        {
            return NotFound();
        }
        var msg = $"The city({existingCity.CityName}) has been deleted";
        _logger.LogInformation($"City With name {existingCity.CityName} have been deleted");
        await _unitOfWork.City.RemoveAsync(existingCity);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}