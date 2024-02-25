using InventoryManagerWeb.Dtos.IntakeDto;
using InventoryManagerWeb.Dtos.OutTakeDto;
using InventoryManagerWeb.Entities;

namespace InventoryManagerWeb.Services;

public class ItemCountService
{
    public int IntakeQuantity(CreateIntakeDto intake, InventoryItem inventoryItem)
    {
        if (intake.IsReceivedInPackages && intake.IsReceivedInPieces)
        {
            return (inventoryItem.QuantityPerPackage * intake.NumberOfPackages) +
                intake.NumberOfPieces ?? 0;
        }
        if (intake.IsReceivedInPackages && !intake.IsReceivedInPieces)
        {
            return inventoryItem.QuantityPerPackage * intake.NumberOfPackages ?? 0;
        }
        if (!intake.IsReceivedInPackages && intake.IsReceivedInPieces)
        {
            return intake.NumberOfPieces ?? 0;
        }
        if (!intake.IsReceivedInPackages && !intake.IsReceivedInPieces)
        {
            throw new ArgumentNullException("intake and inventoryItem cannot be null");
        }
        return 0;
    }
    public int IntakeQuantity(UpdateIntakeDto intake, InventoryItem inventoryItem)
    {
        if (intake.IsReceivedInPackages && intake.IsReceivedInPieces)
        {
            return (inventoryItem.QuantityPerPackage * intake.NumberOfPackages) +
                intake.NumberOfPieces ?? 0;
        }
        if (intake.IsReceivedInPackages && !intake.IsReceivedInPieces)
        {
            return inventoryItem.QuantityPerPackage * intake.NumberOfPackages ?? 0;
        }
        if (!intake.IsReceivedInPackages && intake.IsReceivedInPieces)
        {
            return intake.NumberOfPieces ?? 0;
        }
        if (!intake.IsReceivedInPackages && !intake.IsReceivedInPieces)
        {
            throw new ArgumentNullException("intake and inventoryItem cannot be null");
        }
        return 0;
    }
    
    public int OutTakeQuantity(CreateOutTakeDto outTake, InventoryItem inventoryItem)
    {
        if (outTake.IsTakenInPackages && outTake.IsTakenInPieces)
        {
            return (inventoryItem.QuantityPerPackage * outTake.NumberOfPackages) +
                outTake.NumberOfPieces ?? 0;
        }
        if (outTake.IsTakenInPackages && !outTake.IsTakenInPieces)
        {
            return inventoryItem.QuantityPerPackage * outTake.NumberOfPackages ?? 0;
        }
        if (!outTake.IsTakenInPackages && outTake.IsTakenInPieces)
        {
            return outTake.NumberOfPieces ?? 0;
        }
        if (!outTake.IsTakenInPackages && !outTake.IsTakenInPieces)
        {
            throw new ArgumentNullException("intake and inventoryItem cannot be null");
        }
        return 0;
    }
    public int OutTakeQuantity(UpdateOutTakeDto outTake, InventoryItem inventoryItem)
    {
        if (outTake.IsTakenInPackages && outTake.IsTakenInPieces)
        {
            return (inventoryItem.QuantityPerPackage * outTake.NumberOfPackages) +
                outTake.NumberOfPieces ?? 0;
        }
        if (outTake.IsTakenInPackages && !outTake.IsTakenInPieces)
        {
            return inventoryItem.QuantityPerPackage * outTake.NumberOfPackages ?? 0;
        }
        if (!outTake.IsTakenInPackages && outTake.IsTakenInPieces)
        {
            return outTake.NumberOfPieces ?? 0;
        }
        if (!outTake.IsTakenInPackages && !outTake.IsTakenInPieces)
        {
            throw new ArgumentNullException("intake and inventoryItem cannot be null");
        }
        return 0;
    }
}