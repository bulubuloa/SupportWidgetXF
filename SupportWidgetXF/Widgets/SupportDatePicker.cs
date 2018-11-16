using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

        private string _format = null;
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create<SupportDatePicker, DateTime?>(p => p.NullableDate, null);

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue) 
            { 
                if (null != _format) 
                    Format = _format; 
                Date = NullableDate.Value; 
            }
            else 
            { 
                _format = Format; 
                Format = "././."; 
            }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
           
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Date") 
                NullableDate = Date;
        }
    }
}