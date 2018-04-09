using System;

namespace Market.Exceptions
{
    public class SteamException : Exception
    {
        public SteamException(){}

        public SteamException(string message) : base(message){}

        public SteamException(string message, Exception inner): base(message, inner){}
    }
}