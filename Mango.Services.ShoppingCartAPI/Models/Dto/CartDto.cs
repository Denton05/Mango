namespace Mango.Services.ShoppingCartAPI.Models.Dto;

public class CartDto
{
    #region Properties

    public IEnumerable<CartDetailsDto>? CartDetails { get; set; }

    public CartHeaderDto CartHeader { get; set; }

    #endregion
}
