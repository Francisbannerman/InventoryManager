using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;

namespace InventoryManagerWeb.Services;

public class NotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    public NotificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddNotification(string? msg)
    {
        var notification = new Notification
        {
            notificationId = Guid.NewGuid(), DateTimeSent = DateTimeOffset.Now,
            Message = msg
        };
        _unitOfWork.Notification.AddAsync(notification);
    }

    public void RemoveNotification(Guid id)
    {
        var existingNotification =  _unitOfWork.Notification.Get(
            u => u.notificationId == id);
        _unitOfWork.Notification.RemoveAsync(existingNotification);
    }

    public void GetNotification(Guid id)
    {
        var existingNotification =  _unitOfWork.Notification.Get(
            u => u.notificationId == id);
    }

    public async Task CountNotification()
    {
        var num = await _unitOfWork.Notification.CountAsync();
    }
}