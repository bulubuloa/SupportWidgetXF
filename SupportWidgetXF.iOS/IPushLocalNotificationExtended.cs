using System;
using Foundation;
using SupportWidgetXF.DependencyService;
using SupportWidgetXF.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IPushLocalNotificationExtended))]
namespace SupportWidgetXF.iOS
{
    public class IPushLocalNotificationExtended : IPushLocalNotification
    {
        public void IF_PushLocalNotification(string title, string content, string icon)
        {
            SupportWidgetXFSetup.AppDelegate.InvokeOnMainThread(() =>
            {
                var notification = new UILocalNotification();
                notification.FireDate = NSDate.Now;
                notification.AlertTitle = title;
                notification.AlertBody = content;
                notification.SoundName = UILocalNotification.DefaultSoundName;
                UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            });
        }
    }
}