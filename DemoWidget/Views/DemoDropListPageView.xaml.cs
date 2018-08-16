using System;
using System.Collections.Generic;
using DemoWidget.ViewModels;
using Xamarin.Forms;

namespace DemoWidget.Views
{
    public partial class DemoDropListPageView : ContentPage
    {
        public DemoDropListPageView()
        {
            InitializeComponent();
            BindingContext = new DemoAutoCompletePageViewModel(); 
        }
    }
}