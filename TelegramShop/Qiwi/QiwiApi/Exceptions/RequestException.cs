namespace TelegramShop.Qiwi.QiwiApi.Exceptions
{
    using System;

    public class RequestException : Exception
    {
        public RequestException(string message)
        {
            this.Message = message;
        }

        public override string Message { get; }
    }
}