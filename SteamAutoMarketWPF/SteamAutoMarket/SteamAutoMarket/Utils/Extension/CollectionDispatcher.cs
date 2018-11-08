namespace SteamAutoMarket.Utils.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public static class CollectionDispatcher
    {
        public static void AddDispatch<T>(this ICollection<T> collection, T item)
        {
            Action<T> addMethod = collection.Add;
            Application.Current.Dispatcher.BeginInvoke(addMethod, item);
        }
    }
}