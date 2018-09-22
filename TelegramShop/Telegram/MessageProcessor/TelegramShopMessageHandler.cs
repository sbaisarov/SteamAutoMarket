namespace TelegramShop.Telegram.MessageProcessor
{
    using System;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;
    using global::Telegram.Bot.Types.ReplyMarkups;

    using global::TelegramShop.ShopUser;

    public abstract class TelegramShopMessageHandler
    {
        public static ReplyKeyboardMarkup GetKeyboard(EDialogState state)
        {
            switch (state)
            {
                case EDialogState.Main:
                    return TelegramKeyboard.MainKeyboard;

                case EDialogState.ManageLicense:
                    return TelegramKeyboard.ManageLicenseKeyboard;

                case EDialogState.LicenseRenewSubscription:
                    return TelegramKeyboard.LicenseRenewSubscriptionKeyboard;

                case EDialogState.LicenseDuration:
                    return TelegramKeyboard.LicenseDurationKeyboard;

                case EDialogState.LicenseKeyAdd:
                    return TelegramKeyboard.LicenseAddKeyKeyboard;

                case EDialogState.LicenseKeyRemove:
                    return TelegramKeyboard.LicenseRemoveKeyboard;

                case EDialogState.PaymentMethod:
                    return TelegramKeyboard.PaymentMethodKeyboard;

                case EDialogState.QiwiPaymentVerification:
                    return TelegramKeyboard.QiwiPaymentMethodKeyboard;

                default:
                    throw new ArgumentException();
            }
        }

        public virtual Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            return Task.FromResult(default(object));
        }
    }
}