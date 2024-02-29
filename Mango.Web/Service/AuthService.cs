using Mango.Web.Models;
using Mango.Web.Service.IService;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service;

public class AuthService : IAuthService
{
    #region Fields

    private readonly IBaseService _baseService;

    #endregion

    #region Construction

    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    #endregion

    #region Public Methods

    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.POST,
                                                Data = registrationRequestDto,
                                                Url = AuthAPIBase + "/api/auth/AssignRole"
                                            });
    }

    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.POST,
                                                Data = loginRequestDto,
                                                Url = AuthAPIBase + "/api/auth/login"
                                            });
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto
                                            {
                                                ApiType = ApiType.POST,
                                                Data = registrationRequestDto,
                                                Url = AuthAPIBase + "/api/auth/register"
                                            });
    }

    #endregion
}
