using Extension.Application.AppServices;
using Extension.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Extension.Web.Core.Controllers
{
    [Route("~/auth-api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenAuthAppService _tokenAuthAppService;
        private readonly IAccountRegisterAppService _accountRegisterAppService;

        public AuthController(
            IConfiguration configuration,
            ITokenAuthAppService tokenAuthAppService,
            IAccountRegisterAppService accountRegisterAppService)
        {
            _configuration = configuration;
            _tokenAuthAppService = tokenAuthAppService;
            _accountRegisterAppService = accountRegisterAppService;
        }

        [HttpPost("Login")]
        public async Task<TokenAuthResponse> Login([FromBody] TokenAuthRequest input)
        {
            return await _tokenAuthAppService.Authenticate(input);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterRequest input)
        {
            var result = await _accountRegisterAppService.RegisterUserAsync(input);
            return result.Succeeded ?  Ok("Registration successful"): BadRequest(result.Errors); ;
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string token)
        {
            var result = await _tokenAuthAppService.RegisterUserAsync(input);
            return result.Succeeded ? Ok("Registration successful") : BadRequest(result.Errors); ;
        }
    }
}
