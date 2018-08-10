using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportAutoCompleteAsync : SupportAutoComplete
    {
        public static readonly BindableProperty IsSearchingProperty = BindableProperty.Create("IsSearching", typeof(bool), typeof(SupportAutoCompleteAsync), false);
        public bool IsSearching
        {
            get => (bool)GetValue(IsSearchingProperty);
            set => SetValue(IsSearchingProperty, value);
        }

        public SupportAutoCompleteAsync()
        {

        }
    }
}