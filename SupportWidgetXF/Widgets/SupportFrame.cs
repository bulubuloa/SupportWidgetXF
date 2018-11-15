using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportFrame : Frame
    {
        public static readonly BindableProperty FrameBackgroundColorProperty = BindableProperty.Create("FrameBackgroundColor", typeof(Color), typeof(SupportFrame), Color.White);
        public Color FrameBackgroundColor
        {
            get => (Color)GetValue(FrameBackgroundColorProperty);
            set => SetValue(FrameBackgroundColorProperty, value);
        }

    }
}