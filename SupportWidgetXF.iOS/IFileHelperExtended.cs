using System;
using System.IO;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileHelperExtended))]
namespace SupportWidgetXF.iOS
{
    public class IFileHelperExtended : IFileHelper
    {
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
    }
}