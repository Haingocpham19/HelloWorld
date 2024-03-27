using JwtAuthenticationHandler.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationHandler
{
    public static class JwtAuthenticationConfigExtensions
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, string validIssuer, string validAudience)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = GetTokenValidationParameters(validIssuer, validAudience);
                });
        }

        public static TokenValidationParameters GetTokenValidationParameters(string validIssuer, string validAudience)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                RequireExpirationTime = true,
                ValidAlgorithms = new[] { "RS256" },
                IssuerSigningKey = GetJsonWebKey(),
            };

            return tokenValidationParameters;
        }

        public static JsonWebKey GetJsonWebKey()
        {
            return JwkLoader.LoadFromDefault();
        }
    }
}
