using InventoryManagerWeb.Entities;
using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

// using OfficeOpenXml;

namespace InventoryManagerWeb.Services;

public class ExcelReportService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ExcelReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<byte[]>  DownloadSummary()
    {
        var inventoryItems = await _unitOfWork.InventoryItem.GetAllAsync();
        var intakes =await  _unitOfWork.Intake.GetAllAsync();
        var outTakes =await _unitOfWork.OutTake.GetAllAsync();

        using (var package = new ExcelPackage())
        {
            var inventoryItemsWorksheet = package.Workbook.Worksheets.Add("InventoryItems");
            inventoryItemsWorksheet.Cells[1, 1].Value = "Item Was Created On";
            inventoryItemsWorksheet.Cells[1, 2].Value = "Item Name";
            inventoryItemsWorksheet.Cells[1, 3].Value = "Company";
            inventoryItemsWorksheet.Cells[1, 4].Value = "Quantity In Stock At Zonal Offices";
            var itemsRow = 2;
            foreach (var items in inventoryItems)
            {
                inventoryItemsWorksheet.Cells[itemsRow, 1].Value = items.CreatedTme;
                inventoryItemsWorksheet.Cells[itemsRow, 2].Value = items.ItemName;

                var company = await _unitOfWork.Company.GetAsync(items.CompanyId ?? Guid.Empty);
                inventoryItemsWorksheet.Cells[itemsRow, 3].Value = company.CompanyName;
                
                inventoryItemsWorksheet.Cells[itemsRow, 4].Value = items.Quantity;
                itemsRow++;
            }

            var intakesWorksheet = package.Workbook.Worksheets.Add("Intakes");
            intakesWorksheet.Cells[1, 1].Value = "Date Of Intake";
            intakesWorksheet.Cells[1, 2].Value = "Company";
            intakesWorksheet.Cells[1, 3].Value = "Zone";
            intakesWorksheet.Cells[1, 4].Value = "InventoryItem";
            intakesWorksheet.Cells[1, 5].Value = "Quantity";
            intakesWorksheet.Cells[1, 6].Value = "Company Delivery Executive";
            intakesWorksheet.Cells[1, 7].Value = "City Intake Executive";
            var intakesRow = 2;
            foreach (var intake in intakes)
            {
                intakesWorksheet.Cells[intakesRow, 1].Value = intake.DateOfIntake;

                var company = await _unitOfWork.Company.GetAsync(intake.CompanyId);
                intakesWorksheet.Cells[intakesRow, 2].Value = company.CompanyName;

                var zone = await _unitOfWork.Zone.GetAsync(intake.ZoneId);
                intakesWorksheet.Cells[intakesRow, 3].Value = zone.ZoneName;

                var item = await _unitOfWork.InventoryItem.GetAsync(intake.InventoryItemId);
                intakesWorksheet.Cells[intakesRow, 4].Value = item.ItemName;
                
                intakesWorksheet.Cells[intakesRow, 5].Value = intake.Quantity;
                intakesWorksheet.Cells[intakesRow, 6].Value = intake.CompanyDeliveryRep;
                intakesWorksheet.Cells[intakesRow, 7].Value = intake.CityIntakeRep;
                intakesRow++;
            }

            var outTakesWorksheet = package.Workbook.Worksheets.Add("OutTakes");
            outTakesWorksheet.Cells[1, 1].Value = "Date Of OutTake";
            outTakesWorksheet.Cells[1, 2].Value = "Inventory Item";
            outTakesWorksheet.Cells[1, 3].Value = "Quantity";
            outTakesWorksheet.Cells[1, 4].Value = "Pick Up By Hubtel";
            outTakesWorksheet.Cells[1, 5].Value = "Pick Up By Who";
            outTakesWorksheet.Cells[1, 6].Value = "City OutTake Executive";
            var outTakeRow = 2;
            foreach (var outTake in outTakes)
            {
                outTakesWorksheet.Cells[outTakeRow, 1].Value = outTake.DateOfOutTake;

                var item = await _unitOfWork.InventoryItem.GetAsync(outTake.InventoryItemId);
                outTakesWorksheet.Cells[outTakeRow, 2].Value = item.ItemName;
                
                outTakesWorksheet.Cells[outTakeRow, 3].Value = outTake.Quantity;
                outTakesWorksheet.Cells[outTakeRow, 4].Value = outTake.IsPickUpByHubtel;
                outTakesWorksheet.Cells[outTakeRow, 5].Value = outTake.PickUpByWho;
                outTakesWorksheet.Cells[outTakeRow, 6].Value = outTake.CityRep;
                outTakeRow++;
            }
            byte[] excelBytes = package.GetAsByteArray();
            return excelBytes;
        }
    }
}