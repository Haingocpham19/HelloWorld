using Extension.Application.AppFactory;
using Extension.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Extension.Application.AppServices
{
    public class ApplicationServiceBase
    {
        protected ApplicationServiceBase(IAppFactory appFactory)
        {
            AppFactory = appFactory;
        }

        public IAppFactory AppFactory { get; set; }
    }
}
