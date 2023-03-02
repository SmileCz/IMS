using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Reports;

public class SearchProductTransactionUseCase : ISearchProductTransactionUseCase
{
    private readonly IProductTransactionRepository _productTransactionRepository;

    public SearchProductTransactionUseCase(IProductTransactionRepository productTransactionRepository)
    {
        _productTransactionRepository = productTransactionRepository;
    }

    public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom,
        DateTime? dateTo, ProductTransactionType? transactionType)
    {
        if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
        return await _productTransactionRepository.GetProductionTransactions(inventoryName, dateFrom, dateTo,
            transactionType);
    }
}