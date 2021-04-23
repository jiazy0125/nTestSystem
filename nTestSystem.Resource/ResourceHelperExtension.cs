using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;
using nTestSystem.Framework.Commons;


namespace nTestSystem.Resource
{

	[MarkupExtensionReturnType(typeof(object))]
    public class ResourceHelperExtension : MarkupExtension
    {
        private static readonly ResourceConverter resourceConverter = new ResourceConverter();

        [ConstructorArgument(nameof(Key))]
        public ComponentResourceKey Key { get; set; }

        public object DefaultValue { get; set; }

        public ResourceHelperExtension(ComponentResourceKey key) => Key = key;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key == null)
                throw new NullReferenceException($"{nameof(Key)} cannot be null at the same time.");

            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget))
                throw new ArgumentException(
                    $"The {nameof(serviceProvider)} must implement {nameof(IProvideValueTarget)} interface.");

            if (provideValueTarget.TargetObject?.GetType().FullName == "System.Windows.SharedDp") return this;

            return new Binding(nameof(ResourceHelper.Value))
            {
                Source = new ResourceHelper(Key, provideValueTarget.TargetObject, DefaultValue),
                Mode = BindingMode.OneWay,
                Converter = resourceConverter
            }.ProvideValue(serviceProvider);
        }

        private class ResourceConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {

                return value;

            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }     
    }
}
