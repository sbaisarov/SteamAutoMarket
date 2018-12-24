namespace SteamAutoMarket.UI.Utils
{
    using System.Windows;
    using System.Windows.Media;

    public static class UiElementsUtils
    {
        public static T FindVisualChild<T>(DependencyObject obj)
            where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T variable) return variable;

                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null) return childOfChild;
            }

            return null;
        }
    }
}