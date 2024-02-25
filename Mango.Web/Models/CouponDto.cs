namespace Mango.Web.Models;

public class CouponDto
{
    #region Properties

    public string CouponCode { get; set; }

    public int CouponId { get; set; }

    public double DiscountAmount { get; set; }

    public int MinAmount { get; set; }

    #endregion
}
