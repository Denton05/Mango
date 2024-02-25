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

        [HttpGet]
        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if(ModelState.IsValid)
            {
                var response = await _couponService.CreateCouponAsync(model);

                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction("CouponIndex");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            var response = await _couponService.GetAllCouponsAsync();

            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        #endregion
    }
}
