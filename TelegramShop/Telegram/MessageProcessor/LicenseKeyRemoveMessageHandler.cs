namespace TelegramShop.Telegram.MessageProcessor
{
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class LicenseKeyRemoveMessageHandler : TelegramShopMessageHandler
    {
        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.LicenseKeyRemove);

            await telegramShop.SendMessage(e.Message.Chat.Id, AnswerMessage.LicenseKeyRemoveMenu, GetKeyboard(userModel.CurrentDialogState));
        }
    }
}