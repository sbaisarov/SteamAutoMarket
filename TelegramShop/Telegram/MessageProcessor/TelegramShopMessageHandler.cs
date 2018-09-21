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
                case EDialogState.EMain:
                    return TelegramKeyboard.MainKeyboard;

                case EDialogState.EManageLicense:
                    return TelegramKeyboard.ManageLicenseKeyboard;

                case EDialogState.ELicenseRenewSubscription:
                    return TelegramKeyboard.LicenseRenewSubscriptionKeyboard;

                case EDialogState.ELicenseDuration:
                    return TelegramKeyboard.LicenseDurationKeyboard;

                case EDialogState.ELicenseKeyAdd:
                    return TelegramKeyboard.LicenseAddKeyKeyboard;

                case EDialogState.ELicenseKeyRemove:
                    return TelegramKeyboard.LicenseRemoveKeyboard;

                case EDialogState.EPaymentMethod:
                    return TelegramKeyboard.PaymentMethodKeyboard;

                case EDialogState.EQiwiPaymentVerification:
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