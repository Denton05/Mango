using Mango.Web.Models;

namespace Mango.Web.Service.IService;

public interface IProductService
{
    Task<ResponseDto?> CreateProductAsync(ProductDto productDto);

    Task<ResponseDto?> DeleteProductAsync(int id);

    Task<ResponseDto?> GetAllProductsAsync();

    Task<ResponseDto?> GetProductsByIdAsync(int id);

    Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
}
