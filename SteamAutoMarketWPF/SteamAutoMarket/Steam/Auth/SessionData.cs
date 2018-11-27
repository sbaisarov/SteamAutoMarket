namespace Steam.Auth
{
    using System.Net;

    public class SessionData
    {
        public string OAuthToken { get; set; }

        public string SessionID { get; set; }

        public ulong SteamID { get; set; }

        public string SteamLogin { get; set; }

        public string SteamLoginSecure { get; set; }

        public string WebCookie { get; set; }

        public void AddCookies(CookieContainer cookies)
        {
            cookies.Add(new Cookie("mobileClientVersion", "0 (2.1.3)", "/", ".steamcommunity.com"));
            cookies.Add(new Cookie("mobileClient", "android", "/", ".steamcommunity.com"));

            cookies.Add(new Cookie("steamid", this.SteamID.ToString(), "/", ".steamcommunity.com"));
            cookies.Add(new Cookie("steamLogin", this.SteamLogin, "/", ".steamcommunity.com") { HttpOnly = true });

            cookies.Add(
                new Cookie("steamLoginSecure", this.SteamLoginSecure, "/", ".steamcommunity.com")
                    {
                        HttpOnly = true, Secure = true
                    });
            cookies.Add(new Cookie("Steam_Language", "english", "/", ".steamcommunity.com"));
            cookies.Add(new Cookie("dob", string.Empty, "/", ".steamcommunity.com"));
            cookies.Add(new Cookie("sessionid", this.SessionID, "/", ".steamcommunity.com"));
        }
    }
}