using System;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportNavigationPageWidget), typeof(SupportNavigationPageWidgetRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportNavigationPageWidgetRenderer : NavigationRenderer
    {
        protected SupportNavigationPageWidget supportNavigationPageWidget;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportNavigationPageWidget)
                {
                    supportNavigationPageWidget = Element as SupportNavigationPageWidget;

                    if (supportNavigationPageWidget.IsShadow)
                    {
                        NavigationBar.Layer.MasksToBounds = false;
                        NavigationBar.Layer.ShadowOpacity = 0.3f;
                        NavigationBar.Layer.ShadowOffset = new CGSize(0, 0);
                        NavigationBar.Layer.ShadowColor = UIColor.Gray.CGColor;
                        NavigationBar.Layer.CornerRadius = 0;
                    }
                }
            }
        }
    }
}