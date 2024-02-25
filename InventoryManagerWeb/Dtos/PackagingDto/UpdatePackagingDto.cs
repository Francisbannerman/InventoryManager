using System.ComponentModel.DataAnnotations;

namespace InventoryManagerWeb.Dtos.PackagingDto;

public class UpdatePackagingDto
{
    [Required]
    public string PackagingName { get; set; }
}