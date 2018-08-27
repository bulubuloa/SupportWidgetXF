using System;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportButton), typeof(SupportButtonRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportButtonRenderer : ButtonRenderer
    {
        private SupportButton supportButton;

        public SupportButtonRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportButton)
                {
                    supportButton = Element as SupportButton;

                    Control.ClipsToBounds = true;
                    Control.Layer.CornerRadius = supportButton.CornerRadius;
                }
            }
        }
    }
}