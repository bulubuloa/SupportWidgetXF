using System;
using System.IO;
using SupportWidgetXF.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileHelper))]
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