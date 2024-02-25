namespace InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

public interface IUnitOfWork
{
    ICityRepository City { get; }
    ICompanyRepository Company { get; }
    IInventoryItemRepository InventoryItem { get; }
    IPackagingRepository Packaging { get; }
    IDistributionCenterRepository DistributionCenter { get; }
    IIntakeRepository Intake { get; }
    IOutTakeRepository OutTake { get; }
    IZoneRepository Zone { get; }
    INotificationRepository Notification { get; }
    Task<int> SaveAsync();
}