using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Service;

public class ProductService : IProductService
{
    #region Fields

    private readonly IHttpClientFactory _httpClientFactory;

    #endregion

    #region Construction

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var client = _httpClientFactory.CreateClient("Product");
        var response = await client.GetAsync("/api/product");
        var apiContent = await response.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
        if(responseDto.IsSuccess)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
        }

        return new List<ProductDto>();
    }

    #endregion
}
