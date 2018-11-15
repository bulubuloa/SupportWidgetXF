using System;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportShadowView), typeof(SupportShadowViewRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportShadowViewRenderer : VisualElementRenderer<StackLayout>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportShadowView)
                {
                    var supportShadowTrick = Element as SupportShadowView;
                    if (supportShadowTrick.ShadowDirection == ShadowDirectionEnum.Bottom)
                    {
                        Layer.MasksToBounds = false;
                        Layer.ShadowOpacity = 1f;
                        Layer.ShadowOffset = new CGSize(0, 2);
                        Layer.ShadowColor = UIColor.Gray.CGColor;
                        Layer.CornerRadius = 0;
                    }
                    else if (supportShadowTrick.ShadowDirection == ShadowDirectionEnum.Top)
                    {
                        Layer.MasksToBounds = false;
                        Layer.ShadowOpacity = 1f;
                        Layer.ShadowOffset = new CGSize(0, -2);
                        Layer.ShadowColor = UIColor.Gray.ColorWithAlpha(0.7f).CGColor;
                        Layer.CornerRadius = 0;
                    }
                }
            }
        }
    }
}