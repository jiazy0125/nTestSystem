using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using nTestSystem.Framework.Configurations;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows;

namespace nTestSystem.Models
{
	public class TreeViewItemModel: BindableBase
	{
		public TreeViewItemModel()
		{


			MenuItems = new ObservableCollection<TreeViewItemModel>();
		}

		public string Header { get; set; }
		public string RegionName { get; set; }
		public string ViewName { get; set; }

		public Geometry Icon { get; set; }



		public Visibility Visibility
		{
			get => _visibility;
			set => SetProperty(ref _visibility, value);
		}
		private Visibility _visibility = Visibility.Visible;

		public bool IsExpanded
		{
			get => _isExpanded;
			set => SetProperty(ref _isExpanded, value);
		}
		private bool _isExpanded = true;

		public ObservableCollection<TreeViewItemModel> MenuItems
		{
			get => _menuItems;
			set => SetProperty(ref _menuItems, value);
		}
		private ObservableCollection<TreeViewItemModel> _menuItems;

	}
}
