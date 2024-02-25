namespace InventoryManagerWeb.Dtos.OutTakeDto;

public record UpdateOutTakeDto
{
    public string CityRep { get; init; }
    public bool? IsPickUpByHubtel { get; init; }
    public string? PickUpByWho { get; init; }
    public string? AttachedImage { get; init; }
    public bool IsTakenInPackages { get; set; }
    public bool IsTakenInPieces { get; set; }
    public int? NumberOfPackages { get; set; }
    public int? NumberOfPieces { get; set; }
    public Guid InventoryItemId { get; set; }
}