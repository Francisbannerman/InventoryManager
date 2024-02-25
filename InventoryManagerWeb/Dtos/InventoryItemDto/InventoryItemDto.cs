using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.InventoryItemDto;

public record InventoryItemDto
{
    public Guid Id { get; init; }
    public string ItemName { get; set; }
    public string ItemSize { get; set; }
    public Guid? PackagingId { get; set; }
    [ForeignKey("PackagingId")]
    [ValidateNever]
    public Packaging? PackagingType { get; set; }
    public Guid? CompanyId { get; set; } 
    [ForeignKey("CompanyId")]
    [ValidateNever]
    public Company? Company { get; set; } 
    public int? QuantityPerPackage { get; set; }
    public int? Quantity { get; set; }
    public DateTimeOffset CreatedTme { get; init; }
    public DateTimeOffset UpdateTime { get; init; }
}