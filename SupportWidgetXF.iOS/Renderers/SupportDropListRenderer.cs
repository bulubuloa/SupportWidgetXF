using System;
using System.ComponentModel;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.DropCombo;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportDropList), typeof(SupportDropListRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportDropListRenderer : SupportDropRenderer<SupportDropList>
    {
        private UIView coverView, tapView;

        public override void OnInitializeTextField()
        {
            base.OnInitializeTextField();

            tapView = new UIView();
            tapView.Frame = this.Bounds;
            tapView.AddGestureRecognizer(new UITapGestureRecognizer(() => { ShowData(); }));

            var arrow = new UIImageView();
            arrow.ContentMode = UIViewContentMode.ScaleAspectFit;
            arrow.Image = UIImage.FromBundle("sort_down").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            arrow.Frame = new CGRect(4, 4, 12, 12);
            var contentRight = new UIView(new CGRect(0, 0, 20, 20));
            contentRight.AddSubview(arrow);
            textField.RightView = contentRight;
            textField.RightViewMode = UITextFieldViewMode.Always;
            textField.LeftView = new UIView(new CGRect(0, 0, 5, 5));
            textField.LeftViewMode = UITextFieldViewMode.Always;
            textField.Enabled = false;
            textField.UserInteractionEnabled = false;

            tapView.AddSubview(textField);
        }

        public override void OnInitializeTableSource()
        {
            dropSource = new DropItemSource(SupportItemList, SupportView, HeightOfRow, this,SupportView.IsAllowMultiSelect);
            tableView.Source = dropSource;
        }

        public override CGRect Frame
        {
            get => base.Frame;
            set
            {
                base.Frame = value;
                if (textField != null && tapView != null && value != CGRect.Empty)
                {
                    textField.Frame = new CGRect(0, 0, value.Size.Width, value.Size.Height);
                    this.LayoutSubviews();
                }
            }
        }

        public override void SyncItemSource()
        {
            base.SyncItemSource();
            if(textField!=null && SupportView!=null && SupportItemList!=null && SupportItemList.Count>0)
            {
                if(SupportView.ItemSelectedPosition >= 0 && SupportView.ItemSelectedPosition < SupportItemList.Count)
                    textField.Text = SupportItemList[SupportView.ItemSelectedPosition].IF_GetTitle();
            }
        }

        protected override void OnSetNativeControl()
        {
            SetNativeControl(tapView);
        }

        public override void HideData()
        {
            base.HideData();  
            if(coverView!=null)
                coverView.RemoveFromSuperview();
        }

        public override void IF_ItemSelectd(int position)
        {
            if(!SupportView.IsAllowMultiSelect)
                base.IF_ItemSelectd(position);
            SupportView.SendOnItemSelected(position);
        }

        public override void ShowSubviewAt(CGRect rect, UIView subView, Action didFinishAnimation)
        {
            coverView = new UIView();
            coverView.Frame = new CGRect(0, 0, Window.Bounds.Width, Window.Bounds.Height);
            coverView.BackgroundColor = UIColor.Clear;
            coverView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                ShowData();
            }));

            GetCurrentWindow(this).AddSubview(coverView);

            float height = HeightOfRow * SupportItemList.Count;
            var y = rect.Y + textField.Frame.Height + 2;
            if (height > rect.Height / 2) 
                height = (float)rect.Height / 2;

            subView.Frame = new CGRect(rect.X, y, rect.Width, 0);
            UIView.Animate(0.2, () =>
            {
                subView.Frame = new CGRect(rect.X, y, rect.Width, height);
                subView.SetShadow(2f, 2, 0.8f);
                GetCurrentWindow(this).AddSubview(subView);
            }, didFinishAnimation);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(SupportDropList.ItemSelectedPositionProperty.PropertyName))
            {
                var position = SupportView.ItemSelectedPosition;
                if(position>=0 && position<SupportItemList.Count)
                {
                    FlagShow = true;
                    IF_ItemSelectd(position);
                }
            }
        }
    }
}