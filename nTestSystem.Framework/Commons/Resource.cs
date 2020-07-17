using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace nTestSystem.Framework.Commons
{

    public class Resource : INotifyPropertyChanged
    {
        private readonly ComponentResourceKey _key;
        private readonly object _defValue;
        public event PropertyChangedEventHandler PropertyChanged;


        public Resource(ComponentResourceKey key, object owner = null, object defaultValue=null)
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

        public object Value => ResourceManager.Instance.Get(_key) ?? _defValue;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OnCurrentUICultureChanged(sender, null);
            ResourceManager.Instance.CurrentUICultureChanged += OnCurrentUICultureChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ResourceManager.Instance.CurrentUICultureChanged -= OnCurrentUICultureChanged;
        }

        private void OnCurrentUICultureChanged(object sender, CurrentUICultureChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        public override string ToString() => Value?.ToString() ?? string.Empty;

        public static implicit operator Resource(ComponentResourceKey resourceKey) => new Resource(resourceKey);
    }
}
