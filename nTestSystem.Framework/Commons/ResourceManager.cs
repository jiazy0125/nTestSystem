using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Windows;

namespace nTestSystem.Framework.Commons
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

    public class ResourceManager : INotifyPropertyChanged
    {
        public static ResourceManager Instance { get; } = new ResourceManager();

        private readonly ConcurrentDictionary<string, System.Resources.ResourceManager> _resourceManagerStorage = new ConcurrentDictionary<string, System.Resources.ResourceManager>();
        private CultureInfo _currentUICulture;

        public event EventHandler<CurrentUICultureChangedEventArgs> CurrentUICultureChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private ResourceManager() { }

        public CultureInfo CurrentUICulture
        {
            get => _currentUICulture;
            set
            {
                if (EqualityComparer<CultureInfo>.Default.Equals(_currentUICulture, value)) return;

                OnCurrentUICultureChanged(_currentUICulture, _currentUICulture = value);
                OnPropertyChanged(nameof(CurrentUICulture));
            }
        }

        public void Add(System.Resources.ResourceManager resourceManager)
        {
            if (_resourceManagerStorage.ContainsKey(resourceManager.BaseName))
                throw new ArgumentException($"The ResourceManager named {resourceManager.BaseName} already exists, cannot be added repeatedly. ", nameof(resourceManager));

            _resourceManagerStorage[resourceManager.BaseName] = resourceManager;
        }

        public object Get(ComponentResourceKey key) =>
            GetCurrentResourceManager(key.TypeInTargetAssembly.FullName)?
                .GetObject(key.ResourceId.ToString(), CurrentUICulture) ?? null;

        private System.Resources.ResourceManager GetCurrentResourceManager(string key) =>
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
