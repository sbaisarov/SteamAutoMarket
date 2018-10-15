namespace TelegramShop.Telegram
{
    using System.Threading;
    using System.Threading.Tasks;

    using global::Telegram.Bot;
    using global::Telegram.Bot.Types.Enums;

    public class TelegramLoggerClient
    {
        private readonly long chatId;

        private readonly ITelegramBotClient botClient;

        private readonly Semaphore semaphore = new Semaphore(1, 1);

        public TelegramLoggerClient(string botToken, long chatId)
        {
            this.chatId = chatId;
            this.botClient = new TelegramBotClient(botToken);
        }

        public async Task Log(string message)
        {
            this.semaphore.WaitOne();

            var result = await this.botClient.SendTextMessageAsync(
                             chatId: this.chatId,
                             text: FormatThinLogMessage(message),
                             parseMode: ParseMode.Markdown);

            this.semaphore.Release();
        }

        public async Task BoldLog(string message)
        {
            this.semaphore.WaitOne();

            var result = await this.botClient.SendTextMessageAsync(
                             chatId: this.chatId,
                             text: FormatBoldLogMessage(message),
                             parseMode: ParseMode.Markdown);

            this.semaphore.Release();
        }

        private static string FormatThinLogMessage(string logMessage)
        {
            return "`" + logMessage.Trim().Replace("  ", " ") + "`";
        }

        private static string FormatBoldLogMessage(string logMessage)
        {
            return "*" + logMessage.Trim().Replace("  ", " ") + "*";
        }
    }
}