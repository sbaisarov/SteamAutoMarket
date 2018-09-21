namespace TelegramShop.Telegram.MessageProcessor
{
    using System.Globalization;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using global::TelegramShop.ShopUser;

    public class PaymentMethodMessageHandler : TelegramShopMessageHandler
    {
        private readonly int daysCount;
        private readonly double price;

        private readonly string renewLicenseKey;

        public PaymentMethodMessageHandler(int daysCount, double price, string renewLicenseKey)
        {
            this.daysCount = daysCount;
            this.price = price;
            this.renewLicenseKey = renewLicenseKey;
        }

        public override async Task Process(TelegramShopClient telegramShop, MessageEventArgs e, ShopUserModel userModel)
        {
            ShopUserRepository.UpdateUserDialogState(userModel, EDialogState.PaymentMethod);
            ShopUserRepository.UpdateUserLicenseDuration(userModel, this.daysCount, this.price);

            await telegramShop.SendMessage(e.Message.Chat.Id, this.GetPaymentMethodMessage(userModel), GetKeyboard(userModel.CurrentDialogState));
        }

        private string GetPaymentMethodMessage(ShopUserModel user)
        {
            if (this.renewLicenseKey != null)
            {
                return AnswerMessage.RenewPaymentMethodMenu
                    .Replace("{days}", this.daysCount.ToString())
                    .Replace("{license}", this.renewLicenseKey)
                    .Replace("{price}", this.price.ToString(CultureInfo.InvariantCulture));
            }

            return AnswerMessage.NewLicensePaymentMethodMenu
                .Replace("{days}", this.daysCount.ToString())
                .Replace("{price}", this.price.ToString(CultureInfo.InvariantCulture));
        }
    }
}