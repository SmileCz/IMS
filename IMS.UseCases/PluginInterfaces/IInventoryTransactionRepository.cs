﻿using IMS.CoreBusiness;

namespace IMS.UseCases.Activities;

public interface IInventoryTransactionRepository
{
    Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price);
    Task ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, string doneBy, double price);

    Task<IEnumerable<InventoryTransaction>> GetInventoryTransactions(string inventoryName, DateTime? dateFrom,
        DateTime? dateTo, InventoryTransactionType? transactionType);
}