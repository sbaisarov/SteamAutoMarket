namespace TelegramShop.Telegram
{
    internal class AnswerMessage
    {
        public const string MainMenu = "Please specify your question using menu.";

        public const string LicenseManageMenuTitle = "License manage menu.";

        public const string Contacts = "You may contact: @shatulsky or @shamanovsky.";

        public const string LicenseDurationMenu =
            "Plese select the period of time for which you want to purchase the License.";

        public const string YouHaveNextLicenses = "You have next License keys attached to your account:";

        public const string SendMeLicenseKeyToStartRenew = "Send me one of them to start renew process.";

        public const string LicenseKeyRemoveMenu =
            "Send me the License key witch you want to delete from your License keys list.";

        public const string LicenseKeyAddMenu =
            "Send me license key witch you want to add to your License keys list (key should be valid).";

        public const string LicenseKeyNotExist = "License key not exist.";

        public const string LicenseKeyNotBelongsToYou = "License key not belongs to you.";

        public const string RenewPaymentMethodMenu = "You requested the License renew for license key:\r\n"
                                                     + "`{license}`\r\n"
                                                     + "License type: *{days} days* for *{price} RUB*."
                                                     + "\r\nChose payment method to make payment:";

        public const string NewLicensePaymentMethodMenu = "You requested new License key:\r\n"
                                                          + "License type: *{days} days* for *{price} RUB*.\r\n"
                                                          + "Chose payment method to make payment:";

        public const string ErrorOnRequestProcessing = "Error on processing your request - ";

        public const string IncorrectCommand = "Incorrect command.";

        public const string QiwiPaymentMethod =
            "To complete license purchase you should send *{price} RUB* to the appropriate wallet:\r\n\r\n"
            + "Wallet: *+380955532698*\r\nAmount of money: *{price} RUB*\r\nPrice comment: *{comment}*\r\n\r\n"
            + "All conditions must be met. You have to pay a transfer commission.";

        public const string QiwiTimeToNewRequestLeft =
            "Your payment not found. You can retry your verify request in *{seconds} seconds*.";

        public const string QiwiNewLicenseKeyAdded =
            "New license key was successfuly activated for *{days} days*:\r\n`{license}`\r\n"
            + "Key in now attached to your telegram account.";

        public const string QiwiLicenseKeyDaysAdded =
            "The license was successfuly renewed for *{days} days*:\r\n`{license}`";

        public const string SteamAutoMarketInfo = "Manual - https://goo.gl/XJ6T14 \r\n"
                                                  + "Telegram chat - https://t.me/steamsolutions";

        public const string DemoLicense = "The Demo License is granted only on a personal basis. "
                                          + "To request a Demo License, contact the administrators - @shatulsky or @shamanovsky.";

        public const string SpamYouAreBlockedMessage = "*You are blocked for spamming for 60 minutes*.\r\n"
                                                       + "If you think this is an error please contact the administrators - @shatulsky or @shamanovsky.";

        public const string SpamWarningMessage =
            "*Please dont spam me!*\r\n*{times}* more times spam and you will be blocked for 1 hour!";
    }
}