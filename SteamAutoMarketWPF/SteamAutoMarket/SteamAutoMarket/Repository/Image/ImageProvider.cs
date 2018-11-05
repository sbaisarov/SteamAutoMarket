namespace SteamAutoMarket.Repository.Image
{
    using Core;

    public static class ImageProvider
    {
        public static string GetSmallSteamProfileImage(string steamId)
        {
            var fileName = $"{steamId}-small";

            if (ImageCache.TryGetImage(fileName, out var localImageUri))
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

        public static string GetItemImage(string marketHashName, string imageUrl)
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

            localImageUri = ImageCache.CacheImage(fileName, imageUrl);
            return localImageUri;
        }
    }
}