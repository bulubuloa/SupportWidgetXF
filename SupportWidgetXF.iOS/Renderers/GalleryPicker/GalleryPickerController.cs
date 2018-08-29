using System;
using CoreGraphics;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public class GalleryPickerController : UIViewController
    {
        private UIView ViewTop, ViewBottom;
        private UIButton ButtonDone, ButttonBack;
        private UICollectionView collectionView; 

        private void InitializeLayout()
        {
            try
            {

                NavigationController.NavigationBar.Translucent = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            View.BackgroundColor = UIColor.White;

            ViewTop = new UIView(new CGRect(0,0,View.Bounds.Width,45));
            ViewTop.BackgroundColor = UIColor.FromRGB(64,64,64);

            ButttonBack = new UIButton(new CGRect(0, 8, 50, 30));
            ButttonBack.SetImage(UIImage.FromBundle("arrow_left").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal),UIControlState.Normal);
            ViewTop.AddSubview(ButttonBack);

            View.AddSubview(ViewTop);

            collectionView = new UICollectionView(new CGRect(0, 45, View.Bounds.Width, View.Bounds.Height - 45), new UICollectionViewFlowLayout());
            collectionView.BackgroundColor = UIColor.Green;

            View.AddSubview(collectionView);

            ViewBottom = new UIView(new CGRect(0, View.Bounds.Height-45, View.Bounds.Width, 45));
            ViewBottom.BackgroundColor = UIColor.FromRGB(64, 64, 64).ColorWithAlpha(0.7f);

            ButtonDone = new UIButton(new CGRect(ViewBottom.Frame.Width - 110,8, 100, 30));
            ButtonDone.Layer.BackgroundColor = UIColor.FromRGB(42, 131, 193).CGColor;
            ButtonDone.Layer.CornerRadius = 12;
            ViewBottom.AddSubview(ButtonDone);

            View.AddSubview(ViewBottom);

            ButttonBack.TouchUpInside += (object sender, EventArgs e) => 
            {
                DismissViewController(true, null);
            };

            ButtonDone.TouchUpInside += (object sender, EventArgs e) => 
            {

            };
            //View.AddConstraint(NSLayoutConstraint.Create(ViewBottom, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0));
        }

        public GalleryPickerController()
        {
            InitializeLayout();
        }
    }
}
