using Microsoft.AspNetCore.Http;

namespace WebMVC.Helper
{
    public static class Utils
    {
        public static string GetIpAddress(HttpContext httpContext)
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            return ipAddress ?? "127.0.0.1";
        }
    }
}
