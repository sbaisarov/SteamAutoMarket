namespace TelegramShop.Telegram.MessageProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.Qiwi.QiwiApi.Entities.Payments;
    using global::TelegramShop.Server;
    using global::TelegramShop.ShopUser;

    using TelegramShop.Qiwi.QiwiApi.Enumerations;

    public class QiwiPaymentVerificationMessageHandler : TelegramShopMessageHandler
    {
        private static DateTime lastRequestDate = DateTime.MinValue;

        private static List<Payment> cachedPayments;

        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.QiwiPaymentVerification);

            if (cachedPayments != null && IsCachedOld() == false)
            {
                await telegramShop.SendMessage(
                    e.Message.Chat.Id,
                    GetQiwiTimeToNewRequestLeftMessage(),
                    GetKeyboard(userModel.CurrentDialogState));

                return;
            }

            var payments = telegramShop.Qiwi.GetIncomingTransactions().Result;
            lastRequestDate = DateTime.Now;
            cachedPayments = payments;

            var isPaymentPresent = IsPaymentPresent(
                userModel.LicenseBuyProcess.Comment,
                userModel.LicenseBuyProcess.Price,
                payments);

            if (isPaymentPresent)
            {
                await telegramShop.BoldLog($"@{userModel.Telegram} bought license for {userModel.LicenseBuyProcess.Days} days for {userModel.LicenseBuyProcess.Price} RUB");

                ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.Main);

                if (userModel.LicenseBuyProcess.LicenseKey == null)
                {
                    var newLicenseKey = GetNewLicenseKey(userModel.Telegram);
                    LicenseServerHandler.AddNewLicense(newLicenseKey, userModel.LicenseBuyProcess.Days);

                    ShopUserRepository.AddLicenseKey(userModel, newLicenseKey);

                    await telegramShop.SendMessage(
                        e.Message.Chat.Id,
                        GetQiwiNewLicenseKeyAddedMessage(newLicenseKey, userModel.LicenseBuyProcess.Days),
                        GetKeyboard(userModel.CurrentDialogState));
                }
                else
                {
                    LicenseServerHandler.AddDaysForExistLicense(
                        userModel.LicenseBuyProcess.LicenseKey,
                        userModel.LicenseBuyProcess.Days);

                    await telegramShop.SendMessage(
                        e.Message.Chat.Id,
                        GetQiwiLicenseKeyDaysAddedMessage(
                            userModel.LicenseBuyProcess.LicenseKey,
                            userModel.LicenseBuyProcess.Days),
                        GetKeyboard(userModel.CurrentDialogState));
                }

                ShopUserRepository.UpdatePurchaseUniqueComment(userModel, null);
                ShopUserRepository.UpdateUserBuyLicenseKey(userModel, null);
                ShopUserRepository.UpdateUserLicenseDuration(userModel, 0, 0);
            }
            else
            {
                await telegramShop.SendMessage(
                    e.Message.Chat.Id,
                    GetQiwiTimeToNewRequestLeftMessage(),
                    GetKeyboard(userModel.CurrentDialogState));
            }
        }

        private static string GetNewLicenseKey(string username)
        {
            return username + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        private static string GetQiwiTimeToNewRequestLeftMessage()
        {
            var milliseconds = (lastRequestDate.AddMinutes(1) - DateTime.Now).TotalSeconds;

            var seconds = (int)Math.Round(milliseconds);
            if (seconds == 0)
            {
                seconds = 1;
            }

            return AnswerMessage.QiwiTimeToNewRequestLeft.Replace(
                "{seconds}",
                seconds.ToString(CultureInfo.InvariantCulture));
        }

        private static string GetQiwiNewLicenseKeyAddedMessage(string newLicenseKey, int days)
        {
            return AnswerMessage.QiwiNewLicenseKeyAdded.Replace("{license}", newLicenseKey)
                .Replace("{days}", days.ToString());
        }

        private static string GetQiwiLicenseKeyDaysAddedMessage(string newLicenseKey, int days)
        {
            return AnswerMessage.QiwiLicenseKeyDaysAdded.Replace("{license}", newLicenseKey)
                .Replace("{days}", days.ToString());
        }

        private static bool IsCachedOld()
        {
            return lastRequestDate.AddMinutes(1) < DateTime.Now;
        }

        private static bool IsPaymentPresent(
            string uniquePaymentBody,
            double requiredMoneyAmount,
            List<Payment> payments)
        {
            if (payments == null || !payments.Any() || uniquePaymentBody == null || requiredMoneyAmount <= 0)
            {
                return false;
            }

            return payments.Any(
                p => p.comment == uniquePaymentBody && p.status == PaymentStatus.SUCCESS
                                                    && p.total.amount >= requiredMoneyAmount);
        }
    }
}