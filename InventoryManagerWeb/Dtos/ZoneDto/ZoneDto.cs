using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.ZoneDto;

public record ZoneDto
{
    public Guid Id { get; init; }
    public string ZoneName { get; set; }
    public string ZoneRep { get; set; }
    public string? ZoneCoordinates { get; set; }
    
    public Guid? CityId { get; set; }
    [ForeignKey("CityId")]
    [ValidateNever]
    public City? City { get; set; }
    
    public DateTimeOffset CreatedDate { get; init; }
}