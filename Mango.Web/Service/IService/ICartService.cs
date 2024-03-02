using Mango.Web.Models;

namespace Mango.Web.Service.IService;

public interface ICartService
{
    Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);

    Task<ResponseDto?> GetCartByUserIdAsync(string userId);

    Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);

    Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
}
