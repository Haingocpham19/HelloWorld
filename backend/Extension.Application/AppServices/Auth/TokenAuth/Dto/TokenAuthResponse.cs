namespace Extension.Application.Dto
{
    public class TokenAuthResponse
    {
        public string AccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public string RefreshToken { get; set; }

        public int RefreshTokenExpireInSeconds { get; set; }
    }
}
