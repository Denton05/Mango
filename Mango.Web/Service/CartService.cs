using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service;

public class CartService : ICartService
{
    #region Fields

    private readonly IBaseService _baseService;

    #endregion

    #region Construction

    public CartService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    #endregion

    #region Public Methods

    public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = SD.ApiType.POST,
                                                Data = cartDto,
                                                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
                                            });
    }

    public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = SD.ApiType.GET,
                                                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId
                                            });
    }

    public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = SD.ApiType.POST,
                                                Data = cartDetailsId,
                                                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"
                                            });
    }

    public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = SD.ApiType.POST,
                                                Data = cartDto,
                                                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert"
                                            });
    }

    #endregion
}
