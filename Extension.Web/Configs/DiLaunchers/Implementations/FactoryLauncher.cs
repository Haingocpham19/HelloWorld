using Extension.Domain.Enum;
using Extension.Web.Configs.DiLaunchers.Infrastructure;

namespace Extension.Web.Configs.DiLaunchers.Implementations
{
    public static class FactoryLauncher
    {
        public static IDiLauncher CreateDiLauncher(LauncherType? type)
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
