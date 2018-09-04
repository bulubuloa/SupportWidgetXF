using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets
{
    public class SupportBindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(SupportBindableStackLayout), null);
        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(SupportBindableStackLayout), null);
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private void CreateStack()
        {
            Children.Clear();
            if (ItemsSource == null || ItemsSource.Count() == 0 || ItemsSource.First() == null)
            {
                return;
            }
            CreateCells();
        }

        private void CreateCells()
        {
            foreach (var item in ItemsSource)
            {
                Children.Add(CreateCellView(item));
            }
        }

        private View CreateCellView(object item)
        {
            var view = (View)ItemTemplate.CreateContent();
            var bindableObject = (BindableObject)view;

            if (bindableObject != null)
            {
                bindableObject.BindingContext = item;
            }
            return view;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                CreateStack();
            }
            base.OnPropertyChanged(propertyName);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            CreateStack();
        }

        public SupportBindableStackLayout()
        {
        }
    }
}