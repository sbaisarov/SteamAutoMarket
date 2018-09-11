namespace SteamAutoMarket.WorkingProcess.Caches
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    internal class ImagesCache
    {
        public static readonly string ImagesPath = AppDomain.CurrentDomain.BaseDirectory + "images";

        public static Dictionary<string, Image> ImageCache { get; set; } = new Dictionary<string, Image>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Image GetImage(string name)
        {
            ImageCache.TryGetValue(name, out var image);
            if (image != null)
            {
                return image;
            }

            var fileName = $"{ImagesPath}\\{MakeValidFileName(name)}.jpg";

            if (!File.Exists(fileName))
            {
                return null;
            }

            image = Image.FromFile(fileName);
            ImageCache[name] = image;
            return image;
        }

        public static void CacheImage(string hashName, Image image)
        {
            if (image == null)
            {
                return;
            }

            if (!ImageCache.ContainsKey(hashName))
            {
                ImageCache.Add(hashName, image);
            }

            Directory.CreateDirectory(ImagesPath);
            try
            {
                image.Save($"{ImagesPath}/{MakeValidFileName(hashName)}.jpg");
            }
            catch
            {
                // ignored
            }
        }

        private static string MakeValidFileName(string name)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(name, invalidRegStr, "_");
        }
    }
}