using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Service;

public class CouponService : ICouponService
{
    #region Fields

    private readonly IHttpClientFactory _httpClientFactory;

    #endregion

    #region Construction

    public CouponService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    #endregion

    #region Public Methods

    public async Task<CouponDto> GetCoupon(string couponCode)
    {
        var client = _httpClientFactory.CreateClient("Coupon");
        var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");
        var apiContent = await response.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
        if(responseDto.IsSuccess)
        {
            return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
        }

        return new CouponDto();
    }

    #endregion
}
