using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Service;

public class AuthService : IAuthService
{
    #region Fields

    private readonly AppDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    #endregion

    #region Construction

    public AuthService(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    #endregion

    #region Public Methods

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        if(user != null)
        {
            if(!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        return false;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

        var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if(user == null || !isValid)
        {
            return new LoginResponseDto { User = null, Token = string.Empty };
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        var userDto = new UserDto
                      {
                          Email = user.Email,
                          Id = user.Id,
                          Name = user.Name,
                          PhoneNumber = user.PhoneNumber
                      };

        var loginResponseDto = new LoginResponseDto
                               {
                                   User = userDto,
                                   Token = token
                               };

        return loginResponseDto;
    }

    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        var user = new ApplicationUser
                   {
                       UserName = registrationRequestDto.Email,
                       Email = registrationRequestDto.Email,
                       NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                       Name = registrationRequestDto.Name,
                       PhoneNumber = registrationRequestDto.PhoneNumber
                   };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            if(result.Succeeded)
            {
                return string.Empty;
            }

            return result.Errors.FirstOrDefault().Description;
        }
        catch
        {
        }

        return "Error encountered";
    }

    #endregion
}
