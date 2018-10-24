using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using SupportWidgetXF.Droid.Renderers;
using SupportWidgetXF.Droid.Renderers.DropCombo;
using SupportWidgetXF.Widgets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportDropList), typeof(SupportDropListRenderer))]
namespace SupportWidgetXF.Droid.Renderers
{
    public class SupportDropListRenderer : SupportDropRenderer<SupportDropList, Spinner>
    {
        public SupportDropListRenderer(Context context) : base(context)
        {
        }

        public override void IF_ItemSelectd(int position)
        {
            if (!SupportView.IsAllowMultiSelect)
                base.IF_ItemSelectd(position);
            SupportView.SendOnItemSelected(position);
        }

        protected virtual void OriginalView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            SupportView.SendOnItemSelected(e.Position);
        }

        protected override void OnInitializeBorderView()
        {
            gradientDrawable = new GradientDrawable();
        }

        protected override void OnInitializeOriginalView()
        {
            base.OnInitializeOriginalView();
            OriginalView = new Spinner(Context);
            OriginalView.SetPadding(0,0,0,0);
            if (Build.VERSION.SdkInt < BuildVersionCodes.JellyBean)
            {
                OriginalView.SetBackgroundDrawable(gradientDrawable);
            }
            else
            {
                OriginalView.SetBackground(gradientDrawable);
            }
            OriginalView.ItemSelected += OriginalView_ItemSelected;
        }

        protected override void OnInitializeAdapter()
        {
            base.OnInitializeAdapter();
            if(SupportView.IsAllowMultiSelect)
                dropItemAdapter = new SpinnerMultiSelectAdapter(Context, SupportItemList, SupportView,this);
            else
                dropItemAdapter = new SpinnerAdapter(Context, SupportItemList, SupportView);
            OriginalView.Adapter = dropItemAdapter;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(SupportDropList.ItemSelectedPositionProperty.PropertyName))
            {
                if (!SupportView.IsAllowMultiSelect)
                {
                    var position = SupportView.ItemSelectedPosition;
                    if (position >= 0 && position < SupportItemList.Count)
                    {
                        OriginalView.SetSelection(position);
                    }
                }
            }
        }
    }
}