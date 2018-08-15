using System;
using System.Collections.Generic;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportViewDrop : SupportViewBase
    {
        public static readonly BindableProperty DropModeProperty = BindableProperty.Create("DropMode", typeof(SupportAutoCompleteDropMode), typeof(SupportViewDrop), SupportAutoCompleteDropMode.SingleTitle);
        public SupportAutoCompleteDropMode DropMode
        {
            get { return (SupportAutoCompleteDropMode)GetValue(DropModeProperty); }
            set { SetValue(DropModeProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable<IAutoDropItem>), typeof(SupportViewDrop), null);
        public IEnumerable<IAutoDropItem> ItemsSource
        {
            get { return (IEnumerable<IAutoDropItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceDisplayProperty = BindableProperty.Create("ItemsSourceDisplay", typeof(IEnumerable<IAutoDropItem>), typeof(SupportAutoComplete), null);
        public IEnumerable<IAutoDropItem> ItemsSourceDisplay
        {
            get { return (IEnumerable<IAutoDropItem>)GetValue(ItemsSourceDisplayProperty); }
            set { SetValue(ItemsSourceDisplayProperty, value); }
        }

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create("HasShadow", typeof(bool), typeof(SupportViewDrop), false);
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public static readonly BindableProperty SeperatorColorProperty = BindableProperty.Create("SeperatorColor", typeof(Color), typeof(SupportViewDrop), Color.FromHex("#f1f1f1"));
        public Color SeperatorColor
        {
            get => (Color)GetValue(SeperatorColorProperty);
            set => SetValue(SeperatorColorProperty, value);
        }

        public static readonly BindableProperty SeperatorHeightProperty = BindableProperty.Create("SeperatorHeight", typeof(int), typeof(SupportViewDrop), 1);
        public int SeperatorHeight
        {
            get => (int)GetValue(SeperatorHeightProperty);
            set => SetValue(SeperatorHeightProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(SupportViewDrop), Color.Black);
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty DescriptionTextColorProperty = BindableProperty.Create("DescriptionTextColor", typeof(Color), typeof(SupportViewDrop), Color.DarkGray);
        public Color DescriptionTextColor
        {
            get => (Color)GetValue(DescriptionTextColorProperty);
            set => SetValue(DescriptionTextColorProperty, value);
        }
    }
}