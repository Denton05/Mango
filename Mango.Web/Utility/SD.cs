namespace Mango.Web.Utility;

public class SD
{
    #region Constants

    public const string RoleAdmin = "ADMIN";
    public const string RoleCustomer = "CUSTOMER";
    public const string TokenCookie = "JWTToken";

    #endregion

    #region Properties

    public static string AuthAPIBase { get; set; }

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
