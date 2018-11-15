using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportDatePicker), typeof(SupportDatePickerRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportDatePickerRenderer : DatePickerRenderer
    {
        private SupportDatePicker supportDatePicker;

        public SupportDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportDatePicker)
                {
                    supportDatePicker = Element as SupportDatePicker;
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetCornerRadius((float)supportDatePicker.CornerRadius);
                    gd.SetStroke((int)supportDatePicker.CornerWidth, supportDatePicker.CornerColor.ToAndroid());
                    Control.SetBackground(gd);
                    Control.Gravity = GravityFlags.CenterVertical;
                    Control.SetPadding(10, 0, 0, 0);
                    Control.TextAlignment = Android.Views.TextAlignment.Center;
                }
            }
        }
    }
}