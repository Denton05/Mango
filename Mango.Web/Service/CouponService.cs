using Mango.Web.Models;
using Mango.Web.Service.IService;

namespace Mango.Web.Service;

public class CouponService : ICouponService
{
    #region Fields

    private readonly IBaseService _baseService;

    #endregion

    #region Construction

    public CouponService(BaseService baseService)
    {
        _baseService = baseService;
    }

    #endregion

    #region Public Methods

    public Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> DeleteCouponAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> GetAllCouponsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> GetCouponAsync(string couponCode)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> GetCouponsByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        throw new NotImplementedException();
    }

    #endregion
}
