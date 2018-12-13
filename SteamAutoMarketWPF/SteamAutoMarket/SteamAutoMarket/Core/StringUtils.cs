namespace SteamAutoMarket.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public static class StringUtils
    {
        public static string DictionaryToString<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
            where TKey : class where TValue : class
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                return "''";
            }

            return string.Join(", ", dictionary.Select(item => $"'{item.Key}:{item.Value}'"));
        }
    }
}