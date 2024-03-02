using System.IdentityModel.Tokens.Jwt;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        #endregion

        #region Construction

        public HomeController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        #endregion

        #region Public Methods

        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();
            var response = await _productService.GetAllProductsAsync();

            if(response is { IsSuccess: true })
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto? productDto = new();
            var response = await _productService.GetProductByIdAsync(productId);

            if(response is { IsSuccess: true })
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
        }

        [HttpPost]
        [Authorize]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            var cartDto = new CartDto
                          {
                              CartHeader = new CartHeaderDto
                                           {
                                               UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value
                                           },
                              CartDetails = new List<CartDetailsDto>
                                            {
                                                new()
                                                {
                                                    Count = productDto.Count,
                                                    ProductId = productDto.ProductId
                                                }
                                            }
                          };


            var response = await _cartService.UpsertCartAsync(cartDto);

            if(response is { IsSuccess: true })
            {
                TempData["success"] = "Item has been added to the Shopping Cart";
                return RedirectToAction("Index");
            }

            TempData["error"] = response?.Message;
            return View(productDto);
        }

        #endregion
    }
}
