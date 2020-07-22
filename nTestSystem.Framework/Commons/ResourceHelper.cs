using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace nTestSystem.Framework.Commons
{

    public class ResourceHelper : INotifyPropertyChanged
    {
        private readonly ComponentResourceKey _key;
        private readonly object _defValue;
        public event PropertyChangedEventHandler PropertyChanged;


        public ResourceHelper(ComponentResourceKey key, object owner = null, object defaultValue=null)
        {
            _key = key;
            _defValue = defaultValue;
            switch (owner)
            {
                case FrameworkElement frameworkElement:
                    frameworkElement.Loaded += OnLoaded;
                    frameworkElement.Unloaded += OnUnloaded;
                    break;
                case FrameworkContentElement frameworkContentElement:
                    frameworkContentElement.Loaded += OnLoaded;
                    frameworkContentElement.Unloaded += OnUnloaded;
                    break;
                default:
                    OnLoaded(null, null);
                    break;
            }
        }

        public object Value => ResourceHandler.Instance.Get(_key) ?? _defValue;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OnCurrentUICultureChanged(sender, null);
            ResourceHandler.Instance.CurrentUICultureChanged += OnCurrentUICultureChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ResourceHandler.Instance.CurrentUICultureChanged -= OnCurrentUICultureChanged;
        }

        private void OnCurrentUICultureChanged(object sender, CurrentUICultureChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        public override string ToString() => Value?.ToString() ?? string.Empty;

        public static implicit operator ResourceHelper(ComponentResourceKey resourceKey) => new ResourceHelper(resourceKey);
    }
}
