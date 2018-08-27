using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using SupportWidgetXF.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(IPushLocalNotification))]
namespace SupportWidgetXF.Droid
{
    public class IPushLocalNotificationExtended : IPushLocalNotification
    {
        public void IF_PushLocalNotification(string title, string content,string icon)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 99999);


            var builder = new NotificationCompat.Builder(SupportWidgetXFSetup.Activity)
                .SetContentTitle(title)
                .SetContentText(content)
                .SetPriority(2)
                .SetDefaults(0)
                .SetAutoCancel(true)
                .SetVibrate(new long[1000]);

            try
            {
                if (icon != null)
                {
                    var image = SupportWidgetXFSetup.Activity.Resources.GetIdentifier(icon, "drawable", SupportWidgetXFSetup.Activity.PackageName);
                    builder.SetSmallIcon(image);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            var textStyle = new NotificationCompat.BigTextStyle();
            textStyle.BigText(content);
            builder.SetStyle(textStyle);

            var notigi = builder.Build();
            var notificationManager = SupportWidgetXFSetup.Activity.GetSystemService(Context.NotificationService) as NotificationManager;

            notificationManager.Notify(month, notigi);
        }
    }
}