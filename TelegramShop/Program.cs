namespace TelegramShop
{
    using System.IO;
    using System.Threading;

    using Newtonsoft.Json;

    using TelegramShop.Qiwi;
    using TelegramShop.Telegram;

    internal class Program
    {
        public static readonly SettingsModel Settings =
            JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText("settings.json"));

        public static void Main(string[] args)
        {
            var qiwiPaymentHistoryHandler = new QiwiPaymentHistoryHandler(Settings.QiwiApi, Settings.QiwiNumber);
            var telegramShopClient = new TelegramShopClient(Settings.TelegramBotApi, qiwiPaymentHistoryHandler);
            telegramShopClient.StartMessageReceive();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}