using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IProductTransactionRepository
{
    Task ProductAsync(string productionNumber, Product product, int quantity, string doneBy);
}