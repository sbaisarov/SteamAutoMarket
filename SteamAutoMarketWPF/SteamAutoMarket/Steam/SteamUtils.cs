namespace Steam
{
    using System;
    using System.Linq;

    using Steam.Market.Models;
    using Steam.TradeOffer.Models;

    public class SteamUtils
    {
        public static string GetClearDescription(RgDescription description)
        {
            if (description == null) return string.Empty;

            var descriptionText = string.Empty;

            descriptionText += $"Game: {description.Appid}{Environment.NewLine}";
            descriptionText += $"Name: {description.MarketHashName}{Environment.NewLine}";
            descriptionText += $"Type: {description.Type}{Environment.NewLine}";

            var descriptions = description.Descriptions?.Where(d => !string.IsNullOrWhiteSpace(d.Value.Trim()))
                .ToList();

            var tags = description.Tags?.Where(t => !string.IsNullOrWhiteSpace(t.LocalizedTagName.Trim())).ToList();
            if (tags != null && tags.Any())
            {
                descriptionText +=
                    $"Tags: {string.Join(", ", tags.Select(t => t.LocalizedTagName.Trim()))}{Environment.NewLine}";
            }

            if (descriptions != null && descriptions.Any())
            {
                descriptionText +=
                    $"Description: {string.Join(", ", descriptions.Select(d => d.Value.Trim()))}{Environment.NewLine}";
            }

            return descriptionText;
        }

        public static string GetClearDescription(AssetDescription description)
        {
            if (description == null) return string.Empty;

            var descriptionText = string.Empty;

            descriptionText += $"Game: {description.AppId}{Environment.NewLine}";
            descriptionText += $"Name: {description.MarketHashName}{Environment.NewLine}";
            descriptionText += $"Type: {description.Type}{Environment.NewLine}";

            var descriptions = description.Descriptions?.Where(d => !string.IsNullOrWhiteSpace(d.Value.Trim()))
                .ToList();

            if (descriptions != null && descriptions.Any())
            {
                descriptionText +=
                    $"Description: {string.Join(", ", descriptions.Select(d => d.Value.Trim()))}{Environment.NewLine}";
            }

            return descriptionText;
        }

        public static string GetClearDescription(MyListingsSalesItem item)
        {
            if (item == null) return string.Empty;

            var descriptionText = string.Empty;
            descriptionText += $"Name: {item.HashName}{Environment.NewLine}";
            descriptionText += $"Game: {item.Game}{Environment.NewLine}";
            descriptionText += $"App id: {item.AppId}{Environment.NewLine}";
            descriptionText += $"Sale id: {item.SaleId}{Environment.NewLine}";
            descriptionText += $"Url: {item.Url}{Environment.NewLine}";

            return descriptionText;
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

            if (type.Contains("Sale Trading Card"))
            {
                return "Sale Trading Card";
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
    }
}