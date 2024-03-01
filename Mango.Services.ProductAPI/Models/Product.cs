using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models;

public class Product
{
    #region Properties

    public string CategoryName { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(1, 1000)]
    public double Price { get; set; }

    [Key]
    public int ProductId { get; set; }

    #endregion
}
