using InventoryManagerWeb.Dtos.PackagingDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("Packaging")]
public class PackagingController : ControllerBase
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
    public PackagingController(IUnitOfWork unitOfWork, NotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IEnumerable<PackagingDto>> GetPackagingsAsync()
    {
        var packagings = (await _unitOfWork.Packaging.GetAllAsync()).Select(packaging => packaging.AsDto());
        return packagings;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PackagingDto>> GetPackagingAsync(Guid id)
    {
        var packaging = await _unitOfWork.Packaging.GetAsync(id);
        if (packaging is null)
        {
            return NotFound();
        }
        return packaging.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<PackagingDto>> CreatePackagingAsync(CreatePackagingDto packagingDto)
    {
        var msg = $"A new packaging({packagingDto.PackagingName}) has been created";
        Packaging packaging = new()
        {
            Id = Guid.NewGuid(), PackagingName = packagingDto.PackagingName
        };
        await _unitOfWork.Packaging.AddAsync(packaging);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetPackagingAsync), new { id = packaging.Id }, packaging.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePackagingAsync(Guid id, UpdatePackagingDto packagingDto)
    {
        var existingPackaging = await _unitOfWork.Packaging.GetAsync(id);
        if (existingPackaging is null)
        {
            return NotFound();
        }
        existingPackaging.PackagingName = packagingDto.PackagingName;
        
        await _unitOfWork.Packaging.EditAsync(existingPackaging);
       await  _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePackagingAsync(Guid id)
    {
        var existingPackaging = await _unitOfWork.Packaging.GetAsync(id);
        if (existingPackaging is null)
        {
            return NotFound();
        }
        var msg = $"The packaging ({existingPackaging.PackagingName}) has been created";
        await _unitOfWork.Packaging.RemoveAsync(existingPackaging);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}