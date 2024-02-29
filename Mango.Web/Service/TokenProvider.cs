using Mango.Web.Service.IService;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service;

public class TokenProvider : ITokenProvider
{
    #region Fields

    private readonly IHttpContextAccessor _contextAccessor;

    #endregion

    #region Construction

    public TokenProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    #endregion

    #region Public Methods

    public void ClearToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(TokenCookie);
    }

    public string? GetToken()
    {
        string? token = null;
        var hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(TokenCookie, out token);
        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(TokenCookie, token);
    }

    #endregion
}
