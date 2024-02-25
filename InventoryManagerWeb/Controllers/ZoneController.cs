using InventoryManagerWeb.Dtos.ZoneDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("Zone")]
public class ZoneController : Controller
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
    public ZoneController(IUnitOfWork unitOfWork, NotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IEnumerable<ZoneDto>> GetZonesAsync()
    {
        var zones = (await _unitOfWork.Zone.GetAllAsync()).Select(zone => zone.AsDto());

        foreach (var zone in zones)
        {
            if (zone.CityId != null)
            {
                var thisZone = await _unitOfWork.City.GetByIdAsync(new Guid(zone.CityId.ToString()));
                zone.City = thisZone;
            }
        }
        return zones;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ZoneDto>> GetZoneAsync(Guid id)
    {
        var zone = await _unitOfWork.Zone.GetAsync(id);
        if (zone is null)
        {
            return NotFound();
        }
        var city = await _unitOfWork.City.GetByIdAsync(new Guid(zone.CityId.ToString()));
        zone.City = city;
        return zone.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ZoneDto>> CreateZoneAsync(CreateZoneDto zoneDto)
    {
        City existingCity = await _unitOfWork.City.GetByIdAsync(new Guid(zoneDto.CityId.ToString()));
        var msg = $"A new zone({zoneDto.ZoneName}), has been created in {existingCity.CityName}";
        Zone zone = new()
        {
            Id = Guid.NewGuid(), ZoneName = zoneDto.ZoneName, ZoneRep = zoneDto.ZoneRep,
            ZoneCoordinates = zoneDto.ZoneCoordinates, CityId = zoneDto.CityId,
            City = existingCity, CreatedDate = DateTimeOffset.Now
        };
        await _unitOfWork.Zone.AddAsync(zone);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetZoneAsync), new { id = zone.Id }, zone.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateZoneAsync(Guid id, UpdateZoneDto zoneDto)
    {
        var existingZone = await _unitOfWork.Zone.GetAsync(id);
        if (existingZone is null)
        {
            return NotFound();
        }
        existingZone.ZoneName = zoneDto.ZoneName;
        existingZone.ZoneRep = zoneDto.ZoneRep;
        existingZone.ZoneCoordinates = zoneDto.ZoneCoordinates;
        existingZone.CityId = zoneDto.CityId;
        
        await _unitOfWork.Zone.EditAsync(existingZone);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteZoneAsync(Guid id)
    {
        var existingZone = await _unitOfWork.Zone.GetAsync(id);
        City existingCity = await _unitOfWork.City.GetByIdAsync(new Guid(existingZone.CityId.ToString()));

        if (existingZone is null)
        {
            return NotFound();
        }
        var msg = $"The zone({existingZone.ZoneName}), has been deleted from {existingCity.CityName}";
        await _unitOfWork.Zone.RemoveAsync(existingZone);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}