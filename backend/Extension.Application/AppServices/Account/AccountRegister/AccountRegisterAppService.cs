using Extension.Application.AppFactory;
using Extension.Application.Dto;
using Extension.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Extension.Application.AppServices
{
    public interface IAccountRegisterAppService
    {
        public Task<IdentityResult> RegisterUserAsync(AccountRegisterRequest request);
    }
    public class AccountRegisterAppService : ApplicationServiceBase, IAccountRegisterAppService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountRegisterAppService(IAppFactory appFactory, UserManager<ApplicationUser> userManager) : base(appFactory)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(AccountRegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
            };

            return await _userManager.CreateAsync(user, request.Password);
        }
    }
}
