using System;
using Android.Content;
using Android.Graphics;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportGradientView), typeof(SupportGradientViewRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportGradientViewRenderer : VisualElementRenderer<StackLayout>
    {
        private Xamarin.Forms.Color[] Colors { get; set; }
        private SupportGradientViewMode Mode { get; set; }

        public SupportGradientViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;
            try
            {
                if (e.NewElement is SupportGradientView supportGradientView)
                {
                    Colors = new Xamarin.Forms.Color[] { supportGradientView.StartColor, supportGradientView.EndColor };
                    Mode = supportGradientView.Mode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            Android.Graphics.LinearGradient gradient = null;

            int[] colors = new int[Colors.Length];
            for (int i = 0, l = Colors.Length; i < l; i++)
            {
                colors[i] = Colors[i].ToAndroid().ToArgb();
            }

            switch (Mode)
            {
                case SupportGradientViewMode.ToRight:
                    gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case SupportGradientViewMode.ToLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, 0, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case SupportGradientViewMode.ToTop:
                    gradient = new Android.Graphics.LinearGradient(0, Height, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case SupportGradientViewMode.ToBottom:
                    gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case SupportGradientViewMode.ToTopLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, Height, 0, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case SupportGradientViewMode.ToTopRight:
                    gradient = new Android.Graphics.LinearGradient(0, Height, Width, 0, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case SupportGradientViewMode.ToBottomLeft:
                    gradient = new Android.Graphics.LinearGradient(Width, 0, 0, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
                case SupportGradientViewMode.ToBottomRight:
                    gradient = new Android.Graphics.LinearGradient(0, 0, Width, Height, colors, null, Android.Graphics.Shader.TileMode.Mirror);
                    break;
            }

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };

            paint.SetShader(gradient);
            canvas.DrawPaint(paint);

            base.DispatchDraw(canvas);
        }
    }
}
