
using System;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using SupportWidgetXF.DependencyService;

namespace SupportWidgetXF.Widgets
{
    public class SupportRadioGroup : StackLayout
    {
        public SupportRadioGroup()
        {
            this.ChildAdded += RadioButtonGroupView_ChildAdded;
            this.ChildrenReordered += RadioButtonGroupView_ChildrenReordered;
        }

        private void RadioButtonGroupView_ChildrenReordered(object sender, EventArgs e)
        {
            UpdateAllEvent();
        }
        private void UpdateAllEvent()
        {
            foreach (var item in this.Children)
            {
                if (item is SupportRadioButton)
                {
                    (item as SupportRadioButton).Clicked -= UpdateSelected;
                    (item as SupportRadioButton).Clicked += UpdateSelected;
                }
            }
        }

        private void RadioButtonGroupView_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is SupportRadioButton)
            {
                (e.Element as SupportRadioButton).Clicked -= UpdateSelected;
                (e.Element as SupportRadioButton).Clicked += UpdateSelected;
            }
        }

        void UpdateSelected(object selected, EventArgs e)
        {
            foreach (var item in this.Children)
            {
                if (item is SupportRadioButton)
                    (item as SupportRadioButton).IsChecked = item == selected;
            }
        }

        public async void DisplayValidation()
        {
            this.BackgroundColor = Color.Red;
            await Task.Delay(500);
            this.BackgroundColor = Color.Transparent;
        }

        /// <summary>
        /// Returns selected radio button's index from inside of this.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                int index = 0;
                foreach (var item in this.Children)
                {
                    if (item is SupportRadioButton)
                    {
                        if ((item as SupportRadioButton).IsChecked)
                            return index;
                        index++;
                    }
                }
                return -1;
            }
            set
            {
                int index = 0;
                foreach (var item in this.Children)
                {
                    if (item is SupportRadioButton)
                    {
                        (item as SupportRadioButton).IsChecked = index == value;
                        index++;
                    }
                }
            }
        }
        /// <summary>
        /// Returns selected radio button's Value from inside of this.
        /// You can change the selectedItem too by sending a Value which matches ones of radio button's value
        /// </summary>
        public object SelectedItem
        {
            get
            {
                foreach (var item in this.Children)
                {
                    if (item is SupportRadioButton && (item as SupportRadioButton).IsChecked)
                        return (item as SupportRadioButton).Value;
                }
                return null;
            }
            set
            {
                foreach (var item in this.Children)
                {
                    if (item is SupportRadioButton)
                        (item as SupportRadioButton).IsChecked = (item as SupportRadioButton).Value == value;
                }
            }
        }

        public bool IsRequired { get; set; }

        public bool IsValidated { get => this.IsRequired && this.SelectedItem != null; }

        public string ValidationMessage { get; set; }
    }

    /// <summary>
    /// Radio Button with Text
    /// </summary>
    public class SupportRadioButton : StackLayout
    {
        Label lblEmpty = new Label { TextColor = Color.Gray, Text = "◯", HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center };
        Label lblFilled = new Label { TextColor = Color.Accent, Text = "●", IsVisible = false, Scale = 0.9, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center };
        Label lblText = new Label { Text = "", VerticalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.CenterAndExpand };
        private bool _isDisabled;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SupportRadioButton()
        {
            if (Device.RuntimePlatform != Device.iOS)
                lblText.FontSize = lblText.FontSize *= 1.5;
            lblEmpty.FontSize = lblText.FontSize * 1.3;
            lblFilled.FontSize = lblText.FontSize * 1.3;
            Orientation = StackOrientation.Horizontal;
            this.Children.Add(new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    lblEmpty,
                    lblFilled
                },
                MinimumWidthRequest = 35,
            });
            this.Children.Add(lblText);
            this.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(Tapped) });
        }

        /// <summary>
        /// Quick generating constructor.
        /// </summary>
        /// <param name="value">Value to keep in radio button</param>
        /// <param name="displayMember">If you send an ojbect as value. Which property will be displayed. Or override .ToString() inside of your object.</param>
        /// <param name="isChecked"> Checked or not situation</param>
        public SupportRadioButton(object value, string displayMember, bool isChecked = false) : this()
        {
            this.Value = value;
            this.IsChecked = isChecked;
            string text;
            if (!String.IsNullOrEmpty(displayMember))
                text = value.GetType().GetProperty(displayMember)?.GetValue(value).ToString();
            else
                text = value.ToString();
            lblText.Text = text ?? " ";
        }
        /// <summary>
        /// Quick generating constructor.
        /// </summary>
        /// <param name="text">Text to display right of Radio button </param>
        /// <param name="isChecked">IsSelected situation</param>
        public SupportRadioButton(string text, bool isChecked = false) : this()
        {
            Value = text;
            lblText.Text = text;
            this.IsChecked = isChecked;
        }

        public event EventHandler Clicked;
        public ICommand ClickCommand { get; set; }

        /// <summary>
        /// Value to keep inside of Radio Button
        /// </summary>
        public object Value { get; set; }
        public bool IsChecked { get => lblFilled.IsVisible; set => lblFilled.IsVisible = value; }
        public bool IsDisabled { get => _isDisabled; set { _isDisabled = value; this.Opacity = value ? 0.6 : 1; } }

        /// <summary>
        /// Text Description of Radio Button. It will be displayed right of Radio Button
        /// </summary>
        public string Text { get => lblText.Text; set => lblText.Text = value; }

        /// <summary>
        /// Fontsize of Description Text
        /// </summary>
        public double FontSize { get => lblText.FontSize; set { lblText.FontSize = value; lblFilled.FontSize = value * 1.3; lblEmpty.FontSize = value * 1.3; } }

        /// <summary>
        /// Color of Radio Button's checked.
        /// </summary>
        public Color Color { get => lblFilled.TextColor; set => lblFilled.TextColor = value; }

        /// <summary>
        /// Color of description text of Radio Button
        /// </summary>
        public Color TextColor { get => lblText.TextColor; set => lblText.TextColor = value; }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool),
                                                                                            typeof(SupportRadioButton), false, propertyChanged: (bo, ov, nv) => (bo as SupportRadioButton).IsChecked = (bool)nv);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
                                                                                       typeof(SupportRadioButton), "", propertyChanged: (bo, ov, nv) => (bo as SupportRadioButton).Text = (string)nv);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double),
                                                                                           typeof(SupportRadioButton), 20.0, propertyChanged: (bo, ov, nv) => (bo as SupportRadioButton).FontSize = (double)nv);
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color),
                                                                                        typeof(SupportRadioButton), Color.Default, propertyChanged: (bo, ov, nv) => (bo as SupportRadioButton).Color = (Color)nv);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color),
                                                                                            typeof(SupportRadioButton), Color.Default, propertyChanged: (bo, ov, nv) => (bo as SupportRadioButton).TextColor = (Color)nv);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create("FontFamily", typeof(string), typeof(SupportRadioButton), Xamarin.Forms.DependencyService.Get<IFont>().IF_GetDefaultFontFamily());
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == FontFamilyProperty.PropertyName)
            {
                if (lblText != null)
                    lblText.FontFamily = FontFamily;
            }
        }

        void Tapped()
        {
            if (IsDisabled) return;
            IsChecked = !IsChecked;
            Clicked?.Invoke(this, new EventArgs());
            ClickCommand?.Execute(this);
        }
    }
}