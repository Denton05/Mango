namespace Mango.Web.Models;

public class LoginResponseDto
{
    #region Properties

    public string Token { get; set; }

    public UserDto User { get; set; }

    #endregion
}
