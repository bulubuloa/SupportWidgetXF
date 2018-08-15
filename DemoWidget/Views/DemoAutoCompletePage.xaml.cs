using System;
using System.Collections.Generic;
using DemoWidget.ViewModels;
using Xamarin.Forms;

namespace DemoWidget.Views
{
    public partial class DemoAutoCompletePage : ContentPage
    {
        void Dkm_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine(singleTitle.Text);
        }


        public DemoAutoCompletePage()
        {
            InitializeComponent();
            BindingContext = new DemoAutoCompletePageViewModel();

            dkm.Clicked += Dkm_Clicked;
        }
    }
}