using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Droid.Renderers
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
        private Frame ButtonFrame;
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
            ButtonFrame = new Frame()
            {
                BackgroundColor = Color.Transparent,
                HasShadow = false,
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
                Command = ClickedCommand
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
                Text = ButtonTitle,
                TextColor = SupButtonTitleColor,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            ButtonImage = new Image()
            {
                Source = ButtonIcon,
                HeightRequest = IconSize,
                WidthRequest = IconSize,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            StackInside = new StackLayout()
            {
                Spacing = IconTitleSpacing,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            switch (IconTitlePostion)
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
            ButtonFrame.Content = StackInside;
        }

        public static readonly Xamarin.Forms.BindableProperty IconTitlePostionProperty = Xamarin.Forms.BindableProperty.Create("IconTitlePostion", typeof(IconTitlePostionEnum), typeof(SupportButtonXF), IconTitlePostionEnum.IconLeft);
        public IconTitlePostionEnum IconTitlePostion
        {
            get => (IconTitlePostionEnum)GetValue(IconTitlePostionProperty);
            set => SetValue(IconTitlePostionProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty ButtonTitleProperty = Xamarin.Forms.BindableProperty.Create("ButtonTitle", typeof(string), typeof(SupportButtonXF), "");
        public string ButtonTitle
        {
            get => (string)GetValue(ButtonTitleProperty);
            set => SetValue(ButtonTitleProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty ButtonIconProperty = Xamarin.Forms.BindableProperty.Create("ButtonIcon", typeof(string), typeof(SupportButtonXF), "");
        public string ButtonIcon
        {
            get => (string)GetValue(ButtonIconProperty);
            set => SetValue(ButtonIconProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty SupButtonBackgroundColorProperty = Xamarin.Forms.BindableProperty.Create("SupButtonBackgroundColor", typeof(Color), typeof(SupportButtonXF), Color.Transparent);
        public Color SupButtonBackgroundColor
        {
            get => (Color)GetValue(SupButtonBackgroundColorProperty);
            set => SetValue(SupButtonBackgroundColorProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty SupButtonTitleColorProperty = Xamarin.Forms.BindableProperty.Create("SupButtonTitleColor", typeof(Color), typeof(SupportButtonXF), Color.Transparent);
        public Color SupButtonTitleColor
        {
            get => (Color)GetValue(SupButtonTitleColorProperty);
            set => SetValue(SupButtonTitleColorProperty, value);
        }

        public static readonly Xamarin.Forms.BindableProperty IconTitleSpacingProperty = Xamarin.Forms.BindableProperty.Create("IconTitleSpacing", typeof(int), typeof(SupportButtonXF), 0);
        public int IconTitleSpacing
        {
            get => (int)GetValue(IconTitleSpacingProperty);
            set => SetValue(IconTitleSpacingProperty, value);
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

        public static readonly Xamarin.Forms.BindableProperty IconSizeProperty = Xamarin.Forms.BindableProperty.Create("IconSize", typeof(int), typeof(SupportButtonXF), 15);
        public int IconSize
        {
            get => (int)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
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

        public SupportButtonXF()
        {
            Initialize();
            InitializeArrange();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(ButtonTitleProperty.PropertyName))
            {
                if (ButtonTitleLabel != null)
                {
                    ButtonTitleLabel.Text = ButtonTitle;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(ButtonIconProperty.PropertyName))
            {
                if (ButtonImage != null)
                {
                    ButtonImage.Source = ButtonIcon;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(IconTitleSpacingProperty.PropertyName))
            {
                if (StackInside != null)
                {
                    StackInside.Spacing = IconTitleSpacing;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(SupButtonTitleColorProperty.PropertyName))
            {
                if (ButtonTitleLabel != null)
                {
                    ButtonTitleLabel.TextColor = SupButtonTitleColor;
                }
                else
                {
                    InitializeArrange();
                }
            }
            else if (propertyName.Equals(SupButtonBackgroundColorProperty.PropertyName))
            {
                if (ButtonFrame != null)
                {
                    ButtonFrame.BackgroundColor = SupButtonBackgroundColor;
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
        }
    }
}