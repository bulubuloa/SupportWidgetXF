using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportAutoComplete : SupportViewBase
    {
        public static readonly BindableProperty DropModeProperty = BindableProperty.Create("DropMode", typeof(SupportAutoCompleteDropMode), typeof(SupportAutoComplete), SupportAutoCompleteDropMode.SingleTitle);
        public SupportAutoCompleteDropMode DropMode
        {
            get { return (SupportAutoCompleteDropMode)GetValue(DropModeProperty); }
            set { SetValue(DropModeProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable<IAutoDropItem>), typeof(SupportAutoComplete), null);
        public IEnumerable<IAutoDropItem> ItemsSource
        {
            get { return (IEnumerable<IAutoDropItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceOriginalProperty = BindableProperty.Create("ItemsSourceOriginal", typeof(IEnumerable<IAutoDropItem>), typeof(SupportAutoComplete), null);
        public IEnumerable<IAutoDropItem> ItemsSourceOriginal
        {
            get { return (IEnumerable<IAutoDropItem>)GetValue(ItemsSourceOriginalProperty); }
            set { SetValue(ItemsSourceOriginalProperty, value); }
        }

        public static readonly BindableProperty ItemSelecetedEventProperty = BindableProperty.Create("ItemSelecetedEvent", typeof(Action<int>), typeof(SupportAutoComplete));
        public Action<int> ItemSelecetedEvent
        {
            get { return (Action<int>)GetValue(ItemSelecetedEventProperty); }
            set { SetValue(ItemSelecetedEventProperty, value); }
        }

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

        public static readonly BindableProperty PaddingInsideProperty = BindableProperty.Create("PaddingInside", typeof(double), typeof(SupportAutoComplete), 0d);
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

        public static readonly BindableProperty SeperatorColorProperty = BindableProperty.Create("SeperatorColor", typeof(Color), typeof(SupportAutoComplete), Color.FromHex("#f1f1f1"));
        public Color SeperatorColor
        {
            get => (Color)GetValue(SeperatorColorProperty);
            set => SetValue(SeperatorColorProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(SupportAutoComplete), Color.Black);
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty DescriptionTextColorProperty = BindableProperty.Create("DescriptionTextColor", typeof(Color), typeof(SupportAutoComplete), Color.DarkGray);
        public Color DescriptionTextColor
        {
            get => (Color)GetValue(DescriptionTextColorProperty);
            set => SetValue(DescriptionTextColorProperty, value);
        }

        public static readonly BindableProperty SeperatorHeightProperty = BindableProperty.Create("SeperatorHeight", typeof(int), typeof(SupportAutoComplete), 1);
        public int SeperatorHeight
        {
            get => (int)GetValue(SeperatorHeightProperty);
            set => SetValue(SeperatorHeightProperty, value);
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
            if (TextChangeFinished != null)
                TextChangeFinished?.Invoke(this, new TextChangedEventArgs(finishText, finishText));
            else
                RunFilterAutocomplete(finishText);
        }

        private CancellationTokenSource tokenSearch;
        private void RunFilterAutocomplete(string text)
        {
            ItemsSourceOriginal = null;

            if (tokenSearch != null)
                tokenSearch.Cancel();
            tokenSearch = new CancellationTokenSource();

            if (text != null)
            {
                //Task.Run(() =>
                //{
                //    var key = text.ToLower();
                //    var result = ItemsSource.ToList().Where(x => x.IF_GetTitle().ToLower().Contains(key) || x.IF_GetDescription().ToLower().Contains(key)).Take(30);
                //    ItemsSourceOriginal = result;
                //}, tokenSearch.Token);

                var key = text.ToLower();
                var result = ItemsSource.ToList().Where(x => x.IF_GetTitle().ToLower().Contains(key) || x.IF_GetDescription().ToLower().Contains(key)).Take(30);
                ItemsSourceOriginal = result;
            }
        }

        protected override void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == IsValidProperty.PropertyName)
            {
                if (!IsValid)
                    CurrentCornerColor = InvalidCornerColor;
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                if (!IsFocus)
                    SendTextChangeFinished(Text);
            }
        }

        public SupportAutoComplete()
        {

        }
    }
}