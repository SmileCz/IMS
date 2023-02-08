using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double Price { get; set; }
}