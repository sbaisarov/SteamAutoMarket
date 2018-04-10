using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Market.Models;

namespace Market
{
    public static class Utils
    {
        public static string HashNameFromUrl(string url)
        {
            var urlSplit = url.Split('/');
            return HttpUtility.UrlDecode(urlSplit.Last().Split('?')[0], Encoding.UTF8);
        }

        public static int AppIdFromUrl(string url)
        {
            var urlSplit = url.Split('/');
            return int.Parse(urlSplit[urlSplit.Length - 2]);
        }

        public static string GetCaptchaImageUrl(string captchaGid)
        {
            var captcha = Urls.SteamCommunity + "/public/captcha.php?gid=" + captchaGid;
            return captcha;
        }

        public static string FirstCharacterToLower(string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str, 0))
                return str;

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static double MinPriceForOrder(this WalletInfo walletInfo)
        {
            return walletInfo.WalletFeeMinimum + walletInfo.WalletFee + walletInfo.WalletFee;
        }

        public static DateTime SteamTimeConvertor(string time)
        {
            if (DateTime.TryParseExact(time, "MMM dd yyyy HH: +0", CultureInfo.GetCultureInfo("en-US"),
                DateTimeStyles.None, out DateTime converted))
            {
                return converted;
            }
            else
            {
                throw new ArgumentException("Not a valid time string");
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static double PriseWithoutFee(double price, WalletInfo walletInfo)
        {
            return PriseWithoutFee(price, walletInfo.WalletFeePercent, walletInfo.WalletPublisherFeePercent, walletInfo.WalletFeeMinimum);
        }

        public static double PriceReFee(double price, WalletInfo walletInfo)
        {
            var priseWithoutFee = PriseWithoutFee(price, walletInfo.WalletFeePercent, walletInfo.WalletPublisherFeePercent, walletInfo.WalletFeeMinimum);
            var priseWithFee = PriseWithFee(priseWithoutFee, walletInfo.WalletFeePercent, walletInfo.WalletPublisherFeePercent, walletInfo.WalletFeeMinimum);
            return priseWithFee;
        }

        public static double PriceReFee(double price, int walletFeePercent, int publisherFeePercent, double feeMinimum)
        {
            var priseWithoutFee = PriseWithoutFee(price, walletFeePercent, publisherFeePercent, feeMinimum);
            var priseWithFee = PriseWithFee(priseWithoutFee, walletFeePercent, publisherFeePercent, feeMinimum);
            return priseWithFee;
        }

        public static double PriseWithoutFee(double price, int walletFeePercent, int publisherFeePercent, double feeMinimum)
        {
            var tempPrice = 100 * price;
            var totalFee = 100 + walletFeePercent + publisherFeePercent;

            var publisherFee = Math.Max(Math.Floor(tempPrice / totalFee * publisherFeePercent), feeMinimum * 100);
            var steamFee = Math.Max(Math.Floor(tempPrice / totalFee * walletFeePercent), feeMinimum * 100);

            var feePrice = publisherFee + steamFee;

            var priseWithoutFee = Math.Max(Math.Round((tempPrice - feePrice) / 100, 2), feeMinimum);
            return priseWithoutFee;
        }

        public static double PriseWithFee(double price, WalletInfo walletInfo)
        {
            return PriseWithFee(price, walletInfo.WalletFeePercent, walletInfo.WalletPublisherFeePercent, walletInfo.WalletFeeMinimum);
        }

        public static double PriseWithFee(double price, int walletFeePercent, int publisherFeePercent, double feeMinimum)
        {
            var tempPrice = price * 100;
            var tempFeeMinimum = feeMinimum * 100;
            var tempPublisherFeePercent = (double)publisherFeePercent / 100;
            var tempWalletFeePercent = (double) walletFeePercent/100;

            var priseWithFee = Math.Max(tempPrice, tempFeeMinimum) + Math.Max(Math.Floor(tempPublisherFeePercent * tempPrice), tempFeeMinimum) + Math.Max(Math.Floor(tempWalletFeePercent * tempPrice), tempFeeMinimum);
            return priseWithFee / 100;
        }

        public static double PriceCorrection(double price, WalletInfo walletInfo)
        {
            var minPrice = MinPriceForOrder(walletInfo);
            if (price <= minPrice) return minPrice;

            var tempPrice = price;
            while (PriceReFee(tempPrice, walletInfo) > price)
            {
                tempPrice = tempPrice - 0.01;
            }

            return tempPrice;
        }

        public static string CreateEconomyImage(string imageCode)
        {
            return Urls.CdnImage + imageCode;
        }
    }
}