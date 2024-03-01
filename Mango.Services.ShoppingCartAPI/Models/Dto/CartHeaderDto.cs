namespace Mango.Services.ShoppingCartAPI.Models.Dto;

public class CartHeaderDto
{
    #region Properties

    public int CartHeaderId { get; set; }

    public double CartTotal { get; set; }

    public string? CouponCode { get; set; }

    public double Discount { get; set; }

    public string? UserId { get; set; }

    #endregion
}
