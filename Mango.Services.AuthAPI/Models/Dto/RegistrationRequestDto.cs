namespace Mango.Services.AuthAPI.Models.Dto;

public class RegistrationRequestDto
{
    #region Properties

    public string Email { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    #endregion
}
