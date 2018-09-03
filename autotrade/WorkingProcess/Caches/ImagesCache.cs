using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace autotrade.WorkingProcess.Caches
{
    internal class ImagesCache
    {
        public static readonly string IMAGES_PATH = AppDomain.CurrentDomain.BaseDirectory + "images";
        public static Dictionary<string, Image> ImageCache { get; set; } = new Dictionary<string, Image>();

        public static Image GetImage(string hashName)
        {
            ImageCache.TryGetValue(hashName, out var image);
            if (image != null) return image;

            var fileName = $"{IMAGES_PATH}\\{MakeValidFileName(hashName)}.jpg";
            if (File.Exists(fileName))
            {
                image = Image.FromFile(fileName);
                ImageCache[hashName] = image;
                return image;
            }

            return null;
        }

        public static void CacheImage(string hashName, Image image)
        {
            if (image == null) return;

            if (!ImageCache.ContainsKey(hashName)) ImageCache.Add(hashName, image);
            Directory.CreateDirectory(IMAGES_PATH);
            try
            {
                image.Save($"{IMAGES_PATH}/{MakeValidFileName(hashName)}.jpg");
            }
            catch
            {
            }

            ;
        }

        private static string MakeValidFileName(string name)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(name, invalidRegStr, "_");
        }
    }
}