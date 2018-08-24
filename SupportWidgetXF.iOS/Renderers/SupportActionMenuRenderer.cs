using System;
using System.ComponentModel;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.iOS.Renderers.DropCombo;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportActionMenu), typeof(SupportActionMenuRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportActionMenuRenderer : SupportDropRenderer<SupportActionMenu>
    {
        private UIView coverView, tapView;

        public override void OnInitializeTextField()
        {
            base.OnInitializeTextField();

            tapView = new UIView();
            tapView.Frame = this.Bounds;
            tapView.AddGestureRecognizer(new UITapGestureRecognizer(() => { ShowData(); }));

            textField.RightViewMode = UITextFieldViewMode.Always;
            textField.LeftView = new UIView(new CGRect(0, 0, 5, 5));
            textField.LeftViewMode = UITextFieldViewMode.Always;
            textField.Enabled = false;
            textField.UserInteractionEnabled = false;
            textField.Hidden = true;  

            tapView.AddSubview(textField);
        }

        public override void OnInitializeTableSource()
        {
            dropSource = new DropItemSource(SupportItemList, SupportView, HeightOfRow, this);
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

        protected override void OnSetNativeControl()
        {
            SetNativeControl(tapView);
        }

        public override void HideData()
        {
            base.HideData();
            if (coverView != null)
                coverView.RemoveFromSuperview();
        }

        public override void IF_ItemSelectd(int position)
        {
            base.IF_ItemSelectd(position);
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
    }
}