using System;
using Android.App;

namespace SupportWidgetXF.Droid
{
    public static class SupportWidgetXFSetup
    {
        public static Activity Activity;

        public static void Initialize(Activity _Activity)
        {
            Activity = _Activity;
        }
    }
}