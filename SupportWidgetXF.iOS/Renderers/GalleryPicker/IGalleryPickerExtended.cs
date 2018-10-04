using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Newtonsoft.Json;
using Photos;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.iOS.Renderers.GalleryPicker;
using SupportWidgetXF.Models;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IGalleryPickerExtended))]
namespace SupportWidgetXF.iOS.Renderers.GalleryPicker
{
    public class IGalleryPickerExtended : IGalleryPicker
    {
        IGalleryPickerResultListener galleryPickerResultListener;
        JsonSerializerSettings jsonSerializerSettings;
        int CodeRequest;

        public IGalleryPickerExtended()
        {
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            MessagingCenter.Subscribe<GalleryPickerController, List<PhotoSetNative>>(this, Utils.SubscribeImageFromGallery, (arg1, arg2) => {
                var itemResult = new List<GalleryImageXF>();
                foreach (var item in arg2)
                {
                    itemResult.Add(item.galleryImageXF);
                }
                galleryPickerResultListener.IF_PickedResult(itemResult,CodeRequest);
            });

            MessagingCenter.Subscribe<XFCameraController, List<PhotoSetNative>>(this, Utils.SubscribeImageFromCamera, (arg1, arg2) => {
                var itemResult = new List<GalleryImageXF>();
                foreach (var item in arg2)
                {
                    itemResult.Add(item.galleryImageXF);
                }
                galleryPickerResultListener.IF_PickedResult(itemResult,CodeRequest);
            });
        }

        public void IF_OpenCamera(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options, int _CodeRequest)
        {
            CodeRequest = _CodeRequest;
            galleryPickerResultListener = pickerResultListener;
            UIStoryboard storyboard = UIStoryboard.FromName("UtilStoryboard", null);
            XFCameraController controller = (XFCameraController)storyboard.InstantiateViewController("XFCameraController");
            NaviExtensions.OpenController(controller);
        }

        public void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options, int _CodeRequest)
        {
            CodeRequest = _CodeRequest;
            galleryPickerResultListener = pickerResultListener;
            NaviExtensions.OpenController(new GalleryPickerController());
        }

        public async Task<GalleryImageXF> IF_SyncPhotoFromCloud(IGalleryPickerResultListener galleryPickerResultListener, GalleryImageXF imageSet, SyncPhotoOptions options)
        {
            try
            {
                bool FinishSync = false;

                Debug.WriteLine(imageSet.OriginalPath);

                var sortOptions = new PHFetchOptions();
                sortOptions.SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", false) };

                var FeechPhotoByIdentifiers = PHAsset.FetchAssetsUsingLocalIdentifiers(
                    new string[] { imageSet.OriginalPath }, 
                    sortOptions).Cast<PHAsset>().FirstOrDefault();

                if(FeechPhotoByIdentifiers != null)
                {
                    var requestOptions = new PHImageRequestOptions()
                    {
                        NetworkAccessAllowed = true,
                        DeliveryMode = PHImageRequestOptionsDeliveryMode.HighQualityFormat,
                        ResizeMode = PHImageRequestOptionsResizeMode.None,
                    };

                    var requestSize = new CoreGraphics.CGSize(options.Width, options.Height);
                    requestSize = PHImageManager.MaximumSize;

                    PHImageManager.DefaultManager.RequestImageForAsset(FeechPhotoByIdentifiers, requestSize, PHImageContentMode.AspectFit, requestOptions, (result, info) => {
                        if(result!=null)
                        {
                            var newImage = result.ResizeImage(options);
                            imageSet.ImageRawData = newImage.AsJPEG(options.Quality).ToArray();
                        }
                        FinishSync = true;
                    });

                    do
                    {
                        if (FinishSync)
                        {
                            return imageSet;
                        }
                        await Task.Delay(1000);
                    } while (!FinishSync);
                }

                return imageSet;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return imageSet;
            }
        }
    }
}