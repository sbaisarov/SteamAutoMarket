namespace SteamAutoMarket.Utils.Resources
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DynamicResourceBinding : MarkupExtension
    {
        private BindingProxy bindingSource;

        private BindingTrigger bindingTrigger;

        public DynamicResourceBinding(object resourceKey)
        {
            this.ResourceKey = resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));
        }

        public IValueConverter Converter { get; set; }

        public CultureInfo ConverterCulture { get; set; }

        public object ConverterParameter { get; set; }

        public object ResourceKey { get; set; }

        public string StringFormat { get; set; }

        public object TargetNullValue { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var dynamicResource = new DynamicResourceExtension(this.ResourceKey);
            this.bindingSource = new BindingProxy(dynamicResource.ProvideValue(null));

            var dynamicResourceBinding = new Binding
                                             {
                                                 Source = this.bindingSource,
                                                 Path = new PropertyPath(BindingProxy.ValueProperty),
                                                 Mode = BindingMode.OneWay
                                             };

            var targetInfo = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (targetInfo.TargetObject is DependencyObject dependencyObject)
            {
                dynamicResourceBinding.Converter = this.Converter;
                dynamicResourceBinding.ConverterParameter = this.ConverterParameter;
                dynamicResourceBinding.ConverterCulture = this.ConverterCulture;
                dynamicResourceBinding.StringFormat = this.StringFormat;
                dynamicResourceBinding.TargetNullValue = this.TargetNullValue;

                if (dependencyObject is FrameworkElement targetFrameworkElement)
                {
                    targetFrameworkElement.Resources.Add(this.bindingSource, this.bindingSource);
                }

                return dynamicResourceBinding.ProvideValue(serviceProvider);
            }

            var findTargetBinding = new Binding { RelativeSource = new RelativeSource(RelativeSourceMode.Self) };

            this.bindingTrigger = new BindingTrigger();

            var wrapperBinding = new MultiBinding
                                     {
                                         Bindings =
                                             {
                                                 dynamicResourceBinding, findTargetBinding, this.bindingTrigger.Binding
                                             },
                                         Converter = new InlineMultiConverter(this.WrapperConvert)
                                     };

            return wrapperBinding.ProvideValue(serviceProvider);
        }

        private object WrapperConvert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var dynamicResourceBindingResult = values[0];
            var bindingTargetObject = values[1];

            if (this.Converter != null)
            {
                dynamicResourceBindingResult = this.Converter.Convert(
                    dynamicResourceBindingResult,
                    targetType,
                    this.ConverterParameter,
                    this.ConverterCulture);
            }

            if (dynamicResourceBindingResult == null)
            {
                dynamicResourceBindingResult = this.TargetNullValue;
            }
            else if (targetType == typeof(string) && this.StringFormat != null)
            {
                dynamicResourceBindingResult = string.Format(this.StringFormat, dynamicResourceBindingResult);
            }

            if (!(bindingTargetObject is FrameworkElement targetFrameworkElement)
                || targetFrameworkElement.Resources.Contains(this.bindingSource))
            {
                return dynamicResourceBindingResult;
            }

            targetFrameworkElement.Resources[this.bindingSource] = this.bindingSource;

            SynchronizationContext.Current.Post(state => { this.bindingTrigger.Refresh(); }, null);

            return dynamicResourceBindingResult;
        }
    }
}