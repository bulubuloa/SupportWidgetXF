using System;
using Xamarin.Forms.Platform.iOS;

namespace SupportWidgetXF.iOS
{
    public static class SupportWidgetXFSetup
    {
        public static FormsApplicationDelegate AppDelegate;

        public static void Initialize(FormsApplicationDelegate _AppDelegate)
        {
            AppDelegate = _AppDelegate;
        }
    }
}