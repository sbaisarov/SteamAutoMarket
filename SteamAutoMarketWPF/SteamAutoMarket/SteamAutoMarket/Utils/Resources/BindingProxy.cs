namespace SteamAutoMarket.Utils.Resources
{
    using System.Windows;

    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(object),
            typeof(BindingProxy));

        public BindingProxy()
        {
        }

        public BindingProxy(object value) => this.Value = value;

        public object Value
        {
            get => this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        protected override Freezable CreateInstanceCore() => new BindingProxy();
    }
}