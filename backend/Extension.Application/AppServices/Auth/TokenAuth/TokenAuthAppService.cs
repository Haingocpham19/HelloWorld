using Extension.Application.AppFactory;
using Extension.Application.Dto;
using Extension.Domain.Entities;
using Extension.Infrastructure.Authentication.JwtBearer;
using JwtAuthenticationHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Extension.Application.AppServices
{
    public interface ITokenAuthAppService
    {
        public Task<TokenAuthResponse> GetToken(TokenAuthRequest request);
        public Task<RefreshTokenReponse> RefreshToken(string token);
    }

    public class TokenAuthAppService : ApplicationServiceBase, ITokenAuthAppService
    {
        private readonly TokenAuthConfiguration _tokenAuthConfiguration;
        private readonly IdentityOptions _identityOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<JwtBearerOptions> _jwtOptions;
        private readonly JwtAuthenticationConfigExtensions _jwtAuthenticationConfigExtensions;

        public TokenAuthAppService(
            IOptions<TokenAuthConfiguration> tokenAuthConfiguration,
            IOptions<IdentityOptions> identityOptions,
            UserManager<ApplicationUser> userManager,
            IAppFactory appFactory,
            IOptions<JwtBearerOptions> jwtOptions) : base(appFactory)
        {
            _tokenAuthConfiguration = tokenAuthConfiguration.Value;
            _identityOptions = identityOptions.Value;
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _jwtAuthenticationConfigExtensions = new JwtAuthenticationConfigExtensions(_tokenAuthConfiguration.Issuer, _tokenAuthConfiguration.Audience);
        }

        public async Task<TokenAuthResponse> GetToken(TokenAuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var claims = CreateJwtClaims(user);
                var accessToken = CreateToken(claims, _tokenAuthConfiguration.AccessTokenExpiration);
                var refreshToken = CreateToken(claims, _tokenAuthConfiguration.RefreshTokenExpiration);
                return new TokenAuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpireInSeconds = (int)_tokenAuthConfiguration.RefreshTokenExpiration.TotalSeconds,
                    ExpireInSeconds = (int)_tokenAuthConfiguration.AccessTokenExpiration.TotalSeconds,
                };
            }

            return null;
        }

        public async Task<RefreshTokenReponse> RefreshToken(string refreshToken)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            // Check if refresh token is valid
            string userId = _jwtAuthenticationConfigExtensions.IsValidRefreshToken(refreshToken);
            if (string.IsNullOrEmpty(userId))
            {
                throw new ValidationException("Refresh token is not valid!");
            }

            try
            {
                // Retrieve user
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new ValidationException("User not found!");
                }

                // Create JWT claims
                var claims = CreateJwtClaims(user);

                // Create new access token
                var accessToken = CreateToken(claims, _tokenAuthConfiguration.AccessTokenExpiration);

                // Return refresh token response
                return new RefreshTokenReponse(accessToken, (int)_tokenAuthConfiguration.AccessTokenExpiration.TotalSeconds);
            }
            catch (ValidationException)
            {
                throw; // Re-throw ValidationException
            }
            catch (Exception e)
            {
                throw new ValidationException("Error refreshing token!", e);
            }
        }


        private IEnumerable<Claim> CreateJwtClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(_identityOptions.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            return claims;
        }

        private string CreateToken(IEnumerable<Claim> claims, TimeSpan expiration)
        {
            var creds = new SigningCredentials(_jwtAuthenticationConfigExtensions.GetJsonWebKey(), SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenAuthConfiguration.Issuer,
                audience: _tokenAuthConfiguration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(expiration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
