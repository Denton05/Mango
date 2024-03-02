using System.IdentityModel.Tokens.Jwt;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        #region Fields

        private readonly ICartService _cartService;

        #endregion

        #region Construction

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        #endregion

        #region Private Methods

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            var response = await _cartService.GetCartByUserIdAsync(userId);
            if(response is { IsSuccess: true })
            {
                var cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }

            return new CartDto();
        }

        #endregion

        #region Public Methods

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        #endregion
    }
}
