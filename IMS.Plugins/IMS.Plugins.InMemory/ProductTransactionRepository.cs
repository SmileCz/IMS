using IMS.CoreBusiness;
using IMS.UseCases.Activities;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class ProductTransactionRepository : IProductTransactionRepository
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryTransactionRepository _inventoryTransactionRepository;


    private readonly IProductRepository _productRepository;
    private readonly List<ProductTransaction> _productTransactions = new();

    public ProductTransactionRepository(IProductRepository productRepository,
        IInventoryTransactionRepository inventoryTransactionRepository, IInventoryRepository inventoryRepository)
    {
        _productRepository = productRepository;
        _inventoryTransactionRepository = inventoryTransactionRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task ProductAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        //decrease the inventories
        var currentProduct = await _productRepository.GetProductByIdAsync(product.Id);
        if (currentProduct is not null)
        {
            foreach (var productInventory in currentProduct.ProductInventories)
            {
                if (productInventory.Inventory is null) return;
                await _inventoryTransactionRepository.ProduceAsync(productionNumber,
                    productInventory.Inventory,
                    productInventory.InventoryQuality * quantity,
                    doneBy,
                    -1);
                var currentInventory = await _inventoryRepository.GetInventoriesByIdAsync(productInventory.InventoryId);
                currentInventory.Quantity -= productInventory.InventoryQuality * quantity;
                await _inventoryRepository.UpdateInventoryAsync(currentInventory);
            }
        }

        //add product transaction
        _productTransactions.Add(new ProductTransaction()
        {
            ProductionNumber = productionNumber,
            ProductId = product.Id,
            QuantityBefore = product.Quantity,
            ActivityType = ProductTransactionType.ProduceProduct,
            QuantityAfter = product.Quantity + quantity,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy
        });
        //update iventory (decrease amoung of unit in WH)
    }

    public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, string doneBy,
        double unitPrice)
    {
        _productTransactions.Add(new ProductTransaction()
        {
            SoNumber = salesOrderNumber,
            ProductId = product.Id,
            QuantityBefore = product.Quantity,
            ActivityType = ProductTransactionType.SaleProduct,
            QuantityAfter = product.Quantity - quantity,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = unitPrice
        });

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<ProductTransaction>> GetProductionTransactions(string productName, DateTime? dateFrom,
        DateTime? dateTo,
        ProductTransactionType? transactionType)
    {
        var products = (await _productRepository.GetProductsByNameAsync(string.Empty)).ToList();

        var query = from pt in _productTransactions
            join prod in products on pt.ProductId equals prod.Id
            where
                (string.IsNullOrWhiteSpace(productName) ||
                 prod.Name.ToLower().IndexOf(productName.ToLower(), StringComparison.Ordinal) >= 0)
                &&
                (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date)
                &&
                (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date)
                &&
                (!transactionType.HasValue || transactionType.Value == pt.ActivityType)
            select new ProductTransaction()
            {
                Product = prod,
                ActivityType = pt.ActivityType,
                DoneBy = pt.DoneBy,
                Id = pt.Id,
                ProductId = pt.ProductId,
                SoNumber = pt.SoNumber,
                QuantityBefore = pt.QuantityBefore,
                QuantityAfter = pt.QuantityAfter,
                ProductionNumber = pt.ProductionNumber,
                TransactionDate = pt.TransactionDate,
                UnitPrice = pt.UnitPrice
            };
        return query;
    }
}