using System;
using CoreGraphics;
using SupportWidgetXF.iOS.Renderers;
using SupportWidgetXF.Widgets;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(SupportDropList), typeof(SupportDropListRenderer))]
namespace SupportWidgetXF.iOS.Renderers
{
    public class SupportDropListRenderer : SupportDropRenderer<SupportDropList>
    {
        private UIView coverView;

        public SupportDropListRenderer()
        {
        }

        public override void OnInitializeTextField()
        {
            base.OnInitializeTextField();

            textField.AddGestureRecognizer(new UITapGestureRecognizer(() => {
                if (SupportView.ItemsSourceDisplay == null)
                {
                    SupportView.OnDropListTouch();
                    NotifyAdapterChanged();
                }
                ShowData(); 
            }));

            var arrow = new UIImageView();
            arrow.ContentMode = UIViewContentMode.ScaleAspectFit;
            arrow.Image = UIImage.FromBundle("icn_droplist").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            arrow.Frame = new CGRect(4, 4, 12, 12);
            var contentRight = new UIView(new CGRect(0, 0, 20, 20));
            contentRight.AddSubview(arrow);
            textField.RightView = contentRight;
            textField.RightViewMode = UITextFieldViewMode.Always;
            textField.LeftView = new UIView(new CGRect(0, 0, 5, 5));
            textField.LeftViewMode = UITextFieldViewMode.Always;
            textField.Enabled = false;
        }

        public override void HideData()
        {
            base.HideData();  
            if(coverView!=null)
                coverView.RemoveFromSuperview();
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