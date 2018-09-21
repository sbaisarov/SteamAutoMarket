namespace TelegramShop.Telegram
{
    public enum EDialogState
    {
        Main,

        ManageLicense,

        LicenseRenewSubscription,

        LicenseDuration,

        LicenseKeyAdd,

        LicenseKeyRemove,

        PaymentMethod,

        QiwiPaymentVerification,
    }
}