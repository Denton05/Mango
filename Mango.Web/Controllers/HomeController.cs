using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IProductService _productService;

        #endregion

        #region Construction

        public HomeController(IProductService productService)
        {
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

        #endregion
    }
}
