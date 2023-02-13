using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class ProductRepository : IProductRepository
{
    private List<Product> _products;

    public ProductRepository()
    {
        _products = new List<Product>()
        {
            new() { Id = 1, Name = "Bike", Quantity = 10, Price = 150 },
            new() { Id = 2, Name = "Car", Quantity = 5, Price = 25000 },
        };
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);

        return await Task.FromResult<IEnumerable<Product>>(_products
            .Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList());
    }

    public Task AddProductAsync(Product product)
    {
        if (_products.Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
            return Task.CompletedTask;

        var maxId = _products.Max(x => x.Id);
        product.Id = maxId + 1;

        _products.Add(product);

        return Task.CompletedTask;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var product = _products.First(x => x.Id == productId);
        var newProduct = new Product();
        newProduct.Id = product.Id;
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.Quantity = product.Quantity;
        newProduct.ProductInventories = new();

        if (product.ProductInventories.Count <= 0) return await Task.FromResult(newProduct);

        foreach (var prodInv in product.ProductInventories)
        {
            var newProdInv = new ProductInventory
            {
                InventoryId = prodInv.InventoryId,
                ProductId = prodInv.ProductId,
                Product = product,
                Inventory = new Inventory(),
                InventoryQuality = prodInv.InventoryQuality
            };
            if (prodInv.Inventory is not null)
            {
                newProdInv.Inventory.Id = prodInv.Inventory.Id;
                newProdInv.Inventory.Name = prodInv.Inventory.Name;
                newProdInv.Inventory.Quantity = prodInv.Inventory.Quantity;
                newProdInv.Inventory.Price = prodInv.Inventory.Price;
            }

            newProduct.ProductInventories.Add(newProdInv);
        }

        return await Task.FromResult(newProduct);
    }

    public Task UpdateProductAsync(Product product)
    {
        if (_products.Any(x => x.Id != product.Id && x.Name.ToLower() == product.Name.ToLower()))
            return Task.CompletedTask;


        var prod = _products.FirstOrDefault(x => x.Id == product.Id);

        if (prod is not null)
        {
            prod.Name = product.Name;
            prod.Price = product.Price;
            prod.Quantity = product.Quantity;
            prod.ProductInventories = prod.ProductInventories;
        }

        return Task.CompletedTask;
    }
}