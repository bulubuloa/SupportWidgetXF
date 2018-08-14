using System;
using SupportWidgetXF.DependencyService;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SupportWidgetXF.iOS.iFontExtend))]
namespace SupportWidgetXF.iOS
{
    public class iFontExtend : IFont
    {
        public string IF_GetDefaultFontFamily()
        {
            return new UITextField().Font.Name;
        }
    }
}