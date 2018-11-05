namespace Steam.Market.Exceptions
{
    using System;

    public class RequestException : SteamException
    {
        public RequestException(string message)
            : base(message)
        {
        }

        public RequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}