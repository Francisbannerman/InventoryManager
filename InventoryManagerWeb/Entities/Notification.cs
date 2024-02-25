namespace InventoryManagerWeb.Entities;

public record Notification
{
    public Guid notificationId { get; init; }
    public DateTimeOffset DateTimeSent { get; init; }
    public string Message { get; set; }
}