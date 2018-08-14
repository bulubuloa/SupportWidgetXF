using System;
using System.Collections.Generic;
using SupportWidgetXF.Models;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
	public class SupportActionMenu : Button
    {
        public static readonly BindableProperty MenuItemsSourceProperty = BindableProperty.Create("MenuItemsSource", typeof(IEnumerable<IAutoDropItem>), typeof(SupportDropList), new List<IAutoDropItem>());
        public IEnumerable<IAutoDropItem> MenuItemsSource
        {
            get { return (IEnumerable<IAutoDropItem>)GetValue(MenuItemsSourceProperty); }
            set { SetValue(MenuItemsSourceProperty, value); }
        }

        public event EventHandler<ObjectEventArgs> OpenedSubMenu;
        public void SendMenuOpened()
        {
            OpenedSubMenu?.Invoke(this, new ObjectEventArgs(this.BindingContext));
        }

        public SupportActionMenu()
        {
        }
    }
}