using System;
using SupportWidgetXF.Models;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportTabbedPage : TabbedPage
    {
        public static readonly BindableProperty IsShadowProperty = BindableProperty.Create("IsShadow", typeof(bool), typeof(SupportTabbedPage), false);
        public bool IsShadow
        {
            get => (bool)GetValue(IsShadowProperty);
            set => SetValue(IsShadowProperty, value);
        }

        public static readonly BindableProperty PageSelectedPositionProperty = BindableProperty.Create("PageSelectedPosition", typeof(int), typeof(SupportTabbedPage), 0);
        public int PageSelectedPosition
        {
            get => (int)GetValue(PageSelectedPositionProperty);
            set => SetValue(PageSelectedPositionProperty, value);
        }

        public event EventHandler<IntegerEventArgs> PageSelectPostionChanged;
        public void SendPageSelectPositionChanged(int position)
        {
            PageSelectPostionChanged?.Invoke(this, new IntegerEventArgs(position));
        }
    }
}