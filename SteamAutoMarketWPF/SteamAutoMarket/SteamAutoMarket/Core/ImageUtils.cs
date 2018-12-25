namespace SteamAutoMarket.Core
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Media.Imaging;

    using RestSharp;

    public static class ImageUtils
    {
        public static BitmapImage ToBitmapImage(this Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static string GetSteamProfileFullImageUri(string steamId)
        {
            try
            {
                var client = new RestClient("https://steamcommunity.com");
                var request = new RestRequest($"/profiles/{steamId}/?xml=1");
                var response = client.Execute(request);
                var content = response.Content;

                var result = Regex.Match(content, @"<avatarFull><!\[CDATA\[(.*)\]\]></avatarFull>");
                var imageUrl = result.Groups[1].ToString();

                return imageUrl;
            }
            catch (Exception ex)
            {
                Logger.Log.Warn("Error on getting profile image", ex);
                return null;
            }
        }

        public static string GetSteamProfileSmallImageUri(string steamId)
        {
            try
            {
                var client = new RestClient("https://steamcommunity.com");
                var request = new RestRequest($"/profiles/{steamId}/?xml=1");
                var response = client.Execute(request);
                var content = response.Content;

                var result = Regex.Match(content, @"<avatarMedium><!\[CDATA\[(.*)\]\]></avatarMedium>");
                var imageUrl = result.Groups[1].ToString();

                return imageUrl;
            }
            catch (Exception ex)
            {
                Logger.Log.Warn("Error on getting profile image", ex);
                return null;
            }
        }
    }
}