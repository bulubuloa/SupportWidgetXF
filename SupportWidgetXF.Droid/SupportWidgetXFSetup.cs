using System;
using Android.App;
using Android.OS;
using Rg.Plugins.Popup;

namespace SupportWidgetXF.Droid
{
    public static class SupportWidgetXFSetup
    {
        public static Activity Activity;

        public static void Initialize(Activity _Activity, Bundle bundle)
        {
            Activity = _Activity;
            Popup.Init(_Activity, bundle);
        }
    }
}