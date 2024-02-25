namespace InventoryManagerWeb.Entities;

public record Packaging
{
    public Guid Id { get; init; }
    public string PackagingName { get; set; }
}