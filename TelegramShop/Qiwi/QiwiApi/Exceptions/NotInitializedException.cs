namespace TelegramShop.Qiwi.QiwiApi.Exceptions
{
    using System;

    public class NotInitializedException : Exception
    {
        public override string Message
        {
            get { return "QiwiApi has not been initalized. Call 'QiwiApi.Initialize(*token*) first."; }
        }
    }
}