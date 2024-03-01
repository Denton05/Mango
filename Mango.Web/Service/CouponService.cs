using Mango.Web.Models;
using Mango.Web.Service.IService;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service;

public class CouponService : ICouponService
{
    #region Fields

    private readonly IBaseService _baseService;

    #endregion

    #region Construction

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    #endregion

    #region Public Methods

    public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.POST,
                                                Data = couponDto,
                                                Url = CouponAPIBase + "/api/coupon/"
                                            });
    }

    public async Task<ResponseDto?> DeleteCouponAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.DELETE,
                                                Url = CouponAPIBase + "/api/coupon/" + id
                                            });
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.GET,
                                                Url = CouponAPIBase + "/api/coupon"
                                            });
    }

    public async Task<ResponseDto?> GetCouponAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.GET,
                                                Url = CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
                                            });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.GET,
                                                Url = CouponAPIBase + "/api/coupon/" + id
                                            });
    }

    public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.PUT,
                                                Data = couponDto,
                                                Url = CouponAPIBase + "/api/coupon/"
                                            });
    }

    #endregion
}
