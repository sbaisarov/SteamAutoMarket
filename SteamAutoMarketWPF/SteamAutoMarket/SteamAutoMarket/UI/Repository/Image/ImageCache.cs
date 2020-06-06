namespace SteamAutoMarket.UI.Repository.Image
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;

    using SteamAutoMarket.Core;

    public static class ImageCache
    {
        public const string ImageFormat = "jpg";

        public static readonly string ImagesPath = AppDomain.CurrentDomain.BaseDirectory + "images";

        public static Dictionary<string, string> Cache { get; set; } = new Dictionary<string, string>();

        public static string CacheImage(string name, string remoteUri)
        {
            var localFileName = $"{ImagesPath}\\{MakeValidFileName(name)}.{ImageFormat}";

            if (remoteUri == null)
            {
                return null;
            }

            if (File.Exists(localFileName))
            {
                Cache[name] = localFileName;
                return localFileName;
            }

            try
            {
                Directory.CreateDirectory(ImagesPath);
                SaveImage(localFileName, remoteUri);
                Cache[name] = localFileName;

                return localFileName;
            }
            catch (Exception e)
            {
                Logger.Log.Error($"Error on image {remoteUri} download - {e.Message}", e);
                return null;
            }
        }

        public static bool IsImageCached(string name)
        {
            if (name == null)
            {
                return false;
            }

            return Cache.ContainsKey(name);
        }

        public static bool TryGetImage(string name, out string localImageUri)
        {
            try
            {
                if (Cache.TryGetValue(name, out localImageUri))
                {
                    return true;
                }

                localImageUri = $"{ImagesPath}\\{MakeValidFileName(name)}.{ImageFormat}";
                if (!File.Exists(localImageUri))
                {
                    localImageUri = null;
                    return false;
                }

                Cache[name] = localImageUri;

                return true;
            }
            catch (Exception e)
            {
                Logger.Log.Error($"Error on getting cached image - {e.Message}", e);
                localImageUri = null;
                return false;
            }
        }

        private static string MakeValidFileName(string name)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(name, invalidRegStr, "_");
        }

        private static void SaveImage(string localFileName, string remoteFileUrl)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(remoteFileUrl, localFileName);
            }
        }
    }
}