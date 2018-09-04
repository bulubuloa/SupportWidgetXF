
using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Graphics;
using Java.IO;
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
            MessagingCenter.Subscribe<GalleryPickerActivity,List<ImageSet>>(this, "ReturnImage",(arg1,arg2) => {
                galleryPickerResultListener.IF_PickedResult(arg2);
            });
        }

        public void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener)
        {
            galleryPickerResultListener = pickerResultListener;

            var pickerIntent = new Intent(SupportWidgetXFSetup.Activity, typeof(GalleryPickerActivity));
            SupportWidgetXFSetup.Activity.StartActivity(pickerIntent);
        }
    }
}