using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Magnify.Converter
{
    class InverseBooleanToVisibleOrCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isCollapsed = (bool)value;
            return isCollapsed ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
