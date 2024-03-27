using Microsoft.Extensions.DependencyInjection;

namespace Extension.Web.Configs.DiLaunchers.Infrastructure
{
    public interface IDiLauncher
    {
        void Run(IServiceCollection container);
    }
}
