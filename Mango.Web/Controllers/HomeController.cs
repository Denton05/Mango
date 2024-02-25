using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ICouponService _couponService;

        #endregion

        #region Construction

        public HomeController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        #endregion

        #region Public Methods

        public async Task<IActionResult> Index()
        {
            var result = await _couponService.GetAllCouponsAsync();
            return View();
        }

        #endregion
    }
}
