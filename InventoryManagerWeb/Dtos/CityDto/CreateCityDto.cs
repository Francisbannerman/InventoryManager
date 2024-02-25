using System.ComponentModel.DataAnnotations;

namespace InventoryManagerWeb.Dtos;

public class CreateCityDto
{
    [Required]
    public string CityName { get; init; }
    [Required]
    public string CityRep { get; init; }
}