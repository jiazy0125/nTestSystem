using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public static string SlideMenuRegion  = "SlideMenuRegion";
        public static string SlideMenuMainRegion  = "SlideMenuMainRegion";
        public static string SubItemRegion = "SubItemRegion";


        public IRegionManager RegionManager { get; }



        public MainWindowViewModel(IRegionManager rm)
        {
            RegionManager = rm;
           
        }
    }
}
