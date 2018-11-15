using System;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SupportWidgetXF.Widgets
{
    public class SupportNavigationPageWidget : Xamarin.Forms.NavigationPage
    {
        public static readonly Xamarin.Forms.BindableProperty IsShadowProperty = Xamarin.Forms.BindableProperty.Create("IsShadow", typeof(bool), typeof(SupportNavigationPageWidget), true);
        public bool IsShadow
        {
            get => (bool)GetValue(IsShadowProperty);
            set => SetValue(IsShadowProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty IsShowSearchBarProperty = Xamarin.Forms.BindableProperty.Create("IsShowSearchBar", typeof(bool), typeof(SupportNavigationPageWidget), true);
        public bool IsShowSearchBar
        {
            get => (bool)GetValue(IsShowSearchBarProperty);
            set => SetValue(IsShowSearchBarProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty IsTranslucentProperty = Xamarin.Forms.BindableProperty.Create("IsTranslucent", typeof(bool), typeof(SupportNavigationPageWidget), false);
        public bool IsTranslucent
        {
            get => (bool)GetValue(IsTranslucentProperty);
            set => SetValue(IsTranslucentProperty, value);
        }

        public SupportNavigationPageWidget() : base()
        {
            this.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetIsNavigationBarTranslucent(false);
        }
    }
}
