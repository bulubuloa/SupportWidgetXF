using System;
using Android.Content;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportShadowView), typeof(SupportShadowViewRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportShadowViewRenderer : VisualElementRenderer<StackLayout>
    {
        public SupportShadowViewRenderer(Context context) : base(context)
        {
        }

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
                        SetBackgroundResource(Resource.Drawable.shadowclonenavigation_bottom);
                    }
                    else if (supportShadowTrick.ShadowDirection == ShadowDirectionEnum.Top)
                    {
                        SetBackgroundResource(Resource.Drawable.shadowclone);
                    }
                }
            }
        }
    }
}
