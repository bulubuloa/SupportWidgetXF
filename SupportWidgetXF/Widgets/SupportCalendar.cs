using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SupportWidgetXF.DependencyService;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public enum SupportCalendarType
    {
        Full, Lite
    }

    public class SupportCalendar : StackLayout
    {
        protected Grid gridCalendar;

        protected List<Frame> frameDays;
        protected List<Label> frameDayOfWeeks;

        protected Label labelMonth, labelYear;
        protected ScrollView scrollViewCalendar;

        protected double WidthForLite = 0d;
        protected int CurrentDayOfMonthSelected = DateTime.UtcNow.Day;

        public static readonly BindableProperty DesignLayoutProperty = BindableProperty.Create("DesignLayout", typeof(SupportCalendarType), typeof(SupportCalendar), SupportCalendarType.Full);
        public SupportCalendarType DesignLayout
        {
            get => (SupportCalendarType)GetValue(DesignLayoutProperty);
            set => SetValue(DesignLayoutProperty, value);
        }

        public static readonly BindableProperty YearProperty = BindableProperty.Create("Year", typeof(int), typeof(SupportCalendar), DateTime.UtcNow.Year);
        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public static readonly BindableProperty FontSizeOfDayProperty = BindableProperty.Create("FontSizeOfDay", typeof(double), typeof(SupportCalendar), 13d);
        public double FontSizeOfDay
        {
            get => (double)GetValue(FontSizeOfDayProperty);
            set => SetValue(FontSizeOfDayProperty, value);
        }

        public static readonly BindableProperty FontSizeOfDayOfWeekProperty = BindableProperty.Create("FontSizeOfDayOfWeek", typeof(double), typeof(SupportCalendar), 13d);
        public double FontSizeOfDayOfWeek
        {
            get => (double)GetValue(FontSizeOfDayOfWeekProperty);
            set => SetValue(FontSizeOfDayOfWeekProperty, value);
        }

        public static readonly BindableProperty FontSizeOfMonthYearProperty = BindableProperty.Create("FontSizeOfMonthYear", typeof(double), typeof(SupportCalendar), 13d);
        public double FontSizeOfMonthYear
        {
            get => (double)GetValue(FontSizeOfMonthYearProperty);
            set => SetValue(FontSizeOfMonthYearProperty, value);
        }

        public static readonly BindableProperty MonthProperty = BindableProperty.Create("Month", typeof(int), typeof(SupportCalendar), DateTime.UtcNow.Month);
        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        public static readonly BindableProperty SpacingMonthAndDayProperty = BindableProperty.Create("SpacingMonthAndDay", typeof(int), typeof(SupportCalendar), 10);
        public int SpacingMonthAndDay
        {
            get => (int)GetValue(SpacingMonthAndDayProperty);
            set => SetValue(SpacingMonthAndDayProperty, value);
        }

        public static readonly BindableProperty DayColorProperty = BindableProperty.Create("DayColor", typeof(Color), typeof(SupportCalendar), Color.Black);
        public Color DayColor
        {
            get => (Color)GetValue(DayColorProperty);
            set => SetValue(DayColorProperty, value);
        }

        public static readonly BindableProperty DayOfWeekColorProperty = BindableProperty.Create("DayOfWeekColor", typeof(Color), typeof(SupportCalendar), Color.Gray);
        public Color DayOfWeekColor
        {
            get => (Color)GetValue(DayOfWeekColorProperty);
            set => SetValue(DayOfWeekColorProperty, value);
        }

        public static readonly BindableProperty MonthYearColorProperty = BindableProperty.Create("MonthYearColor", typeof(Color), typeof(SupportCalendar), Color.Black);
        public Color MonthYearColor
        {
            get => (Color)GetValue(MonthYearColorProperty);
            set => SetValue(MonthYearColorProperty, value);
        }

        public static readonly BindableProperty HighLightColorProperty = BindableProperty.Create("HighLightColor", typeof(Color), typeof(SupportCalendar), Color.Yellow);
        public Color HighLightColor
        {
            get => (Color)GetValue(HighLightColorProperty);
            set => SetValue(HighLightColorProperty, value);
        }

        public static readonly BindableProperty CurrentDateSelectedProperty = BindableProperty.Create("CurrentDateSelected", typeof(DateTime), typeof(SupportCalendar), DateTime.UtcNow);
        public DateTime CurrentDateSelected
        {
            get => (DateTime)GetValue(CurrentDateSelectedProperty);
            set => SetValue(CurrentDateSelectedProperty, value);
        }

        public static readonly BindableProperty FontFamilyOfDayProperty = BindableProperty.Create("FontFamilyOfDay", typeof(string), typeof(SupportCalendar), Xamarin.Forms.DependencyService.Get<IFont>().IF_GetDefaultFontFamily());
        public string FontFamilyOfDay
        {
            get { return (string)GetValue(FontFamilyOfDayProperty); }
            set { SetValue(FontFamilyOfDayProperty, value); }
        }

        public static readonly BindableProperty FontFamilyOfDayOfWeekProperty = BindableProperty.Create("FontFamilyOfDayOfWeek", typeof(string), typeof(SupportCalendar), Xamarin.Forms.DependencyService.Get<IFont>().IF_GetDefaultFontFamily());
        public string FontFamilyOfDayOfWeek
        {
            get { return (string)GetValue(FontFamilyOfDayOfWeekProperty); }
            set { SetValue(FontFamilyOfDayOfWeekProperty, value); }
        }

        public static readonly BindableProperty FontFamilyOfMonthYearProperty = BindableProperty.Create("FontFamilyOfMonthYear", typeof(string), typeof(SupportCalendar), Xamarin.Forms.DependencyService.Get<IFont>().IF_GetDefaultFontFamily());
        public string FontFamilyOfMonthYear
        {
            get { return (string)GetValue(FontFamilyOfMonthYearProperty); }
            set { SetValue(FontFamilyOfMonthYearProperty, value); }
        }
        /*
         * Command
         */
        public static readonly BindableProperty OnDateSelectedCommandProperty = BindableProperty.Create("OnDateSelectedCommand", typeof(ICommand), typeof(SupportCalendar), null);
        public ICommand OnDateSelectedCommand
        {
            get { return (ICommand)GetValue(OnDateSelectedCommandProperty); }
            set { SetValue(OnDateSelectedCommandProperty, value); }
        }

        /*
        * Event
        */
        public event EventHandler<DateTime> OnDateSelected;
        public void SendOnDateSelected(int dayOfMonth)
        {
            CurrentDateSelected = new DateTime(Year, Month, dayOfMonth);
            CurrentDayOfMonthSelected = dayOfMonth;

            OnDateSelected?.Invoke(this, CurrentDateSelected);
            OnDateSelectedCommand?.Execute(CurrentDateSelected);

            SetSelectedDayOfMonth(dayOfMonth);
        }

        private Label InitializeMonth()
        {
            return labelMonth = new Label()
            {
                Text = DateTime.UtcNow.ToString("MMMM").ToUpper(),
                TextColor = MonthYearColor,
                FontSize = FontSizeOfMonthYear,
                FontFamily = FontFamilyOfMonthYear,
                Margin = new Thickness(0, 0, 0, SpacingMonthAndDay),
            };
        }

        private Label InitializeYear()
        {
            return labelYear = new Label()
            {
                Text = Year + "",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                TextColor = MonthYearColor,
                FontSize = FontSizeOfMonthYear,
                FontFamily = FontFamilyOfMonthYear,
                Margin = new Thickness(0, 0, 0, SpacingMonthAndDay),
            };
        }

        private Label InitializeDayOfWeek(string text)
        {
            return new Label()
            {
                Text = text,
                FontSize = FontSizeOfDayOfWeek,
                TextColor = DayOfWeekColor,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
        }

        private Frame InitializeLabelDay(int dayOfMonth)
        {
            var radius = (FontSizeOfDay + 1);
            var frame = new Frame()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = radius*2,
                HeightRequest = radius*2,
                Padding = 0,
                CornerRadius = (int)radius,
                HasShadow = false,
            };

            var label = new Label()
            {
                TextColor = DayColor,
                Text = dayOfMonth + "",
                FontSize = FontSizeOfDay,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontFamily = FontFamilyOfDay
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                SendOnDateSelected(dayOfMonth);
            };
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            frame.Content = label;

            return frame;
        }

        private void SetSelectedDayOfMonth(int dayOfMonth)
        {
            int position = dayOfMonth - 1;
            frameDays.ForEach(obj => obj.BackgroundColor = Color.Transparent);
            var itemSet = frameDays[position];
            itemSet.BackgroundColor = HighLightColor;

            if(DesignLayout == SupportCalendarType.Lite && scrollViewCalendar != null)
            {
                if(dayOfMonth > 3 && dayOfMonth < 28)
                    scrollViewCalendar.ScrollToAsync(WidthForLite * (dayOfMonth - 4), 0, true);
            }
        }

        private void InitializeFullLayout()
        {
            frameDays = new List<Frame>();

            gridCalendar = new Grid()
            {
                ColumnSpacing = 0,
                Padding = 0
            };

            gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridCalendar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            if(DesignLayout == SupportCalendarType.Full)
            {
                gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                gridCalendar.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                gridCalendar.Children.Add(InitializeDayOfWeek("Sun"), 0, 1);
                gridCalendar.Children.Add(InitializeDayOfWeek("Mon"), 1, 1);
                gridCalendar.Children.Add(InitializeDayOfWeek("Tue"), 2, 1);
                gridCalendar.Children.Add(InitializeDayOfWeek("Wed"), 3, 1);
                gridCalendar.Children.Add(InitializeDayOfWeek("Thu"), 4, 1);
                gridCalendar.Children.Add(InitializeDayOfWeek("Fri"), 5, 1);
                gridCalendar.Children.Add(InitializeDayOfWeek("Sat"), 6, 1);
            }

            gridCalendar.Children.Add(InitializeMonth(), 0, 3, 0, 1);
            gridCalendar.Children.Add(InitializeYear(), 3, 7, 0, 1);

            var totalDay = System.DateTime.DaysInMonth(Year, Month);
            var date = new DateTime(Year, Month, 1);

            var firstPosition = (int)date.DayOfWeek;
            if(DesignLayout == SupportCalendarType.Full)
            {
                int CurrentRow = 2, CurrentCol = firstPosition;
                for (int i = 1; i <= totalDay; i++)
                {
                    var item = InitializeLabelDay(i);
                    frameDays.Add(item);

                    gridCalendar.Children.Add(item, CurrentCol++, CurrentRow);
                    if (CurrentCol == 7)
                    {
                        CurrentCol = 0;
                        CurrentRow++;
                    }
                }
            }
            else
            {
                var stackView = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 0,
                    Padding = 0
                };

                var firstDay = new DateTime(Year, Month, 1);
                WidthForLite = (Width - Padding.Left - Padding.Right) / 7;

                for (int i = 0; i < totalDay; i++)
                {
                    var currentDay = firstDay.AddDays(i);

                    var itemDay = InitializeLabelDay(currentDay.Day);
                    var itemDayOfWeek = InitializeDayOfWeek(currentDay.ToString("ddd"));
                    var stackChild = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        WidthRequest = WidthForLite
                    };
                    stackChild.Children.Add(itemDayOfWeek);
                    stackChild.Children.Add(itemDay);

                    frameDays.Add(itemDay);
                    stackView.Children.Add(stackChild);
                }

                scrollViewCalendar = new ScrollView()
                {
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                    Orientation = ScrollOrientation.Horizontal,
                    Padding = 0,
                };
                scrollViewCalendar.Content = stackView;
                gridCalendar.Children.Add(scrollViewCalendar, 0, 7, 1, 2);
            }

            Children.Clear();
            Children.Add(gridCalendar);
            CurrentDayOfMonthSelected = DateTime.UtcNow.Day;

            SetSelectedDayOfMonth(CurrentDayOfMonthSelected);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(YearProperty.PropertyName))
            {
                InitializeFullLayout();
            }
            else if (propertyName.Equals(MonthProperty.PropertyName))
            {
                InitializeFullLayout();
            }
            else if (propertyName.Equals(DesignLayoutProperty.PropertyName))
            {
                InitializeFullLayout();
            }
            else if (propertyName.Equals(DayColorProperty.PropertyName))
            {
               if(frameDays!=null)
                {
                    foreach (var item in frameDays)
                    {
                        var content = item.Content as Label;
                        content.TextColor = DayColor;
                    }
                }
            }
            else if (propertyName.Equals(FontFamilyOfDayProperty.PropertyName))
            {
                if (frameDays != null)
                {
                    foreach (var item in frameDays)
                    {
                        var content = item.Content as Label;
                        content.FontFamily = FontFamilyOfDay;
                    }
                }
            }
            else if (propertyName.Equals(FontSizeOfDayProperty.PropertyName))
            {
                if (frameDays != null)
                {
                    foreach (var item in frameDays)
                    {
                        var content = item.Content as Label;
                        content.FontSize = FontSizeOfDay;
                    }
                }
            }
            else if (propertyName.Equals(DayOfWeekColorProperty.PropertyName))
            {
                if (frameDayOfWeeks != null)
                {
                    foreach (var item in frameDayOfWeeks)
                    {
                        item.TextColor = DayOfWeekColor;
                    }
                }
            }
            else if (propertyName.Equals(FontFamilyOfDayOfWeekProperty.PropertyName))
            {
                if (frameDayOfWeeks != null)
                {
                    foreach (var item in frameDayOfWeeks)
                    {
                        item.FontFamily = FontFamilyOfDayOfWeek;
                    }
                }
            }
            else if (propertyName.Equals(FontSizeOfDayOfWeekProperty.PropertyName))
            {
                if (frameDayOfWeeks != null)
                {
                    foreach (var item in frameDayOfWeeks)
                    {
                        item.FontSize = FontSizeOfDayOfWeek;
                    }
                }
            }
            else if (propertyName.Equals(FontSizeOfMonthYearProperty.PropertyName))
            {
                if (labelMonth != null)
                {
                    labelMonth.FontSize = FontSizeOfMonthYear;
                }
                if (labelYear != null)
                {
                    labelYear.FontSize = FontSizeOfMonthYear;
                }

            }
            else if (propertyName.Equals(MonthYearColorProperty.PropertyName))
            {
                if (labelMonth != null)
                {
                    labelMonth.TextColor = MonthYearColor;
                }
                if (labelYear != null)
                {
                    labelYear.TextColor = MonthYearColor;
                }

            }
            else if (propertyName.Equals(FontFamilyOfMonthYearProperty.PropertyName))
            {
                if (labelMonth != null)
                {
                    labelMonth.FontFamily = FontFamilyOfMonthYear;
                }
                if (labelYear != null)
                {
                    labelYear.FontFamily = FontFamilyOfMonthYear;
                }
            }
            else if (propertyName.Equals(SpacingMonthAndDayProperty.PropertyName))
            {
                if (labelMonth != null)
                {
                    labelMonth.Margin = new Thickness(0, 0, 0, SpacingMonthAndDay);
                }
                if (labelYear != null)
                {
                    labelYear.Margin = new Thickness(0, 0, 0, SpacingMonthAndDay);
                }
            }
            else if (propertyName.Equals(HighLightColorProperty.PropertyName))
            {
                SetSelectedDayOfMonth(CurrentDayOfMonthSelected);
            }
        }

        public SupportCalendar()
        {
            //InitializeFullLayout();
            SizeChanged += (sender, e) => 
            {
                Debug.WriteLine(Width + "/" + Height);
                InitializeFullLayout();
            };
        }
    }
}