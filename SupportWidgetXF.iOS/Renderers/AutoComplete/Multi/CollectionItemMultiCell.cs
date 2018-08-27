using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.AutoComplete.Multi
{
    public class CollectionItemMultiCell : UICollectionViewCell
    {
        UIButton label;
        UIImageView imgIcon;

        public string Text
        {
            get
            {
                return label.TitleLabel.Text;
            }
            set
            {
                label.SetTitle(value, UIControlState.Normal);
                SetNeedsDisplay();
            }
        }

        [Export("initWithFrame:")]
        CollectionItemMultiCell(CGRect frame) : base(frame)
        {
            label = new UIButton(CoreGraphics.CGRect.Empty);
            label.SetTitleColor(UIColor.Black, UIControlState.Normal);
            label.TitleLabel.Lines = 1;
            label.Font = UIFont.SystemFontOfSize(13);
            label.Frame = new CGRect(5, ContentView.Frame.Y, ContentView.Frame.Width - 20, ContentView.Frame.Height);
            label.TitleLabel.TextAlignment = UITextAlignment.Left;
            label.TranslatesAutoresizingMaskIntoConstraints = false;

            ContentView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterY, 1, 0));
            ContentView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Leading, 1, 2));
            //ContentView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Trailing, 1, -2));

            ContentView.AddSubview(label);

            imgIcon = new UIImageView(CoreGraphics.CGRect.Empty);
            imgIcon.Image = UIImage.FromFile("close").ImageWithRenderingMode(UIKit.UIImageRenderingMode.AlwaysOriginal);
            imgIcon.BackgroundColor = UIColor.Clear;
            imgIcon.ContentMode = UIViewContentMode.ScaleAspectFit;
            imgIcon.Frame = new CGRect(label.Frame.X, label.Frame.Y, 10, 10);
            imgIcon.TranslatesAutoresizingMaskIntoConstraints = false;
            ContentView.AddSubview(imgIcon);

            ContentView.AddConstraint(NSLayoutConstraint.Create(imgIcon, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterY, 1, 0));
            ContentView.AddConstraint(NSLayoutConstraint.Create(imgIcon, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Trailing, 1, -2));
            //ContentView.AddConstraint(NSLayoutConstraint.Create(imgIcon, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Leading, 1, 5));

            ContentView.Layer.BackgroundColor = UIColor.Gray.CGColor;
            ContentView.Layer.CornerRadius = 4;
        }
    }
}