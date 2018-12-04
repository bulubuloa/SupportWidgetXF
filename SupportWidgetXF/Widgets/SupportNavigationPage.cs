using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportNavigationPage : NavigationPage
    {
        public enum TitleAlignment
        {
            Start,
            Center,
            End
        }

        public static readonly BindableProperty TitlePositionProperty = BindableProperty.Create("TitlePosition", typeof(TitleAlignment), typeof(SupportNavigationPage), Device.RuntimePlatform == Device.iOS ? TitleAlignment.Center : TitleAlignment.Start);
        public TitleAlignment TitlePosition
        {
            get => (TitleAlignment)GetValue(TitlePositionProperty);
            set => SetValue(TitlePositionProperty, value);
        }

        public static readonly BindableProperty TitleFontProperty = BindableProperty.Create("TitleFont", typeof(Font), typeof(SupportNavigationPage), Font.SystemFontOfSize(NamedSize.Medium));
        public Font TitleFont
        {
            get => (Font)GetValue(TitleFontProperty);
            set => SetValue(TitleFontProperty, value);
        }

        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create("TitleColor", typeof(Color?), typeof(SupportNavigationPage), null);
        public Color? TitleColor
        {
            get => (Color?)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        public static readonly BindableProperty SubtitleProperty = BindableProperty.Create("Subtitle", typeof(string), typeof(SupportNavigationPage), null);
        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        public static readonly BindableProperty SubtitleFontProperty = BindableProperty.Create("SubtitleFont", typeof(Font), typeof(SupportNavigationPage), Font.SystemFontOfSize(NamedSize.Small));
        public Font SubtitleFont
        {
            get => (Font)GetValue(SubtitleFontProperty);
            set => SetValue(SubtitleFontProperty, value);
        }

        public static readonly BindableProperty SubtitleColorProperty = BindableProperty.CreateAttached("SubtitleColor", typeof(Color?), typeof(SupportNavigationPage), null);
        public static Color? GetSubtitleColor(BindableObject view)
        {

            return (Color?)view.GetValue(SubtitleColorProperty);
        }

        public static readonly BindableProperty IsShadowProperty = BindableProperty.Create("IsShadow", typeof(bool), typeof(SupportNavigationPage), true);
        public bool IsShadow
        {
            get => (bool)GetValue(IsShadowProperty);
            set => SetValue(IsShadowProperty, value);
        }

        public static readonly BindableProperty IsShowSearchBarProperty = BindableProperty.Create("IsShowSearchBar", typeof(bool), typeof(SupportNavigationPage), true);
        public bool IsShowSearchBar
        {
            get => (bool)GetValue(IsShowSearchBarProperty);
            set => SetValue(IsShowSearchBarProperty, value);
        }

        public static readonly BindableProperty IsTranslucentProperty = BindableProperty.Create("IsTranslucent", typeof(bool), typeof(SupportNavigationPage), false);
        public bool IsTranslucent
        {
            get => (bool)GetValue(IsTranslucentProperty);
            set => SetValue(IsTranslucentProperty, value);
        }

        public SupportNavigationPage() : base()
        {
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.SetIsNavigationBarTranslucent(this, true);
            BarBackgroundColor = Color.Transparent;
            BarTextColor = (Color)Application.Current.Resources["PrimaryColor"];
        }
    }
}
