﻿using IMS.CoreBusiness;

namespace IMS.UseCases.Reports;

public interface ISearchInventoryTransactionUseCase
{
    Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom,
        DateTime? dateTo, InventoryTransactionType? transactionType);
}