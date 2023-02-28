using System.ComponentModel.DataAnnotations;
using IMS.WebApp.ViewModels;

namespace IMS.WebApp.ViewModelsValidations;

public class SaleEnsureEnoughProductQuantity : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var sellViewModel = validationContext.ObjectInstance as SellViewModel;
        if (sellViewModel?.Product == null) return ValidationResult.Success;
        if (sellViewModel.Product.Quantity < sellViewModel.QuantityToSell)
        {
            return new ValidationResult(
                $"There is not enough product. There is only {sellViewModel.Product.Quantity} in the warehouse.",
                new[] { validationContext.MemberName }!);
        }

        return ValidationResult.Success;
    }
}