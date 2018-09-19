using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.Droid.Renderers.GalleryPicker;
using SupportWidgetXF.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(IGalleryPickerExtended))]
namespace SupportWidgetXF.Droid.Renderers.GalleryPicker
{
    public class IGalleryPickerExtended : DependencyService.IGalleryPicker
    {
        IGalleryPickerResultListener galleryPickerResultListener;

        public IGalleryPickerExtended()
        {
            MessagingCenter.Subscribe<GalleryPickerActivity,List<GalleryImageXF>>(this, Utils.SubscribeImageFromGallery,(arg1,arg2) => {
                galleryPickerResultListener.IF_PickedResult(arg2);
            });
        }

        public void IF_OpenCamera(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options)
        {
            galleryPickerResultListener = pickerResultListener;
            var pickerIntent = new Intent(SupportWidgetXFSetup.Activity, typeof(GalleryPickerActivity));
            pickerIntent.PutExtra(Utils.SubscribeImageFromCamera, Utils.SubscribeImageFromCamera);
            SupportWidgetXFSetup.Activity.StartActivity(pickerIntent);
        }

        public void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options)
        {
            galleryPickerResultListener = pickerResultListener;
            var pickerIntent = new Intent(SupportWidgetXFSetup.Activity, typeof(GalleryPickerActivity));
            SupportWidgetXFSetup.Activity.StartActivity(pickerIntent);
        }

        public Task<GalleryImageXF> IF_SyncPhotoFromCloud(IGalleryPickerResultListener galleryPickerResultListener, GalleryImageXF imageSet, SyncPhotoOptions options)
        {
            var bitmap = imageSet.OriginalPath.GetOriginalBitmapFromPath(options);
            using (var streamBitmap = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg,(int)(options.Quality * 100), streamBitmap);
                imageSet.ImageRawData = streamBitmap.ToArray().ToArray();
                bitmap.Recycle();
                return Task.FromResult<GalleryImageXF>(imageSet);
            }
        }
    }
}