using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagerWeb.Dtos.DistributionCenterDto;

public record CreateDistributionCenterDto
{
    public string DistributionCenterName { get; set; }
    public string DistributionCenterRep { get; set; }
    public string Coordinates { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? ZoneId { get; set; }
}