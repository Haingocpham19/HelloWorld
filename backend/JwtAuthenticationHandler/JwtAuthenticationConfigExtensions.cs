using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAuthenticationHandler
{
    public static class JwtAuthenticationConfigExtensions
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, string issuerSigningKey, string validIssuer, string validAudience)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = GetTokenValidationParameters(issuerSigningKey, validIssuer, validAudience);
                });
        }

        public static TokenValidationParameters GetTokenValidationParameters(string secretKey, string validIssuer, string validAudience)
        {
            // Tạo khóa xác thực từ secretKey
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Tạo danh sách các khóa xác thực
            var issuerSigningKeys = new List<SecurityKey>();

            // Tạo một SymmetricSecurityKey mới với keyId cụ thể và thêm vào danh sách
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            {
                KeyId = "auth-key-id"
            };

            issuerSigningKeys.Add(securityKey);

            // Tạo các tham số xác thực token
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                RequireExpirationTime = true,
                IssuerSigningKeys = issuerSigningKeys
            };

            return tokenValidationParameters;
        }
    }
}
