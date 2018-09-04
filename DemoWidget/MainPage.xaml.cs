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
    public partial class MainPage : ContentPage
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

            bttGallery.Clicked += async (object sender, EventArgs e) => {
                await Navigation.PushAsync(new DemoGalleryPickerPage());
            };
        }
    }
}