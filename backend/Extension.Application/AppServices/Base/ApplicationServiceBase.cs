using Extension.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Extension.Application.AppServices
{
    public class ApplicationServiceBase
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
    }
}
