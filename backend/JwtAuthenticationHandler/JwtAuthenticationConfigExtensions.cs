using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAuthenticationHandler
{
    public static class JwtAuthenticationConfigExtensions
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, string issuerSigningKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }
    }
}
