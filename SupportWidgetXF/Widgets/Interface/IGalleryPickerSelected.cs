using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets.Interface
{
    public interface IGalleryPickerSelected
    {
        void IF_ImageSelected(int positionDirectory, int positionImage);
        void IF_ImageSelected(int positionDirectory, int positionImage, ImageSource imageSource);
        void IF_CameraSelected(int pos);
    }
}
