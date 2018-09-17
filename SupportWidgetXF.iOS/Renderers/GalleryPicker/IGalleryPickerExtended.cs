using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        bool stopX = false;

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

            MessagingCenter.Subscribe<GalleryPickerController, List<PhotoSetNative>>(this, "ReturnImageGallery", (arg1, arg2) => {
               
                var itemResult = new List<ImageSet>();

                foreach (var item in arg2)
                {
                    var xxx = new ImageSet();
                    xxx.Checked = item.Checked;
                    xxx.Path = item.Path;
                    xxx.SourceXF = item.SourceXF;
                    xxx.Stream = item.Stream;
                    xxx.Cloud = item.FromCloud;
                    xxx.OriginalRaw = item.Image.LocalIdentifier;
                    itemResult.Add(xxx);
                }
                galleryPickerResultListener.IF_PickedResult(itemResult);
            });

            MessagingCenter.Subscribe<XFCameraController, List<PhotoSetNative>>(this, "ReturnImageCamera", (arg1, arg2) => {

                var itemResult = new List<ImageSet>();

                foreach (var item in arg2)
                {
                    var xxx = new ImageSet();
                    xxx.Checked = item.Checked;
                    xxx.Path = item.Path;
                    xxx.SourceXF = item.SourceXF;
                    xxx.Stream = item.Stream;
                    itemResult.Add(xxx);
                }
                galleryPickerResultListener.IF_PickedResult(itemResult);
            });
        }

        public void IF_OpenCamera(IGalleryPickerResultListener pickerResultListener)
        {
            galleryPickerResultListener = pickerResultListener;
            UIStoryboard storyboard = UIStoryboard.FromName("UtilStoryboard", null);
            XFCameraController controller = (XFCameraController)storyboard.InstantiateViewController("XFCameraController");
            NaviExtensions.OpenController(controller);
        }

        public void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener)
        {
            galleryPickerResultListener = pickerResultListener;
            NaviExtensions.OpenController(new GalleryPickerController());
        }

        public void IF_SyncPhotoFromCloud(IGalleryPickerResultListener galleryPickerResultListener, ImageSet imageSet)
        {
            try
            {
                Debug.WriteLine(imageSet.OriginalRaw);

                var sortOptions = new PHFetchOptions();
                sortOptions.SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", false) };

                // var item = JsonConvert.DeserializeObject<PHAsset>(imageSet.OriginalRaw, jsonSerializerSettings);
                var xx = PHAsset.FetchAssetsUsingLocalIdentifiers(new string[] { imageSet.OriginalRaw }, sortOptions).Cast<PHAsset>().FirstOrDefault();
                if(xx!=null)
                {
                    var options = new PHImageRequestOptions()
                    {
                        NetworkAccessAllowed = true,
                        DeliveryMode = PHImageRequestOptionsDeliveryMode.HighQualityFormat,
                    };

                    options.ProgressHandler += Options_ProgressHandler;

                    PHImageManager.DefaultManager.RequestImageForAsset(xx, new CoreGraphics.CGSize(1280,960),PHImageContentMode.AspectFit, options,(result, info) => {
                        Debug.WriteLine(result.Size);
                        if(result.Size.Width>1280)
                        {
                            var newImage = result.ResizeImage();
                            Debug.WriteLine(newImage.Size);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        void Options_ProgressHandler(double progress, NSError error, out bool stopX, NSDictionary info)
        {

        }

       

        public byte[] ToByteArray(Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }
    }
}