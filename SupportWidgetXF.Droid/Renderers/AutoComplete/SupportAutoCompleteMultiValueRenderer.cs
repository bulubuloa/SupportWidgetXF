using System;
using Android.Content;
using Android.Widget;
using SupportWidgetXF.Droid.Renderers.AutoComplete;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportAutoCompleteMultiValue), typeof(SupportAutoCompleteMultiValueRenderer))]
namespace SupportWidgetXF.Droid.Renderers.AutoComplete
{
    public class SupportAutoCompleteMultiValueRenderer : SupportBaseAutoCompleteRenderer<SupportAutoCompleteMultiValue, MultiAutoCompleteTextView>
    {
        public SupportAutoCompleteMultiValueRenderer(Context context) : base(context)
        {
        }
    }
}