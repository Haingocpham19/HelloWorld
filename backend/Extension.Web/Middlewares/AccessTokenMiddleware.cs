using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using System.Threading.Tasks;
using JwtAuthenticationHandler;
using System.Collections.Generic;

namespace Extension.Web.Middlewares
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _issuerSigningKey;
        private readonly string _validIssuer;
        private readonly string _validAudience;

        public AccessTokenMiddleware(RequestDelegate next, string issuerSigningKey, string validIssuer, string validAudience)
        {
            _next = next;
            _issuerSigningKey = issuerSigningKey;
            _validIssuer = validIssuer;
            _validAudience = validAudience;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy token từ request header
            StringValues accessTokenHeader;
            context.Request.Headers.TryGetValue("Authorization", out accessTokenHeader);
            string accessToken = accessTokenHeader.ToString().Replace("Bearer ", "");

            // Kiểm tra token
            if (!string.IsNullOrEmpty(accessToken) && IsValidAccessToken(accessToken))
            {
                await _next(context); // Chuyển tiếp yêu cầu nếu token hợp lệ
            }
            else
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Access denied: Invalid access token");
            }
        }

        private bool IsValidAccessToken(string accessToken)
        {
            try
            {
                // Giải mã token để kiểm tra nội dung
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = JwtAuthenticationConfigExtensions.GetTokenValidationParameters(this._issuerSigningKey, this._validIssuer, this._validAudience);

                // Xác thực token và lấy ra các thông tin trong token
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out validatedToken);

                // Nếu xác thực thành công, token là hợp lệ
                return true;
            }
            catch
            {
                // Nếu có bất kỳ lỗi nào xảy ra trong quá trình xác thực, token không hợp lệ
                return false;
            }
        }

    }

    public static class AccessTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseAccessTokenMiddleware(this IApplicationBuilder builder,string issuerSigningKey, string validIssuer, string validAudience)
        {
            return builder.UseMiddleware<AccessTokenMiddleware>(issuerSigningKey, validIssuer, validAudience);
        }
    }
}
