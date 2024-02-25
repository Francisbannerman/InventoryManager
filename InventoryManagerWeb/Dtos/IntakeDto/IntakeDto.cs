using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.IntakeDto;

public record IntakeDto
{
    public Guid Id { get; init; }
    public DateTimeOffset DateOfIntake { get; init; }
    
    public Guid CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    [ValidateNever]
    public Company Company { get; set; }
    
    public Guid ZoneId { get; set; }
    [ForeignKey("ZoneId")]
    [ValidateNever]
    public Zone Zone { get; set; }
    
    public string CompanyDeliveryRep { get; set; }
    public string CityIntakeRep { get; set; }
    
    public Guid InventoryItemId { get; set; }
    [ForeignKey("InventoryItemId")]
    [ValidateNever]
    public InventoryItem Item { get; set; }
    
    public string AttachedImage { get; set; }
    public int? Quantity { get; set; }
    public int? NumberOfPackages { get; set; }
    public int? NumberOfPieces { get; set; }
    public bool IsReceivedInPackages { get; set; }
    public bool IsReceivedInPieces { get; set; }
    public DateTimeOffset? UpdatedDateAndTime { get; set; }
}