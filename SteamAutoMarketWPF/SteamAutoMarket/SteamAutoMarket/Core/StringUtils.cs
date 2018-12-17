namespace SteamAutoMarket.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Reflection;

    public static class StringUtils
    {
        private const string CollectionJoinSymbol = ", ";

        private const string EmptyCollectionSymbol = "''";

        public static string CookieContainerToString(CookieContainer cookieContainer)
        {
            if (cookieContainer == null || cookieContainer.Count == 0)
            {
                return EmptyCollectionSymbol;
            }

            var cookieCollection = new CookieCollection();

            var table = (Hashtable)cookieContainer.GetType().InvokeMember(
                "m_domainTable",
                BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance,
                null,
                cookieContainer,
                new object[] { });

            foreach (var tableKey in table.Keys)
            {
                var strTableKey = (string)tableKey;

                if (strTableKey[0] == '.')
                {
                    strTableKey = strTableKey.Substring(1);
                }

                var list = (SortedList)table[tableKey].GetType().InvokeMember(
                    "m_list",
                    BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance,
                    null,
                    table[tableKey],
                    new object[] { });

                foreach (var listKey in list.Keys)
                {
                    var url = "https://" + strTableKey + (string)listKey;
                    cookieCollection.Add(cookieContainer.GetCookies(new Uri(url)));
                }
            }

            if (cookieContainer.Count == 0)
            {
                return EmptyCollectionSymbol;
            }

            var cookiesList = new List<Cookie>(cookieContainer.Count);
            foreach (Cookie cookie in cookieCollection)
            {
                cookiesList.Add(cookie);
            }

            return string.Join(CollectionJoinSymbol, cookiesList.Select(item => item.ToString()));
        }

        public static string DictionaryToString<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
            where TKey : class where TValue : class
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                return EmptyCollectionSymbol;
            }

            return string.Join(CollectionJoinSymbol, dictionary.Select(item => $"'{item.Key}:{item.Value}'"));
        }

        public static string NameValueCollectionToString(NameValueCollection col)
        {
            if (col == null || col.Count == 0)
            {
                return EmptyCollectionSymbol;
            }

            return DictionaryToString(col.AllKeys.ToDictionary(x => x, x => col[x]));
        }
    }
}