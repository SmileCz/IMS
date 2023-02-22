using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels;

public class PurchaseViewModel
{
    [Required]
    public string PoNumber { get; set; } = string.Empty;

    [Required]
    public int InventoryId { get; set; }

    [Required]
    public int QuantityToPurchase { get; set; }

    public double InventoryPrice { get; set; }
}