using Extension.Application.AppFactory;
using Extension.Application.Dto;
using Extension.Domain.Entities;
using Extension.Infrastructure.Authentication.JwtBearer;
using JwtAuthenticationHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Extension.Application.AppServices
{
    public interface ITokenAuthAppService
    {
        public Task<TokenAuthResponse> Authenticate(TokenAuthRequest request);
        //public Task<RefreshTokenReponse> RefreshToken(string token);
    }

    public class TokenAuthAppService : ApplicationServiceBase, ITokenAuthAppService
    {
        private readonly TokenAuthConfiguration _tokenAuthConfiguration;
        private readonly IdentityOptions _identityOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<JwtBearerOptions> _jwtOptions;

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
        }

        public async Task<TokenAuthResponse> Authenticate(TokenAuthRequest request)
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

        //public async Task<RefreshTokenReponse> RefreshToken(string refreshToken)
        //{
        //    if (string.IsNullOrWhiteSpace(refreshToken))
        //    {
        //        throw new ArgumentNullException(nameof(refreshToken));
        //    }

        //    if (!IsRefreshTokenValid(refreshToken, out var principal))
        //    {
        //        throw new ValidationException("Refresh token is not valid!");
        //    }

        //    try
        //    {
        //        var userId = principal?.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

        //        if (string.IsNullOrEmpty(userId))
        //        {
        //            throw new ValidationException("Invalid user identifier in refresh token!");
        //        }

        //        var user = await _userManager.FindByIdAsync(userId);

        //        if (user == null)
        //        {
        //            throw new ValidationException("User not found!");
        //        }

        //        var claims = await CreateJwtClaims(user);
        //        var accessToken = CreateToken(claims);

        //        return new RefreshTokenReponse
        //        (
        //            accessToken,
        //            (int)_tokenAuthConfiguration.AccessTokenExpiration.TotalSeconds
        //        );
        //    }
        //    catch (ValidationException)
        //    {
        //        throw; // Re-throw ValidationException
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ValidationException("Error refreshing token!", e);
        //    }
        //}


        //private bool IsRefreshTokenValid(string refreshToken, out ClaimsPrincipal principal)
        //{
        //    principal = null;

        //    try
        //    {
        //        var validationParameters = new TokenValidationParameters
        //        {
        //            ValidAudience = _configuration.Audience,
        //            ValidIssuer = _configuration.Issuer,
        //            IssuerSigningKey = _configuration.SecurityKey
        //        };

        //        foreach (var validator in _jwtOptions.Value.SecurityTokenValidators)
        //        {
        //            if (!validator.CanReadToken(refreshToken))
        //            {
        //                continue;
        //            }

        //            try
        //            {
        //                principal = validator.ValidateToken(refreshToken, validationParameters, out _);

        //                if (principal.Claims.FirstOrDefault(x => x.Type == AppConsts.TokenType)?.Value == TokenType.RefreshToken.To<int>().ToString())
        //                {
        //                    return true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Debug(ex.ToString(), ex);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Debug(ex.ToString(), ex);
        //    }

        //    return false;
        //}

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
            var creds = new SigningCredentials(JwtAuthenticationConfigExtensions.GetJsonWebKey(), SecurityAlgorithms.RsaSha256);

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
