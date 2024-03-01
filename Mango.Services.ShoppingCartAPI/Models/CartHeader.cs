using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models;

public class CartHeader
{
    #region Properties

    [Key]
    public int CartHeaderId { get; set; }

    [NotMapped]
    public double CartTotal { get; set; }

    public string? CouponCode { get; set; }

    [NotMapped]
    public double Discount { get; set; }

    public string? UserId { get; set; }

    #endregion
}
