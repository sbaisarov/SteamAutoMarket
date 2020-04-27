namespace SteamAutoMarket.UI.Utils.Extension
{
    using System.Collections.Generic;
    using System.Windows;

    public static class CollectionDispatcher
    {
        public static void AddDispatch<T>(this ICollection<T> collection, T item)
        {
            Application.Current.Dispatcher.Invoke(() => { collection.Add(item); });
        }

        public static void AddRangeDispatch<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                    {
                        foreach (var item in items)
                        {
                            collection.Add(item);
                        }
                    });
        }

        public static void ClearDispatch<T>(this ICollection<T> collection)
        {
            Application.Current.Dispatcher.Invoke(collection.Clear);
        }

        public static void RemoveDispatch<T>(this ICollection<T> collection, T item)
        {
            Application.Current.Dispatcher.Invoke(() => collection.Remove(item));
        }

        public static void ReplaceDispatch<T>(this ICollection<T> collection, ICollection<T> newCollection)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                    {
                        collection.Clear();
                        foreach (var item in newCollection)
                        {
                            collection.Add(item);
                        }
                    });
        }
    }
}