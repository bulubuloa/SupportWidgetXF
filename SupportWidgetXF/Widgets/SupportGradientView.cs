using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public enum SupportGradientViewMode
    {
        ToRight,
        ToLeft,
        ToTop,
        ToBottom,
        ToTopLeft,
        ToTopRight,
        ToBottomLeft,
        ToBottomRight
    }

    public class SupportGradientView : StackLayout
    {
        public Color StartColor { set; get; }
        public Color EndColor { get; set; }
        public SupportGradientViewMode Mode { set; get; }

        public SupportGradientView()
        {
            Mode = SupportGradientViewMode.ToBottom;
        }
    }
}