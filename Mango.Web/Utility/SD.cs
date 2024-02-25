namespace Mango.Web.Utility;

public class SD
{
    #region Properties

    public static string CouponAPIBase { get; set; }

    #endregion

    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
