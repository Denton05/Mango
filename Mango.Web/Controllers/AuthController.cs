using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        #endregion
    }
}
