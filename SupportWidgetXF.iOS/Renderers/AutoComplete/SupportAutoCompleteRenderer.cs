using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.AutoComplete;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportAutoComplete), typeof(SupportAutoCompleteRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportAutoCompleteRenderer : SupportBaseAutoCompleteRenderer<SupportAutoComplete>
    {

    }
}