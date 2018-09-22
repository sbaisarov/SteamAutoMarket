namespace TelegramShop
{
    using System.IO;

    using Newtonsoft.Json;

    using TelegramShop.Qiwi;
    using TelegramShop.Telegram;

    internal class Program
    {
        public static readonly SettingsModel Settings =
            JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText("settings.json"));

        public static void Main(string[] args)
        {
            var qiwi = new QiwiPaymentHistoryHandler(Settings.QiwiApi, Settings.QiwiNumber);
            var telegram = new TelegramShopClient(Settings.TelegramBotApi, qiwi);
            telegram.StartMessageReceive();
        }
    }
}