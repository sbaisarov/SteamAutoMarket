namespace SteamAutoMarket.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Media.Imaging;

    public static class ResourceUtils
    {
        public static string GetResourceImageUri(string name) =>
            @"pack://application:,,,/" + Assembly.GetCallingAssembly().GetName().Name + ";component/"
            + $"resources/{name}";

        public static Image LoadResourceImage(string name)
        {
            if (name[0] == '/')
            {
                name = name.Substring(1);
            }

            var path = GetResourceImageUri(name);
            var bitmap = new BitmapImage(new Uri(path, UriKind.Absolute));

            return BitmapImage2Bitmap(bitmap);
        }

        public static T[] ToArray<T>(params T[] values) => values;

        public static List<T> ToList<T>(params T[] values) => new List<T>(values);

        public static Bitmap BitmapImage2Bitmap(BitmapSource bitmapImage)
        {
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                var bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}