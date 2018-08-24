using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public enum ShadowDirectionEnum
    {
        Top, Bottom
    }

    public class SupportShadowView : StackLayout
    {
        public static readonly BindableProperty ShadowDirectionProperty = BindableProperty.Create("ShadowDirection", typeof(SupportShadowView), typeof(SupportShadowView), ShadowDirectionEnum.Bottom);
        public ShadowDirectionEnum ShadowDirection
        {
            get => (ShadowDirectionEnum)GetValue(ShadowDirectionProperty);
            set => SetValue(ShadowDirectionProperty, value);
        }
    }
}