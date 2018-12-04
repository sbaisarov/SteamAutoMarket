namespace SteamAutoMarket.Core
{
    using System;
    using System.Text.RegularExpressions;

    using RestSharp;

    public class ImageUtils
    {
        public static string GetSteamProfileFullImageUri(string steamId)
        {
            try
            {
                var client = new RestClient("https://steamcommunity.com");
                var request = new RestRequest($"/profiles/{steamId}/?xml=1");
                var response = client.Execute(request);
                var content = response.Content;

                var result = Regex.Match(content, @"<avatarFull><!\[CDATA\[(.*)\]\]></avatarFull>");
                var imageUrl = result.Groups[1].ToString();

                return imageUrl;
            }
            catch (Exception ex)
            {
                UiGlobalVariables.FileLogger.Warn("Error on getting profile image", ex);
                return null;
            }
        }

        public static string GetSteamProfileSmallImageUri(string steamId)
        {
            try
            {
                var client = new RestClient("https://steamcommunity.com");
                var request = new RestRequest($"/profiles/{steamId}/?xml=1");
                var response = client.Execute(request);
                var content = response.Content;

                var result = Regex.Match(content, @"<avatarMedium><!\[CDATA\[(.*)\]\]></avatarMedium>");
                var imageUrl = result.Groups[1].ToString();

                return imageUrl;
            }
            catch (Exception ex)
            {
                UiGlobalVariables.FileLogger.Warn("Error on getting profile image", ex);
                return null;
            }
        }
    }
}