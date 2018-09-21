namespace TelegramShop.Telegram.MessageProcessor
{
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class LicensePurchaseDurationMessageHandler : TelegramShopMessageHandler
    {
        private readonly string licenseKey;

        public LicensePurchaseDurationMessageHandler(string licenseKey)
        {
            this.licenseKey = licenseKey;
        }

        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.LicenseDuration);
            ShopUserRepository.UpdateUserBuyLicenseKey(userModel, this.licenseKey);

            await telegramShop.SendMessage(e.Message.Chat.Id, AnswerMessage.LicenseDurationMenu, GetKeyboard(userModel.CurrentDialogState));
        }
    }
}