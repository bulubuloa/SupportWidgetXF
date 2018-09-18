using System.Collections.Generic;
using System.Threading.Tasks;
using SupportWidgetXF.Models;

namespace SupportWidgetXF.DependencyService
{
    public class SyncPhotoOptions
    {
        public int Width { set; get; }
        public int Height { set; get; }
        public float Quality { set; get; }

        public SyncPhotoOptions()
        {
            Width = 1280;
            Height = 960;
            Quality = 0.8f;
        }
    }

    public interface IGalleryPickerResultListener
    {
        void IF_PickedResult(List<GalleryImageXF> result);
    }

    public interface IGalleryPicker
    {
        void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options);
        void IF_OpenCamera(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options);
        Task<GalleryImageXF> IF_SyncPhotoFromCloud(IGalleryPickerResultListener galleryPickerResultListener, GalleryImageXF imageSet, SyncPhotoOptions options);
    }
}