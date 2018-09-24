using System;
using System.Linq;
using System.Runtime.CompilerServices;
using SupportWidgetXF.Models;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportAutoComplete : SupportViewDrop
    {
        /*
         * Properties
         */
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(SupportAutoComplete), "");
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty PaddingInsideProperty = BindableProperty.Create("PaddingInside", typeof(double), typeof(SupportAutoComplete), 5d);
        public double PaddingInside
        {
            get { return (double)GetValue(PaddingInsideProperty); }
            set { SetValue(PaddingInsideProperty, value); }
        }

        public static readonly BindableProperty FocusCornerColorProperty = BindableProperty.Create("FocusCornerColor", typeof(Color), typeof(SupportAutoComplete), Color.Default);
        public Color FocusCornerColor
        {
            get => (Color)GetValue(FocusCornerColorProperty);
            set => SetValue(FocusCornerColorProperty, value);
        }

        public static readonly BindableProperty CurrentCornerColorProperty = BindableProperty.Create("CurrentCornerColor", typeof(Color), typeof(SupportAutoComplete), Color.Default);
        public Color CurrentCornerColor
        {
            get => (Color)GetValue(CurrentCornerColorProperty);
            set => SetValue(CurrentCornerColorProperty, value);
        }

        public static readonly BindableProperty InvalidCornerColorProperty = BindableProperty.Create("InvalidCornerColor", typeof(Color), typeof(SupportAutoComplete), Color.Default);
        public Color InvalidCornerColor
        {
            get => (Color)GetValue(InvalidCornerColorProperty);
            set => SetValue(InvalidCornerColorProperty, value);
        }

        public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create("PlaceHolderColor", typeof(Color), typeof(SupportAutoComplete), Color.Gray);
        public Color PlaceHolderColor
        {
            get => (Color)GetValue(PlaceHolderColorProperty);
            set => SetValue(PlaceHolderColorProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create("IsValid", typeof(bool), typeof(SupportAutoComplete), true);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public static readonly BindableProperty NextViewProperty = BindableProperty.Create("NextView", typeof(View), typeof(SupportAutoComplete), null);
        public View NextView
        {
            get => (View)GetValue(NextViewProperty);
            set => SetValue(NextViewProperty, value);
        }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(SupportEntryReturnType), typeof(SupportAutoComplete), SupportEntryReturnType.Done, BindingMode.OneWay);
        public SupportEntryReturnType ReturnType
        {
            get => (SupportEntryReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }
        /*
         * Function
         */


        /*
         * Event
         */
        public event EventHandler<TextChangedEventArgs> OnTextChanged;
        public event EventHandler OnReturnKeyClicked;
        public event EventHandler<IntegerEventArgs> OnItemSelected;
        public event EventHandler<FocusEventArgs> OnTextFocused;

        public void SendOnTextChanged(string text)
        {
            OnTextChanged?.Invoke(this, new TextChangedEventArgs(text, text));
        }

        public void SendOnReturnKeyClicked()
        {
            NextView?.Focus();
            OnReturnKeyClicked?.Invoke(this,null);
        }

        public void SendOnItemSelected(int position)
        {
            OnItemSelected?.Invoke(this, new IntegerEventArgs(position));
        }

        public void SendOnTextFocused(bool hasFocus)
        {
            IsFocus = hasFocus;
            CurrentCornerColor = IsFocus ? FocusCornerColor : CornerColor;
            OnTextFocused?.Invoke(this, new FocusEventArgs(this,hasFocus));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(IsValidProperty.PropertyName))
            {
                if (!IsValid)
                    CurrentCornerColor = InvalidCornerColor;
            }
            else if (propertyName.Equals(TextProperty.PropertyName))
            {
                if (!IsFocus)
                    SendOnTextChanged(Text);
            }
        }
    }
}