using System;
using System.Windows;
using System.Windows.Controls;

using gsm.MVVM.Models.Menu;

namespace gsm.MVVM.TemplateSelectors
{
    public class MainMenuDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var template = base.SelectTemplate(item, container);

            if (container is FrameworkElement frameworkElement)
            {
                if (item is MainMenuItem menuItem)
                {
                    if (menuItem.MenuItems.Count > 0)
                    {
                        template = frameworkElement.TryFindResource("RootMenuTemplate") as DataTemplate;
                    }
                    else
                    {
                        template = frameworkElement.TryFindResource("ChildMenuTemplate") as DataTemplate;
                    }
                }
            }

            return template;
        }

    }
}
