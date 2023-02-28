using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness;
using IMS.WebApp.ViewModelsValidations;

namespace IMS.WebApp.ViewModels;

public class SellViewModel
{
    [Required]
    public string SalesOrderNumber { get; set; } = string.Empty;

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater or equal to 1.")]
    [SaleEnsureEnoughProductQuantity]
    public int QuantityToSell { get; set; }

    public double UnitPrice { get; set; }
    public Product? Product { get; set; }
}