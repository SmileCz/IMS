using IMS.CoreBusiness;

namespace IMS.UseCases.Activities;

public interface IInventoryTransactionRepository
{
    Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price);
}