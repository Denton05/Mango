using Mango.Services.ShoppingCartAPI;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

builder.WebAppAuthentication();
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
