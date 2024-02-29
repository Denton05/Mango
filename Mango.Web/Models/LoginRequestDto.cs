using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models;

public class LoginRequestDto
{
    #region Properties

    [Required]
    public string Password { get; set; }

    [Required]
    public string UserName { get; set; }

    #endregion
}
