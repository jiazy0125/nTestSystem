using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Modularity;
using Prism.Unity;

namespace nTestSystem
{
	[Obsolete]
	class Bootstrapper: UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            //创建主界面
            return Container.TryResolve<Shell>();
        }

        
        protected override void InitializeShell()
        {
            //显示主界面
            base.InitializeShell();

			Application.Current.MainWindow = (Window)Shell;
			Application.Current.MainWindow.Show();
        }

        
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            //注册模块
            ModuleCatalog moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //moduleCatalog.AddModule(typeof(HelloWorldModule.HelloWorldModule));
        }
    }
}
