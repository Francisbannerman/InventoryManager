using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Entities;

public record DistributionCenter
{
    public Guid Id { get; init; }
    public string DistributionCenterName { get; set; }
    public string DistributionCenterRep { get; set; }
    public string Coordinates { get; set; }
    
    public Guid? CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    [ValidateNever]
    public Company? Company { get; set; }
    
    public Guid? ZoneId { get; set; }
    [ForeignKey("ZoneId")]
    [ValidateNever]
    public Zone? Zone { get; set; }
    
    public DateTimeOffset CreatedDate { get; init; }
}