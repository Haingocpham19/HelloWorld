using Extension.Application.AppServices;
using Extension.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Extension.Web.Core.Controllers
{
    [Route("~/authen-api/[controller]")]
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(TokenAuthRequest input)
        {
            var loginResult = await _tokenAuthAppService.Authenticate(input);
            return loginResult is null ? Unauthorized() : Ok(loginResult);
        }
    }
}
