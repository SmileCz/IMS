using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory;

public class InventoryRepository :  IInventoryRepository
{

    private List<Inventory> _inventories;

    public InventoryRepository()
    {
        _inventories = new List<Inventory> {
            new() { Id = 1, Name = "Bike Seat",Quantity = 10,Price = 2},
            new() { Id = 2, Name = "Bike Body",Quantity = 10,Price = 15},
            new() { Id = 3, Name = "Bike Wheels",Quantity = 20,Price = 8},
            new() { Id = 4, Name = "Bike Pedels",Quantity = 20,Price = 1}
        };
    }
    public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);

        return await Task.FromResult(_inventories.Where(x =>
            x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
    }

    public Task AddInventoryAsync(Inventory inventory)
    {
        if (_inventories.Any(x => x.Name.Equals(inventory.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }

        var maxId = _inventories.Max(x => x.Id);
        inventory.Id = maxId + 1;
        _inventories.Add(inventory);

        return Task.CompletedTask;
    }

    public Task UpdateInventoryAsync(Inventory inventory)
    {
        if (_inventories.Any(x =>
                x.Id != inventory.Id && x.Name.Equals(inventory.Name, StringComparison.OrdinalIgnoreCase)))
            return Task.CompletedTask;

        var inv = _inventories.FirstOrDefault(x => x.Id == inventory.Id);
        if (inv == null) return Task.CompletedTask;
        inv.Name = inventory.Name;
        inv.Price = inventory.Price;
        inv.Quantity = inventory.Quantity;

        return Task.CompletedTask;
    }

    public async Task<Inventory> GetInventoriesByIdAsync(int inventoryId)
    {
        var inv =  (_inventories.First(x => x.Id == inventoryId));
        var newInv =  new Inventory
        {
            Id = inv.Id,
            Name = inv.Name, 
            Price = inv.Price,
            Quantity = inv.Quantity
        };
        return await Task.FromResult(newInv);
    }
}