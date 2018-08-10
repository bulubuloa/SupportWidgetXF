using System;
using Android.App;

namespace SupportWidgetXF.Android
{
    public static class SupportWidgetXFSetup
    {
        public static Activity Context;

        public static void Initialize(Activity activity)
        {
            Context = activity;
        }
    }
}