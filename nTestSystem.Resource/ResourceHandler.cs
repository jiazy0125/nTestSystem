using System.Collections.Concurrent;
using System.Collections.Generic;
using nTestSystem.Aggregator;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Windows;
using Prism.Events;
using System;

namespace nTestSystem.Resource
{

	public class CurrentUICultureChangedEventArgs : EventArgs
    {
        public CultureInfo OldUICulture { get; }

        public CultureInfo NewUICulture { get; }

        public CurrentUICultureChangedEventArgs(CultureInfo oldUiCulture, CultureInfo newUiCulture)
        {
            OldUICulture = oldUiCulture;
            NewUICulture = newUiCulture;
        }
    }

    public class ResourceHandler : INotifyPropertyChanged
    {
        public delegate void CultureInfoChange();
        public static ResourceHandler Instance { get; } = new ResourceHandler();

        public static IEventAggregator EventAggregator { private get; set; }

        private readonly ConcurrentDictionary<string, ResourceManager> _resourceManagerStorage = new ConcurrentDictionary<string, ResourceManager>();
        
        private CultureInfo _currentUICulture;

        public event EventHandler<CurrentUICultureChangedEventArgs> CurrentUICultureChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public CultureInfoChange CulturetInfoChanged;

        private ResourceHandler() { }

        public CultureInfo CurrentUICulture
        {
            get => _currentUICulture;
            set
            {
                if (EqualityComparer<CultureInfo>.Default.Equals(_currentUICulture, value)) return;

                OnCurrentUICultureChanged(_currentUICulture, _currentUICulture = value);
                OnPropertyChanged(nameof(CurrentUICulture));
                CulturetInfoChanged?.Invoke();
                EventAggregator?.GetEvent<ResourceChanged>()?.Publish(true);
            }
        }

        public void Add(ResourceManager resourceManager)
        {
            if (_resourceManagerStorage.ContainsKey(resourceManager.BaseName))
                throw new ArgumentException($"The ResourceManager named {resourceManager.BaseName} already exists, cannot be added repeatedly. ", nameof(resourceManager));

            _resourceManagerStorage[resourceManager.BaseName] = resourceManager;
        }

        public object Get(ComponentResourceKey key) =>
            GetCurrentResourceManager(key.TypeInTargetAssembly.FullName)?
                .GetObject(key.ResourceId.ToString(), CurrentUICulture) ?? null;

        private ResourceManager GetCurrentResourceManager(string key) =>
            _resourceManagerStorage.TryGetValue(key, out var value) ? value : null;

        protected virtual void OnCurrentUICultureChanged(CultureInfo oldCulture, CultureInfo newCulture)
        {
            CurrentUICultureChanged?.Invoke(this, new CurrentUICultureChangedEventArgs(oldCulture, newCulture));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
