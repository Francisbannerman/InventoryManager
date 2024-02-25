namespace InventoryManagerWeb.Entities;

public record Company
{
    public Guid Id { get; init; }
    public string CompanyName { get; set; }
    public string CompanyRep { get; set; }
    public string Coordinates { get; set; }
    public DateTimeOffset CreatedDate { get; init; }
}