using System;
using System.ComponentModel;
using CoreGraphics;
using MapKit;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.MapView;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportMapView), typeof(SupportMapViewRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportMapViewRenderer : MapRenderer
    {
        private SupportMapView supportMapView;
        private UIView customPinView;

        public SupportMapViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                    nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                    //nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                    //nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                }
            }

            if (e.NewElement != null)
            {
                supportMapView = (SupportMapView)e.NewElement;
                var nativeMap = Control as MKMapView;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                //nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                //nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is MKUserLocation)
                return null;

            if (annotation is MKPointAnnotation)
            {
                MKPointAnnotation mKAnnotation = annotation as MKPointAnnotation;
                var customPin = GetCustomPin(mKAnnotation);
                if (customPin == null)
                {
                    return null;
                }
                else
                {
                    MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
                    if (annotationView == null)
                    {
                        annotationView = new CustomMKAnnotationView(customPin.ActionInfo);
                        annotationView.Image = UIImage.FromFile("icon_map_pin");
                        annotationView.CalloutOffset = new CGPoint(0, 0);
                        annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("ren2own_logo_map"));
                        annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                        //((CustomMKAnnotationView)annotationView).Id = customPin.Id.ToString();
                        //((CustomMKAnnotationView)annotationView).Url = customPin.Url;
                    }
                    annotationView.CanShowCallout = true;
                    return annotationView;
                }
            }
            return null;
        }

        SupportMapPin GetCustomPin(MKPointAnnotation annotation)
        {
            try
            {
                var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
                foreach (var pin in supportMapView.SupportPins)
                {
                    if (pin.Position.Latitude.ToString().Equals(position.Latitude.ToString())
                       && pin.Position.Longitude.ToString().Equals(position.Longitude.ToString()))
                    {
                        return pin;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            if (customView.ActionInfo != null)
                customView.ActionInfo();
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            customPinView = new UIView();

            //if (customView.Id == "Xamarin")
            //{
            //    customPinView.Frame = new CGRect(0, 0, 200, 84);
            //    var image = new UIImageView(new CGRect(0, 0, 200, 84));
            //    image.Image = UIImage.FromFile("flash_on.png");
            //    customPinView.AddSubview(image);
            //    customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
            //    e.View.AddSubview(customPinView);
            //}
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }
    }
}