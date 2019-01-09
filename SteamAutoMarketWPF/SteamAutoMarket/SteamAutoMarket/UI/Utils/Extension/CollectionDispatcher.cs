namespace SteamAutoMarket.UI.Utils.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public static class CollectionDispatcher
    {
        public static void AddDispatch<T>(this ICollection<T> collection, T item)
        {
            Action<T> addMethod = collection.Add;
            Application.Current.Dispatcher.Invoke(addMethod, item);
        }

        public static void ClearDispatch<T>(this ICollection<T> collection)
        {
            Action method = collection.Clear;
            Application.Current.Dispatcher.Invoke(method);
        }

        public static void ReplaceDispatch<T>(this ICollection<T> collection, ICollection<T> newCollection)
        {
            collection.ClearDispatch();
            Application.Current.Dispatcher.Invoke(
                () =>
                    {
                        foreach (var item in newCollection)
                        {
                            collection.Add(item);
                        }
                    });
        }
    }
}