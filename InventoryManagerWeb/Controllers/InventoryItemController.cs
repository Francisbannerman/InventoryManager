using InventoryManagerWeb.Dtos.InventoryItemDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("InventoryItem")]
public class InventoryItemController : ControllerBase
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
    public InventoryItemController(IUnitOfWork unitOfWork, NotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IEnumerable<InventoryItemDto>> GetInventoryItemsAsync()
    {
        var inventoryItems = (await _unitOfWork.InventoryItem.GetAllAsync()).Select(inventoryItem => inventoryItem.AsDto());

        foreach (var item in inventoryItems)
        {
            if (item.PackagingId != null && item.CompanyId != null)
            {
                var packaging = await _unitOfWork.Packaging.GetByIdAsync(new Guid(item.PackagingId.ToString()));
                var company = await _unitOfWork.Company.GetByIdAsync(new Guid(item.CompanyId.ToString()));
                item.PackagingType = packaging;
                item.Company = company;
            }
        }
        return inventoryItems;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryItemDto>> GetInventoryItemAsync(Guid id)
    {
        var inventoryItem = await _unitOfWork.InventoryItem.GetAsync(id);
        if (inventoryItem is null)
        {
            return NotFound();
        }
        var packaging = await _unitOfWork.Packaging.GetByIdAsync(new Guid(inventoryItem.PackagingId.ToString()));
        var company = await _unitOfWork.Company.GetByIdAsync(new Guid(inventoryItem.CompanyId.ToString()));
        inventoryItem.PackagingType = packaging;
        inventoryItem.Company = company;
        return inventoryItem.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<InventoryItemDto>> CreateInventoryItemAsync(CreateInventoryItemDto inventoryItemDto)
    {
        Packaging existingPackaging = await _unitOfWork.Packaging.GetByIdAsync(new Guid(inventoryItemDto.PackagingId.ToString()));
        Company existingCompany = await _unitOfWork.Company.GetByIdAsync(new Guid(inventoryItemDto.CompanyId.ToString()));
        var msg = $"A new item({inventoryItemDto.ItemName}) has been created for the company {existingCompany.CompanyName}";
        InventoryItem inventoryItem = new()
        {
            Id = Guid.NewGuid(), 
            ItemName = inventoryItemDto.ItemName,
            ItemSize = inventoryItemDto.ItemSize, 
            PackagingId = inventoryItemDto.PackagingId,
            PackagingType = existingPackaging, 
            CompanyId = inventoryItemDto.CompanyId, 
            Company = existingCompany,
            QuantityPerPackage = inventoryItemDto.QuantityPerPackage,
            CreatedTme = DateTimeOffset.Now
        };
        await _unitOfWork.InventoryItem.AddAsync(inventoryItem);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetInventoryItemAsync), new { id = inventoryItem.Id }, inventoryItem.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateInventoryItemAsync(Guid id, UpdateInventoryItemDto inventoryItemDto)
    {
        var existingInventoryItem = await _unitOfWork.InventoryItem.GetAsync(id);
        if (existingInventoryItem is null)
        {
            return NotFound();
        }
        existingInventoryItem.ItemName = inventoryItemDto.ItemName;
        existingInventoryItem.ItemSize = inventoryItemDto.ItemSize;
        existingInventoryItem.PackagingId = inventoryItemDto.PackagingId;
        existingInventoryItem.CompanyId = inventoryItemDto.CompanyId;
        existingInventoryItem.QuantityPerPackage = inventoryItemDto.QuantityPerPackage;
        existingInventoryItem.UpdateTime = DateTimeOffset.Now;

        await _unitOfWork.InventoryItem.EditAsync(existingInventoryItem);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInventoryItemAsync(Guid id)
    {
        var existingInventoryItem = await _unitOfWork.InventoryItem.GetAsync(id);
        Company existingCompany = await _unitOfWork.Company.GetByIdAsync(new Guid(existingInventoryItem.CompanyId.ToString()));
        if (existingInventoryItem is null)
        {
            return NotFound();
        }
        var msg = $"The item({existingInventoryItem.ItemName}) has been deleted for the " +
                  $"company({existingCompany.CompanyName})";
        await _unitOfWork.InventoryItem.RemoveAsync(existingInventoryItem);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}