

using Comati3.DTOs;
using Comati3.Models;

namespace Comati3.Services
{
    public class Cookies
    {
        public bool SetCookies(HttpResponse Response, LoginDTO user)
        {
            var rawData = $"{user.Phone}:{user.Password}";

            var cookieOptions = new CookieOptions
            {
                Path = "/", // Specify the path where the cookie is valid
                HttpOnly = true, // The cookie is not accessible via JavaScript
                Secure = false, // The cookie is only sent over HTTPS
                Expires = DateTimeOffset.UtcNow.AddDays(1) // Set expiration to 1 day
            };
            Response.Cookies.Append("user", user.Password, cookieOptions);
            return true;
        }
    }
}
