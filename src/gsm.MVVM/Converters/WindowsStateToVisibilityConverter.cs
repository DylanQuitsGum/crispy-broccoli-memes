using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace gsm.MVVM.Converters
{
    public class WindowStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null && value is WindowState windowState)
            {
                var visibility = Visibility.Hidden;

                var param = parameter?.ToString() ?? string.Empty;

                switch (param)
                {
                    case "Maximize":
                        visibility = (windowState == WindowState.Maximized) ? Visibility.Hidden : Visibility.Visible;
                        break;
                    case "Restore":
                        visibility = (windowState == WindowState.Normal) ? Visibility.Hidden : Visibility.Visible;
                        break;
                    default:

                        break;
                }

                return visibility;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
