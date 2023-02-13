﻿using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness.Validations;

public class ProductEnsurePriceIsGreaterThanInventoriesCostAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var product = validationContext.ObjectInstance as Product;
        if (product is not null)
        {
            if (!ValidatePricing(product))
            {
                return new ValidationResult(
                    $"The product's price is less than the inventories cost:{TotalInventoriesCost(product).ToString("c")}!",
                    new List<string>() { validationContext.MemberName });
            }
        }

        return ValidationResult.Success;
    }

    private double TotalInventoriesCost(Product product)
    {
        if (product is null || product.ProductInventories is null) return 0;

        return product.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuality ?? 0);
    }

    private bool ValidatePricing(Product product)
    {
        if (product.ProductInventories is null || product.ProductInventories.Count <= 0) return true;
        return !(TotalInventoriesCost(product) >= product.Price);
    }
}