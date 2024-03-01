using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models;

public class ProductDto
{
    #region Properties

    public string CategoryName { get; set; }

    [Range(1, 100)]
    public int Count { get; set; } = 1;

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int ProductId { get; set; }

    #endregion
}
