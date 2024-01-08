using Extension.Domain.Enum;
using Extension.Web.Configs.DiLaunchers.Infrastructure;

namespace Extension.Web.Configs.DiLaunchers.Implementations
{
    public class FactoryLauncher : IDiLauncherFactory
    {
        public IDiLauncher CreateDiLauncher(LauncherType type)
        {
            switch (type)
            {
                case LauncherType.Local:
                    return new DebugDiLauncher();
                case LauncherType.Production:
                    return new DebugDiLauncher();
                default:
                    return new DebugDiLauncher();
            }
        }
    }
}
