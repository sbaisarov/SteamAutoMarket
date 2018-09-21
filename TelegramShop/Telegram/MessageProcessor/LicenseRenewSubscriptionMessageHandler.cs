namespace TelegramShop.Telegram.MessageProcessor
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class LicenseRenewSubscriptionMessageHandler : TelegramShopMessageHandler
    {
        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.ELicenseRenewSubscription);
            var keyboard = GetKeyboard(userModel.CurrentDialogState);

            await telegramShop.SendMessage(e.Message.Chat.Id, GetAllUsersLicenseMessage(userModel), keyboard);

            await telegramShop.SendMessage(e.Message.Chat.Id, AnswerMessage.SendMeLicenseKeyToStartRenew, keyboard);
        }

        private static string GetAllUsersLicenseMessage(ShopUserModel user)
        {
            var message = new StringBuilder(AnswerMessage.YouHaveNextLicenses);
            foreach (var key in user.LicenseKeys)
            {
                message.Append(Environment.NewLine).Append("`").Append(key).Append("`");
            }

            return message.ToString();
        }
    }
}