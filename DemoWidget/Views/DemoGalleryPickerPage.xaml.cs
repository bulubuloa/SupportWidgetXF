using DemoWidget.ViewModels;
using Xamarin.Forms;

namespace DemoWidget.Views
{
    public partial class DemoGalleryPickerPage : ContentPage
    {
        public DemoGalleryPickerPage()
        {
            InitializeComponent();
            BindingContext = new DemoAutoCompletePageViewModel();
        }
    }
}