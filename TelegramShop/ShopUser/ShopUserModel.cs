namespace TelegramShop.ShopUser
{
    using System;
    using System.Collections.Generic;

    using TelegramShop.Telegram;

    public class ShopUserModel
    {
        public string Telegram { get; set; }

        public EDialogState CurrentDialogState { get; set; } = EDialogState.Main;

        public HashSet<string> LicenseKeys { get; set; } = new HashSet<string>();

        public LicenseBuyProcessModel LicenseBuyProcess { get; set; } = new LicenseBuyProcessModel();

        public DateTime LastMessageDate { get; set; }

        public int SpamWarning { get; set; } = 0;
    }
}