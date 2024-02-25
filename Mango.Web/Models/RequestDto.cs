using Mango.Web.Utility;

namespace Mango.Web.Models;

using static SD;

public class RequestDto
{
    #region Properties

    public string AccessToken { get; set; }

    public ApiType ApiType { get; set; } = ApiType.GET;

    public object Data { get; set; }

    public string Url { get; set; }

    #endregion
}
