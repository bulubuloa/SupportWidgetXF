using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SupportWidgetXF.Models;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportDropList : SupportViewDrop
    {
        public SupportDropList()
        {
        }

        public static readonly BindableProperty ItemSelectedPositionProperty = BindableProperty.Create("ItemSelectedPosition", typeof(int), typeof(SupportDropList), 0);
        public int ItemSelectedPosition
        {
            get { return (int)GetValue(ItemSelectedPositionProperty); }
            set { SetValue(ItemSelectedPositionProperty, value); }
        }

        public static readonly BindableProperty MultiItemSelectedPositionProperty = BindableProperty.Create("MultiItemSelectedPosition", typeof(IEnumerable<int>), typeof(SupportDropList), new List<int> { });
        public IEnumerable<int> MultiItemSelectedPosition
        {
            get { return (IEnumerable<int>)GetValue(MultiItemSelectedPositionProperty); }
            set { SetValue(MultiItemSelectedPositionProperty, value); }
        }

        public static readonly BindableProperty IsAllowMultiSelectProperty = BindableProperty.Create("IsAllowMultiSelect", typeof(bool), typeof(SupportDropList), false);
        public bool IsAllowMultiSelect
        {
            get { return (bool)GetValue(IsAllowMultiSelectProperty); }
            set { SetValue(IsAllowMultiSelectProperty, value); }
        }

        public event EventHandler<IntegerEventArgs> ItemSelected;
        public void SendItemSelected(int position)
        {
            ItemSelected?.Invoke(this, new IntegerEventArgs(position));
        }

        public event EventHandler<MultiIntegerEventArgs> MultiItemSelected;
        public void SendMultiItemSelected(IEnumerable<int> position)
        {
            MultiItemSelected?.Invoke(this, new MultiIntegerEventArgs(position));
        }

        public void OnDropListTouch()
        {
            ItemsSourceDisplay = ItemsSource;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(ItemsSourceProperty.PropertyName))
            {
                SendItemSelected(ItemSelectedPosition);
                SendMultiItemSelected(MultiItemSelectedPosition);
            }
        }
    }
}