using Extension.Application.AppServices;
using Extension.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Extension.Web.Core.Controllers
{
    [Route("~/auth-api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRegisterAppService _accountRegisterAppService;

        public AccountController(
            IAccountRegisterAppService accountRegisterAppService)
        {
            _accountRegisterAppService = accountRegisterAppService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterRequest input)
        {
            var result = await _accountRegisterAppService.RegisterUserAsync(input);
            return result.Succeeded ? Ok("Registration successful") : BadRequest(result.Errors); ;
        }
    }
}
