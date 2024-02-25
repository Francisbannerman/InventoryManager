using InventoryManagerWeb.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerWeb.Settings;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Zone> Zones { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<Packaging> Packagings { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<DistributionCenter> DistributionCenters { get; set; }
    public DbSet<Intake> Intakes { get; set; }
    public DbSet<OutTake> OutTakes { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    // public List<City> cityList()
    // {
    //     List<City> cities = new List<City>();
    //     cities.Add(new City{Id = Guid.NewGuid(), CityName = "City101", 
    //         CityRep = "rep101", CreatedDate = DateTimeOffset.Now});
    //     cities.Add(new City{Id = Guid.NewGuid(), CityName = "City102", 
    //         CityRep = "rep102", CreatedDate = DateTimeOffset.Now});
    //     cities.Add(new City{Id = Guid.NewGuid(), CityName = "City103", 
    //         CityRep = "rep103", CreatedDate = DateTimeOffset.Now});
    //     return cities;
    // }
    //
    // public List<Company> companyList()
    // {
    //     List<Company> companies = new List<Company>();
    //     companies.Add(new Company{Id = Guid.NewGuid(),CompanyName = "companyName101", 
    //         CompanyRep = "rep101", Coordinates = "1837-873", CreatedDate = DateTimeOffset.Now});
    //     companies.Add(new Company{Id = Guid.NewGuid(),CompanyName = "companyName102", 
    //         CompanyRep = "rep102", Coordinates = "7379-543", CreatedDate = DateTimeOffset.Now});
    //     companies.Add(new Company{Id = Guid.NewGuid(),CompanyName = "companyName103", 
    //         CompanyRep = "rep103", Coordinates = "4390-925", CreatedDate = DateTimeOffset.Now});
    //     return companies;
    // }
    //
    // public List<InventoryItem> inventoryItemList()
    // {
    //     List<InventoryItem> inventoryItems = new List<InventoryItem>();
    //     inventoryItems.Add(new InventoryItem{Id = Guid.NewGuid(), ItemName = "item101", ItemSize = "5g",
    //         QuantityPerPackage = 6, CreatedTme = DateTimeOffset.Now});
    //     inventoryItems.Add(new InventoryItem{Id = Guid.NewGuid(), ItemName = "item102", ItemSize = "7g",
    //         QuantityPerPackage = 16, CreatedTme = DateTimeOffset.Now});
    //     inventoryItems.Add(new InventoryItem{Id = Guid.NewGuid(), ItemName = "item103", ItemSize = "9g",
    //         QuantityPerPackage = 26, CreatedTme = DateTimeOffset.Now});
    //     return inventoryItems;
    // }
    //
    // public List<Packaging> packagingList()
    // {
    //     List<Packaging> packagings = new List<Packaging>();
    //     packagings.Add(new Packaging{Id = Guid.NewGuid(), PackagingName = "Packaging101"});
    //     packagings.Add(new Packaging{Id = Guid.NewGuid(), PackagingName = "Packaging102"});
    //     packagings.Add(new Packaging{Id = Guid.NewGuid(), PackagingName = "Packaging103"});
    //     return packagings;
    // }
    //
    // public List<ApplicationUser> applicationUserList()
    // {
    //     List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
    //     applicationUsers.Add(new ApplicationUser{Id = Guid.NewGuid(), Name = "MeThat", 
    //         userDateJoined = DateTimeOffset.Now});
    //     return applicationUsers;
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Packaging>().HasData(packagingList());
        // modelBuilder.Entity<InventoryItem>().HasData(inventoryItemList());
        // modelBuilder.Entity<City>().HasData(cityList());
        // modelBuilder.Entity<ApplicationUser>().HasData(applicationUserList());
        // modelBuilder.Entity<Company>().HasData(companyList());

        base.OnModelCreating(modelBuilder);
    }
}
