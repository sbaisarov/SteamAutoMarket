namespace TelegramShop.Telegram
{
    using global::Telegram.Bot.Types.ReplyMarkups;

    internal class TelegramKeyboard
    {
        public static readonly ReplyKeyboardMarkup MainKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.SteamAutoMarketInfo, MenuMessage.ManageLicense)
            .Add(MenuMessage.Contacts, MenuMessage.LicenseStatus)
            .GetKeyboard();

        public static readonly ReplyKeyboardMarkup ManageLicenseKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.BuyNewLicense, MenuMessage.LicenseRenewSubscription)
            .Add(MenuMessage.DemoLicense, MenuMessage.Back)
            .GetKeyboard();

        public static readonly ReplyKeyboardMarkup LicenseRenewSubscriptionKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.LicenseAddNewKey, MenuMessage.LicenseRemoveKey)
            .Add(MenuMessage.Back)
            .GetKeyboard();

        public static readonly ReplyKeyboardMarkup LicenseDurationKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.OneWeekDuration, MenuMessage.OneMonthDuration)
            .Add(MenuMessage.ThreeMonthDuration, MenuMessage.SixMonthDuration)
            .Add(MenuMessage.OneYearDuration, MenuMessage.Back)
            .GetKeyboard();

        public static readonly ReplyKeyboardMarkup LicenseAddKeyKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.Back)
            .GetKeyboard();

        public static readonly ReplyKeyboardMarkup LicenseRemoveKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.Back)
            .GetKeyboard();

        public static readonly ReplyKeyboardMarkup PaymentMethodKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.QiwiPaymentMethod)
            .Add(MenuMessage.Back)
            .GetKeyboard();

        public static readonly ReplyKeyboardMarkup QiwiPaymentMethodKeyboard = new KeyboardBuilder()
            .Add(MenuMessage.QiwiCheckTransactionStatus)
            .Add(MenuMessage.Back)
            .GetKeyboard();
    }
}