using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace SupportWidgetXF.Converters
{
    public class TextNormalToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value?.ToString())) return value.ToString().First().ToString().ToUpper() + value.ToString().Substring(1);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}