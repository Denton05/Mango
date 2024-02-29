using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        #region Fields

        private readonly IAuthService _authService;

        #endregion

        #region Construction

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public IActionResult Login()
        {
            var loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>
                           {
                               new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin },
                               new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer }
                           };
            ViewBag.RoleList = roleList;
            return View();
        }

        #endregion
    }
}
