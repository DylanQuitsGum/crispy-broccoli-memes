using System;
using System.Collections.ObjectModel;

namespace gsm.MVVM.Models.Menu
{
    public class MainMenuItem
    {
        public MainMenuItem()
        : this(1)
        {

        }

        public MainMenuItem(int maxConcurrentOpen)
        {
            MaxConcurrentOpen = (maxConcurrentOpen > 1) ? maxConcurrentOpen : 1;
        }

        public string DisplayText { get; set; }

        public int MaxConcurrentOpen { get; set; }

        public string View { get; set; }

        private ObservableCollection<MainMenuItem> _menuItems;
        public ObservableCollection<MainMenuItem> MenuItems => _menuItems ??= new ObservableCollection<MainMenuItem>();
    }
}
