using System.ComponentModel.DataAnnotations;
using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Dtos.CompanyDto;

public class UpdateCompanyDto
{
    public string CompanyName { get; set; }
    public string CompanyRep { get; set; }
    public string Coordinates { get; set; }

}