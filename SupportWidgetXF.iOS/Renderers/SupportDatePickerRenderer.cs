using System;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportDatePicker), typeof(SupportDatePickerRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportDatePicker)
                {
                    var datePicker = Element as SupportDatePicker;

                    Control.Layer.CornerRadius = (float)datePicker.CornerRadius;
                    Control.Layer.BorderWidth = (float)datePicker.CornerWidth;
                    Control.Layer.BorderColor = datePicker.CornerColor.ToCGColor();
                }
            }
        }
    }
}