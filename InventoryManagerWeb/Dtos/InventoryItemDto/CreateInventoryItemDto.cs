using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.InventoryItemDto;

public class CreateInventoryItemDto
{
    public string ItemName { get; set; }
    public string ItemSize { get; set; }
    public Guid? PackagingId { get; set; }
    public Guid? CompanyId { get; set; }
    public int? QuantityPerPackage { get; set; }
}