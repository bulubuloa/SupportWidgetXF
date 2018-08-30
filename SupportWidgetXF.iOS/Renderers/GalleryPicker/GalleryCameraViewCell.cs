using System;

using Foundation;
using SupportWidgetXF.Widgets.Interface;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public partial class GalleryCameraViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("GalleryCameraViewCell");
        public static readonly UINib Nib;

        static GalleryCameraViewCell()
        {
            Nib = UINib.FromName("GalleryCameraViewCell", NSBundle.MainBundle);
        }

        protected GalleryCameraViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public GalleryCameraViewCell()
        {

        }

        private Action ActionClick;
        public void BindDataToCell(IGalleryPickerSelected action, int index)
        {
            bttClick.Tag = index;

            if (ActionClick == null)
            {
                ActionClick = delegate {
                    action.IF_CameraSelected((int)bttClick.Tag);
                };
                bttClick.TouchUpInside += (sender, e) =>
                {
                    ActionClick();
                };
            }
        }
    }
}