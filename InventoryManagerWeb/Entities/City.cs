namespace InventoryManagerWeb.Entities;

public record City
{
    public Guid Id { get; init; }
    public string CityName { get; set; }
    public string CityRep { get; set; }
    public DateTimeOffset CreatedDate { get; init; }
}