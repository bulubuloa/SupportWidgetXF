using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportLabel : Label
    {

        public static readonly BindableProperty HasAsteriskProperty = BindableProperty.Create(nameof(HasAsterisk), typeof(bool), typeof(SupportLabel), false);
        public bool HasAsterisk
        {
            get => (bool)GetValue(HasAsteriskProperty);
            set => SetValue(HasAsteriskProperty, value);
        }
        public static readonly BindableProperty DisplayAsHtmlProperty = BindableProperty.Create(nameof(DisplayAsHtml), typeof(bool), typeof(SupportLabel), false);
        public bool DisplayAsHtml
        {
            get => (bool)GetValue(DisplayAsHtmlProperty);
            set => SetValue(DisplayAsHtmlProperty, value);
        }
    }
}
