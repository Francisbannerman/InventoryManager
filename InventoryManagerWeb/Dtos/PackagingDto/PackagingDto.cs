namespace InventoryManagerWeb.Dtos.PackagingDto;

public record PackagingDto
{
    public Guid Id { get; set; }
    public string PackagingName { get; set; }
}