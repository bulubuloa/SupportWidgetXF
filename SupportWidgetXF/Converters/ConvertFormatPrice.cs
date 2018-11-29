using System;
using System.Globalization;
using SupportWidgetXF.Extensions;
using Xamarin.Forms;

namespace SupportWidgetXF.Converters
{
    public class ConvertFormatPrice : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double?)
            {
                var param = value as double?;
                return param.ToString().FangToCurrencyFormated();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
