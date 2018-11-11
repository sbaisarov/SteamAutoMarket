﻿namespace SteamAutoMarket.Utils.Extension
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

        public static void ReplaceDispatch<T>(this ICollection<T> collection, ICollection<T> newCollection)
        {
            collection.ClearDispatch();
            foreach (var item in newCollection)
            {
                collection.AddDispatch(item);
            }
        }

        public static void ClearDispatch<T>(this ICollection<T> collection)
        {
            Action method = collection.Clear;
            Application.Current.Dispatcher.BeginInvoke(method);
        }
    }
}