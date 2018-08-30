using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Photos;
using SupportWidgetXF.Models;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public partial class GalleryDirectoryViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("GalleryDirectoryViewCell");
        public static readonly UINib Nib;

        static GalleryDirectoryViewCell()
        {
            Nib = UINib.FromName("GalleryDirectoryViewCell", NSBundle.MainBundle);
        }

        protected GalleryDirectoryViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public GalleryDirectoryViewCell(){}

        private Action ActionClick;

        public void BindDataToCell(PHAssetCollection galleryDirectory, Action action)
        {

            var sortOptions = new PHFetchOptions();
            //sortOptions.SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", true) };

            var items = PHAsset.FetchAssets(galleryDirectory, sortOptions).Cast<PHAsset>().ToList();

            txtTitle.Text = galleryDirectory.LocalizedTitle;
            txtDescription.Text = "(" + items.Count + ")";
            //.BackgroundColor = UIColor.FromRGB(241, 241, 241);

            imageView.ClipsToBounds = true;
            imageView.ContentMode = UIViewContentMode.ScaleAspectFill;

            try
            {
                var options = new PHImageRequestOptions
                {
                    Synchronous = true
                };
                PHImageManager.DefaultManager.RequestImageForAsset(items[items.Count-1], imageView.Bounds.Size, PHImageContentMode.AspectFit, options, (requestedImage, _) => {
                    imageView.Image = requestedImage;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            if (ActionClick == null)
            {
                ActionClick = action;
                bttClick.TouchUpInside += (sender, e) =>
                {
                    ActionClick();
                };
            }
        }
    }
}