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
        void IF_PickedResult(List<GalleryImageXF> result,int _CodeRequest);
    }

    public interface IGalleryPicker
    {
        void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options, int CodeRequest);
        void IF_OpenCamera(IGalleryPickerResultListener pickerResultListener, SyncPhotoOptions options, int CodeRequest);
        Task<GalleryImageXF> IF_SyncPhotoFromCloud(IGalleryPickerResultListener galleryPickerResultListener, GalleryImageXF imageSet, SyncPhotoOptions options);
    }
}