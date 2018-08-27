using System;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportEditor : Editor
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(double), typeof(SupportEditor), 0d);
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty CornerWidthProperty = BindableProperty.Create("CornerWidth", typeof(double), typeof(SupportEditor), 0d);
        public double CornerWidth
        {
            get => (double)GetValue(CornerWidthProperty);
            set => SetValue(CornerWidthProperty, value);
        }

        public static readonly BindableProperty CornerColorProperty = BindableProperty.Create("CornerColor", typeof(Color), typeof(SupportEditor), Color.Default);
        public Color CornerColor
        {
            get => (Color)GetValue(CornerColorProperty);
            set => SetValue(CornerColorProperty, value);
        }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(SupportEntryReturnType), typeof(SupportEditor), SupportEntryReturnType.Done, BindingMode.OneWay);
        public SupportEntryReturnType ReturnType
        {
            get => (SupportEntryReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        public static readonly BindableProperty NextViewProperty = BindableProperty.Create("NextView", typeof(View), typeof(SupportEditor));
        public View NextView
        {
            get => (View)GetValue(NextViewProperty);
            set => SetValue(NextViewProperty, value);
        }

        public new event EventHandler Completed;
        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }

        public void OnNext()
        {
            NextView?.Focus();
        }
    }
}
