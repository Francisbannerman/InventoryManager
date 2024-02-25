using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Entities;

public class ApplicationUser : IdentityUser
{
    [Required]
    public Guid Id { get; init; }
    [Required]
    public string Name { get; init; }
    
    public Guid? ZoneId { get; init; }
    [ForeignKey("ZoneId")]
    [ValidateNever]
    public Zone? Zone { get; init; }
    
    public string? Email { get; init; }
    public string? Role { get; init; }
    public DateTimeOffset userDateJoined { get; init; }
}