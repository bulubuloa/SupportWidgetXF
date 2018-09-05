using System;
using System.Collections.Generic;
using System.Linq;
using MBProgressHUD;
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

        public IGalleryPickerExtended()
        {
            MessagingCenter.Subscribe<GalleryPickerController, List<PhotoSetNative>>(this, "ReturnImage", (arg1, arg2) => {
               
                var itemResult = new List<ImageSet>();

                var options = new PHContentEditingInputRequestOptions()
                {
                    NetworkAccessAllowed = true,
                };
                int Count = 0;

                foreach (var item in arg2)
                {
                    var xxx = new ImageSet();
                    xxx.Checked = item.Checked;
                    xxx.Path = item.Path;
                    xxx.SourceXF = item.SourceXF;
                    itemResult.Add(xxx);

                    //if(string.IsNullOrEmpty(xxx.Path))
                    //{
                    //    item.Image.RequestContentEditingInput(options, (contentEditingInput, requestStatusInfo) =>
                    //    {
                    //        if (contentEditingInput != null)
                    //        {
                    //            xxx.Path = contentEditingInput.FullSizeImageUrl.ToString().Substring(7);
                    //            itemResult.Add(xxx);
                    //        }

                    //        Count += 1;
                    //        if (Count == arg2.Count)
                    //            galleryPickerResultListener.IF_PickedResult(itemResult);
                    //    });
                    //}
                    //else
                    //{
                    //    itemResult.Add(xxx);

                    //    Count += 1;
                    //    if (Count == arg2.Count)
                    //        galleryPickerResultListener.IF_PickedResult(itemResult);
                    //}
                }

                galleryPickerResultListener.IF_PickedResult(itemResult);
            });
        }

        public void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener)
        {

            galleryPickerResultListener = pickerResultListener;
            NaviExtensions.OpenController(new GalleryPickerController());
        }

       
    }
}