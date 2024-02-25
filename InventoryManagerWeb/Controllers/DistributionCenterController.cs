using InventoryManagerWeb.Dtos.DistributionCenterDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("DistributionCenter")]
public class DistributionCenterController : Controller
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
    public DistributionCenterController(IUnitOfWork unitOfWork, NotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IEnumerable<DistributionCenterDto>> GetDistributionCentersAsync()
    {
        var distributionCenters =
            (await _unitOfWork.DistributionCenter.GetAllAsync()).Select(
                distributionCenter => distributionCenter.AsDto());

        foreach (var center in distributionCenters)
        {
            if (center.CompanyId != null && center.ZoneId != null)
            {
                var company = await _unitOfWork.Company.GetByIdAsync(new Guid(center.CompanyId.ToString()));
                var zone = await _unitOfWork.Zone.GetByIdAsync(new Guid(center.ZoneId.ToString()));
                center.Company = company;
                center.Zone = zone;
            }
        }
        return distributionCenters;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DistributionCenterDto>> GetDistributionCenterAsync(Guid id)
    {
        var distributionCenter = await _unitOfWork.DistributionCenter.GetAsync(id);
        if (distributionCenter is null)
        {
            return NotFound();
        }
        var company = await _unitOfWork.Company.GetByIdAsync(new Guid(distributionCenter.CompanyId.ToString()));
        var zone = await _unitOfWork.Zone.GetByIdAsync(new Guid(distributionCenter.ZoneId.ToString()));
        distributionCenter.Company = company;
        distributionCenter.Zone = zone;
        return distributionCenter.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<DistributionCenterDto>> CreateDistributionCenterAsync(
        CreateDistributionCenterDto distributionCenterDto)
    {
        Company existingCompany =
            await _unitOfWork.Company.GetByIdAsync(new Guid(distributionCenterDto.CompanyId.ToString()));
        Zone existingZone = await _unitOfWork.Zone.GetByIdAsync(new Guid(distributionCenterDto.ZoneId.ToString()));
        var msg = $"A new distribution center({distributionCenterDto.DistributionCenterName}), has been created in " +
                  $"{existingZone.ZoneName} for the company {existingCompany.CompanyName}";

        DistributionCenter distributionCenter = new()
        {
            Id = Guid.NewGuid(), DistributionCenterName = distributionCenterDto.DistributionCenterName,
            DistributionCenterRep = distributionCenterDto.DistributionCenterRep, 
            Coordinates = distributionCenterDto.Coordinates, CompanyId = distributionCenterDto.CompanyId,
            CreatedDate = DateTimeOffset.Now, Company = existingCompany, ZoneId = distributionCenterDto.ZoneId,
            Zone = existingZone
        };
        await _unitOfWork.DistributionCenter.AddAsync(distributionCenter);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetDistributionCenterAsync), new { id = distributionCenter.Id },
            distributionCenter.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDistributionCenterAsync(Guid id,
        UpdateDistributionCenterDto distributionCenterDto)
    {
        var existingDistributionCenter = await _unitOfWork.DistributionCenter.GetAsync(id);
        if (existingDistributionCenter is null)
        {
            return NotFound();
        }
        existingDistributionCenter.DistributionCenterName = distributionCenterDto.DistributionCenterName;
        existingDistributionCenter.DistributionCenterRep = distributionCenterDto.DistributionCenterRep;
        existingDistributionCenter.Coordinates = distributionCenterDto.Coordinates;
        existingDistributionCenter.CompanyId = distributionCenterDto.CompanyId;
        existingDistributionCenter.ZoneId = distributionCenterDto.ZoneId;
        
        await _unitOfWork.DistributionCenter.EditAsync(existingDistributionCenter);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDistributionCenterAsync(Guid id)
    {
        var existingDistributionCenter = await _unitOfWork.DistributionCenter.GetAsync(id);
        Company existingCompany = await _unitOfWork.Company.GetByIdAsync(new Guid(existingDistributionCenter.CompanyId.ToString()));
        Zone existingZone = await _unitOfWork.Zone.GetByIdAsync(new Guid(existingDistributionCenter.ZoneId.ToString()));
        if (existingDistributionCenter is null)
        {
            return NotFound();
        }
        var msg = $"The distribution center({existingDistributionCenter.DistributionCenterName}), has been deleted in " +
                  $"{existingZone.ZoneName} for the company {existingCompany.CompanyName}";
        await _unitOfWork.DistributionCenter.RemoveAsync(existingDistributionCenter);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}