using Extension.Application.AppFactory;
using Extension.Application.Dto;
using Extension.Domain.CommanConstant;
using Extension.Domain.Entities;
using Extension.Infrastructure.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Extension.Application.AppServices
{
    public interface ITokenAuthAppService
    {
        public Task<TokenAuthResponse> Authenticate(TokenAuthRequest request);
    }

    public class TokenAuthAppService : ApplicationServiceBase, ITokenAuthAppService
    {
        private readonly TokenAuthConfiguration _tokenAuthConfiguration;
        private readonly IdentityOptions _identityOptions;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenAuthAppService(
            IOptions<TokenAuthConfiguration> tokenAuthConfiguration,
            IOptions<IdentityOptions> identityOptions,
            UserManager<ApplicationUser> userManager,
            IAppFactory appFactory) : base(appFactory)
        {
            _tokenAuthConfiguration = tokenAuthConfiguration.Value;
            _identityOptions = identityOptions.Value;
            _userManager = userManager;
        }

        public async Task<TokenAuthResponse> Authenticate(TokenAuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var claims = await CreateJwtClaims(user);
                var accessToken = CreateToken(claims);
                var refreshToken = CreateToken(claims, AppConst.RefreshTokenExpiration);
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

        private async Task<IEnumerable<Claim>> CreateJwtClaims(ApplicationUser user, TimeSpan? expiration = null)
        {
            var tokenValidityKey = Guid.NewGuid().ToString();
            var claims = new List<Claim>
            {
                new Claim(_identityOptions.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(AppConsts.TokenValidityKey, tokenValidityKey)
            };

            await _userManager.AddClaimsAsync(user, claims);

            return claims;
        }

        private string CreateToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenAuthConfiguration.Issuer,
                audience: _tokenAuthConfiguration.Audience,
                claims: claims,
                notBefore: now,
                signingCredentials: _tokenAuthConfiguration.SigningCredentials,
                expires: expiration == null ? (DateTime?)null : now.Add(expiration.Value)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
