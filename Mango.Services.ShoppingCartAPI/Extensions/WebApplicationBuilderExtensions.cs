using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.ShoppingCartAPI.Extensions;

public static class WebApplicationBuilderExtensions
{
    #region Public Methods

    public static WebApplicationBuilder WebAppAuthentication(this WebApplicationBuilder builder)
    {
        var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret");
        var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
        var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");
        var key = Encoding.ASCII.GetBytes(secret);
        builder.Services.AddAuthentication(options =>
                                           {
                                               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                           })
               .AddJwtBearer(options =>
                             {
                                 options.TokenValidationParameters = new TokenValidationParameters
                                                                     {
                                                                         ValidateIssuerSigningKey = true,
                                                                         IssuerSigningKey = new SymmetricSecurityKey(key),
                                                                         ValidateIssuer = true,
                                                                         ValidateAudience = true,
                                                                         ValidIssuer = issuer,
                                                                         ValidAudience = audience
                                                                     };
                             });
        return builder;
    }

    #endregion
}
