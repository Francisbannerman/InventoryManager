using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Entities;

public record InventoryItem
{
    private int? _quantityPerPackage;
    public InventoryItem()
    {
        _quantityPerPackage = 1;
    }
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

    public int? QuantityPerPackage
    {
        get { return _quantityPerPackage ?? 1; }
        set { _quantityPerPackage = value; }
    }
    public int? Quantity { get; set; }
    public DateTimeOffset CreatedTme { get; init; }
    public DateTimeOffset UpdateTime { get; set; }
}