using System;
namespace SupportWidgetXF.DependencyService
{
    public interface IPushLocalNotification
    {
        void IF_PushLocalNotification(string title, string content,string icon);
    }
}