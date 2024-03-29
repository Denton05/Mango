﻿using Mango.Web.Models;

namespace Mango.Web.Service.IService;

public interface IAuthService
{
    Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);

    Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

    Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
}
