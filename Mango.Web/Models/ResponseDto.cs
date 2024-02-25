namespace Mango.Web.Models;

public class ResponseDto
{
    #region Properties

    public bool IsSuccess { get; set; } = true;

    public string Message { get; set; } = string.Empty;

    public object? Result { get; set; }

    #endregion
}
