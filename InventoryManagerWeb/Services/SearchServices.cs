using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

namespace InventoryManagerWeb.Services;

public class SearchServices
{
    private readonly IUnitOfWork _unitOfWork;
    public SearchServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task <List<InventoryItem>> SearchInventoryItem(string itemName)
    {
        var allItems = (await _unitOfWork.InventoryItem.GetAllAsync()).Where(item =>
            (string.IsNullOrEmpty(itemName) || item.ItemName.Contains(itemName))).ToList();
        return allItems;
    }
    
    public async Task <List<Company>> SearchCompany(string companyName)
    {
        var allCompanies = (await _unitOfWork.Company.GetAllAsync()).Where(item =>
            (string.IsNullOrEmpty(companyName) || item.CompanyName.Contains(companyName))).ToList();
        return allCompanies;
    }
    
    public async Task <List<Intake>> SearchIntakeByCompany(string companyName)
    {
        var allIntakesByCompany = (await _unitOfWork.Intake.GetAllAsync()).Where(item =>
            (item.Company.CompanyName == companyName || item.Company.CompanyName.Contains(companyName))).ToList();
        return allIntakesByCompany;
    }
    
    public async Task <List<Intake>> SearchIntakeByItemName(string itemName)
    {
        var allIntakesByCompany = (await _unitOfWork.Intake.GetAllAsync()).Where(item =>
            (item.Item.ItemName == itemName || item.Item.ItemName.Contains(itemName))).ToList();
        return allIntakesByCompany;
    }
    
    public async Task <List<OutTake>> SearchOutTakeByCityRep(string cityRepName)
    {
        var allOutTakesByCityRep = (await _unitOfWork.OutTake.GetAllAsync()).Where(item =>
            (item.CityRep == cityRepName || item.CityRep.Contains(cityRepName))).ToList();
        return allOutTakesByCityRep;
    }
    
    public async Task <List<Intake>> SearchIntakeByDate(DateTimeOffset date)
    {
        var allIntakesByDate = (await _unitOfWork.Intake.GetAllAsync()).Where(item =>
            (item.DateOfIntake == date )).ToList();
        return allIntakesByDate;
    }
    
    public async Task <List<OutTake>> SearchOuttakeByDate(DateTimeOffset date)
    {
        var allOutTakesByCompany = (await _unitOfWork.OutTake.GetAllAsync()).Where(item =>
            (item.DateOfOutTake == date )).ToList();
        return allOutTakesByCompany;
    }
}