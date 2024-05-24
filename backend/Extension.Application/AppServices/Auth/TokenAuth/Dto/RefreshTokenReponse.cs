namespace Extension.Application.Dto
{
    public class RefreshTokenReponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public RefreshTokenReponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
