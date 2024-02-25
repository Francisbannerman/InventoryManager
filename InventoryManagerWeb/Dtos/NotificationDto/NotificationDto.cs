namespace InventoryManagerWeb.Dtos.NotificationDto;

public class NotificationDto
{
    public Guid notificationId { get; init; }
    public DateTimeOffset DateTimeSent { get; init; }
    public string Message { get; set; }
    public int Count { get; set; }
}