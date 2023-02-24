using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Activities;

public class ProduceProductUseCase : IProduceProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTransactionRepository _productTransactionRepository;

    public ProduceProductUseCase(IProductTransactionRepository productTransactionRepository,
        IProductRepository productRepository)
    {
        _productTransactionRepository = productTransactionRepository;
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        //add transaction record;
        await _productTransactionRepository.ProductAsync(productionNumber, product, quantity, doneBy);

        //decrease the quantity inventories
        //update the quantity of product
        product.Quantity += quantity;
        await _productRepository.UpdateProductAsync(product);
    }
}