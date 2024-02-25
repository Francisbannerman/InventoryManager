using InventoryManagerWeb.Dtos.IntakeDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("Intake")]
public class IntakeController : Controller
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
    private ItemCountService _itemCountService;
    private readonly NotificationService _notificationService;
    public IntakeController(IUnitOfWork unitOfWork, ItemCountService itemCountService, NotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _itemCountService = itemCountService;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IEnumerable<IntakeDto>> GetIntakesAsync()
    {
        var intakes = (await _unitOfWork.Intake.GetAllAsync()).Select(intake => intake.AsDto());

        foreach (var intake in intakes)
        {
            if (intake.ZoneId != null && intake.CompanyId != null && intake.InventoryItemId != null)
            {
                var zone = await _unitOfWork.Zone.GetByIdAsync(new Guid(intake.ZoneId.ToString()));
                var company = await _unitOfWork.Company.GetByIdAsync(new Guid(intake.CompanyId.ToString()));
                var inventoryItem =
                    await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(intake.InventoryItemId.ToString()));
                intake.Zone = zone;
                intake.Company = company;
                intake.Item = inventoryItem;
            }
        }
        return intakes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IntakeDto>> GetIntakeAsync(Guid id)
    {
        var intake = await _unitOfWork.Intake.GetAsync(id);
        if (intake is null)
        {
            return NotFound();
        }
        var zone = await _unitOfWork.Zone.GetByIdAsync(new Guid(intake.ZoneId.ToString()));
        var company = await _unitOfWork.Company.GetByIdAsync(new Guid(intake.CompanyId.ToString()));
        var inventoryItem =
            await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(intake.InventoryItemId.ToString()));
        intake.Zone = zone;
        intake.Company = company;
        intake.Item = inventoryItem;
        return intake.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<IntakeDto>> CreateIntakeAsync(CreateIntakeDto intakeDto)
    {
        Zone existingZone = await _unitOfWork.Zone.GetByIdAsync(new Guid(intakeDto.ZoneId.ToString()));
        Company existingCompany = await _unitOfWork.Company.GetByIdAsync(new Guid(intakeDto.CompanyId.ToString()));
        InventoryItem existingInventoryItem = await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(intakeDto.InventoryItemId.ToString()));
        
        var quantity = _itemCountService.IntakeQuantity(intakeDto, existingInventoryItem);
        
        var msg = $"A new in-take has been created in {existingZone.ZoneName} from company named {existingCompany.CompanyName}" +
                  $"for {quantity}pieces of {existingInventoryItem.ItemName} by {intakeDto.CityIntakeRep}";
        
        Intake intake = new()
        {
            Id = Guid.NewGuid(),
            DateOfIntake = DateTimeOffset.Now, 
            CompanyId = intakeDto.CompanyId,
            Company = existingCompany,
            ZoneId = intakeDto.ZoneId,
            Zone = existingZone,
            InventoryItemId = intakeDto.InventoryItemId,
            Item = existingInventoryItem,
            CompanyDeliveryRep = intakeDto.CompanyDeliveryRep, 
            CityIntakeRep = intakeDto.CityIntakeRep,
            AttachedImage = intakeDto.AttachedImage,
            Quantity = quantity,
            IsReceivedInPackages = intakeDto.IsReceivedInPackages,
            IsReceivedInPieces = intakeDto.IsReceivedInPieces,
            NumberOfPieces = intakeDto.NumberOfPieces,
            NumberOfPackages = intakeDto.NumberOfPackages
        };
        await _unitOfWork.Intake.AddAsync(intake);
        intake.Item.Quantity += quantity;
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetIntakeAsync), new { id = intake.Id }, intake.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateIntakeAsync(Guid id, UpdateIntakeDto intakeDto)
    {
        var existingIntake = await _unitOfWork.Intake.GetAsync(id);
        InventoryItem existingInventoryItem = await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(intakeDto.InventoryItemId.ToString()));
        if (existingIntake is null)
        {
            return NotFound();
        }
        existingIntake.CompanyId = intakeDto.CompanyId;
        existingIntake.ZoneId = intakeDto.ZoneId;
        existingIntake.InventoryItemId = intakeDto.InventoryItemId;
        existingIntake.CompanyDeliveryRep = intakeDto.CompanyDeliveryRep;
        existingIntake.CityIntakeRep = intakeDto.CityIntakeRep;
        existingIntake.AttachedImage = intakeDto.AttachedImage;
        existingIntake.Quantity = _itemCountService.IntakeQuantity(intakeDto, existingInventoryItem);
        existingIntake.UpdatedDateAndTime = DateTimeOffset.Now;
        existingIntake.IsReceivedInPackages = intakeDto.IsReceivedInPackages;
        existingIntake.IsReceivedInPieces = intakeDto.IsReceivedInPieces;
        existingIntake.NumberOfPieces = intakeDto.NumberOfPieces;
        existingIntake.NumberOfPackages = intakeDto.NumberOfPackages;
            
        await _unitOfWork.Intake.EditAsync(existingIntake);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteIntakeAsync(Guid id)
    {
        var existingIntake = await _unitOfWork.Intake.GetAsync(id);
        
        Zone existingZone = await _unitOfWork.Zone.GetByIdAsync(new Guid(existingIntake.ZoneId.ToString()));
        Company existingCompany = await _unitOfWork.Company.GetByIdAsync(new Guid(existingIntake.CompanyId.ToString()));
        InventoryItem existingInventoryItem = await _unitOfWork.InventoryItem.GetByIdAsync(new Guid(existingIntake.InventoryItemId.ToString()));
        
        if (existingIntake is null)
        {
            return NotFound();
        }
        var msg = $"The in-take with id({existingIntake.Id}) has been deleted. The deleted in-take was made-out of " +
                  $"{existingIntake.Quantity}pieces of {existingInventoryItem.ItemName} from {existingCompany.CompanyName} in {existingZone.ZoneName}";
        
        await _unitOfWork.Intake.RemoveAsync(existingIntake);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}