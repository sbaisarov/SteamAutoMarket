namespace TelegramShop.Qiwi.QiwiApi.Exceptions
{
    using System;

    public class WalletNotFoundException : Exception
    {
        public override string Message
        {
            get { return "Wallet not found exception (404). Wallet was not found."; }
        }
    }
}