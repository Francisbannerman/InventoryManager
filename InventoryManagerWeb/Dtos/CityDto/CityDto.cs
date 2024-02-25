using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Dtos;

public record CityDto
{
    public Guid Id { get; init; }
    public string CityName { get; init; }
    public string CityRep { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}