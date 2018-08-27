using System;
using Android.Content;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportButton), typeof(SupportButtonRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportButtonRenderer : ButtonRenderer
    {
        private SupportButton supportButton;

        public SupportButtonRenderer(Context context) : base(context)
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
                    if (Control != null)
                    {
                        Control.SetAllCaps(false);
                        Control.SetPadding(0, 0, 0, 0);
                        Control.TextAlignment = Android.Views.TextAlignment.Center;
                    }
                }
            }
        }
    }
}