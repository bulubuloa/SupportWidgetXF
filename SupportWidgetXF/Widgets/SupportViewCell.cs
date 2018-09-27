using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportViewCell : ViewCell
    {
        public static BindableProperty ParentBindingContextProperty = BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(SupportViewCell), null);
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }
    }
}