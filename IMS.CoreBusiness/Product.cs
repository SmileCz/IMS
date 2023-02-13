using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness.Validations;

namespace IMS.CoreBusiness;

public class Product
{
    public int Id { get; set; }

    [Required] [StringLength(150)] public string Name { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
    public double Price { get; set; }

    [ProductEnsurePriceIsGreaterThanInventoriesCost]
    public List<ProductInventory> ProductInventories { get; set; } = new();

    public void AddInventory(Inventory inventory)
    {
        if (!ProductInventories.Any(x => x.Inventory != null && x.Inventory.Name.Equals(inventory.Name)))
        {
            ProductInventories.Add(new ProductInventory()
            {
                InventoryId = inventory.Id,
                ProductId = Id,
                Inventory = inventory,
                Product = this,
                InventoryQuality = 1
            });
        }
    }
}