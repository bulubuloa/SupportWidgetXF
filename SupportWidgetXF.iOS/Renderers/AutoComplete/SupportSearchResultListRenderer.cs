using System.ComponentModel;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.AutoComplete;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportSearchResultList), typeof(SupportSearchResultListRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportSearchResultListRenderer : SupportBaseAutoCompleteRenderer<SupportSearchResultList>
    {

        protected override void RunFilterAutocomplete(string text)
        {
            //base.RunFilterAutocomplete(text);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName.Equals(SupportViewDrop.ItemsSourceProperty.PropertyName))
            {
                OnInitializeTableSource();
                tableView.ReloadData();
                FlagShow = false;
                ShowData();
            }
        }
    }
}