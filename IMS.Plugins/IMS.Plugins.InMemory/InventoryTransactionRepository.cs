using IMS.CoreBusiness;
using IMS.UseCases.Activities;

namespace IMS.Plugins.InMemory;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private List<InventoryTransaction> _inventoryTransactions = new();

    public Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price)
    {
        _inventoryTransactions.Add(new InventoryTransaction()
        {
            PONumber = poNumber,
            InventoryId = inventory.Id,
            QuantityBefore = inventory.Quantity,
            ActivityType = InventoryTransactionType.PurchaseInventory,
            QuantityAfter = inventory.Quantity + quantity,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = price
        });

        return Task.CompletedTask;
    }
}