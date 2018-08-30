using System;

using Foundation;
using Photos;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public partial class GalleryItemPhotoViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("GalleryItemPhotoViewCell");
        public static readonly UINib Nib;

        static GalleryItemPhotoViewCell()
        {
            Nib = UINib.FromName("GalleryItemPhotoViewCell", NSBundle.MainBundle);
        }

        protected GalleryItemPhotoViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public GalleryItemPhotoViewCell(){

        }

        private Action ActionClick;
        public void BindDataToCell(PhotoSetNative pHAsset, Action action)
        {
            imgIcon.ClipsToBounds = true;
            imgIcon.ContentMode = UIViewContentMode.ScaleAspectFill;

            CheckBox.Checked = pHAsset.Checked;

            var options = new PHImageRequestOptions
            {
                Synchronous = true
            };

            PHImageManager.DefaultManager.RequestImageForAsset(pHAsset.Image, Bounds.Size, PHImageContentMode.AspectFit, options, (requestedImage, _) => {
                imgIcon.Image = requestedImage;
            });

            if (ActionClick == null)
            {
                ActionClick = action;
                CheckBox.TouchUpInside += (sender, e) =>
                {
                    ActionClick();
                };
            }
        }
    }
}