using System;
using System.Globalization;
using SupportWidgetXF.Models;
using Xamarin.Forms;

namespace SupportWidgetXF.Converters
{
    public class DropListSelectedEventConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is IntegerEventArgs)
                {
                    var ars = value as IntegerEventArgs;
                    return ars.IntegerValue;
                }
                else if (value is MultiIntegerEventArgs)
                {
                    var ars = value as MultiIntegerEventArgs;
                    return ars.ListIntegerValue;
                }
                return value;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
