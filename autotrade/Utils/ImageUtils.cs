using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace autotrade.Utils {
    class ImageUtils {
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
            var res = new Bitmap(width, height);
            res.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            using (var g = Graphics.FromImage(res)) {
                var dst = new Rectangle(0, 0, res.Width, res.Height);
                g.DrawImage(img, dst);
            }
            return res;
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
    }
}
