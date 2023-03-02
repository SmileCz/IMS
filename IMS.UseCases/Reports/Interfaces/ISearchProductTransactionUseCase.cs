using IMS.CoreBusiness;

namespace IMS.UseCases.Reports;

public interface ISearchProductTransactionUseCase
{
    Task<IEnumerable<ProductTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom,
        DateTime? dateTo, ProductTransactionType? transactionType);
}