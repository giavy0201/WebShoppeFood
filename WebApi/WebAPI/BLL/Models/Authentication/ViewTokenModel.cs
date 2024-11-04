namespace BLL.Models.Authentication
{
    public class ViewTokenModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
    }
}
