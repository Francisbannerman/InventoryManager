using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Entities;

public record OutTake
{
    public Guid Id { get; init; }
    public DateTimeOffset DateOfOutTake { get;init; }
    public string CityRep { get; set; }
    public bool? IsPickUpByHubtel { get; set; }
    public string? PickUpByWho { get; set; }
    public string? AttachedImage { get; set; }
    public int Quantity { get; set; }
    public bool IsTakenInPieces { get; set; }
    public bool IsTakenInPackages { get; set; }
    public int? NumberOfPackages { get; set; }
    public int? NumberOfPieces { get; set; }
    public Guid InventoryItemId { get; set; }
    [ForeignKey("InventoryItemId")]
    [ValidateNever]
    public InventoryItem Item { get; set; }
}