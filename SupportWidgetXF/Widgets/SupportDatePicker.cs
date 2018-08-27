using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportDatePicker : DatePicker
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(double), typeof(SupportDatePicker), 0d);
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty CornerWidthProperty = BindableProperty.Create("CornerWidth", typeof(double), typeof(SupportDatePicker), 0d);
        public double CornerWidth
        {
            get { return (double)GetValue(CornerWidthProperty); }
            set { SetValue(CornerWidthProperty, value); }
        }

        public static readonly BindableProperty CornerColorProperty = BindableProperty.Create("CornerColor", typeof(Color), typeof(SupportDatePicker), Color.Default);
        public Color CornerColor
        {
            get { return (Color)GetValue(CornerColorProperty); }
            set { SetValue(CornerColorProperty, value); }
        }
    }
}