using System;
using System.Linq;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportAutoComplete : SupportViewDrop
    {
        public static readonly BindableProperty SetItemSelectionProperty = BindableProperty.Create("SetItemSelection", typeof(Action<int>), typeof(SupportAutoComplete));
        public Action<int> SetItemSelection
        {
            get { return (Action<int>)GetValue(SetItemSelectionProperty); }
            set { SetValue(SetItemSelectionProperty, value); }
        }

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

        public static readonly BindableProperty IsWrapSourceProperty = BindableProperty.Create("IsWrapSource", typeof(bool), typeof(SupportAutoComplete), false);
        public bool IsWrapSource
        {
            get => (bool)GetValue(IsWrapSourceProperty);
            set => SetValue(IsWrapSourceProperty, value);
        }

        public event EventHandler TextInputCompleted;
        public void InvokeCompleted()
        {
            if (this.TextInputCompleted != null)
                this.TextInputCompleted.Invoke(this, null);
        }

        public void OnInputCompleted()
        {
            NextView?.Focus();
        }

        public void RunReturnAction()
        {
            var type = ReturnType;
            switch (type)
            {
                case SupportEntryReturnType.Go:
                    InvokeCompleted();
                    break;
                case SupportEntryReturnType.Next:
                    OnInputCompleted();
                    break;
                case SupportEntryReturnType.Send:
                    InvokeCompleted();
                    break;
                case SupportEntryReturnType.Search:
                    InvokeCompleted();
                    break;
                case SupportEntryReturnType.Done:
                    InvokeCompleted();
                    break;
                default:
                    InvokeCompleted();
                    break;
            }
        }

        public event EventHandler<FocusEventArgs> AutocompleteFocused;
        public void SendAutocompleteFocused(bool _IsFocus)
        {
            IsFocus = _IsFocus;
            AutocompleteFocused?.Invoke(this, new FocusEventArgs(this, IsFocus));
        }

        public event EventHandler<TextChangedEventArgs> TextChangeFinished;
        public void SendTextChangeFinished(string finishText)
        {
            if (IsWrapSource)
            {
                if (TextChangeFinished != null)
                    TextChangeFinished?.Invoke(this, new TextChangedEventArgs(finishText, finishText));
            }
            else
            {
                RunFilterAutocomplete(finishText);
            }
        }

        private void RunFilterAutocomplete(string text)
        {
            ItemsSourceDisplay = null;

            if (text != null && ItemsSource != null)
            {
                var key = text.ToLower();
                var result = ItemsSource.ToList().Where(x => x.IF_GetTitle().ToLower().Contains(key) || x.IF_GetDescription().ToLower().Contains(key)).Take(30);
                ItemsSourceDisplay = result;
            }
        }

        protected override void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
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
                    SendTextChangeFinished(Text);
            }
            else if (propertyName.Equals(ItemsSourceProperty.PropertyName))
            {
                if (IsWrapSource)
                {
                    var result = ItemsSource.ToList();
                    ItemsSourceDisplay = null;
                    ItemsSourceDisplay = result;
                }
            }
        }
    }
}