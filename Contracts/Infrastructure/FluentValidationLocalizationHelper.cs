

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.Extensions.Localization;

namespace Contracts.Infrastructure;

public static class FluentValidationLocalizationHelper
{
    public static void RegisterDefaultValidationMessages(IStringLocalizer localizer)
    {
        ValidatorOptions.Global.LanguageManager = new CustomFluentValidationLanguageManager(localizer);
    }

    private class CustomFluentValidationLanguageManager : LanguageManager
    {
        public CustomFluentValidationLanguageManager(IStringLocalizer localizer)
        {
            var currentCulture = CultureInfo.CurrentUICulture;

            while (!Equals(currentCulture, CultureInfo.InvariantCulture))
            {
                foreach (var message in localizer.GetAllStrings(includeParentCultures:true))
                {
                    AddTranslation(currentCulture.Name,message.Name,message.Value);
                }

                currentCulture = currentCulture.Parent;
            }

        }
    }
}

