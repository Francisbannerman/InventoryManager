using InventoryManagerWeb.Dtos.CompanyDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;

[ApiController]
[Route("Company")]
public class CompanyController : ControllerBase
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
     public CompanyController(IUnitOfWork unitOfWork, NotificationService notificationService)
     {
         _unitOfWork = unitOfWork;
         _notificationService = notificationService;
     }
     
     [HttpGet]
    public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync()
    {
        var companies = (await _unitOfWork.Company.GetAllAsync()).Select(company => company.AsDto());
        return companies;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompanyAsync(Guid id)
    {
        var company = await _unitOfWork.Company.GetAsync(id);
        if (company is null)
        {
            return NotFound();
        }
        return company.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> CreateCompanyAsync(CreateCompanyDto companyDto)
    {
        var msg = $"A new company({companyDto.CompanyName}), has been created";
        Company company = new()
        {
            Id = Guid.NewGuid(), CompanyName = companyDto.CompanyName, Coordinates = companyDto.Coordinates,
            CompanyRep = companyDto.CompanyRep, CreatedDate = DateTimeOffset.Now
        };
        await _unitOfWork.Company.AddAsync(company);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return CreatedAtAction(nameof(GetCompanyAsync), new { id = company.Id }, company.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCompanyAsync(Guid id, UpdateCompanyDto companyDto)
    {
        var existingCompany = await _unitOfWork.Company.GetAsync(id);
        if (existingCompany is null)
        {
            return NotFound();
        }
        existingCompany.CompanyName = companyDto.CompanyName;
        existingCompany.CompanyRep = companyDto.CompanyRep;
        existingCompany.Coordinates = companyDto.Coordinates;
        
        await _unitOfWork.Company.EditAsync(existingCompany);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompanyAsync(Guid id)
    {
        var existingCompany = await _unitOfWork.Company.GetAsync(id);
        if (existingCompany is null)
        {
            return NotFound();
        }
        var msg = $"The company({existingCompany.CompanyName}) has been deleted";
        await _unitOfWork.Company.RemoveAsync(existingCompany);
        _notificationService.AddNotification(msg);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}