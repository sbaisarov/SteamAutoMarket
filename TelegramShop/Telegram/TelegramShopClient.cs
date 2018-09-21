namespace TelegramShop.Telegram
{
    using System.Threading;
    using System.Threading.Tasks;

    using global::Telegram.Bot;
    using global::Telegram.Bot.Args;
    using global::Telegram.Bot.Types;
    using global::Telegram.Bot.Types.Enums;
    using global::Telegram.Bot.Types.ReplyMarkups;

    using global::TelegramShop.Qiwi;

    public class TelegramShopClient
    {
        private readonly Semaphore semaphore;

        public TelegramShopClient(string botToken, QiwiPaymentHistoryHandler qiwi)
        {
            this.BotClient = new TelegramBotClient(botToken);
            this.semaphore = new Semaphore(1, 1);
            this.Qiwi = qiwi;
            this.BotClient.OnMessage += this.BotOnMessage;
        }

        public QiwiPaymentHistoryHandler Qiwi { get; }

        public ITelegramBotClient BotClient { get; }


        public void StartMessageReceive()
        {
            this.BotClient.StartReceiving();
            Thread.Sleep(Timeout.Infinite);
        }

        public async Task<Message> SendMessage(ChatId chatId, string message, IReplyMarkup keyboard)
        {
            this.semaphore.WaitOne();
            var result = await this.BotClient.SendTextMessageAsync(chatId: chatId, text: message, parseMode: ParseMode.Markdown, replyMarkup: keyboard);
            this.semaphore.Release();

            return result;
        }

        private void BotOnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == null)
            {
                return;
            }

            this.ProcessMenuMessages(e);
        }

        private async void ProcessMenuMessages(MessageEventArgs e)
        {
            await MessageLogicHandler.ProcessMessage(this, e);
        }
    }
}