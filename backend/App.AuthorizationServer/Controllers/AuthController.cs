using Extension.Application.AppServices;
using Extension.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Extension.Web.Core.Controllers
{
    [Route("~/auth-api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenAuthAppService _tokenAuthAppService;

        public AuthController(
            IConfiguration configuration,
            ITokenAuthAppService tokenAuthAppService)
        {
            _configuration = configuration;
            _tokenAuthAppService = tokenAuthAppService;
        }

        [HttpPost("GetToken")]
        public async Task<TokenAuthResponse> GetToken([FromBody] TokenAuthRequest input)
        {
            return await _tokenAuthAppService.GetToken(input);
        }

        [HttpPost("RefreshToken")]
        public async Task<RefreshTokenReponse> RefreshToken(string token)
        {
            return await _tokenAuthAppService.RefreshToken(token);
        }
    }
}
