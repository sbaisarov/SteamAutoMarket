namespace TelegramShop.Telegram.MessageProcessor
{
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class BackMessageHandler : TelegramShopMessageHandler
    {
        private readonly EDialogState dialogState;

        private readonly string message;

        public BackMessageHandler(EDialogState dialogState, string message)
        {
            this.dialogState = dialogState;
            this.message = message;
        }

        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, this.dialogState);

            await telegramShop.SendMessage(e.Message.Chat.Id, this.message, GetKeyboard(userModel.CurrentDialogState));
        }
    }
}