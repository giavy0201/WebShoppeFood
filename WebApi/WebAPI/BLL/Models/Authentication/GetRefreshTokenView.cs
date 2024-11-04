namespace BLL.Models.Authentication
{
    public class GetRefreshTokenView
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
