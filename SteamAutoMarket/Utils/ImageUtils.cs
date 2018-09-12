namespace SteamAutoMarket.Utils
{
    using System;
    using System.Drawing;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using RestSharp;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.WorkingProcess.Caches;

    internal class ImageUtils
    {
        private static string lastRequestedImageHashName;

        public static Image DownloadImage(string url)
        {
            try
            {
                var req = WebRequest.Create(url);
                var res = req.GetResponse();
                var imgStream = res.GetResponseStream();
                if (imgStream == null)
                {
                    return null;
                }

                var img1 = Image.FromStream(imgStream);
                imgStream.Close();
                return img1;
            }
            catch (WebException)
            {
                return null;
            }
        }

        public static Image ResizeImage(Image img, int width, int height)
        {
            try
            {
                if (img.Width == width && img.Height == height)
                {
                    return img;
                }

                var res = new Bitmap(width, height);
                res.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                using (var g = Graphics.FromImage(res))
                {
                    var dst = new Rectangle(0, 0, res.Width, res.Height);
                    g.DrawImage(img, dst);
                }

                return res;
            }
            catch (InvalidOperationException)
            {
                Thread.Sleep(500);
                return ResizeImage(img, width, height);
            }
        }

        public static Image GetSteamProfileSmallImage(ulong steamId)
        {
            try
            {
                var image = ImagesCache.GetImage($"{steamId}");
                if (image != null)
                {
                    return image;
                }

                var client = new RestClient("https://steamcommunity.com");
                var request = new RestRequest($"/profiles/{steamId}/?xml=1", Method.GET);
                var response = client.Execute(request);
                var content = response.Content;

                var result = Regex.Match(content, @"<avatarIcon><!\[CDATA\[(.*)\]\]></avatarIcon>");
                var imageUrl = result.Groups[1].ToString();

                image = DownloadImage(imageUrl);
                ImagesCache.CacheImage($"{steamId}", image);
                return image;
            }
            catch (Exception ex)
            {
                Logger.Error("Error on getting profile image", ex);
                return null;
            }
        }

        public static void UpdateItemImageOnPanelAsync(AssetDescription assetDescription, Panel imageBox)
        {
            UpdateImageOnItemPanelAsync(assetDescription.MarketHashName, assetDescription.IconUrl, imageBox);
        }

        public static void UpdateItemImageOnPanelAsync(RgDescription description, Panel imageBox)
        {
            UpdateImageOnItemPanelAsync(description.MarketHashName, description.IconUrl, imageBox);
        }

        public static void UpdateImageOnItemPanelAsync(string hash, string iconUrl, Panel imageBox)
        {
            Task.Run(
                () =>
                    {
                        lastRequestedImageHashName = hash;

                        var image = ImagesCache.GetImage(hash);

                        if (image != null)
                        {
                            if (lastRequestedImageHashName == hash)
                            {
                                imageBox.BackgroundImage = ResizeImage(image, 100, 100);
                            }
                        }
                        else
                        {
                            if (lastRequestedImageHashName == hash)
                            {
                                imageBox.BackgroundImage = Resources.DefaultItem;
                            }

                            image = DownloadImage(
                                $"https://steamcommunity-a.akamaihd.net/economy/image/{iconUrl}/192fx192f");

                            if (image != null)
                            {
                                ImagesCache.CacheImage(hash, image);
                                if (lastRequestedImageHashName == hash)
                                {
                                    imageBox.BackgroundImage = ResizeImage(image, 100, 100);
                                }
                            }
                            else
                            {
                                if (lastRequestedImageHashName == hash)
                                {
                                    imageBox.BackgroundImage = Resources.DefaultItem;
                                }
                            }
                        }
                    });
        }
    }
}