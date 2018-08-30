using System;
using System.Drawing;
using CoreGraphics;
using CoreImage;
using UIKit;

namespace SupportWidgetXF.iOS.Renderers.GalleryPickerSupport
{
    public static class UIImageExtensions
    {
        public static UIImage ResizeImageWithAspectRatio(this UIImage imageSource, float scale)
        {
            if (scale > 1.0f)
                return imageSource;

            using (var c = CIContext.Create())
            {
                var sourceImage = CIImage.FromCGImage(imageSource.CGImage);

                var f = new CILanczosScaleTransform
                {
                    Scale = scale,
                    Image = sourceImage,
                    AspectRatio = 1.0f
                };

                var output = f.OutputImage;
                var cgi = c.CreateCGImage(output, output.Extent);
                return UIImage.FromImage(cgi, 1.0f, imageSource.Orientation);
            }
        }

        public static UIImage ResizeImageWithAspectRatio(this UIImage sourceImage, float maxWidth, float maxHeight)
        {
            var sourceSize = sourceImage.Size;
            var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
            if (maxResizeFactor > 1)
                return sourceImage;
            var width = maxResizeFactor * sourceSize.Width;
            var height = maxResizeFactor * sourceSize.Height;
            UIGraphics.BeginImageContext(new CGSize(width, height));
            sourceImage.Draw(new CGRect(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }

        public static UIImage ResizeImage(this UIImage sourceImage, float width, float height)
        {
            UIGraphics.BeginImageContext(new SizeF(width, height));
            sourceImage.Draw(new RectangleF(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }

        public static UIImage CropImage(this UIImage sourceImage, int crop_x, int crop_y, int width, int height)
        {
            var imgSize = sourceImage.Size;
            UIGraphics.BeginImageContext(new SizeF(width, height));
            var context = UIGraphics.GetCurrentContext();
            var clippedRect = new RectangleF(0, 0, width, height);
            context.ClipToRect(clippedRect);
            var drawRect = new CGRect(-crop_x, -crop_y, imgSize.Width, imgSize.Height);
            sourceImage.Draw(drawRect);
            var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return modifiedImage;
        }
    }
}