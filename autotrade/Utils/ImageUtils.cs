using autotrade.Steam.TradeOffer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.Utils {
    class ImageUtils {
        public static Image GetResourceImage(string name) {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            string[] names = myAssembly.GetManifestResourceNames();
            Stream stream = myAssembly.GetManifestResourceStream("autotrade.Resources." + name);
            return Image.FromStream(stream);
        }

        public static Image DownloadImage(string url) {
            try {
                WebRequest req = WebRequest.Create(url);
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image img1 = Image.FromStream(imgStream);
                imgStream.Close();
                return img1;
            } catch (WebException) {
                return null;
            }
        }

        public static Image ResizeImage(Image img, int width, int height) {
            try {
                if (img.Width == width && img.Height == height) return img;

                var res = new Bitmap(width, height);
                res.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                using (var g = Graphics.FromImage(res)) {
                    var dst = new Rectangle(0, 0, res.Width, res.Height);
                    g.DrawImage(img, dst);
                }
                return res;
            } catch (InvalidOperationException) {
                Thread.Sleep(500);
                return ResizeImage(img, width, height);
            }
        }

        public static Image GetSteamProfileSmallImage(ulong steamId) {
            try {
                Image image = WorkingProcess.ImagesCache.GetImage($"{steamId}");
                if (image != null) return image;

                var client = new RestClient("https://steamcommunity.com");
                var request = new RestRequest($"/profiles/{steamId}/?xml=1", Method.GET);
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                Match result = Regex.Match(content, @"<avatarIcon><!\[CDATA\[(.*)\]\]></avatarIcon>");
                var imageUrl = result.Groups[1].ToString();

                image = DownloadImage(imageUrl);
                WorkingProcess.ImagesCache.CacheImage($"{steamId}", image);
                return image;
            } catch (Exception e) {
                Logger.Error("Error on geting profile image", e);
                return null;
            }
        }

        public static void UpdateItemImageOnPanelAsync(AssetDescription assetDescription, Panel imageBox) {
            UpdateItemImageOnPanelAsync(assetDescription.MarketHashName, assetDescription.IconUrl, imageBox);
        }

        public static void UpdateItemImageOnPanelAsync(RgDescription description, Panel imageBox) {
            UpdateItemImageOnPanelAsync(description.market_hash_name, description.icon_url, imageBox);
        }

        public static void UpdateItemImageOnPanelAsync(string hash, string iconUrl, Panel imageBox) {
            Task.Run(() => {
                Image image = WorkingProcess.ImagesCache.GetImage(hash);

                if (image != null) {
                    imageBox.BackgroundImage = ResizeImage(image, 100, 100);
                } else {
                    image = DownloadImage("https://steamcommunity-a.akamaihd.net/economy/image/" + iconUrl + "/192fx192f");
                    if (image != null) {
                        WorkingProcess.ImagesCache.CacheImage(hash, image);
                        imageBox.BackgroundImage = ResizeImage(image, 100, 100);
                    } else {
                        imageBox.BackgroundImage = GetDefaultResourceImage("ItemImageBox.BackgroundImage", typeof(SaleControl));
                    }
                }
            });
        }

        public static Image GetDefaultResourceImage(string name, Type type) {
            return ((Image)((new System.ComponentModel.ComponentResourceManager(type)).GetObject(name)));
        }

    }
}
