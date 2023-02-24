using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels;

public class PurchaseViewModel
{
    [Required]
    public string PoNumber { get; set; } = string.Empty;

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "You have to select an inventory")]
    public int InventoryId { get; set; }

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quality has to be greater than 0")]
    public int QuantityToPurchase { get; set; }

    public double InventoryPrice { get; set; }
}