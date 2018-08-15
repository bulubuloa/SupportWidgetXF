using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;
using SupportWidgetXF.Models.Widgets;
using SupportWidgetXF.Widgets;
using SupportWidgetXF.Widgets.Interface;
using Xamarin.Forms.Platform.Android;

namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportDropRenderer<TSupport,TOrignal> : ViewRenderer<TSupport, Android.Views.View>,IDropItemSelected where TSupport : SupportViewDrop where TOrignal : Android.Views.View
    {
        protected TSupport SupportView;
        protected TOrignal OriginalView;
        protected ArrayAdapter dropItemAdapter;
        protected List<IAutoDropItem> SupportItemList = new List<IAutoDropItem>();

        public SupportDropRenderer(Context context) : base(context)
        {
            
        }

        protected virtual void RefreshhAdapter()
        {
        }

        protected virtual void NotifyAdapterChanged()
        {
            SupportItemList.Clear();
            if (SupportView.ItemsSource != null)
            {
                SupportItemList.AddRange(SupportView.ItemsSource.ToList());
            }
            if (dropItemAdapter != null)
                dropItemAdapter.NotifyDataSetChanged();
        }

        protected virtual void OnInitializeOriginalView()
        {
            
        }

        protected virtual void OnInitializeBorderView()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TSupport> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && e.NewElement is TSupport)
            {
                SupportView = e.NewElement as TSupport;
                OnInitializeBorderView();
                OnInitializeOriginalView();
                NotifyAdapterChanged();
                RefreshhAdapter();
                SetNativeControl(OriginalView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(SupportViewDrop.ItemsSource)))
            {
                RefreshhAdapter();
            }
        }

        public virtual void IF_ItemSelectd(int position)
        {
            
        }
    }
}
