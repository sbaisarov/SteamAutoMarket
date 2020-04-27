namespace SteamAutoMarket.Steam
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Repository.Context;

    [Obfuscation(Exclude = true)]
    public static class SteamUtils
    {
        public static string GetClearDescription(FullRgItem item)
        {
            var description = item?.Description;
            if (item == null || description == null) return string.Empty;

            var descriptionTextBuilder = new StringBuilder();
            descriptionTextBuilder.Append($"Amount: {item.Asset.Amount}{Environment.NewLine}");
            descriptionTextBuilder.Append($"Game: {description.Appid}{Environment.NewLine}");

            if (description.Type.EndsWith("Trading Card"))
            {
                var appid = GetClearCardAppid(description.MarketHashName);
                descriptionTextBuilder.Append($"Set: {SetsHelper.GetSetsCount(appid, UiGlobalVariables.SteamManager.Proxy)}{Environment.NewLine}");
            }

            descriptionTextBuilder.Append($"Name: {description.MarketHashName}{Environment.NewLine}");
            descriptionTextBuilder.Append($"Type: {description.Type}{Environment.NewLine}");

            var descriptions = description.Descriptions?.Where(d => !string.IsNullOrWhiteSpace(d.Value.Trim())).ToArray();

            if (descriptions != null && descriptions.Any())
            {
                var descriptionText = string.Join(", ", descriptions.Select(d => d.Value.Trim()));
                descriptionTextBuilder.Append($"Description: {descriptionText}{Environment.NewLine}");
            }

            var tags = description.Tags?.Where(t => !string.IsNullOrWhiteSpace(t.LocalizedTagName.Trim())).ToArray();
            if (tags != null && tags.Any())
            {
                var tagsText = string.Join(", ", tags.Select(t => t.LocalizedTagName.Trim()));
                descriptionTextBuilder.Append($"Tags: {tagsText}{Environment.NewLine}");
            }

            return descriptionTextBuilder.ToString();
        }

        public static string GetClearDescription(FullTradeItem item)
        {
            var description = item?.Description;
            if (item == null || description == null)
            {
                return string.Empty;
            }

            var descriptionBuilder = new StringBuilder();

            descriptionBuilder.Append($"Game: {description.AppId}{Environment.NewLine}");
            descriptionBuilder.Append($"Name: {description.MarketHashName}{Environment.NewLine}");
            descriptionBuilder.Append($"Type: {description.Type}{Environment.NewLine}");

            var descriptions = description.Descriptions?.Where(d => !string.IsNullOrWhiteSpace(d.Value.Trim()))
                .ToArray();

            if (descriptions != null && descriptions.Any())
            {
                descriptionBuilder.Append($"Description: {string.Join(", ", descriptions.Select(d => d.Value.Trim()))}{Environment.NewLine}");
            }

            return descriptionBuilder.ToString();
        }

        public static string GetClearDescription(MyListingsSalesItem item)
        {
            if (item == null) return string.Empty;

            var descriptionBuilder = new StringBuilder();
            descriptionBuilder.Append($"Name: {item.HashName}{Environment.NewLine}");
            descriptionBuilder.Append($"Game: {item.Game}{Environment.NewLine}");
            descriptionBuilder.Append($"App id: {item.AppId}{Environment.NewLine}");
            descriptionBuilder.Append($"Sale id: {item.SaleId}{Environment.NewLine}");
            descriptionBuilder.Append($"Url: {item.Url}{Environment.NewLine}");

            return descriptionBuilder.ToString();
        }

        public static string GetClearItemType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return "[None]";
            }

            if (type.Contains("Sale Foil Trading Card"))
            {
                return "Sale Foil Trading Card";
            }

            if (type.Contains("The Steam Winter Sale") || type.Contains("The Steam Summer Sale"))
            {
                if (type.Contains("Foil"))
                {
                    return "Sale Foil Trading Card";
                }

                if (type.Contains("Trading Card"))
                {
                    return "Sale Trading Card";
                }

                if (type.Contains("Emoticon"))
                {
                    return "Sale Emoticon";
                }

                if (type.Contains("Consumable"))
                {
                    return "Sale Consumable";
                }

                if (type.Contains("Background"))
                {
                    return "Sale Background";
                }
            }

            if (type.Contains("Foil Trading Card"))
            {
                return "Foil Trading Card";
            }

            if (type.Contains("Trading Card"))
            {
                return "Trading Card";
            }

            if (type.Contains("Emoticon"))
            {
                return "Emoticon";
            }

            if (type.Contains("Background"))
            {
                return "Background";
            }

            if (type.Contains("Sale Item"))
            {
                return "Sale Item";
            }

            return type;
        }

        public static DateTime ParseSteamUnixDate(int date)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(date).ToLocalTime();
            return dateTime;
        }

        public static string GetClearDescription(FullHistoryTradeItem item)
        {
            var description = item?.Description;
            if (item == null || description == null) return string.Empty;

            var descriptionTextBuilder = new StringBuilder($"Amount: {item.Asset.Amount}{Environment.NewLine}");
            descriptionTextBuilder.Append($"Game: {description.AppId}{Environment.NewLine}");
            descriptionTextBuilder.Append($"Name: {description.MarketHashName}{Environment.NewLine}");
            descriptionTextBuilder.Append($"Type: {description.Type}{Environment.NewLine}");

            var descriptions = description.Descriptions?.Where(d => !string.IsNullOrWhiteSpace(d.Value.Trim()))
                .ToArray();

            if (descriptions != null && descriptions.Any())
            {
                descriptionTextBuilder.Append($"Description: {string.Join(", ", descriptions.Select(d => d.Value.Trim()))}{Environment.NewLine}");
            }

            return descriptionTextBuilder.ToString();
        }

        private static string GetClearCardAppid(string marketHashName)
        {
            const string defaultValue = "0";
            try
            {
                return string.IsNullOrEmpty(marketHashName) ? defaultValue : Regex.Match(marketHashName, "(\\d+)-").Groups[1].Value;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
