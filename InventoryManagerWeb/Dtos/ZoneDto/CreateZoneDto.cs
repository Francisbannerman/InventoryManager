using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.ZoneDto;

public record CreateZoneDto
{
    public string ZoneName { get; set; }
    public string ZoneRep { get; set; }
    public string? ZoneCoordinates { get; set; }
    public Guid? CityId { get; set; }
}