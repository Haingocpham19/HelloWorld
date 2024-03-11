namespace Extension.Application
{
    public class AppConst
    {
        public static TimeSpan AccessTokenExpiration = TimeSpan.FromDays(1);

        public static TimeSpan RefreshTokenExpiration = TimeSpan.FromDays(365);
    }
}
