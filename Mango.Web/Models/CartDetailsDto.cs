namespace Mango.Web.Models;

public class CartDetailsDto
{
    #region Properties

    public int CartDetailsId { get; set; }

    public CartHeaderDto? CartHeader { get; set; }

    public int CartHeaderId { get; set; }

    public int Count { get; set; }

    public ProductDto? Product { get; set; }

    public int ProductId { get; set; }

    #endregion
}
