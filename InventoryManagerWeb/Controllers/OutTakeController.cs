using InventoryManagerWeb.Dtos.OutTakeDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("OutTake")]
public class OutTakeController : Controller
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
    private readonly ItemCountService _itemCountService;
    private readonly NotificationService _notificationService;
    public OutTakeController(IUnitOfWork unitOfWork, ItemCountService itemCountService,
        NotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _itemCountService = itemCountService;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IEnumerable<OutTakeDto>> GetOutTakesAsync()
    {
        var outTakes = (await _unitOfWork.OutTake.GetAllAsync()).Select(outTake => outTake.AsDto());

        foreach (var outTake in outTakes)
        {
            if (outTake.InventoryItemId != null)
            {
                var inventoryItem =
                    await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(outTake.InventoryItemId.ToString()));
                outTake.Item = inventoryItem;
            }
        }
        return outTakes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OutTakeDto>> GetOutTakeAsync(Guid id)
    {
        var outTake = await _unitOfWork.OutTake.GetAsync(id);
        if (outTake is null)
        {
            return NotFound();
        }
        var inventoryItem =
            await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(outTake.InventoryItemId.ToString()));
        outTake.Item = inventoryItem;
        return outTake.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<OutTakeDto>> CreateOutTakeAsync(CreateOutTakeDto outTakeDto)
    {
        InventoryItem existingInventoryItem = await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(outTakeDto.InventoryItemId.ToString()));
        var quantity = _itemCountService.OutTakeQuantity(outTakeDto, existingInventoryItem);
        var msg = $"A new take-Out has been created by {outTakeDto.CityRep}, to pick up {quantity}pieces of {existingInventoryItem.ItemName}";

        OutTake outTake = new()
        {
            Id = Guid.NewGuid(), 
            DateOfOutTake = DateTimeOffset.Now,
            CityRep = outTakeDto.CityRep,
            IsPickUpByHubtel = outTakeDto.IsPickUpByHubtel,
            PickUpByWho = outTakeDto.PickUpByWho,
            AttachedImage = outTakeDto.AttachedImage,
            Quantity = quantity,
            IsTakenInPieces = outTakeDto.IsTakenInPieces,
            IsTakenInPackages = outTakeDto.IsTakenInPackages,
            NumberOfPackages = outTakeDto.NumberOfPackages,
            NumberOfPieces = outTakeDto.NumberOfPieces,
            InventoryItemId = outTakeDto.InventoryItemId,
            Item = existingInventoryItem
        };
        if (outTake.Item.Quantity < quantity)
        {
            throw new InvalidOperationException($"{outTake.Item.ItemName}s " +
                                                $"Quantity({outTake.Item.Quantity}) is smaller than the " +
                                                $"Quantity({quantity}) You Want To Take Out");
        }
        await _unitOfWork.OutTake.AddAsync(outTake);
        outTake.Item.Quantity -= quantity;
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetOutTakeAsync), new { id = outTake.Id }, outTake.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOutTakeAsync(Guid id, UpdateOutTakeDto outTakeDto)
    {
        var existingOutTake = await _unitOfWork.OutTake.GetAsync(id);
        InventoryItem existingInventoryItem = await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(outTakeDto.InventoryItemId.ToString()));

        if (existingOutTake is null)
        {
            return NotFound();
        }
        existingOutTake.CityRep = outTakeDto.CityRep;
        existingOutTake.IsPickUpByHubtel = outTakeDto.IsPickUpByHubtel;
        existingOutTake.PickUpByWho = outTakeDto.PickUpByWho;
        existingOutTake.AttachedImage = outTakeDto.AttachedImage;
        existingOutTake.Quantity = _itemCountService.OutTakeQuantity(outTakeDto, existingInventoryItem);
        existingOutTake.IsTakenInPackages = outTakeDto.IsTakenInPackages;
        existingOutTake.IsTakenInPieces = outTakeDto.IsTakenInPieces;
        existingOutTake.NumberOfPieces = outTakeDto.NumberOfPieces;
        existingOutTake.NumberOfPackages = outTakeDto.NumberOfPackages;
        existingOutTake.InventoryItemId = outTakeDto.InventoryItemId;
            
        await _unitOfWork.OutTake.EditAsync(existingOutTake);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOutTakeAsync(Guid id)
    {
        var existingOutTake = await _unitOfWork.OutTake.GetAsync(id);
        InventoryItem existingInventoryItem = await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(existingOutTake.InventoryItemId.ToString()));

        if (existingOutTake is null)
        {
            return NotFound();
        }
        var msg = $"The take-Out with id=({id}) has been deleted. The deleted take-Out was made of " +
                  $"{existingOutTake.Quantity}pieces of {existingInventoryItem.ItemName}";
        await _unitOfWork.OutTake.RemoveAsync(existingOutTake);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}