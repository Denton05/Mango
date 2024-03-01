using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Models;

public class CartDetails
{
    #region Properties

    [Key]
    public int CartDetailsId { get; set; }

    [ForeignKey("CartHeaderId")]
    public CartHeader CartHeader { get; set; }

    public int CartHeaderId { get; set; }

    public int Count { get; set; }

    [NotMapped]
    public ProductDto Product { get; set; }

    public int ProductId { get; set; }

    #endregion
}
