namespace TelegramShop.Telegram.MessageProcessor
{
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class LicenseManageMessageHandler : TelegramShopMessageHandler
    {
        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.ManageLicense);

            await telegramShop.SendMessage(e.Message.Chat.Id, AnswerMessage.LicenseManageMenuTitle, GetKeyboard(userModel.CurrentDialogState));
        }
    }
}