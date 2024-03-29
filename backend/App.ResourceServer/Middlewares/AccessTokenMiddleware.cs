using JwtAuthenticationHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

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
            if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(new JwtAuthenticationConfigExtensions(_validIssuer, _validAudience).IsValidRefreshToken(accessToken)))
            {
                await _next(context); // Chuyển tiếp yêu cầu nếu token hợp lệ
            }
            else
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Access denied: Invalid access token");
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
