using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Settings;

namespace InventoryManagerWeb.Repositories.PostgresDbs;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public ICityRepository City { get; init; }
    public ICompanyRepository Company { get; init; }
    public IInventoryItemRepository InventoryItem { get; init; }
    public IPackagingRepository Packaging { get; init; }
    public IDistributionCenterRepository DistributionCenter { get; init; }
    public IIntakeRepository Intake { get; init; }
    public IOutTakeRepository OutTake { get; init; }
    public IZoneRepository Zone { get; init; }
    public INotificationRepository Notification { get; init; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        City = new CityRepository(_db);
        Company = new CompanyRepository(_db);
        InventoryItem = new InventoryItemRepository(_db);
        Packaging = new PackagingRepository(_db);
        DistributionCenter = new DistributionCenterRepository(_db);
        Intake = new IntakeRepository(_db);
        OutTake = new OutTakeRepository(_db);
        Zone = new ZoneRepository(_db);
        Notification = new NotificationRepository(_db);
    }

    public async Task<int> SaveAsync()
    {
        return await _db.SaveChangesAsync();
    }
}