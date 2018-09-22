namespace TelegramShop.Telegram
{
    using System;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.Server;
    using global::TelegramShop.ShopUser;
    using global::TelegramShop.Telegram.MessageProcessor;

    public class MessageLogicHandler
    {
        public static async Task ProcessMessage(TelegramShopClient telegramShop, MessageEventArgs e)
        {
            var user = ShopUserRepository.GetUser(e.Message.From);
            
            try
            {
                if (await SpamHandler.IsSpamming(telegramShop, e, user))
                {
                    return;
                }

                await GetMenuProcessor(user, e).Process(telegramShop, e, user);
            }
            catch (Exception ex)
            {
                await telegramShop.SendMessage(
                    e.Message.Chat.Id,
                    AnswerMessage.ErrorOnRequestProcessing + ex.Message,
                    TelegramShopMessageHandler.GetKeyboard(user.CurrentDialogState));
            }
        }

        public static TelegramShopMessageHandler GetMenuProcessor(ShopUserModel user, MessageEventArgs e)
        {
            TelegramShopMessageHandler messageProcessor;

            switch (user.CurrentDialogState)
            {
                case EDialogState.Main:
                    messageProcessor = GetMainPageProcessor(e, user);
                    break;

                case EDialogState.ManageLicense:
                    messageProcessor = GetManageLicensePageProcessor(e);
                    break;

                case EDialogState.LicenseRenewSubscription:
                    messageProcessor = GetLicenseRenewSubscriptionPageProcessor(e, user);
                    break;

                case EDialogState.LicenseDuration:
                    messageProcessor = GetLicenseDurationPageProcessor(e, user);
                    break;

                case EDialogState.LicenseKeyAdd:
                    messageProcessor = GetLicenseKeyAddPageProcessor(e, user);
                    break;

                case EDialogState.LicenseKeyRemove:
                    messageProcessor = GetLicenseKeyRemovePageProcessor(e, user);
                    break;

                case EDialogState.PaymentMethod:
                    messageProcessor = GetPaymentMethodPageProcessor(e);
                    break;

                case EDialogState.QiwiPaymentVerification:
                    messageProcessor = GetQiwiPaymentPageProcessor(e);
                    break;

                default: throw new ArgumentException(AnswerMessage.IncorrectCommand);
            }

            return messageProcessor;
        }

        private static TelegramShopMessageHandler GetMainPageProcessor(MessageEventArgs e, ShopUserModel user)
        {
            switch (e.Message.Text)
            {
                case MenuMessage.Contacts: return new TextInfoMessageHandler(AnswerMessage.Contacts);
                case MenuMessage.ManageLicense: return new LicenseManageMessageHandler();
                case MenuMessage.LicenseStatus: return new TextInfoMessageHandler(LicenseServerHandler.GetLicenseStatus(user.LicenseKeys));
                case MenuMessage.SteamAutoMarketInfo: return new TextInfoMessageHandler(AnswerMessage.SteamAutoMarketInfo);
                default: throw new ArgumentException(AnswerMessage.IncorrectCommand);
            }
        }

        private static TelegramShopMessageHandler GetManageLicensePageProcessor(MessageEventArgs e)
        {
            switch (e.Message.Text)
            {
                case MenuMessage.BuyNewLicense: return new LicensePurchaseDurationMessageHandler(null);
                case MenuMessage.LicenseRenewSubscription: return new LicenseRenewSubscriptionMessageHandler();
                case MenuMessage.DemoLicense: return new TextInfoMessageHandler(AnswerMessage.DemoLicense);
                case MenuMessage.Back: return new BackMessageHandler(EDialogState.Main, AnswerMessage.MainMenu);
                default: throw new ArgumentException(AnswerMessage.IncorrectCommand);
            }
        }

        private static TelegramShopMessageHandler GetLicenseRenewSubscriptionPageProcessor(MessageEventArgs e, ShopUserModel user)
        {
            if (user.LicenseKeys.Contains(e.Message.Text))
            {
                return new LicensePurchaseDurationMessageHandler(e.Message.Text);
            }

            switch (e.Message.Text)
            {
                case MenuMessage.LicenseAddNewKey: return new LicenseKeyAddMessageHandler();
                case MenuMessage.LicenseRemoveKey: return new LicenseKeyRemoveMessageHandler();
                case MenuMessage.Back: return new BackMessageHandler(EDialogState.Main, AnswerMessage.MainMenu);
                default: throw new ArgumentException(AnswerMessage.IncorrectCommand);
            }
        }

        private static TelegramShopMessageHandler GetLicenseDurationPageProcessor(MessageEventArgs e, ShopUserModel user)
        {
            switch (e.Message.Text)
            {
                case MenuMessage.OneWeekDuration: return new PaymentMethodMessageHandler(7, 100, user.LicenseBuyProcess.LicenseKey);
                case MenuMessage.OneMonthDuration: return new PaymentMethodMessageHandler(30, 300, user.LicenseBuyProcess.LicenseKey);
                case MenuMessage.ThreeMonthDuration: return new PaymentMethodMessageHandler(90, 800, user.LicenseBuyProcess.LicenseKey);
                case MenuMessage.SixMonthDuration: return new PaymentMethodMessageHandler(180, 1500, user.LicenseBuyProcess.LicenseKey);
                case MenuMessage.OneYearDuration: return new PaymentMethodMessageHandler(365, 2800, user.LicenseBuyProcess.LicenseKey);
                case MenuMessage.Back: return new BackMessageHandler(EDialogState.Main, AnswerMessage.MainMenu);
                default: throw new ArgumentException(AnswerMessage.IncorrectCommand);
            }
        }

        private static TelegramShopMessageHandler GetLicenseKeyAddPageProcessor(MessageEventArgs e, ShopUserModel user)
        {
            if (e.Message.Text == MenuMessage.Back)
            {
                return new BackMessageHandler(EDialogState.Main, AnswerMessage.MainMenu);
            }
        
            if (LicenseServerHandler.IsLicenseExist(e.Message.Text))
            {
                ShopUserRepository.AddLicenseKey(user, e.Message.Text);
                return new LicenseRenewSubscriptionMessageHandler();
            }

            throw new ArgumentException(AnswerMessage.LicenseKeyNotExist);
        }

        private static TelegramShopMessageHandler GetLicenseKeyRemovePageProcessor(MessageEventArgs e, ShopUserModel user)
        {
            if (e.Message.Text == MenuMessage.Back)
            {
                return new BackMessageHandler(EDialogState.Main, AnswerMessage.MainMenu);
            }

            if (user.LicenseKeys.Contains(e.Message.Text))
            {
                ShopUserRepository.RemoveLicenseKey(user, e.Message.Text);
                return new LicenseRenewSubscriptionMessageHandler();
            }

            throw new ArgumentException(AnswerMessage.LicenseKeyNotBelongsToYou);
        }

        private static TelegramShopMessageHandler GetPaymentMethodPageProcessor(MessageEventArgs e)
        {
            switch (e.Message.Text)
            {
                case MenuMessage.QiwiPaymentMethod: return new QiwiPaymentMethodMessageHandler();
                case MenuMessage.Back: return new BackMessageHandler(EDialogState.Main, AnswerMessage.MainMenu);
                default: throw new ArgumentException(AnswerMessage.IncorrectCommand);
            }
        }

        private static TelegramShopMessageHandler GetQiwiPaymentPageProcessor(MessageEventArgs e)
        {
            switch (e.Message.Text)
            {
                case MenuMessage.QiwiCheckTransactionStatus: return new QiwiPaymentVerificationMessageHandler();
                case MenuMessage.Back: return new BackMessageHandler(EDialogState.Main, AnswerMessage.MainMenu);
                default: throw new ArgumentException(AnswerMessage.IncorrectCommand);
            }
        }
    }
}