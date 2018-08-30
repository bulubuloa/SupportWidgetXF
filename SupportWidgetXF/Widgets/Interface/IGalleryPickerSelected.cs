using System;
namespace SupportWidgetXF.Widgets.Interface
{
    public interface IGalleryPickerSelected
    {
        void IF_ImageSelected(int positionDirectory, int positionImage);
        void IF_CameraSelected(int pos);
    }
}
