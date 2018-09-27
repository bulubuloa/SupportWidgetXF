using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SupportWidgetXF.Widgets
{
    public class SupportMapPin : Pin
    {
        public Action ActionInfo { set; get; }
    }

    public class SupportMapView : Map
    {
        public static readonly BindableProperty SupportPinsProperty = BindableProperty.Create("SupportPins", typeof(List<SupportMapPin>), typeof(SupportMapView),new List<SupportMapPin>() );
        public List<SupportMapPin> SupportPins
        {
            get { return (List<SupportMapPin>)GetValue(SupportPinsProperty); }
            set { SetValue(SupportPinsProperty, value); }
        }

        public SupportMapView()
        {
        }
    }
}