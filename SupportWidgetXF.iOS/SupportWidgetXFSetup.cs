using System;
using Rg.Plugins.Popup;
using Xamarin.Forms.Platform.iOS;

namespace SupportWidgetXF.iOS
{
    public static class SupportWidgetXFSetup
    {
        public static FormsApplicationDelegate AppDelegate;

        public static void Initialize(FormsApplicationDelegate _AppDelegate)
        {
            AppDelegate = _AppDelegate;
            Popup.Init();
        }
    }
}