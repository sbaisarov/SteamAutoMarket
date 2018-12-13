namespace SteamAutoMarket.UI.Repository.Image
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using SteamAutoMarket.Core;

    public static class ImageProvider
    {
        public static string GetItemImage(Action<string> setOutput, string marketHashName, string imageUrl)
        {
            var fileName = marketHashName;

            if (ImageCache.TryGetImage(marketHashName, out var localImageUri))
            {
                return localImageUri;
            }

            if (imageUrl == null)
            {
                return null;
            }

            Task.Run(
                () =>
                    {
                        localImageUri = ImageCache.CacheImage(
                            fileName,
                            $"https://steamcommunity-a.akamaihd.net/economy/image/{imageUrl}/192fx192f");
                        Thread.Sleep(500);
                        setOutput(localImageUri);
                    });

            return null;
        }

        public static string GetSmallSteamProfileImage(string steamId, bool useCache)
        {
            var fileName = $"{steamId}-small";

            if (useCache && ImageCache.TryGetImage(fileName, out var localImageUri))
            {
                return localImageUri;
            }

            var remoteImageUri = ImageUtils.GetSteamProfileSmallImageUri(steamId);
            if (remoteImageUri == null)
            {
                return ResourceUtils.GetResourceImageUri("NoAvatarSmall.jpg");
            }

            localImageUri = ImageCache.CacheImage(fileName, remoteImageUri);
            return localImageUri;
        }
    }
}