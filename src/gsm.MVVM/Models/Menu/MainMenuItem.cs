using System;
using System.Collections.ObjectModel;

namespace gsm.MVVM.Models.Menu
{
    public class MainMenuItem
    {
        public string DisplayText { get; set; }

        public string View { get; set; }

        private ObservableCollection<MainMenuItem> _menuItems;
        public ObservableCollection<MainMenuItem> MenuItems => _menuItems ??= new ObservableCollection<MainMenuItem>();
    }
}
