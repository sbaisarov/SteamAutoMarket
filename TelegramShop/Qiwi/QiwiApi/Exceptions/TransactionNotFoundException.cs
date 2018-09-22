namespace TelegramShop.Qiwi.QiwiApi.Exceptions
{
    using System;

    public class TransactionNotFoundException : Exception
    {
        public override string Message
        {
            get { return "Transaction not found exception (404).Transaction were not found or no valid pyments with such signs."; }
        }
    }
}