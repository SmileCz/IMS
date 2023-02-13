using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness.Validations;

public class ProductEnsurePriceIsGreaterThanInventoriesCostAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not Product product) return ValidationResult.Success;
        if (ValidatePricing(product)) return ValidationResult.Success;
        if (validationContext.MemberName != null)
            return new ValidationResult(
                $"The product's price is less than the inventories cost:{TotalInventoriesCost(product):c}!",
                new List<string>() { validationContext.MemberName });

        return ValidationResult.Success;
    }

    private static double TotalInventoriesCost(Product? product)
    {
        if (product?.ProductInventories is null) return 0;

        return product.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuality ?? 0);
    }

    private static bool ValidatePricing(Product? product)
    {
        if (product?.ProductInventories is null || product.ProductInventories.Count <= 0) return true;
        return !(TotalInventoriesCost(product) >= product.Price);
    }
}