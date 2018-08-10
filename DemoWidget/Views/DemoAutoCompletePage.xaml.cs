using System;
using System.Collections.Generic;
using DemoWidget.ViewModels;
using Xamarin.Forms;

namespace DemoWidget.Views
{
    public partial class DemoAutoCompletePage : ContentPage
    {
        public DemoAutoCompletePage()
        {
            InitializeComponent();
            BindingContext = new DemoAutoCompletePageViewModel();
        }
    }
}