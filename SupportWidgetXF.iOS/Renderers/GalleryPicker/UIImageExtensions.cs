using System;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public static class UIImageExtensions
    {
        //1.2 megapixel 1280 x 960
        public static UIImage ResizeImage(this UIImage sourceImage, int width = 1280)
        {
            var scale = width / sourceImage.Size.Width;
            var height = sourceImage.Size.Height * scale;
            UIGraphics.BeginImageContext(new CoreGraphics.CGSize(width, height));
            sourceImage.Draw(new CoreGraphics.CGRect(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
    }
}
