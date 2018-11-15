using System;
using Android.Content;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(SupportFrame), typeof(SupportFrameRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportFrameRenderer : FrameRenderer
    {
        protected SupportFrame supportFrame;

        public SupportFrameRenderer(Context context) : base(context)
        {
        }

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
    }
}