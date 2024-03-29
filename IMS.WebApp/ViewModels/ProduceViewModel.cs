﻿using System.ComponentModel.DataAnnotations;
using IMS.CoreBusiness;
using IMS.WebApp.ViewModelsValidations;

namespace IMS.WebApp.ViewModels;

public class ProduceViewModel
{
    [Required]
    public string ProductionNumber { get; set; } = string.Empty;

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "You have to select a product")]
    public int ProductId { get; set; }

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quality has to be greater than 0")]
    [ProduceEnsureEnoughInventoryQuantity]
    public int QuantityToProduce { get; set; }

    public Product? Product { get; set; } = null;
}