using JwtAuthenticationHandler.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtAuthenticationHandler
{
    public class JwtAuthenticationConfigExtensions
    {
        private readonly string validIssuer;
        private readonly string validAudience;

        public JwtAuthenticationConfigExtensions(string validIssuer, string validAudience)
        {
            this.validIssuer = validIssuer;
            this.validAudience = validAudience;
        }

        public void ConfigureJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = GetTokenValidationParameters();
                });
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = this.validIssuer,
                ValidAudience = this.validAudience,
                RequireExpirationTime = true,
                ValidAlgorithms = new[] { "RS256" },
                IssuerSigningKey = GetJsonWebKey(),
            };

            return tokenValidationParameters;
        }

        public JsonWebKey GetJsonWebKey()
        {
            return JwkLoader.LoadFromDefault();
        }

        public  string IsValidRefreshToken(string refreshToken)
        {
            try
            {
                // Decode the refresh token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = GetTokenValidationParameters(); // Implement this method to configure token validation parameters

                // Validate the token and extract token information
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out validatedToken);

                // If validation succeeds, the token is valid
                return principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                // If any error occurs during token validation, the token is invalid
                return string.Empty;
            }
        }
    }
}
