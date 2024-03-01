using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class ProductController : Controller
{
    #region Fields

    private readonly IProductService _productService;

    #endregion

    #region Construction

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    #endregion

    #region Public Methods

    [HttpGet]
    public IActionResult ProductCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductDto model)
    {
        if(ModelState.IsValid)
        {
            var response = await _productService.CreateProductAsync(model);

            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Product created successfully";
                return RedirectToAction("ProductIndex");
            }

            TempData["error"] = response?.Message;
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ProductDelete(int productId)
    {
        var response = await _productService.GetProductByIdAsync(productId);

        if(response != null && response.IsSuccess)
        {
            var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        }

        TempData["error"] = response?.Message;
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductDto productDto)
    {
        var response = await _productService.DeleteProductAsync(productDto.ProductId);

        if(response != null && response.IsSuccess)
        {
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("ProductIndex");
        }

        TempData["error"] = response?.Message;
        return View(productDto);
    }

    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto>? list = new();
        var response = await _productService.GetAllProductsAsync();

        if(response != null && response.IsSuccess)
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
