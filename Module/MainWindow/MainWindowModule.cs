﻿using MainWindow.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MainWindow
{
    public class MainWindowModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindowView>();
        }
    }
}