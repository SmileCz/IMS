using System.ComponentModel.DataAnnotations;
using IMS.WebApp.ViewModels;

namespace IMS.WebApp.ViewModelsValidations;

public class ProduceEnsureEnoughInventoryQuantityAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var produceViewModel = validationContext.ObjectInstance as ProduceViewModel;

        if (produceViewModel?.Product?.ProductInventories == null) return ValidationResult.Success;
        foreach (var pi in produceViewModel.Product.ProductInventories)
        {
            if (pi.Inventory != null &&
                pi.InventoryQuality * produceViewModel.QuantityToProduce > pi.Inventory.Quantity)
            {
                return new ValidationResult(
                    $"The inventory ({pi.Inventory.Name}) is not enough to produce {produceViewModel.QuantityToProduce} products.",
                    new[] { validationContext.MemberName }!);
            }
        }

        return ValidationResult.Success;
    }
}