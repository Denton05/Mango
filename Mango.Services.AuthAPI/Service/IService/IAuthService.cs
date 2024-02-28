using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Service.IService;

public interface IAuthService
{
    Task<bool> AssignRole(string email, string roleName);

    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

    Task<string> Register(RegistrationRequestDto registrationRequestDto);
}
