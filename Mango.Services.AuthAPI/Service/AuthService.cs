using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service;

public class AuthService : IAuthService
{
    #region Fields

    private readonly AppDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    #endregion

    #region Construction

    public AuthService(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    #endregion

    #region Public Methods

    public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
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
                var userToReturn = _context.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                var userDto = new UserDto
                              {
                                  Email = userToReturn.Email,
                                  ID = userToReturn.Id,
                                  Name = userToReturn.Name,
                                  PhoneNumber = userToReturn.PhoneNumber
                              };

                return "";
            }

            return result.Errors.FirstOrDefault().Description;
        }
        catch
        {
        }

        return "Error Encountered";
    }

    #endregion
}
