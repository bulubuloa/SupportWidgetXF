using System;
using Xamarin.Forms;

namespace SupportWidgetXF.Widgets.Interface
{
    public interface IGalleryPickerSelected
    {
        void IF_ImageSelected(int positionDirectory, int positionImage, ImageSource imageSource, byte[] stream);
        void IF_CameraSelected(int pos);
    }
}
