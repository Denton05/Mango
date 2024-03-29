﻿using Mango.Web.Models;
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

                if(response is { IsSuccess: true })
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction("CouponIndex");
                }

                TempData["error"] = response?.Message;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            var response = await _couponService.GetCouponByIdAsync(couponId);

            if(response is { IsSuccess: true })
            {
                var model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }

            TempData["error"] = response?.Message;
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            var response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            if(response is { IsSuccess: true })
            {
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction("CouponIndex");
            }

            TempData["error"] = response?.Message;
            return View(couponDto);
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            var response = await _couponService.GetAllCouponsAsync();

            if(response is { IsSuccess: true })
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        #endregion
    }
}
