using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoWidget.Views;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.Models;
using Xamarin.Forms;

namespace DemoWidget
{
    public partial class MainPage : ContentPage, IGalleryPickerResultListener
    {
        public MainPage()
        {
            InitializeComponent();

            bttAutocomplete.Clicked += async (sender, e) => {
                await Navigation.PushAsync(new DemoAutoCompletePage());
            };

            bttSupportEntry.Clicked += async (sender, e) => {
                await Navigation.PushAsync(new DemoSupportEntryPageView());
            };

            bttDropList.Clicked += async (sender, e) => {
                await Navigation.PushAsync(new DemoDropListPageView());
            };

            bttGallery.Clicked += (object sender, EventArgs e) => {
                DependencyService.Get<IGalleryPicker>().IF_OpenGallery(this);
            };
        }

        public void IF_PickedResult(List<ImageSet> result)
        {

        }
    }
}