namespace TelegramShop.Telegram.MessageProcessor
{
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class LicenseKeyAddMessageHandler : TelegramShopMessageHandler
    {
        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.LicenseKeyAdd);

            await telegramShop.SendMessage(e.Message.Chat.Id, AnswerMessage.LicenseKeyAddMenu, GetKeyboard(userModel.CurrentDialogState));
        }
    }
}