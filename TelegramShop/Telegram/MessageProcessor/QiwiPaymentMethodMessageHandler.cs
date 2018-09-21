namespace TelegramShop.Telegram.MessageProcessor
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class QiwiPaymentMethodMessageHandler : TelegramShopMessageHandler
    {
        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.EQiwiPaymentVerification);

            await telegramShop.SendMessage(e.Message.Chat.Id, GetQiwiPaymentMessage(userModel), GetKeyboard(userModel.CurrentDialogState));
        }

        private static string GetUniqueComment()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        private static string GetQiwiPaymentMessage(ShopUserModel user)
        {
            var comment = GetUniqueComment();
            ShopUserRepository.UpdatePurchaseUniqueComment(user, comment);

            return AnswerMessage.QiwiPaymentMethod
                .Replace("{price}", user.LicenseBuyProcess.Price.ToString(CultureInfo.InvariantCulture))
                .Replace("{comment}", comment);
        }

    }
}