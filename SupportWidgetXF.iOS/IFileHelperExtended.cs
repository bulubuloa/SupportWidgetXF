using System;
using System.IO;
using Newtonsoft.Json;
using Photos;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileHelperExtended))]
namespace SupportWidgetXF.iOS
{
    public class IFileHelperExtended : IFileHelper
    {
        public void IF_GetImageSourceFilePath(ImageSource imageSource, string filePath)
        {
            var options = new PHImageRequestOptions
            {
                Synchronous = true
            };
            var pHAsset = JsonConvert.DeserializeObject<PHAsset>(filePath);
            PHImageManager.DefaultManager.RequestImageForAsset(pHAsset, new CoreGraphics.CGSize(100,100), PHImageContentMode.AspectFit, options,(result, info) => {
                imageSource = ImageSource.FromStream(() => result.AsJPEG(0.7f).AsStream());
            });
        }

        public string IF_GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        public Stream IF_GetStreamFilePath(string filePath)
        {
            //PHImageManager.DefaultManager.RequestImageForAsset(item.Image, new CoreGraphics.CGSize(800,800), PHImageContentMode.AspectFit, options, (requestedImage, _) => {
            //    galleryPickerResultListener.IF_PickedResultSequence(requestedImage.AsJPEG(0.7f).AsStream());

            //});
            throw new Exception();
        }
    }
}