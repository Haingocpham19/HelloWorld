using Extension.Application.AppFactory;
using Extension.Application.AppServices;
using Extension.Domain.Entities;
using Extension.Infrastructure;
using Extension.Infrastructure.Authentication.JwtBearer;
using Extension.Web.Core.Services;
using JwtAuthenticationHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Access configuration
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Extension");

// Access specific configuration values
string validIssuer = configuration.GetSection("JwtSettings:Issuer").Value;
string validAudience = configuration.GetSection("JwtSettings:Audience").Value;

builder.Services.ConfigureJwtAuthentication(validIssuer, validAudience);

builder.Services.AddDbContext<ExtensionDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ExtensionDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureSwagger();

// Configure TokenAuthConfiguration
builder.Services.Configure<TokenAuthConfiguration>(options =>
{
    options.SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecurityKey"]));
    options.Issuer = configuration["JwtSettings:Issuer"];
    options.Audience = configuration["JwtSettings:Audience"];
    options.SigningCredentials = new SigningCredentials(options.SecurityKey, SecurityAlgorithms.HmacSha256);
    options.AccessTokenExpiration = TimeSpan.FromHours(30); // Set your desired expiration time
    options.RefreshTokenExpiration = TimeSpan.FromDays(7); // Set your desired expiration time
});

builder.Services.AddScoped<IAppFactory, AppFactory>();
builder.Services.AddScoped<ITokenAuthAppService, TokenAuthAppService>();
builder.Services.AddScoped<IAccountRegisterAppService, AccountRegisterAppService>();

builder.Services.AddControllers();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


// test revert 1213213