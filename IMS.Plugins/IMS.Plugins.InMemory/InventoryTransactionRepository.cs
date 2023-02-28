using IMS.CoreBusiness;
using IMS.UseCases.Activities;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly List<InventoryTransaction> _inventoryTransactions = new();

    public InventoryTransactionRepository(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

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

    public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactions(string inventoryName,
        DateTime? dateFrom, DateTime? dateTo,
        InventoryTransactionType? transactionType)
    {
        var inventories = (await _inventoryRepository.GetInventoriesByNameAsync(string.Empty)).ToList();

        var query = from it in _inventoryTransactions
            join inv in inventories on it.InventoryId equals inv.Id
            where
                (string.IsNullOrWhiteSpace(inventoryName) || inv.Name.ToLower().IndexOf(inventoryName.ToLower()) >= 0)
                &&
                (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date)
                &&
                (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date)
                &&
                (!transactionType.HasValue || transactionType.Value == it.ActivityType)
            select new InventoryTransaction
            {
                Inventory = inv,
                ActivityType = it.ActivityType,
                DoneBy = it.DoneBy,
                Id = it.Id,
                InventoryId = it.InventoryId,
                PoNumber = it.PoNumber,
                QuantityBefore = it.QuantityBefore,
                QuantityAfter = it.QuantityAfter,
                ProductionNumber = it.ProductionNumber,
                TransactionDate = it.TransactionDate,
                UnitPrice = it.UnitPrice
            };
        return query;
    }
}