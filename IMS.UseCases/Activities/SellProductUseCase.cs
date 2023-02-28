using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities;

public class SellProductUseCase : ISellProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTransactionRepository _productTransactionRepository;

    public SellProductUseCase(IProductTransactionRepository productTransactionRepository,
        IProductRepository productRepository)
    {
        _productTransactionRepository = productTransactionRepository;
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy,
        double unitPrice)
    {
        await _productTransactionRepository.SellProductAsync(salesOrderNumber, product, quantity, doneBy, unitPrice);
        product.Quantity -= quantity;
        await _productRepository.UpdateProductAsync(product);
    }
}