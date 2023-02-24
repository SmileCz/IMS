using IMS.CoreBusiness;
using IMS.UseCases.Activities;

namespace IMS.Plugins.InMemory;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private readonly List<InventoryTransaction> _inventoryTransactions = new();

    public Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price)
    {
        _inventoryTransactions.Add(new InventoryTransaction()
        {
            PoNumber = poNumber,
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

    public Task ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy,
        double price)
    {
        _inventoryTransactions.Add(new InventoryTransaction()
        {
            ProductionNumber = productionNumber,
            InventoryId = inventory.Id,
            QuantityBefore = inventory.Quantity,
            ActivityType = InventoryTransactionType.ProduceProduct,
            QuantityAfter = inventory.Quantity - quantityToConsume,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = price
        });


        return Task.CompletedTask;
    }
}