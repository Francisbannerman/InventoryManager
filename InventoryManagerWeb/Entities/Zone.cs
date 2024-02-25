using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Entities;

public record Zone
{
    public Guid Id { get; init; }
    public string ZoneName { get; set; }
    public string ZoneRep { get; set; }
    public string? ZoneCoordinates { get; set; }
    
    public Guid? CityId { get; set; }
    // [ForeignKey("CityId")]
    [ValidateNever]
    public City? City { get; set; }
    
    public DateTimeOffset CreatedDate { get; init; }
}