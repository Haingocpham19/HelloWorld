using Extension.Domain.Enum;

namespace Extension.Web.Configs.DiLaunchers.Infrastructure
{
    public interface IDiLauncherFactory
    {
        IDiLauncher CreateDiLauncher(LauncherType type);
    }
}
