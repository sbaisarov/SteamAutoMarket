namespace SteamAutoMarket.UI.Repository.Image
{
    using SteamAutoMarket.Core;

    public static class ImageProvider
    {
        public static string GetItemImage(string marketHashName, string imageUrl)
        {
            if (imageUrl == null)
            {
                return null;
            }

            if (ImageCache.TryGetImage(marketHashName, out var localImageUri))
            {
                return localImageUri;
            }

            try
            {
                return ImageCache.CacheImage(marketHashName, $"https://steamcommunity-a.akamaihd.net/economy/image/{imageUrl}/192fx192f");
            }
            catch
            {
                return null;
            }
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
