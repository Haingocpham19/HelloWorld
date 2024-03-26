namespace Extension.Application.Dto
{
    public class RefreshTokenReponse
    {
        public string? AccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public RefreshTokenReponse(string accessToken, int expireInSeconds)
        {
            AccessToken = accessToken;
            ExpireInSeconds = expireInSeconds;
        }
    }
}
