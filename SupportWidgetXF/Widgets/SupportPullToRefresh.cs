using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportPullToRefresh : ContentView
    {
        public SupportPullToRefresh()
        {
            IsClippedToBounds = true;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        public static readonly BindableProperty IsRefreshingProperty = BindableProperty.Create(nameof(IsRefreshing), typeof(bool), typeof(SupportPullToRefresh), false);
        public bool IsRefreshing
        {
            get { return (bool)GetValue(IsRefreshingProperty); }
            set
            {
                if ((bool)GetValue(IsRefreshingProperty) == value)
                    OnPropertyChanged(nameof(IsRefreshing));
                SetValue(IsRefreshingProperty, value);
            }
        }
    }
}