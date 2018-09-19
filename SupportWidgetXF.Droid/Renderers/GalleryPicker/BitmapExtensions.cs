using System;
using System.IO;
using Android.Graphics;
using SupportWidgetXF.DependencyService;

namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public static class BitmapExtensions
    {
        public static Bitmap GetOriginalBitmapFromPath(this string fileName, SyncPhotoOptions syncPhotoOptions)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > syncPhotoOptions.Height || outWidth > syncPhotoOptions.Width)
            {
                inSampleSize = outWidth < outHeight ? outHeight / syncPhotoOptions.Height : outWidth / syncPhotoOptions.Width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }

        public static Bitmap SyncBitmapWithQuality(this Bitmap bitmap, SyncPhotoOptions syncPhotoOptions)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            float scaleSize = 1f;

            if (height > syncPhotoOptions.Height || width > syncPhotoOptions.Width)
            {
                scaleSize = width < height ? ((float)syncPhotoOptions.Height) / height : ((float)syncPhotoOptions.Width) / width;

            }

            Matrix matrix = new Matrix();
            matrix.PostScale(scaleSize, scaleSize);

            Bitmap resizedBitmap = Bitmap.CreateBitmap(bitmap, 0, 0, width, height, matrix, false);
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, (int)(syncPhotoOptions.Quality * 100), stream);
            }
            bitmap.Recycle();
            return resizedBitmap;
        }

        public static Bitmap getResizedBitmap(Bitmap bm, SyncPhotoOptions SyncPhotoOptions)
        {
            int width = bm.Width;
            int height = bm.Height;

            float scaleSize = 1f;

            if (height > SyncPhotoOptions.Height || width > SyncPhotoOptions.Width)
            {
                scaleSize = width < height ? ((float)SyncPhotoOptions.Height) / height : ((float)SyncPhotoOptions.Width) / width;
                Matrix matrix = new Matrix();
                matrix.PostScale(scaleSize, scaleSize);

                Bitmap resizedBitmap = Bitmap.CreateBitmap(bm, 0, 0, width, height, matrix, false);
                using (var stream = new MemoryStream())
                {
                    resizedBitmap.Compress(Bitmap.CompressFormat.Jpeg,80, stream);
                }
                bm.Recycle();
                return resizedBitmap;
            }
            else
            {
                return bm;
            }
        }
    }
}
