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

    public Task<bool> ExistsAsync(Product product)
    {
        throw new NotImplementedException();
    }
}