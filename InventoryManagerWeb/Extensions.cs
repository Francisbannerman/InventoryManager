using InventoryManagerWeb.Dtos;
using InventoryManagerWeb.Dtos.CompanyDto;
using InventoryManagerWeb.Dtos.DistributionCenterDto;
using InventoryManagerWeb.Dtos.IntakeDto;
using InventoryManagerWeb.Dtos.InventoryItemDto;
using InventoryManagerWeb.Dtos.NotificationDto;
using InventoryManagerWeb.Dtos.OutTakeDto;
using InventoryManagerWeb.Dtos.PackagingDto;
using InventoryManagerWeb.Dtos.ZoneDto;
using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Services;
using Quartz;
using Quartz.Impl;

namespace InventoryManagerWeb;

public static class Extensions
{
    public static CityDto AsDto(this City city)
    {
        return new CityDto
        {
            Id = city.Id, CityName = city.CityName,
            CityRep = city.CityRep, CreatedDate = city.CreatedDate
        };
    }
    
    public static CompanyDto AsDto(this Company company)
    {
        return new CompanyDto
        {
            Id = company.Id, CompanyName = company.CompanyName,
            CompanyRep = company.CompanyRep, CreatedDate = company.CreatedDate,
            Coordinates = company.Coordinates
        };
    }
    
    public static InventoryItemDto AsDto(this InventoryItem inventoryItem)
    {
        return new InventoryItemDto
        {
            Id = inventoryItem.Id, ItemName = inventoryItem.ItemName,
            ItemSize = inventoryItem.ItemSize, PackagingType = inventoryItem.PackagingType,
            QuantityPerPackage = inventoryItem.QuantityPerPackage,
            CreatedTme = inventoryItem.CreatedTme, PackagingId = inventoryItem.PackagingId,
            UpdateTime = inventoryItem.UpdateTime, Company = inventoryItem.Company,
            CompanyId = inventoryItem.CompanyId, Quantity = inventoryItem.Quantity, 
        };
    }
    
    public static PackagingDto AsDto(this Packaging packaging)
    {
        return new PackagingDto
        {
            Id = packaging.Id, PackagingName = packaging.PackagingName
        };
    }

    public static DistributionCenterDto AsDto(this DistributionCenter distributionCenter)
    {
        return new DistributionCenterDto
        {
            Id = distributionCenter.Id, DistributionCenterRep = distributionCenter.DistributionCenterRep,
            DistributionCenterName = distributionCenter.DistributionCenterName, 
            Coordinates = distributionCenter.Coordinates, CompanyId = distributionCenter.CompanyId,
            Company = distributionCenter.Company, CreatedDate = distributionCenter.CreatedDate,
            Zone = distributionCenter.Zone, ZoneId = distributionCenter.ZoneId
        };
    }

    public static IntakeDto AsDto(this Intake intake)
    {
        return new IntakeDto
        {
            Id = intake.Id, DateOfIntake = intake.DateOfIntake, CompanyId = intake.CompanyId,
            Company = intake.Company, ZoneId = intake.ZoneId, Zone = intake.Zone,
            CompanyDeliveryRep = intake.CompanyDeliveryRep, CityIntakeRep = intake.CityIntakeRep,
            InventoryItemId = intake.InventoryItemId, Item = intake.Item,
            Quantity = intake.Quantity, AttachedImage = intake.AttachedImage,
            UpdatedDateAndTime = intake.UpdatedDateAndTime,
            IsReceivedInPackages = intake.IsReceivedInPackages, IsReceivedInPieces = intake.IsReceivedInPieces,
            NumberOfPackages = intake.NumberOfPackages, NumberOfPieces = intake.NumberOfPieces
        };
    }

    public static OutTakeDto AsDto(this OutTake outTake)
    {
        return new OutTakeDto
        {
            Id = outTake.Id, DateOfOutTake = outTake.DateOfOutTake, CityRep = outTake.CityRep,
            IsPickUpByHubtel = outTake.IsPickUpByHubtel, PickUpByWho = outTake.PickUpByWho,
            AttachedImage = outTake.AttachedImage, Quantity = outTake.Quantity,
            IsTakenInPackages = outTake.IsTakenInPackages, IsTakenInPieces = outTake.IsTakenInPieces,
            NumberOfPieces = outTake.NumberOfPieces, NumberOfPackages = outTake.NumberOfPackages,
            InventoryItemId = outTake.InventoryItemId, Item = outTake.Item
        };
    }

    public static ZoneDto AsDto(this Zone zone)
    {
        return new ZoneDto
        {
            Id = zone.Id, ZoneName = zone.ZoneName, ZoneRep = zone.ZoneRep,
            ZoneCoordinates = zone.ZoneCoordinates, CityId = zone.CityId, City = zone.City,
            CreatedDate = zone.CreatedDate
        };
    }
}