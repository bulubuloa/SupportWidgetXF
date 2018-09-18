using System;
using SupportWidgetXF.DependencyService;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public static class UIImageExtensions
    {
        //1.2 megapixel 1280 x 960
        public static UIImage ResizeImage(this UIImage sourceImage, SyncPhotoOptions options)
        {
            nfloat scale = 1.0f;
            CoreGraphics.CGSize cGSize;

            if(sourceImage.Size.Width>sourceImage.Size.Height)
            {
                //scale by width
                scale = options.Width / sourceImage.Size.Width;
                cGSize = new CoreGraphics.CGSize(options.Width, sourceImage.Size.Height * scale);
            }
            else
            {
                //scale by height
                scale = options.Width / sourceImage.Size.Height;
                cGSize = new CoreGraphics.CGSize(sourceImage.Size.Width * scale, options.Width);
            }

            UIGraphics.BeginImageContext(cGSize);
            sourceImage.Draw(new CoreGraphics.CGRect(0, 0, cGSize.Width, cGSize.Height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
    }
}