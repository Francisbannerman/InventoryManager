using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.OutTakeDto;

public record OutTakeDto
{
    public Guid Id { get; init; }
    public DateTimeOffset DateOfOutTake { get;init; }
    public string CityRep { get; init; }
    public bool? IsPickUpByHubtel { get; init; }
    public string? PickUpByWho { get; init; }
    public string? AttachedImage { get; init; }
    public int Quantity { get; init; }
    public bool? IsTakenInPackages { get; set; }
    public bool? IsTakenInPieces { get; set; }
    public int? NumberOfPackages { get; set; }
    public int? NumberOfPieces { get; set; }
    public Guid InventoryItemId { get; set; }
    [ForeignKey("InventoryItemId")]
    [ValidateNever]
    public InventoryItem Item { get; set; }
}