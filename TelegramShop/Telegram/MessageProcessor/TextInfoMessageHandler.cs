namespace TelegramShop.Telegram.MessageProcessor
{
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class TextInfoMessageHandler : TelegramShopMessageHandler
    {
        private readonly string text;

        public TextInfoMessageHandler(string text)
        {
            this.text = text;
        }

        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel user)
        {
            await telegramShop.SendMessage(e.Message.Chat.Id, this.text, GetKeyboard(user.CurrentDialogState));
        }
    }
}