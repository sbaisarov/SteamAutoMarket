using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess {
    class ImagesCache {
        public static Dictionary<string, Image> ImageCache { get; set; } = new Dictionary<string, Image>();
        private static readonly string imagesPath = $"{Environment.CurrentDirectory}/images";

        public static Image GetImage(string hashName) {
            ImageCache.TryGetValue(hashName, out Image image);
            if (image != null) return image;

            string fileName = $"{imagesPath}/{MakeValidFileName(hashName)}.jpg";
            if (File.Exists(fileName)) {
                image = Image.FromFile(fileName);
                ImageCache[hashName] = image;
                return image;
            }

            return null;
        }

        public static void CacheImage(string hashName, Image image) {
            if (image == null) return;

            if (!ImageCache.ContainsKey(hashName)) {
                ImageCache.Add(hashName, image);
            }
            Directory.CreateDirectory(imagesPath);
            image.Save($"{imagesPath}/{MakeValidFileName(hashName)}.jpg");
        }

        private static string MakeValidFileName(string name) {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }
    }
}
