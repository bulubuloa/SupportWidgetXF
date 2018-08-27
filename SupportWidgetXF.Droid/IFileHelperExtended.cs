using System;
using System.IO;
using SupportWidgetXF.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileHelper))]
namespace SupportWidgetXF.Droid
{
    public class IFileHelperExtended : IFileHelper
    {
        public string IF_GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}