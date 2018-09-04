using System.Collections.Generic;
using SupportWidgetXF.Models;

namespace SupportWidgetXF.DependencyService
{
    public interface IGalleryPickerResultListener
    {
        void IF_PickedResult(List<ImageSet> result);
    }

    public interface IGalleryPicker
    {
        void IF_OpenGallery(IGalleryPickerResultListener pickerResultListener);
    }
}