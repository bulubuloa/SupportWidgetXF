using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SupportWidgetXF.DependencyService;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public enum IconTitlePostionEnum
    {
        IconLeft, IconRight, IconTop, IconBotom
    }

    public class SupportButtonXF : Grid
    {
        private Label ButtonTitleLabel;
        private Image ButtonImage;
        private SupportButton ButtonBehide;
        private SupportFrame ButtonFrame;
        private StackLayout StackInside;

        private void Initialize()
        {
            ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(1, GridUnitType.Star)
            });
            ButtonFrame = new SupportFrame()
            {
                BackgroundColor = Color.Transparent,
                HasShadow = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                CornerRadius = 0
            };
            ButtonBehide = new SupportButton()
            {
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Command = ClickedCommand,
                CommandParameter = CommandParameter
            };
            ButtonTitleLabel = new Label();
            ButtonImage = new Image();
            Children.Add(ButtonFrame, 0, 1, 0, 1);
            Children.Add(ButtonBehide, 0, 1, 0, 1);
        }

        private void InitializeArrange()
        {
            ButtonTitleLabel = new Label()
            {
                Text = TitleText,
                TextColor = TitleColor,
                FontSize = TitleFontSize,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };
            ButtonImage = new Image()
            {
                Source = IconSource,
                HeightRequest = IconSize,
                WidthRequest = IconSize,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            StackInside = new StackLayout()
            {
                Spacing = IconAndTitleSpacing,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            switch (IconAndTitleArrange)
            {
                case IconTitlePostionEnum.IconTop:
                    StackInside.Orientation = StackOrientation.Vertical;
                    StackInside.Children.Add(ButtonImage);
                    StackInside.Children.Add(ButtonTitleLabel);
                    break;
                case IconTitlePostionEnum.IconBotom:
                    StackInside.Orientation = StackOrientation.Vertical;
                    StackInside.Children.Add(ButtonTitleLabel);
                    StackInside.Children.Add(ButtonImage);
                    break;
                case IconTitlePostionEnum.IconLeft:
                    StackInside.Orientation = StackOrientation.Horizontal;
                    StackInside.Children.Add(ButtonImage);
                    StackInside.Children.Add(ButtonTitleLabel);
                    break;
                case IconTitlePostionEnum.IconRight:
                    StackInside.Orientation = StackOrientation.Horizontal;
                    StackInside.Children.Add(ButtonTitleLabel);
                    StackInside.Children.Add(ButtonImage);
                    break;
                default:
                    break;
            }
            ButtonFrame.HasShadow = Shadow;
            ButtonFrame.Content = StackInside;
        }

        public static readonly Xamarin.Forms.BindableProperty IconAndTitleArrangeProperty = Xamarin.Forms.BindableProperty.Create("IconAndTitleArrange", typeof(IconTitlePostionEnum), typeof(SupportButtonXF), IconTitlePostionEnum.IconLeft);
        public IconTitlePostionEnum IconAndTitleArrange
        {
            get => (IconTitlePostionEnum)GetValue(IconAndTitleArrangeProperty);
            set => SetValue(IconAndTitleArrangeProperty, value);
        }
        public static readonly Xamarin.Forms.BindableProperty IconAndTitleSpacingProperty = Xamarin.Forms.BindableProperty.Create("IconAndTitleSpacing", typeof(int), typeof(SupportButtonXF), 0);
        public int IconAndTitleSpacing
        {
            get => (int)GetValue(IconAndTitleSpacingProperty);
            set => SetValue(IconAndTitleSpacingProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty TitleTextProperty = Xamarin.Forms.BindableProperty.Create("TitleText", typeof(string), typeof(SupportButtonXF), "");
        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty TitleColorProperty = Xamarin.Forms.BindableProperty.Create("TitleColor", typeof(Color), typeof(SupportButtonXF), Color.Transparent);
        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty TitleFontSizeProperty = Xamarin.Forms.BindableProperty.Create("TitleFontSize", typeof(double), typeof(SupportButtonXF), 12d);
        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create("FontFamily", typeof(string), typeof(SupportViewBase), Xamarin.Forms.DependencyService.Get<IFont>().IF_GetDefaultFontFamily());
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }


        public static readonly Xamarin.Forms.BindableProperty IconSourceProperty = Xamarin.Forms.BindableProperty.Create("IconSource", typeof(string), typeof(SupportButtonXF), "");
        public string IconSource
        {
            get => (string)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty IconSizeProperty = Xamarin.Forms.BindableProperty.Create("IconSize", typeof(int), typeof(SupportButtonXF), 15);
        public int IconSize
        {
            get => (int)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty FrameBackgroundColorProperty = Xamarin.Forms.BindableProperty.Create("FrameBackgroundColor", typeof(Color), typeof(SupportButtonXF), Color.Transparent);
        public Color FrameBackgroundColor
        {
            get => (Color)GetValue(FrameBackgroundColorProperty);
            set => SetValue(FrameBackgroundColorProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty ShadowProperty = Xamarin.Forms.BindableProperty.Create("Shadow", typeof(bool), typeof(SupportButtonXF), false);
        public bool Shadow
        {
            get => (bool)GetValue(ShadowProperty);
            set => SetValue(ShadowProperty, value);
        }


        public static readonly Xamarin.Forms.BindableProperty CornerRadiusProperty = Xamarin.Forms.BindableProperty.Create("CornerRadius", typeof(int), typeof(SupportButtonXF), 0);
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty CornerWidthProperty = Xamarin.Forms.BindableProperty.Create("CornerWidth", typeof(int), typeof(SupportButtonXF), 0);
        public int CornerWidth
        {
            get => (int)GetValue(CornerWidthProperty);
            set => SetValue(CornerWidthProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty CornerColorProperty = Xamarin.Forms.BindableProperty.Create("CornerColor", typeof(Color), typeof(SupportButtonXF), Color.Transparent);
        public Color CornerColor
        {
            get => (Color)GetValue(CornerColorProperty);
            set => SetValue(CornerColorProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty ClickedCommandProperty = Xamarin.Forms.BindableProperty.Create("ClickedCommand", typeof(ICommand), typeof(SupportButtonXF), null);
        public ICommand ClickedCommand
        {
            get => (ICommand)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(SupportButtonXF), null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public SupportButtonXF()
        {
            Initialize();
            InitializeArrange();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(TitleTextProperty.PropertyName))
            {
                if (ButtonTitleLabel != null)
                {
                    ButtonTitleLabel.Text = TitleText;
                }
                else
                {
                    InitializeArrange();
                }
            }
            if (propertyName.Equals(TitleFontSizeProperty.PropertyName))
            {
                if (ButtonTitleLabel != null)
                {
                    ButtonTitleLabel.FontSize = TitleFontSize;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(IconSourceProperty.PropertyName))
            {
                if (ButtonImage != null)
                {
                    ButtonImage.Source = IconSource;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(IconAndTitleArrangeProperty.PropertyName))
            {
                if (StackInside != null)
                {
                    StackInside.Orientation = (IconAndTitleArrange == IconTitlePostionEnum.IconLeft || IconAndTitleArrange == IconTitlePostionEnum.IconRight) ? StackOrientation.Horizontal : StackOrientation.Vertical;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(IconAndTitleSpacingProperty.PropertyName))
            {
                if (StackInside != null)
                {
                    StackInside.Spacing = IconAndTitleSpacing;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(TitleColorProperty.PropertyName))
            {
                if (ButtonTitleLabel != null)
                {
                    ButtonTitleLabel.TextColor = TitleColor;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(FrameBackgroundColorProperty.PropertyName))
            {
                if (ButtonFrame != null)
                {
                    ButtonFrame.BackgroundColor = FrameBackgroundColor;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(CornerRadiusProperty.PropertyName))
            {
                if (ButtonFrame != null)
                {
                    ButtonFrame.CornerRadius = CornerRadius;
                }
                else
                {
                    InitializeArrange();
                }
            }

            else if (propertyName.Equals(CornerColorProperty.PropertyName))
            {
                if (ButtonFrame != null)
                {
                    ButtonFrame.BorderColor = CornerColor;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(ShadowProperty.PropertyName))
            {
                if (ButtonFrame != null)
                {
                    ButtonFrame.HasShadow = Shadow;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(IconSizeProperty.PropertyName))
            {
                if (ButtonImage != null)
                {
                    ButtonImage.WidthRequest = IconSize;
                    ButtonImage.HeightRequest = IconSize;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(ClickedCommandProperty.PropertyName))
            {
                if (ButtonBehide != null)
                {
                    ButtonBehide.Command = ClickedCommand;
                }
            }
            else if (propertyName.Equals(CommandParameterProperty.PropertyName))
            {
                if (ButtonBehide != null)
                {
                    ButtonBehide.CommandParameter = CommandParameter;
                }
            }
        }
    }
}
