namespace TelegramShop.Qiwi.QiwiApi.Exceptions
{
    using System;

    public class RequestException : Exception
    {
        private string _message;
        public override string Message
        {
            get { return this._message; }
        }

        public RequestException(string message)
        {
            this._message = message;
        }
    }
}