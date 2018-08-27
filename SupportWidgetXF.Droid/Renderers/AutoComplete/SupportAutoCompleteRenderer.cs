using Android.Content;
using Android.Widget;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Droid.Renderers.AutoComplete;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportAutoComplete), typeof(SupportAutoCompleteRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportAutoCompleteRenderer : SupportBaseAutoCompleteRenderer<SupportAutoComplete,AutoCompleteTextView>
    {
        public SupportAutoCompleteRenderer(Context context) : base(context)
        {
        }

        protected override void OnInitializeOriginalView()
        {
            OriginalView = new AutoCompleteTextView(Context);
            base.OnInitializeOriginalView();
        }
    }
}