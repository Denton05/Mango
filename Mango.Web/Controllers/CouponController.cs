using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        #region Fields

        private readonly ICouponService _couponService;

        #endregion

        #region Construction

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        #endregion

        #region Public Methods

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            var response = await _couponService.GetAllCouponsAsync();

            if(response != null && response.IsSuccess)
            {
                var a = Convert.ToString(response.Result);
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        #endregion
    }
}
