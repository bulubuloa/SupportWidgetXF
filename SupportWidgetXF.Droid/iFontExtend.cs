using System;
using SupportWidgetXF.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(SupportWidgetXF.Droid.iFontExtend))]
namespace SupportWidgetXF.Droid
{
    public class iFontExtend : IFont
    {
        public string IF_GetDefaultFontFamily()
        {
            return "";
        }
    }
}