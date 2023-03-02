using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IProductTransactionRepository
{
    Task ProductAsync(string productionNumber, Product product, int quantity, string doneBy);
    Task SellProductAsync(string salesOrderNumber, Product product, int quantity, string doneBy, double unitPrice);

    Task<IEnumerable<ProductTransaction>> GetProductionTransactions(string productName, DateTime? dateFrom,
        DateTime? dateTo, ProductTransactionType? transactionType);
}