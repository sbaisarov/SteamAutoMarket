namespace TelegramShop.ShopUser
{
    public class LicenseBuyProcessModel
    {
        public string LicenseKey { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Days { get; set; }

        public string Comment { get; set; }
    }
}