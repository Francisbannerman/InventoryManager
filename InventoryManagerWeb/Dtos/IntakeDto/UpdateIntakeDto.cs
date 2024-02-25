using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.IntakeDto;

public record UpdateIntakeDto
{
    public Guid CompanyId { get; set; }
    public Guid ZoneId { get; set; }
    public string CompanyDeliveryRep { get; set; }
    public string CityIntakeRep { get; set; }
    public Guid InventoryItemId { get; set; }
    public string AttachedImage { get; set; }
    public int? NumberOfPackages { get; set; }
    public int? NumberOfPieces { get; set; }
    public bool IsReceivedInPackages { get; set; }
    public bool IsReceivedInPieces { get; set; }
}