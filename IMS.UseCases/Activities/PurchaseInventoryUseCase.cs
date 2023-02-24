using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities;

public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryTransactionRepository _inventoryTransactionRepository;

    public PurchaseInventoryUseCase(IInventoryTransactionRepository inventoryTransactionRepository,
        IInventoryRepository inventoryRepository)
    {
        _inventoryTransactionRepository = inventoryTransactionRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy)
    {
        // insert record in a transaction table
        await _inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, doneBy, inventory.Price);
        // update inventory quantity
        inventory.Quantity += quantity;
        await _inventoryRepository.UpdateInventoryAsync(inventory);
    }
}