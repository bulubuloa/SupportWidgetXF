using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoWidget.Views;
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
        }
    }
}