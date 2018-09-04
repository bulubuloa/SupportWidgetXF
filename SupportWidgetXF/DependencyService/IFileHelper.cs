using System;
using Xamarin.Forms;

namespace SupportWidgetXF.DependencyService
{
    public interface IFileHelper
    {
        string IF_GetLocalFilePath(string filename);
        System.IO.Stream IF_GetStreamFilePath(string filePath);
        void IF_GetImageSourceFilePath(ImageSource imageSource, string filePath);
    }
}