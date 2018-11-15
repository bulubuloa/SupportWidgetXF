using System;
using MapKit;

namespace SupportWidgetXF.iOS.Renderers.MapView
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public Action ActionInfo { set; get; }

        public CustomMKAnnotationView(Action action) : base()
        {
            this.ActionInfo = action;
        }
    }
}