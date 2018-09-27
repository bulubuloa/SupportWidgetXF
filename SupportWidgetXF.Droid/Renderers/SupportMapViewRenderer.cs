using System;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportMapView), typeof(SupportMapViewRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportMapViewRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        private SupportMapView supportMapView;

        public SupportMapViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                supportMapView = (SupportMapView)e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.icon_map_pin));
            return marker;
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            if (customPin.ActionInfo != null)
                customPin.ActionInfo();
        }

        SupportMapPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in supportMapView.SupportPins)
            {
                if (pin.Position.Latitude.ToString().Equals(position.Latitude.ToString())
                      && pin.Position.Longitude.ToString().Equals(position.Longitude.ToString()))
                {
                    return pin;
                }
            }
            return null;
        }

        Android.Views.View GoogleMap.IInfoWindowAdapter.GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

                var infoTitle = view.FindViewById<Android.Widget.TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<Android.Widget.TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        Android.Views.View GoogleMap.IInfoWindowAdapter.GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}