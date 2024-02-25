using System.ComponentModel.DataAnnotations;

namespace InventoryManagerWeb.Dtos.PackagingDto;

public class CreatePackagingDto
{
    [Required]
    public string PackagingName { get; set; }
}