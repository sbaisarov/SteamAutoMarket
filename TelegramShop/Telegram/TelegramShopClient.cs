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

        private TelegramLoggerClient TelegramLogger { get; } = new TelegramLoggerClient(
            "622878481:AAGeAze901P0y8pZCUrdoeOAwRKAivh7QeU",
            -289955011);

        public void StartMessageReceive()
        {
            this.BotClient.StartReceiving();
        }

        public async Task<Message> SendMessage(ChatId chatId, string message, IReplyMarkup keyboard)
        {
            this.semaphore.WaitOne();

            var result = await this.BotClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: message,
                             parseMode: ParseMode.Markdown,
                             replyMarkup: keyboard);
            this.semaphore.Release();
            await this.Log($"ME: {message}");
            return result;
        }

        public async Task Log(string message)
        {
            await this.TelegramLogger.Log(message);
        }

        public async Task BoldLog(string message)
        {
            await this.TelegramLogger.BoldLog(message);
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