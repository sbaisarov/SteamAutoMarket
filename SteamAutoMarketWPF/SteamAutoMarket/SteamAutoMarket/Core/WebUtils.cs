namespace SteamAutoMarket.Core
{
    using System;
    using System.Net;

    public static class WebUtils
    {
        public static WebProxy ParseProxy(string s)
        {
            Logger.Log.Debug($"Parsing proxy - {s}");

            var spl = s.Split(':');

            WebProxy webProxyObj;
            Uri proxyUri;

            switch (spl.Length)
            {
                case 2:
                    proxyUri = new UriBuilder { Host = spl[0], Port = int.Parse(spl[1]) }.Uri;
                    webProxyObj = new WebProxy(proxyUri);
                    break;
                case 4:
                    ICredentials credentials = new NetworkCredential(spl[2], spl[3]);
                    proxyUri = new UriBuilder { Host = spl[0], Port = int.Parse(spl[1]) }.Uri;
                    webProxyObj = new WebProxy(proxyUri, true, null, credentials);
                    break;

                default: throw new ArgumentException($"Invalid proxy - {s}");
            }

            return webProxyObj;
        }
    }
}