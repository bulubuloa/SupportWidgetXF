using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SupportWidgetXF.Models;
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportDropList : SupportViewDrop
    {
        /*
         * Properties
         */
        public static readonly BindableProperty ItemSelectedPositionProperty = BindableProperty.Create("ItemSelectedPosition", typeof(int), typeof(SupportDropList), 0, BindingMode.TwoWay);
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

        /*
         * Function
         */


        /*
         * Command
         */
        public static readonly BindableProperty OnItemSelectedCommandProperty = BindableProperty.Create("OnItemSelectedCommand", typeof(ICommand), typeof(SupportDropList), null);
        public ICommand OnItemSelectedCommand
        {
            get { return (ICommand)GetValue(OnItemSelectedCommandProperty); }
            set { SetValue(OnItemSelectedCommandProperty, value); }
        }

        public static readonly BindableProperty OnMultiItemSelectedCommandProperty = BindableProperty.Create("OnMultiItemSelectedCommand", typeof(ICommand), typeof(SupportDropList), null);
        public ICommand OnMultiItemSelectedCommand
        {
            get { return (ICommand)GetValue(OnMultiItemSelectedCommandProperty); }
            set { SetValue(OnMultiItemSelectedCommandProperty, value); }
        }


        /*
         * Event
         */
        public event EventHandler<IntegerEventArgs> OnItemSelected;
        public event EventHandler<MultiIntegerEventArgs> OnMultiItemSelected;

        public void SendOnItemSelected(int position)
        {
            if (ItemSelectedPosition != position)
                ItemSelectedPosition = position;

            ChangeSelectionValue(position);

            var paramete = new IntegerEventArgs(position);
            OnItemSelected?.Invoke(this, paramete);
            if (OnItemSelectedCommand != null)
                OnItemSelectedCommand.Execute(paramete);

            if (IsAllowMultiSelect)
            {
                SendOnMultiItemSelected(GetItemPositionSelected());
            }
        }

        public void SendOnMultiItemSelected(IEnumerable<int> position)
        {
            var paramete = new MultiIntegerEventArgs(position);
            OnMultiItemSelected?.Invoke(this, paramete);
            if (OnMultiItemSelectedCommand != null)
                OnMultiItemSelectedCommand.Execute(paramete);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        /*
         * Private process
         */
        private IEnumerable<int> GetItemPositionSelected()
        {
            var result = new List<int>();
            var items = ItemsSource.ToList();
            for (int i = 0; i < items.Count; i++)
            {
                var xx = items[i];
                if (xx.IF_GetChecked())
                    result.Add(i);
            }
            return result;
        }

        private void ChangeSelectionValue(int position)
        {
            var items = ItemsSource.ToList();

            if (IsAllowMultiSelect)
            {
                var item = items[position];
                item.IF_SetChecked(item.IF_GetChecked() ? false : true);
                RefreshList = new Refresh();
                Text = String.Join(",", items.Where(obj => obj.IF_GetChecked()).Select(obj => obj.IF_GetTitle()));
            }
            else
            {
                ItemSelectedPosition = position;
                Text = items[position].IF_GetTitle();
            }
        }
    }
}