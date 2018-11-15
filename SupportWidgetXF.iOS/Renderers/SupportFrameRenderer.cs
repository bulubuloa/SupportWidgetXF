using System;
using System.Drawing;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SupportFrame), typeof(SupportFrameRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportFrameRenderer : FrameRenderer
    {
        protected SupportFrame supportFrame;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is SupportFrame)
                {
                    supportFrame = Element as SupportFrame;
                }
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            if (supportFrame != null)
                Layer.BackgroundColor = supportFrame.FrameBackgroundColor.ToCGColor();
            else
                Layer.BackgroundColor = UIColor.White.CGColor;


            if (Element.HasShadow)
            {
                Layer.ShadowRadius = 5;
                Layer.ShadowColor = UIColor.Gray.CGColor;
                Layer.ShadowOpacity = 0.2f;
                Layer.ShadowOffset = new SizeF();
            }
            else
                Layer.ShadowOpacity = 0;
        }
    }
}