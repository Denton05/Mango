using Mango.Web.Service.IService;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Mango.Web.Service;

public class UserProvider : IUserProvider
{
    #region Fields

    private readonly IHttpContextAccessor _contextAccessor;

    #endregion

    #region Construction

    public UserProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    #endregion

    #region Public Methods

    public string? GetUserId() => _contextAccessor.HttpContext.User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

    #endregion
}
