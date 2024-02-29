using System.Text;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
                               {
                                   options.AddSecurityDefinition(name: "Bearer",
                                                                 securityScheme: new OpenApiSecurityScheme
                                                                                 {
                                                                                     Name = "Authorization",
                                                                                     Description =
                                                                                         "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                                                                                     In = ParameterLocation.Header,
                                                                                     Type = SecuritySchemeType.ApiKey,
                                                                                     Scheme = "Bearer"
                                                                                 });
                                   options.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                  {
                                                                      {
                                                                          new OpenApiSecurityScheme
                                                                          {
                                                                              Reference = new OpenApiReference
                                                                                          {
                                                                                              Type = ReferenceType.SecurityScheme,
                                                                                              Id = JwtBearerDefaults.AuthenticationScheme
                                                                                          }
                                                                          },
                                                                          new string[] { }
                                                                      }
                                                                  });
                               });

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
builder.Services.AddAuthorization();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
